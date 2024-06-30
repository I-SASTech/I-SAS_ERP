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
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Extensions;
using System.Text.RegularExpressions;
using Focus.Business.Claims.Command.UpdateClaims;
using DocumentFormat.OpenXml.InkML;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.DayStarts.Commands.AddUpdateDayStarts
{
    public class AddUpdateDayStartsCommand : IRequest<DayStartLookupModel>
    {
        //Get all variable in entity
        public Guid? Id { get; set; }
        public DateTime? Date { get; set; }
        public Guid SaleId { get; set; }
        public Guid CounterId { get; set; }
        public Guid LocationId { get; set; }
        public decimal OpeningCash { get; set; }
        public decimal VerifyCash { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool IsDayStart { get; set; }
        public decimal CarryCash { get; set; }
        public decimal CashInHand { get; set; }
        public bool IsExpenseDay { get; set; }
        public bool IsFirstUser { get; set; }
        public decimal SupervisorCash { get; set; }
        public decimal ExpenseCash { get; set; }
        public bool IsOpenDay { get; set; }
        public bool IsSupervisorLogin { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateDayStartsCommand, DayStartLookupModel>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly IUserComponent _userComponent;
            private readonly IPrincipal _principal;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMediator _mediator;
            private string _code;

            //Constructor
            public Handler(IApplicationDbContext context, ILogger<AddUpdateDayStartsCommand> logger,
                SignInManager<ApplicationUser> signInManager, IUserComponent userComponent, IPrincipal principal,
                UserManager<ApplicationUser> userManager, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _signInManager = signInManager;
                _userComponent = userComponent;
                _principal = principal;
                _userManager = userManager;
                _mediator = mediator;
            }


            public async Task<DayStartLookupModel> Handle(AddUpdateDayStartsCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    SignInResult result;
                    ApplicationUser user;

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

                            request.User = user.UserName;
                            if (user is { IsActive: true })
                            {
                                throw new ObjectAlreadyExistsException("User Credentials are Invalid");
                                //return new DayStartLookupModel
                                //{
                                //    IsLoginFail = true
                                //};
                            }
                        }
                        else
                        {
                            user = await _userManager.FindByEmailAsync(request.User);
                            request.User = user.UserName;
                            result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
                            if (user.IsActive)
                            {
                                throw new ObjectAlreadyExistsException("User Credentials are Invalid");
                                //return new DayStartLookupModel
                                //{
                                //    IsLoginFail = true
                                //};
                            }
                        }

                    }
                    else
                    {
                        result = await _signInManager.PasswordSignInAsync(request.User, request.Password, false, lockoutOnFailure: false);
                        user = await _userManager.FindByNameAsync(request.User);
                        request.User = user.UserName;

                        if (user != null && user.IsActive)
                        {
                            throw new ObjectAlreadyExistsException("User Credentials are Invalid");
                            //return new DayStartLookupModel
                            //{
                            //    IsLoginFail = true
                            //};
                        }
                    }

                    //var result = await _signInManager.PasswordSignInAsync(request.User, request.Password, false, lockoutOnFailure: false);

                    if (!result.Succeeded)
                    {
                        throw new ObjectAlreadyExistsException("User Credentials are Invalid");
                        //return new DayStartLookupModel
                        //{
                        //    IsLoginFail = true
                        //};
                    }

                    var requestUser = await _userManager.FindByNameAsync(request.User);

                    var currentLoginUser = _principal.Identity.UserId();

                    var currentUserDetail = await _userManager.FindByIdAsync(currentLoginUser);

                    if(currentUserDetail.UserName != requestUser.UserName && !request.IsSupervisorLogin)
                        throw new ObjectAlreadyExistsException("You are not Supervisor to Start Day of other User");

                    var isPermission = await Context.LoginPermissions
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.UserId == Guid.Parse(requestUser.Id) && (request.IsDayStart ? x.StartDay : x.CloseDay), cancellationToken);

                    //if (request.IsOpenDay)
                    //{
                    //    //requestUser.TerminalId = request.CounterId;

                    //}

                    //if (request.IsSupervisorLogin)
                    //{
                    //    isPermission.StartDay = true;
                    //}
                    if (isPermission == null)
                    {
                        throw new ObjectAlreadyExistsException("User has not Permission to Day Start");
                        //return new DayStartLookupModel
                        //{
                        //    HasPermission = false
                        //};
                    }
                    // Day List
                    var dayLists = Context.DayStarts
                        .AsNoTracking().AsQueryable();
                    //Active Terminal
                    var activeTerminal = dayLists.Where(x => x.IsActive && !x.IsDayStart).Select(x => x.CounterId).ToList();
                    //Active Terminal Exception
                    if (activeTerminal.Contains(request.CounterId))
                    {
                        throw new ObjectAlreadyExistsException("Terminal already assigned to other User");
                    }

                    if (request.CounterId == Guid.Empty)
                    {
                        throw new ObjectAlreadyExistsException("You have not select Counter");

                    }
                    var dayAlreadyExist = dayLists
                        .FirstOrDefault(x => x.StartTerminalFor == currentUserDetail.UserName && !x.IsDayStart && x.IsActive);
                    if (dayAlreadyExist != null)
                    {
                        throw new ObjectAlreadyExistsException("Day Already Exist");
                    }

                    object token = null;

                   

                    if (isPermission.IsSupervisor && result.Succeeded && isPermission.StartDay)
                    {
                       

                        var dayStartId = Guid.Empty;
                        if (request.IsFirstUser)
                        {
                            var previousDayHistory = dayLists.OrderBy(x =>
                                !x.IsActive && x.IsDayStart && x.ToTime != null && x.DayEndUser != null).LastOrDefault();

                            var dayStart = new DayStart
                            {
                                Date = request.Date,
                                SaleId = request.SaleId,
                                CounterId = request.CounterId,
                                //TerminalId = request.TerminalId,
                                LocationId = request.LocationId,
                                OpeningCash = previousDayHistory == null ? 0m : previousDayHistory.TotalCash,
                                FromTime = request.FromTime,
                                ToTime = request.ToTime,
                                IsActive = request.IsActive,
                                IsExpenseDay = request.IsExpenseDay,
                                StartTerminalBy = requestUser.UserName,
                                StartTerminalFor = currentUserDetail.UserName,
                                //SupervisorUserName = loginUser.UserName,
                                IsDayStart = true,
                                DayEndUser = !request.IsDayStart ? request.User : null,
                                DayStartUser = request.IsDayStart ? request.User : null,
                                IsReceived = false
                            };
                            Context.DayStarts.Add(dayStart);
                            dayStartId = dayStart.Id;

                            if (currentLoginUser == requestUser.Id)
                            {
                                var dayStart2 = new DayStart
                                {
                                    Date = request.Date,
                                    SaleId = request.SaleId,
                                    CounterId = request.CounterId,
                                    //TerminalId = request.TerminalId,
                                    LocationId = request.LocationId,
                                    OpeningCash = request.OpeningCash,
                                    VerifyCash = request.VerifyCash,
                                    FromTime = request.FromTime,
                                    ToTime = request.ToTime,
                                    IsActive = request.IsActive,
                                    IsExpenseDay = request.IsExpenseDay,
                                    StartTerminalBy = requestUser.UserName,
                                    StartTerminalFor = currentUserDetail.UserName,
                                    Reason = request.Reason,
                                    //SupervisorUserName = loginUser.UserName,
                                    IsDayStart = false,
                                    DayStartId = dayStartId,
                                    DayEndUser = !request.IsDayStart ? request.User : null,
                                    DayStartUser = request.IsDayStart ? request.User : null,
                                    IsReceived = false
                                };
                                Context.DayStarts.Add(dayStart2);
                                dayStartId = dayStart.Id;
                                string printerName = string.Empty;

                                Random rnd = new Random();
                                for (int i = 0; i < 11; i++)
                                {
                                    _code += rnd.Next(0, 9).ToString();
                                }
                                await _mediator.Send(new AddUpdateSyncRecordCommand()
                                {
                                    SyncRecordModels = Context.SyncLog(),
                                    Code = _code,
                                }, cancellationToken);

                                await Context.SaveChangesAsync(cancellationToken);
                                if (request.IsDayStart)
                                {
                                    printerName = Context.Terminals.FirstOrDefault(x => x.Id == request.CounterId)?.PrinterName;
                                    token = await _mediator.Send(new UpdateClaimsCommand
                                    {
                                        Email = request.User,
                                        User = _principal.Identity.UserId(),
                                        CounterId = request.CounterId,
                                        DayId = dayStart.Id,
                                        ApplicationUser = user
                                    }, CancellationToken.None);
                                }
                                dayStart.OpeningCash = dayStart.OpeningCash + dayStart2.OpeningCash;
                                

                                return new DayStartLookupModel
                                {
                                    DayId = dayStart.Id,
                                    Token = token,
                                    IsExpenseDay = dayStart.IsExpenseDay,
                                    PrinterName = printerName
                                };
                            }
                            else
                            {
                                var dayStart1 = new DayStart
                                {
                                    Date = request.Date,
                                    SaleId = request.SaleId,
                                    CounterId = request.CounterId,
                                    //TerminalId = request.TerminalId,
                                    LocationId = request.LocationId,
                                    OpeningCash = request.OpeningCash,
                                    VerifyCash = request.VerifyCash,
                                    FromTime = request.FromTime,
                                    ToTime = request.ToTime,
                                    IsActive = request.IsActive,
                                    IsExpenseDay = request.IsExpenseDay,
                                    StartTerminalBy = requestUser.UserName,
                                    StartTerminalFor = currentUserDetail.UserName,
                                    Reason = request.Reason,
                                    //SupervisorUserName = loginUser.UserName,
                                    IsDayStart = false,
                                    DayStartId = dayStartId,
                                    DayEndUser = !request.IsDayStart ? request.User : null,
                                    DayStartUser = request.IsDayStart ? request.User : null,
                                    IsReceived = false
                                };
                                Context.DayStarts.Add(dayStart1);
                                string printerName = string.Empty;
                                if (request.IsDayStart)
                                {
                                    printerName = Context.Terminals.FirstOrDefault(x => x.Id == request.CounterId)?.PrinterName;
                                    token = await _mediator.Send(new UpdateClaimsCommand
                                    {
                                        Email = request.User,
                                        User = _principal.Identity.UserId(),
                                        CounterId = request.CounterId,
                                        DayId = dayStart.Id
                                    }, CancellationToken.None);
                                }
                                dayStart.OpeningCash = dayStart.OpeningCash + dayStart1.OpeningCash;

                                Random rnd1 = new Random();
                                for (int i = 0; i < 11; i++)
                                {
                                    _code += rnd1.Next(0, 9).ToString();
                                }
                                await _mediator.Send(new AddUpdateSyncRecordCommand()
                                {
                                    SyncRecordModels = Context.SyncLog(),
                                    Code = _code,
                                }, cancellationToken);

                                await Context.SaveChangesAsync(cancellationToken);

                                return new DayStartLookupModel
                                {
                                    DayId = dayStart.Id,
                                    Token = token,
                                    IsExpenseDay = dayStart.IsExpenseDay,
                                    PrinterName = printerName
                                };
                            }
                        }
                        else
                        {
                            var isDayStart = Context.DayStarts.OrderBy(x => x.IsDayStart && x.IsActive && x.FromTime != null && x.ToTime == null).LastOrDefault();

                            dayStartId = isDayStart.Id;
                            if (currentLoginUser == requestUser.Id)
                            {
                                var dayStart = new DayStart
                                {
                                    Date = request.Date,
                                    SaleId = request.SaleId,
                                    CounterId = request.CounterId,
                                    //TerminalId = request.TerminalId,
                                    LocationId = request.LocationId,
                                    OpeningCash = request.OpeningCash,
                                    VerifyCash = request.VerifyCash,
                                    FromTime = request.FromTime,
                                    ToTime = request.ToTime,
                                    IsActive = request.IsActive,
                                    Reason = request.Reason,
                                    IsExpenseDay = request.IsExpenseDay,
                                    StartTerminalBy = requestUser.UserName,
                                    StartTerminalFor = currentUserDetail.UserName,
                                    //SupervisorUserName = loginUser.UserName,
                                    IsDayStart = false,
                                    DayStartId = dayStartId,
                                    DayEndUser = !request.IsDayStart ? request.User : null,
                                    DayStartUser = request.IsDayStart ? request.User : null,
                                    IsReceived = false
                                };
                                Context.DayStarts.Add(dayStart);
                                string printerName = string.Empty;
                                if (request.IsDayStart)
                                {
                                    printerName = Context.Terminals.FirstOrDefault(x => x.Id == request.CounterId)?.PrinterName;
                                    token = await _mediator.Send(new UpdateClaimsCommand
                                    {
                                        Email = request.User,
                                        User = _principal.Identity.UserId(),
                                        CounterId = request.CounterId,
                                        DayId = dayStart.Id
                                    }, CancellationToken.None);
                                }
                                isDayStart.OpeningCash = isDayStart.OpeningCash + dayStart.OpeningCash;

                                Random rnd2 = new Random();
                                for (int i = 0; i < 11; i++)
                                {
                                    _code += rnd2.Next(0, 9).ToString();
                                }
                                await _mediator.Send(new AddUpdateSyncRecordCommand()
                                {
                                    SyncRecordModels = Context.SyncLog(),
                                    Code = _code,
                                }, cancellationToken);

                                await Context.SaveChangesAsync(cancellationToken);
                                return new DayStartLookupModel
                                {
                                    DayId = dayStart.Id,
                                    Token = token,
                                    IsExpenseDay = dayStart.IsExpenseDay,
                                    PrinterName = printerName
                                };
                            }
                            else
                            {
                                var dayStart = new DayStart
                                {
                                    Date = request.Date,
                                    SaleId = request.SaleId,
                                    CounterId = request.CounterId,
                                    //TerminalId = request.TerminalId,
                                    LocationId = request.LocationId,
                                    OpeningCash = request.OpeningCash,
                                    VerifyCash = request.VerifyCash,
                                    FromTime = request.FromTime,
                                    ToTime = request.ToTime,
                                    IsActive = request.IsActive,
                                    IsExpenseDay = request.IsExpenseDay,
                                    StartTerminalBy = requestUser.UserName,
                                    StartTerminalFor = currentUserDetail.UserName,
                                    Reason = request.Reason,
                                    //SupervisorUserName = loginUser.UserName,
                                    IsDayStart = false,
                                    DayStartId = dayStartId,
                                    DayEndUser = !request.IsDayStart ? request.User : null,
                                    DayStartUser = request.IsDayStart ? request.User : null,
                                    IsReceived = false
                                };
                                Context.DayStarts.Add(dayStart);
                                string printerName = string.Empty;
                                if (request.IsDayStart)
                                {
                                    printerName = Context.Terminals.FirstOrDefault(x => x.Id == request.CounterId)?.PrinterName;
                                    token = await _mediator.Send(new UpdateClaimsCommand
                                    {
                                        Email = request.User,
                                        User = _principal.Identity.UserId(),
                                        CounterId = request.CounterId,
                                        DayId = dayStart.Id,
                                        ApplicationUser = user
                                    }, CancellationToken.None);
                                }
                                isDayStart.OpeningCash = isDayStart.OpeningCash + dayStart.OpeningCash;

                                Random rnd3 = new Random();
                                for (int i = 0; i < 11; i++)
                                {
                                    _code += rnd3.Next(0, 9).ToString();
                                }
                                await _mediator.Send(new AddUpdateSyncRecordCommand()
                                {
                                    SyncRecordModels = Context.SyncLog(),
                                    Code = _code,
                                }, cancellationToken);

                                await Context.SaveChangesAsync(cancellationToken);

                                return new DayStartLookupModel
                                {
                                    DayId = dayStart.Id,
                                    Token = token,
                                    IsExpenseDay = dayStart.IsExpenseDay,
                                    PrinterName = printerName
                                };
                            }
                        }



                    }
                    else if (!isPermission.IsSupervisor && isPermission.StartDay && result.Succeeded)
                    {
                        var dayStartId = Guid.Empty;
                        if (request.IsFirstUser)
                        {
                            var previousDayHistory = dayLists.OrderBy(x => !x.IsActive && x.IsDayStart && x.ToTime != null && x.DayEndUser != null).LastOrDefault();

                            var dayStart = new DayStart
                            {
                                Date = request.Date,
                                SaleId = request.SaleId,
                                CounterId = request.CounterId,
                                //TerminalId = request.TerminalId,
                                LocationId = request.LocationId,
                                OpeningCash = previousDayHistory == null ? 0 : previousDayHistory.TotalCash,
                                VerifyCash = request.VerifyCash,
                                FromTime = request.FromTime,
                                ToTime = request.ToTime,
                                IsActive = request.IsActive,
                                IsExpenseDay = request.IsExpenseDay,
                                StartTerminalBy = requestUser.UserName,
                                StartTerminalFor = currentUserDetail.UserName,
                                Reason = request.Reason,
                                //SupervisorUserName = loginUser.UserName,
                                IsDayStart = true,
                                DayEndUser = !request.IsDayStart ? request.User : null,
                                DayStartUser = request.IsDayStart ? request.User : null,
                                IsReceived = false
                            };
                            Context.DayStarts.Add(dayStart);

                            dayStartId = dayStart.Id;

                            var dayStart1 = new DayStart
                            {
                                Date = request.Date,
                                SaleId = request.SaleId,
                                CounterId = request.CounterId,
                                //TerminalId = request.TerminalId,
                                LocationId = request.LocationId,
                                OpeningCash = request.OpeningCash,
                                VerifyCash = request.VerifyCash,
                                FromTime = request.FromTime,
                                ToTime = request.ToTime,
                                Reason = request.Reason,
                                IsActive = request.IsActive,
                                IsExpenseDay = request.IsExpenseDay,
                                StartTerminalBy = requestUser.UserName,
                                StartTerminalFor = currentUserDetail.UserName,
                                //SupervisorUserName = loginUser.UserName,
                                IsDayStart = false,
                                DayStartId = dayStartId,
                                DayEndUser = !request.IsDayStart ? request.User : null,
                                DayStartUser = request.IsDayStart ? request.User : null,
                                IsReceived = false
                            };
                            Context.DayStarts.Add(dayStart1);
                            string printerName = string.Empty;
                            if (request.IsDayStart)
                            {
                                printerName = Context.Terminals.FirstOrDefault(x => x.Id == request.CounterId)?.PrinterName;
                                token = await _mediator.Send(new UpdateClaimsCommand
                                {
                                    Email = request.User,
                                    User = _principal.Identity.UserId(),
                                    CounterId = request.CounterId,
                                    DayId = dayStart.Id,
                                    ApplicationUser = user
                                }, CancellationToken.None);
                            }
                            dayStart.OpeningCash = dayStart.OpeningCash + dayStart1.OpeningCash;

                            Random rnd4 = new Random();
                            for (int i = 0; i < 11; i++)
                            {
                                _code += rnd4.Next(0, 9).ToString();
                            }
                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = Context.SyncLog(),
                                Code = _code,
                            }, cancellationToken);

                            await Context.SaveChangesAsync(cancellationToken);

                            return new DayStartLookupModel
                            {
                                DayId = dayStart.Id,
                                Token = token,
                                IsExpenseDay = dayStart.IsExpenseDay,
                                PrinterName = printerName
                            };
                        }
                        else
                        {
                            var isDayStart = Context.DayStarts.OrderBy(x => x.IsDayStart && x.IsActive && x.FromTime != null && x.ToTime == null).LastOrDefault();
                            dayStartId = isDayStart.Id;

                            var dayStart = new DayStart
                            {
                                Date = request.Date,
                                SaleId = request.SaleId,
                                CounterId = request.CounterId,
                                //TerminalId = request.TerminalId,
                                LocationId = request.LocationId,
                                OpeningCash = request.OpeningCash,
                                VerifyCash = request.VerifyCash,
                                FromTime = request.FromTime,
                                ToTime = request.ToTime,
                                IsActive = request.IsActive,
                                Reason = request.Reason,
                                IsExpenseDay = request.IsExpenseDay,
                                StartTerminalBy = requestUser.UserName,
                                StartTerminalFor = currentUserDetail.UserName,
                                IsDayStart = false,
                                DayStartId = dayStartId,
                                DayEndUser = !request.IsDayStart ? request.User : null,
                                DayStartUser = request.IsDayStart ? request.User : null,
                                IsReceived = false
                            };
                            Context.DayStarts.Add(dayStart);
                            string printerName = string.Empty;
                            if (request.IsDayStart)
                            {
                                printerName = Context.Terminals.FirstOrDefault(x => x.Id == request.CounterId)?.PrinterName;
                                token = await _mediator.Send(new UpdateClaimsCommand
                                {
                                    Email = request.User,
                                    User = _principal.Identity.UserId(),
                                    CounterId = request.CounterId,
                                    DayId = dayStart.Id,
                                    ApplicationUser =user
                                }, CancellationToken.None);
                            }
                            isDayStart.OpeningCash = isDayStart.OpeningCash + dayStart.OpeningCash;

                            Random rnd5 = new Random();
                            for (int i = 0; i < 11; i++)
                            {
                                _code += rnd5.Next(0, 9).ToString();
                            }
                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = Context.SyncLog(),
                                Code = _code,
                            }, cancellationToken);

                            await Context.SaveChangesAsync(cancellationToken);

                            return new DayStartLookupModel
                            {
                                DayId = dayStart.Id,
                                Token = token,
                                IsExpenseDay = dayStart.IsExpenseDay,
                                PrinterName = printerName
                            };
                        }
                    }
                    return new DayStartLookupModel();
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
