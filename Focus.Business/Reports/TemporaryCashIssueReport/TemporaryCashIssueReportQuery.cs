using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.TemporaryCashIssues.Queries;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Reports.TemporaryCashIssueReport
{
    public class TemporaryCashIssueReportQuery : PagedRequest, IRequest<PagedResult<List<TemporaryCashIssueReportLookUp>>>
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public Guid EmployeeId { get; set; }

        public class Handler : IRequestHandler<TemporaryCashIssueReportQuery, PagedResult<List<TemporaryCashIssueReportLookUp>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<TemporaryCashIssueReportQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<PagedResult<List<TemporaryCashIssueReportLookUp>>> Handle(TemporaryCashIssueReportQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var query = _context.TemporaryCashIssues
                        .AsNoTracking()
                        .Include(x => x.TemporaryCashIssueExpenses)
                        .Include(x => x.TemporaryCashReturns)
                        .Include(x => x.TemporaryCashIssueItems)
                        .Where(x => x.ApprovalStatus == ApprovalStatus.Approved && x.Date.Date.Year == request.Year && x.Date.Date.Month == request.Month && x.CashIssuerId==request.EmployeeId)
                        .AsQueryable();

                    var listEmployee = _context.EmployeeRegistrations.AsNoTracking().AsQueryable();
                    var listCashRequestUsers = _context.CashRequestUsers.AsNoTracking().AsQueryable();
                    

                    query = query.OrderByDescending(x => x.RegistrationNo);

                    var account = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Name == "Temporary Cash", cancellationToken: cancellationToken);



                    var temporaryCash = new List<TemporaryCashIssueReportLookUp>();
                    foreach (var item in query)
                    {
                        var temporaryCashIssueExpenses = new List<TemporaryCashIssueExpensesLookUp>();
                        if (item.TemporaryCashIssueExpenses.Count > 0)
                        {
                            foreach (var expense in item.TemporaryCashIssueExpenses)
                            {
                                if (expense.DocumentType == "Expenses")
                                {
                                    var dailyExpense = await _context.DailyExpenses.FindAsync(expense.DocumentId);

                                    temporaryCashIssueExpenses.Add(new TemporaryCashIssueExpensesLookUp()
                                    {
                                        Id = expense.Id,
                                        Date = dailyExpense?.Date.ToString("d"),
                                        DocumentType = expense.DocumentType,
                                        DocumentNo = dailyExpense?.VoucherNo,
                                        Amount = dailyExpense?.DailyExpenseDetails.Sum(x => x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل" ? ((x.Amount * x.TaxRate.Rate) / (100)) + x.Amount : x.Amount) ?? 0,
                                    });
                                }
                                if (expense.DocumentType == "Supplier Payment")
                                {
                                    var voucher = await _context.PaymentVouchers
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(x => x.Id == expense.DocumentId, cancellationToken: cancellationToken);
                                    temporaryCashIssueExpenses.Add(new TemporaryCashIssueExpensesLookUp()
                                    {
                                        Id = expense.Id,
                                        Date = voucher?.Date.ToString("d"),
                                        DocumentType = expense.DocumentType,
                                        DocumentNo = voucher?.VoucherNumber,
                                        Amount = voucher?.Amount ?? 0,
                                    });
                                }
                            }
                        }

                        var userName = item.IsCashRequesterUser ?
                            listCashRequestUsers.FirstOrDefault(x => x.Id == item.UserId)?.Name :
                            listEmployee.FirstOrDefault(x => x.Id == item.UserId)?.EnglishName;
                        
                        temporaryCash.Add(new TemporaryCashIssueReportLookUp()
                        {
                            Id = item.Id,
                            Date = item.Date.ToString("d"),
                            UserName = userName,
                            RegistrationNo = item.RegistrationNo,
                            UserId = item.UserId,
                            IsCashRequesterUser = item.IsCashRequesterUser,
                            TemporaryCashAccountId = account?.Id,
                            TemporaryCashIssueExpenses = temporaryCashIssueExpenses,
                            Amount = item.TemporaryCashIssueItems.Sum(x => x.Amount),
                            ReturnAmount = item.TemporaryCashReturns.Sum(x => x.Amount),
                            VoucherAmount = temporaryCashIssueExpenses.Sum(x => x.Amount),
                        });
                    }

                    return new PagedResult<List<TemporaryCashIssueReportLookUp>>
                    {
                        Results = temporaryCash
                    };


                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }
        }
    }
}
