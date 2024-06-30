using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.DailyExpenses.Queries.GetDailyExpenseList;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using Microsoft.AspNetCore.Identity;

namespace Focus.Business.DailyExpenses.Commands.AddUpdateDailyExpense
{
    public class UpdateBulkApproval : IRequest<Message>
    {

        public ICollection<Guid> SelectedId { get; set; }
        public ApprovalStatus Action { get; set; }
        public Guid? AccountId { get; set; }
        public string Reason { get; set; }
        public string PaymentType { get; set; }

        public class Handler : IRequestHandler<UpdateBulkApproval, Message>
        {
            private IApplicationDbContext Context { get; set; }
            private ILogger Logger { get; set; }
            private readonly UserManager<ApplicationUser> _userManager;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<UpdateBulkApproval> logger, UserManager<ApplicationUser> userManager, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _userManager = userManager;
                _mediator = mediator;
            }
            public async Task<Message> Handle(UpdateBulkApproval request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    var accounts = Context.Accounts.AsNoTracking().ToList();
                    var expenseAccount = accounts.FirstOrDefault(x => x.Code == "60505001");
                    if (expenseAccount == null)
                        throw new NotFoundException("Expense Account Not Found", null);
                    var cashAccount = accounts.FirstOrDefault(x => x.Code == "10100001");
                    if (cashAccount == null)
                        throw new NotFoundException("Cash Account Not Found", null);
                    var transactions = new List<Transaction>();
                    List<ApplicationUser> users;
                    var employeeList = new List<EmployeeRegistration>();
                    var expensesList = new List<DailyExpenseLookUpModel>();
                    if (request.Action == ApprovalStatus.Rejected && request.PaymentType== "Credit")
                    {
                        users = await _userManager.Users.ToListAsync(cancellationToken: cancellationToken);
                        employeeList = Context.EmployeeRegistrations.AsNoTracking().ToList();
                        expensesList = Context.DailyExpenses.AsNoTracking().Include(x => x.DailyExpenseDetails)
                            .Where(x => request.SelectedId.Contains(x.Id)).Select(dailyExpense => new DailyExpenseLookUpModel
                            {
                                Id = dailyExpense.Id,
                                Date = dailyExpense.Date.ToString("dd/MM/yyyy"),
                                VoucherNo = dailyExpense.VoucherNo,
                                Description = dailyExpense.Description,
                                Reason = dailyExpense.Reason,
                                //BranchId = dailyExpense.BranchId,
                                TotalAmount = dailyExpense.DailyExpenseDetails.Sum(x => x.Amount),
                                UserName = users.Where(x => x.Id == EF.Property<Guid>(dailyExpense, "UserId").ToString()).Select(y => y.EmployeeId.ToString()).FirstOrDefault(),
                            }).ToList();
                    }

                    var expenses = await Context.DailyExpenses.AsNoTracking()
                        .Include(x => x.DailyExpenseDetails)
                        .Where(x => request.SelectedId.Contains(x.Id)).ToListAsync(cancellationToken: cancellationToken);

                    foreach (var expense in expenses)
                    {
                        if (request.Action == ApprovalStatus.Approved)
                        {
                            expense.ApprovalStatus = request.Action;
                            //Expense Account Debit FOR APProved
                            transactions.Add(new Transaction
                            {
                                AccountId = expenseAccount.Id,
                                DocumentId = expense.Id,
                                Date = expense.Date,
                                DocumentNumber = expense.VoucherNo,
                                Description = expense.Description,
                                Debit = expense.DailyExpenseDetails.Sum(x => x.Amount),
                                Credit = 0m,
                                TransactionType = TransactionType.ExpenseVoucher,
                                BranchId = expense.BranchId,
                                Year = DateTime.UtcNow.Year.ToString()
                            });
                            //Cash Account Credit FOR APProved

                            transactions.Add(new Focus.Domain.Entities.Transaction
                            {
                                AccountId = request.AccountId.Value,
                                DocumentId = expense.Id,
                                Date = expense.Date,
                                DocumentNumber = expense.VoucherNo,
                                Description = expense.Description,
                                Credit = expense.DailyExpenseDetails.Sum(x => x.Amount),
                                Debit = 0m,
                                TransactionType = TransactionType.ExpenseVoucher,
                                BranchId = expense.BranchId,
                                Year = DateTime.UtcNow.Year.ToString()
                            });
                        }
                        else

                        {

                            expense.ApprovalStatus = request.Action;
                            expense.Reason = request.Reason;

                            expense.PaymetType = request.PaymentType;

                            if (request.PaymentType == "Credit")
                            {
                                //Credit Cash Account for  Rejection
                                transactions.Add(new Transaction
                                {
                                    AccountId = cashAccount.Id,
                                    DocumentId = expense.Id,
                                    Date = expense.Date,
                                    DocumentNumber = expense.VoucherNo,
                                    Description = expense.Description,
                                    Credit = expense.DailyExpenseDetails.Sum(x => x.Amount),
                                    Debit = 0m,
                                    TransactionType = TransactionType.ExpenseVoucher,
                                    BranchId = expense.BranchId,
                                    Year = DateTime.UtcNow.Year.ToString()
                                });
                                var employeeId = expensesList.Where(x => x.Id == expense.Id).Select(y => y.UserName).FirstOrDefault();
                                if (employeeId==null)
                                    throw new NotFoundException("Employee Not Found", expense.Id);

                                var employeeInfo = employeeList.FirstOrDefault(x => x.Id == Guid.Parse(employeeId));
                                if (employeeInfo == null)

                                    throw new NotFoundException("Employee Not Found", employeeId);
                                //Debit Employee Account for  Rejection
                                transactions.Add(new Transaction
                                {
                                    AccountId = employeeInfo.PayableAccountId.Value,
                                    DocumentId = expense.Id,
                                    Date = expense.Date,
                                    DocumentNumber = expense.VoucherNo,
                                    Description = expense.Description,
                                    Credit = expense.DailyExpenseDetails.Sum(x => x.Amount),
                                    Debit = 0m,
                                    TransactionType = TransactionType.ExpenseVoucher,
                                    BranchId = expense.BranchId,
                                    Year = DateTime.UtcNow.Year.ToString()
                                });

                            }

                        }
                    }

                    Context.DailyExpenses.UpdateRange(expenses);
                    Context.Transactions.AddRange(transactions);

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        BranchId = expenses.FirstOrDefault()?.BranchId,
                        Code = _code,
                    }, cancellationToken);


                    await Context.SaveChangesAsync(cancellationToken);
                    var message = new Message
                    {
                        Id = Guid.NewGuid(),
                        IsAddUpdate = "Status has been changed successfully"
                    };
                    return message;
                }

                catch (Exception exception)
                {
                    var message = new Message
                    {
                        Id = Guid.Empty,
                        IsAddUpdate = "Some Error Occurred /n " + exception.Message
                    };
                    Logger.LogError(exception.Message);

                    return message;
                }

            }
        }
    }
}
