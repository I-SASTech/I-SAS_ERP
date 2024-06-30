using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Brands.Commands.AddUpdateBrand;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.LoginHistories.Commands.AddUpdateLoginHistory
{
    public class AddUpdateLoginHistoryCommand : IRequest<int>
    {
        //Get all variable in entity
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public string IpAddress { get; set; }
        public string OperatingSystem { get; set; }
        public bool IsLogin { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateLoginHistoryCommand, int>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            public readonly IUserHttpContextProvider _ContextProvider;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context,
                ILogger<AddUpdateLoginHistoryCommand> logger,
                IUserHttpContextProvider contextProvider, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _ContextProvider = contextProvider;
                _mediator = mediator;
            }

            [Obsolete]
            public async Task<int> Handle(AddUpdateLoginHistoryCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.IsLogin)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
                        // Get the IP  
                        var ipAddress = Dns.GetHostByName(hostName).AddressList[0].ToString();
                        //var userId = _ContextProvider.GetUserId();
                        var loginHistory = new LoginHistory()
                        {
                            UserId = request.UserId,
                            LoginDateTime = DateTime.UtcNow,
                            OperatingSystem = request.OperatingSystem,
                            IpAddress = ipAddress,
                            CompanyId = request.CompanyId
                        };
                        await Context.LoginHistories.AddAsync(loginHistory, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        var success = await Context.SaveChangesAsync(cancellationToken);
                        if (success > 0)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        //var userId = _ContextProvider.GetUserId();
                        var loggedInUserHistory = Context.LoginHistories.OrderBy(x=>x.Id).LastOrDefault(x => x.UserId == request.UserId && x.CompanyId == request.CompanyId);
                        if (loggedInUserHistory != null)
                        {
                            loggedInUserHistory.LogoutDateTime = DateTime.UtcNow;
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        var success = await Context.SaveChangesAsync(cancellationToken);
                        if (success > 0)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }

                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
