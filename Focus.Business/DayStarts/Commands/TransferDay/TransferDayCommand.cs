using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.DayStarts.Queries.GetOpeninigBalance;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.Users;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.DayStarts.Commands.TransferDay
{
    public class TransferDayCommand : IRequest<Guid>
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
        public string ToUser { get; set; }
        public string ToPassword { get; set; }
        public Guid? DayStartId { get; set; }
        public bool IsOpenDay { get; set; }
        public string CreditReason { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<TransferDayCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly IUserComponent _userComponent;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMediator _mediator;
            private string _code;

            //Constructor
            public Handler(IApplicationDbContext context, ILogger<TransferDayCommand> logger,
                SignInManager<ApplicationUser> signInManager, IUserComponent userComponent,
                UserManager<ApplicationUser> userManager, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _signInManager = signInManager;
                _userComponent = userComponent;
                _userManager = userManager;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(TransferDayCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    SignInResult toResult;
                    ApplicationUser toUser;
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.ToUser.IndexOf('@') > -1)
                    {
                        //Validate email format
                        string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                        Regex re = new Regex(emailRegex);
                        if (!re.IsMatch(request.User))
                        {
                            toResult = await _signInManager.PasswordSignInAsync(request.ToUser, request.ToPassword, false, lockoutOnFailure: false);
                            toUser = await _userManager.FindByNameAsync(request.ToUser);
                            if (toUser != null)
                                request.ToUser = toUser.Email;
                            

                        }
                        else
                        {
                            toUser = await _userManager.FindByEmailAsync(request.ToUser);
                            toResult = await _signInManager.PasswordSignInAsync(toUser.UserName, request.ToPassword, false, lockoutOnFailure: false);

                        }

                        
                    }
                    else
                    {
                        toResult = await _signInManager.PasswordSignInAsync(request.ToUser, request.ToPassword, false, lockoutOnFailure: false);
                        toUser = await _userManager.FindByNameAsync(request.ToUser);
                        if (toUser != null)
                            request.ToUser = toUser.Email;
                        


                    }

                    if (!toResult.Succeeded)
                        throw new ObjectAlreadyExistsException("To User Credentials are Invalid");
                   
                    var isPermission = await Context.LoginPermissions
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.UserId == Guid.Parse(toUser.Id) && x.TransferCounter, cancellationToken);
                    //if (request.IsOpenDay)
                    //{
                    //    //user.TerminalId = null;
                    //    toUser.TerminalId = request.CounterId;
                    //}

                 
                    if (toResult.Succeeded  && isPermission != null)
                    {

                        var terminal = await Context.DayStarts.FirstOrDefaultAsync(x=>x.CounterId == request.CounterId && x.IsActive && !x.IsDayStart,cancellationToken);
                        if (terminal == null)
                            throw new NotFoundException("Terminal Not Found", request.Id);
                        var calculation = await _mediator.Send(new GetOpeningBalanceQuery()
                        {
                            Id = terminal.CounterId
                        });
                        var isAlreadyActiveTerminal =
                             Context.DayStarts.Any(x => x.StartTerminalFor == toUser.UserName && x.IsActive);
                        if (isAlreadyActiveTerminal)
                            throw new ObjectAlreadyExistsException("This user have already assigned terminal");
                        var dayStart =
                            await Context.DayStarts.FirstOrDefaultAsync(x => x.Id == terminal.DayStartId && x.IsDayStart, cancellationToken);

                        if (toUser != null)
                        {
                            terminal.StartTerminalFor = toUser.UserName;
                            terminal.StartTerminalBy = request.User;
                            terminal.DayStartUser = toUser.UserName;
                        }
                        var firstDayStart = await Context.DayStarts.FirstOrDefaultAsync(x=>x.IsDayStart && x.IsActive, cancellationToken);

                        if (toUser != null)
                        {
                            firstDayStart.StartTerminalFor = toUser.UserName;
                            //Context.DayStarts.Update(firstDayStart);
                        }
                        var total =( calculation.CashInHand +calculation.OpeningBalance) -
                                    calculation.DraftExpense;
                        var transferHistory = new TransferHistory
                        {
                            Date = terminal.Date,
                            CounterId = terminal.CounterId,
                            OpeningCash = calculation.OpeningBalance,
                            CashInHand = calculation.CashInHand,
                            VerifyCash = request.VerifyCash,
                            ExpenseCash = calculation.DraftExpense,
                            CarryCash = request.CarryCash,
                            TotalCash = total,
                            FromTime = request.FromTime,
                            ToTime = DateTime.UtcNow,
                            IsActive = true,
                            DayStartUser = terminal.DayStartUser,
                            DayEndUser = request.User,
                            Reason = terminal.Reason,
                            IsExpenseDay = request.IsExpenseDay,
                            StartTerminalBy = request.User,
                            StartTerminalFor = toUser.UserName,
                            DayStartId = terminal.DayStartId,
                            BankAmount = calculation.Bank,
                            NoOfTransaction = calculation.NoOfTransaction,
                            CreditReason = request.CreditReason
                        };

                        Context.TransferHistories.Add(transferHistory);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        var success = await Context.SaveChangesAsync(cancellationToken);
                        if (success > 0)
                        {
                            return transferHistory.Id;
                        }
                        else
                        {
                            throw new ObjectAlreadyExistsException("Not Save to Database");
                        }


                    }
                    else
                    {
                        throw new ObjectAlreadyExistsException("User have no permission");
                    }



                    //return Guid.Empty;
                }
                catch (NotFoundException e)
                {
                    Logger.LogInformation(e.Message);
                    throw new NotFoundException(e.Message, null);
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
