using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.DayStarts.Commands.AddUpdateDayEnd;
using Focus.Business.Exceptions;
using Focus.Business.Extensions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.Users;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.DayStarts.Commands.ReceivingAmount
{
    public class ReceivingAmountCommand : IRequest<Guid>
    {
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public Guid Id { get; set; }

        //Get all variable in entity


        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<ReceivingAmountCommand, Guid>
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
            public Handler(IApplicationDbContext context, ILogger<ReceivingAmountCommand> logger, IPrincipal principal,
                SignInManager<ApplicationUser> signInManager, IUserComponent userComponent,
                UserManager<ApplicationUser> userManager, IMediator mediator)
            {
                Context = context;
                _principal = principal;
                Logger = logger;
                _signInManager = signInManager;
                _userComponent = userComponent;
                _userManager = userManager;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(ReceivingAmountCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    SignInResult result;
                    ApplicationUser user;
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.UserName.IndexOf('@') > -1)
                    {
                        //Validate email format
                        string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                        Regex re = new Regex(emailRegex);
                        if (!re.IsMatch(request.UserName))
                        {
                            result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false,
                                lockoutOnFailure: false);
                            user = await _userManager.FindByNameAsync(request.UserName);
                            if (user != null)
                                request.UserName = user.Email;

                        }
                        else
                        {
                            user = await _userManager.FindByEmailAsync(request.UserName);
                            result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false,
                                lockoutOnFailure: false);

                        }

                    }
                    else
                    {
                        result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false,
                            lockoutOnFailure: false);
                        user = await _userManager.FindByNameAsync(request.UserName);
                        if (user != null)
                            request.UserName = user.Email;


                    }

                    //var isPermission = await Context.LoginPermissions
                    //    .AsNoTracking()
                    //    .FirstOrDefaultAsync(x => x.UserId == Guid.Parse(user.Id) && x.CloseDay, cancellationToken);

                    var currentLoginUser = _principal.Identity.UserId();

                    var currentUserDetail = await _userManager.FindByIdAsync(currentLoginUser);
                    if (result.Succeeded)
                    {
                        var account = Context.EmployeeRegistrations
                                .FirstOrDefault(x => x.Email == user.Email)?.EmployeeAccountId;



                        var terminal = await Context.DayStarts.FindAsync(request.Id);

                        if (terminal == null)
                            throw new NotFoundException("Terminal Not Found", request.Id);

                        user = await _userManager.FindByNameAsync(terminal.StartTerminalFor);
                        Guid? employeeAccount = null;
                        if (user != null)
                        {
                            employeeAccount = Context.EmployeeRegistrations
                                .FirstOrDefault(x => x.Email == user.Email)?.EmployeeAccountId;
                        }

                        terminal.IsReceived = true;
                        terminal.ReceivingAmount = request.Amount;
                        terminal.Reason = request.Reason;
                        var terminalCode = Context.Terminals.FirstOrDefault(x => x.Id == terminal.CounterId)?.Code;
                        if (employeeAccount != null)
                        {
                            var supervisorTransaction = new Domain.Entities.Transaction()
                            {
                                DocumentId = terminal.Id,
                                DocumentNumber = terminalCode,
                                Date = DateTime.UtcNow,
                                DocumentDate = DateTime.UtcNow,
                                ApprovalDate = DateTime.UtcNow,
                                Description = "This cash is added to employee account",
                                Year = DateTime.UtcNow.ToString("yyyy"),
                                AccountId = employeeAccount.Value,
                                TransactionType = TransactionType.DayEnd,
                                Credit = request.Amount,
                                Debit = 0M
                            };
                            Context.Transactions.Add(supervisorTransaction);
                        }


                        if (account != null)
                        {
                            var supervisorTransaction = new Domain.Entities.Transaction()
                            {
                                DocumentId = terminal.Id,
                                DocumentNumber = terminalCode,
                                Date = DateTime.UtcNow,
                                DocumentDate = DateTime.UtcNow,
                                ApprovalDate = DateTime.UtcNow,
                                Description = "This cash is added to Supervisor account",
                                Year = DateTime.UtcNow.ToString("yyyy"),
                                AccountId = account.Value,
                                TransactionType = TransactionType.DayEnd,
                                Credit = 0M,
                                Debit = request.Amount
                            };
                            Context.Transactions.Add(supervisorTransaction);
                        }
                        var isAnyActiveTerminal = await Context.DayStarts.AllAsync(
                            x => x.DayStartId == terminal.DayStartId && !terminal.IsActive && x.IsReceived == false, cancellationToken);
                        if (isAnyActiveTerminal)
                        {
                            var superDay = await Context.DayStarts.FirstOrDefaultAsync(x => x.Id == terminal.DayStartId,
                                cancellationToken);
                            superDay.IsDayStart = false;
                        }
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
