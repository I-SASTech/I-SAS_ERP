using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Users;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.Extensions;
using Focus.Domain.Enum;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.DayStarts.Commands.AddUpdateDayEnd
{
    public class AddUpdateDayEndCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid? Id { get; set; }
        public DateTime? Date { get; set; }
        public Guid SaleId { get; set; }
        public Guid CounterId { get; set; }
        public Guid LocationId { get; set; }
        public decimal CashInHand { get; set; }
        public decimal Bank { get; set; }
        public decimal Expense { get; set; }
        public decimal OpeningCash { get; set; }
        public decimal CarryCash { get; set; }
        public decimal VerifyCash { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        public bool IsActive { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string EndTerminalBy { get; set; }
        public string EndTerminalFor { get; set; }

        public bool IsExpenseDay { get; set; }

        public bool IsSupervisor { get; set; }

        public string SupervisorPassword { get; set; }

        public string SupervisorName { get; set; }
        public string CreditReason { get; set; }
        public bool IsOpenDay { get; set; }
        public bool IsSupervisorTrans { get; set; }
        public decimal RemainingAmount { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateDayEndCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly IUserComponent _userComponent;
            private readonly IPrincipal _principal;
            private readonly UserManager<ApplicationUser> _userManager;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor
            public Handler(IApplicationDbContext context, ILogger<AddUpdateDayEndCommand> logger, IPrincipal principal,
                SignInManager<ApplicationUser> signInManager, IUserComponent userComponent, UserManager<ApplicationUser> userManager, IMediator mediator)
            {
                Context = context;
                _principal = principal;
                Logger = logger;
                _signInManager = signInManager;
                _userComponent = userComponent;
                _userManager = userManager;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddUpdateDayEndCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    SignInResult result;
                    SignInResult supervisorResult;
                    ApplicationUser user;
                    ApplicationUser supervisorUser = null;

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.User.IndexOf('@') > -1)
                    {
                        //Validate email format
                        string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                        Regex re = new Regex(emailRegex);
                        if (!re.IsMatch(request.User))
                        {
                            result = await _signInManager.PasswordSignInAsync(request.User, request.Password, false, lockoutOnFailure: false);
                            user = await _userManager.FindByNameAsync(request.User);
                            if (user != null)
                                request.User = user.Email;

                        }
                        else
                        {
                            user = await _userManager.FindByEmailAsync(request.User);
                            result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

                        }

                    }
                    else
                    {
                        result = await _signInManager.PasswordSignInAsync(request.User, request.Password, false, lockoutOnFailure: false);
                        user = await _userManager.FindByNameAsync(request.User);
                        if (user != null)
                            request.User = user.Email;


                    }

                    if (string.IsNullOrEmpty(request.SupervisorName) &&
                        string.IsNullOrEmpty(request.SupervisorPassword))
                        request.IsSupervisorTrans = false;
                    else
                    {
                        if (!request.IsSupervisor && request.CarryCash > 0)
                        {
                            if (request.SupervisorName.IndexOf('@') > -1)
                            {
                                //Validate email format
                                string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                                    @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                                    @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                                Regex re = new Regex(emailRegex);
                                if (!re.IsMatch(request.SupervisorName))
                                {
                                    supervisorResult = await _signInManager.PasswordSignInAsync(request.SupervisorName, request.SupervisorPassword, false, lockoutOnFailure: false);
                                    supervisorUser = await _userManager.FindByNameAsync(request.SupervisorName);
                                    //if (user != null)
                                    //    request.User = user.Email;

                                }
                                else
                                {
                                    supervisorUser = await _userManager.FindByEmailAsync(request.SupervisorName);
                                    supervisorResult = await _signInManager.PasswordSignInAsync(request.SupervisorName, request.SupervisorPassword, false, lockoutOnFailure: false);

                                }

                            }
                            else
                            {
                                supervisorResult = await _signInManager.PasswordSignInAsync(request.SupervisorName, request.SupervisorPassword, false, lockoutOnFailure: false);
                                supervisorUser = await _userManager.FindByNameAsync(request.SupervisorName);
                            }
                            if (!supervisorResult.Succeeded)
                                throw new ObjectAlreadyExistsException("Supervisor User Credentials are Invalid");
                        }

                        request.IsSupervisorTrans = true;
                    }

                    var isPermission = await Context.LoginPermissions
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.UserId == Guid.Parse(user.Id) && x.CloseDay, cancellationToken);

                    var currentLoginUser = _principal.Identity.UserId();

                    var currentUserDetail = await _userManager.FindByIdAsync(currentLoginUser);
                    if (result.Succeeded && isPermission != null)
                    {
                        if (request.Id != null && request.Id != Guid.Empty)
                        {
                            //Data already exist ; Update data
                            //get data from database using id
                            Guid? account = null;
                            if (request.IsSupervisor)
                            {
                                 account =
                                    Context.EmployeeRegistrations.FirstOrDefault(x => x.Id == user.EmployeeId)?.EmployeeAccountId;
                            }
                            else if(request.CarryCash>0)
                            {

                                if (supervisorUser != null && request.CarryCash > 0)
                                {
                                    account =
                                        Context.EmployeeRegistrations.FirstOrDefault(x => x.Id == supervisorUser.EmployeeId)?.EmployeeAccountId;
                                }
                            }
                            if (!isPermission.CloseDay)
                            {
                                user.TerminalId = null;

                            }
                            var terminal = await Context.DayStarts.FindAsync(request.Id);
                            if(terminal == null)
                                throw new NotFoundException("Terminal Not Found", request.Id);


                            terminal.TotalCash = request.VerifyCash;
                            terminal.IsActive = false;
                            terminal.EndTerminalBy = user.UserName;
                            terminal.EndTerminalFor = currentUserDetail.UserName;

                            terminal.ToTime = DateTime.Now;
                            terminal.CashInHand = request.CashInHand;
                            terminal.ExpenseCash = request.Expense;
                            terminal.BankAmount = request.Bank;
                            terminal.CarryCash = request.CarryCash;
                            if (user.UserName != currentUserDetail.UserName) terminal.SuperVisorName = user.UserName;
                            terminal.CreditReason = request.CreditReason;
                            terminal.DayEndUser = user.UserName;

                            var isLastTerminal =  Context.DayStarts.Count(x => x.IsActive && x.DayStartId == terminal.DayStartId);
                            if (isLastTerminal == 1)
                            {
                                var dayStartWithTerminal = Context.DayStarts.Where(x => x.DayStartId == terminal.DayStartId);
                                var dayStarts = await Context.DayStarts.FirstOrDefaultAsync(x => x.Id == terminal.DayStartId,cancellationToken);

                                
                                dayStarts.CashInHand = dayStartWithTerminal.Sum(x => x.CashInHand) + terminal.CashInHand;
                                dayStarts.CarryCash = dayStartWithTerminal.Sum(x => x.CarryCash) + terminal.CarryCash;
                                dayStarts.ExpenseCash = dayStartWithTerminal.Sum(x => x.ExpenseCash) + terminal.ExpenseCash;
                                dayStarts.BankAmount = dayStartWithTerminal.Sum(x => x.BankAmount) + terminal.BankAmount;
                                dayStarts.TotalCash = (dayStartWithTerminal.Sum(x=>x.OpeningCash) + dayStarts.CashInHand) - dayStarts.ExpenseCash;
                                dayStarts.IsDayStart = true;
                                dayStarts.IsActive = false;
                                dayStarts.ToTime = DateTime.Now;
                                dayStarts.DayEndUser = user.UserName;
                                dayStarts.EndTerminalBy = user.UserName;
                                dayStarts.EndTerminalFor = currentUserDetail.UserName;

                            }
                            //Get EmployeeAccount from user email
                            var employeeAccount = Context.EmployeeRegistrations.FirstOrDefault(x => x.Email == user.Email)?.EmployeeAccountId;

                            if (!request.IsSupervisorTrans && !request.IsSupervisor)
                            {
                                var terminalAccount =
                                    await Context.Terminals.FirstOrDefaultAsync(x => x.Id == terminal.CounterId,
                                        cancellationToken);
                                terminal.IsReceived = false;
                                if (employeeAccount != null)
                                {
                                    var supervisorTransaction = new Domain.Entities.Transaction()
                                    {
                                        DocumentId = terminal.Id,
                                        DocumentNumber = terminalAccount.Code,
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.Date,
                                        ApprovalDate = DateTime.UtcNow,
                                        Description = "This cash is added to employee account",
                                        Year = DateTime.UtcNow.ToString("yyyy"),
                                        AccountId = employeeAccount.Value,
                                        TransactionType = TransactionType.DayEnd,
                                        Credit = 0M,
                                        Debit = request.CarryCash
                                    };
                                    Context.Transactions.Add(supervisorTransaction);
                                    if (request.RemainingAmount < 0)
                                    {
                                        var transactionR = new Domain.Entities.Transaction()
                                        {
                                            DocumentId = terminal.Id,
                                            DocumentNumber = terminalAccount.Code,
                                            Date = DateTime.UtcNow,
                                            DocumentDate = request.Date,
                                            ApprovalDate = DateTime.UtcNow,
                                            Description = "During Verify Cash cashier enter amount extra than total amount",
                                            Year = DateTime.UtcNow.ToString("yyyy"),
                                            AccountId = employeeAccount.Value,
                                            TransactionType = TransactionType.DayEnd,
                                            Credit = 0M,
                                            Debit = request.RemainingAmount
                                        };
                                        Context.Transactions.Add(transactionR);
                                    }
                                    if (request.RemainingAmount > 0)
                                    {
                                        var transactionR = new Domain.Entities.Transaction()
                                        {
                                            DocumentId = terminal.Id,
                                            DocumentNumber = terminalAccount.Code,
                                            Date = DateTime.UtcNow,
                                            DocumentDate = request.Date,
                                            ApprovalDate = DateTime.UtcNow,
                                            Description = "During Verify Cash cashier enter amount less than total amount",
                                            Year = DateTime.UtcNow.ToString("yyyy"),
                                            AccountId = employeeAccount.Value,
                                            TransactionType = TransactionType.DayEnd,
                                            Credit = request.RemainingAmount,
                                            Debit = 0M
                                        };
                                        Context.Transactions.Add(transactionR);
                                    }

                                }
                                
                                if (terminalAccount.CashAccountId != null)
                                {
                                    var transaction = new Domain.Entities.Transaction()
                                    {
                                        DocumentId = terminal.Id,
                                        DocumentNumber = terminalAccount.Code.ToString(),
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.Date,
                                        ApprovalDate = DateTime.UtcNow,
                                        Description = "This cash holded to assigned counter",
                                        Year = DateTime.UtcNow.ToString("yyyy"),
                                        AccountId = terminalAccount.CashAccountId.Value,
                                        TransactionType = TransactionType.DayEnd,
                                        Credit = request.CarryCash + request.RemainingAmount,
                                        Debit = 0M
                                    };

                                    Context.Transactions.Add(transaction);
                                }
                            }
                            else
                            {
                                terminal.IsReceived = true;
                                terminal.ReceivingAmount = request.CarryCash;
                                var terminalAccount =
                                    await Context.Terminals.FirstOrDefaultAsync(x => x.Id == terminal.CounterId,
                                        cancellationToken);
                                if (account != null)
                                {
                                    var supervisorTransaction = new Domain.Entities.Transaction()
                                    {
                                        DocumentId = terminal.Id,
                                        DocumentNumber = terminalAccount.Code,
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.Date,
                                        ApprovalDate = DateTime.UtcNow,
                                        Description = "This cash is added to Supervisor account",
                                        Year = DateTime.UtcNow.ToString("yyyy"),
                                        AccountId = account.Value,
                                        TransactionType = TransactionType.DayEnd,
                                        Credit = 0M,
                                        Debit = request.CarryCash
                                    };
                                    Context.Transactions.Add(supervisorTransaction);
                                }
                               
                                if (terminalAccount.CashAccountId != null)
                                {
                                    var transaction = new Domain.Entities.Transaction()
                                    {
                                        DocumentId = terminal.Id,
                                        DocumentNumber = terminalAccount.Code.ToString(),
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.Date,
                                        ApprovalDate = DateTime.UtcNow,
                                        Description = "This cash holded to assigned counter",
                                        Year = DateTime.UtcNow.ToString("yyyy"),
                                        AccountId = terminalAccount.CashAccountId.Value,
                                        TransactionType = TransactionType.DayEnd,
                                        Credit = request.CarryCash + request.RemainingAmount,
                                        Debit = 0M
                                    };

                                    Context.Transactions.Add(transaction);

                                    if (request.RemainingAmount < 0 && employeeAccount != null)
                                    {
                                        var transactionR = new Domain.Entities.Transaction()
                                        {
                                            DocumentId = terminal.Id,
                                            DocumentNumber = terminalAccount.Code,
                                            Date = DateTime.UtcNow,
                                            DocumentDate = request.Date,
                                            ApprovalDate = DateTime.UtcNow,
                                            Description = "During Verify Cash cashier enter amount extra than total amount",
                                            Year = DateTime.UtcNow.ToString("yyyy"),
                                            AccountId = employeeAccount.Value,
                                            TransactionType = TransactionType.DayEnd,
                                            Credit = 0M,
                                            Debit = request.RemainingAmount
                                        };
                                        Context.Transactions.Add(transactionR);
                                    }
                                    if (request.RemainingAmount > 0 && employeeAccount != null)
                                    {
                                        var transactionR = new Domain.Entities.Transaction()
                                        {
                                            DocumentId = terminal.Id,
                                            DocumentNumber = terminalAccount.Code,
                                            Date = DateTime.UtcNow,
                                            DocumentDate = request.Date,
                                            ApprovalDate = DateTime.UtcNow,
                                            Description = "During Verify Cash cashier enter amount less than total amount",
                                            Year = DateTime.UtcNow.ToString("yyyy"),
                                            AccountId = employeeAccount.Value,
                                            TransactionType = TransactionType.DayEnd,
                                            Credit = request.RemainingAmount,
                                            Debit = 0M
                                        };
                                        Context.Transactions.Add(transactionR);
                                    }
                                }
                            }

                            var deleteHoldInvoices = Context.Sales.Where(x => x.InvoiceType == InvoiceType.Hold);
                            Context.Sales.RemoveRange(deleteHoldInvoices);
                            using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = Context.SyncLog(),
                                Code = _code,
                            }, cancellationToken);

                            var success = await Context.SaveChangesAsync(cancellationToken);

                            scope.Complete();
                           
                            if (success > 0)
                            {
                                return terminal.Id;
                            }
                            else
                            {
                                return Guid.Empty;
                            }
                            
                            
                        }
                        
                    }
                    else
                    {
                        throw new ObjectAlreadyExistsException("User Credentials are Invalid");
                    }

                    return Guid.Empty;
                }
                catch (NotFoundException e)
                {
                    Logger.LogInformation(e.Message);
                    throw new NotFoundException(e.Message,null);
                }
                catch (ObjectAlreadyExistsException e)
                {
                    Logger.LogInformation(e.Message);
                    throw new ObjectAlreadyExistsException(e.Message);
                }
                catch (Exception e)
                {
                    Logger.LogInformation(e.Message);
                    throw new ApplicationException("Something Went Wrong");
                }
            }
        }
    }
}
