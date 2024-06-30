using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Claims.Command.UpdateClaims;
using Focus.Business.DayStarts.Commands.AddUpdateDayStarts;
using Focus.Business.DayStarts.WholeSaleQueries;
using Focus.Business.Exceptions;
using Focus.Business.Extensions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.DayStarts.Commands.WholeSaleDayStartCommand
{
    public class AddUpdateWholeSaleDay : IRequest<WholeSaleDayStartLookUpModel>
    {
        //Get all variable in entity
        public Guid? Id { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateWholeSaleDay, WholeSaleDayStartLookUpModel>
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
            public Handler(IApplicationDbContext context, ILogger<AddUpdateWholeSaleDay> logger,
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


            public async Task<WholeSaleDayStartLookUpModel> Handle(AddUpdateWholeSaleDay request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.Id ==  null)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var currentLoginUser = _principal.Identity.UserId();

                        var currentUserDetail = await _userManager.FindByIdAsync(currentLoginUser);

                        var isPermission = await Context.LoginPermissions
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.UserId == Guid.Parse(currentUserDetail.Id), cancellationToken);


                        if (isPermission == null)
                        {
                            throw new ObjectAlreadyExistsException("User has not Permission to Day Start");

                        }
                        var terminal = Context.Terminals.FirstOrDefault(x=>x.TerminalType == TerminalType.CashCounter);

                        var dayStart = new DayStart
                        {
                            Date = DateTime.UtcNow,
                            SaleId = Guid.Parse(currentUserDetail.Id),
                            CounterId = terminal.Id,
                            LocationId = currentUserDetail.CompanyId,
                            IsActive = true,
                            StartTerminalFor = currentUserDetail.UserName,
                            IsDayStart = true,
                            FromTime = DateTime.UtcNow
                        };
                        Context.DayStarts.Add(dayStart);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        var token = await _mediator.Send(new UpdateClaimsCommand
                        {
                            Email = currentUserDetail.UserName,
                            User = _principal.Identity.UserId(),
                            CounterId = dayStart.CounterId,
                            DayId = dayStart.Id,
                        }, CancellationToken.None);
                        return new WholeSaleDayStartLookUpModel()
                        {
                            CounterName = terminal.Code,
                            Username = currentUserDetail.UserName,
                            IsDayStart = true,
                            FromTime = dayStart.FromTime.Value.ToString("d"),
                            IsActive = true,
                            DayStartId = dayStart.Id,
                            CounterId = dayStart.CounterId,
                            Token = token
                        }; 
                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var dayStart =
                            Context.DayStarts.OrderBy(x => x.IsActive && x.IsDayStart && x.Id == request.Id).LastOrDefault();
                        if (dayStart == null)
                            throw new NotFoundException("Day data not exist",null);
                        dayStart.IsActive = false;
                        dayStart.IsDayStart = false;
                        dayStart.ToTime = DateTime.UtcNow;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        return new WholeSaleDayStartLookUpModel()
                        {
                            CounterName = "TR-0001",
                            Username = "Not",
                            IsDayStart = false,
                            FromTime = dayStart.ToTime.Value.ToString("d"),
                            IsActive = false
                        };
                    }
                   
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
