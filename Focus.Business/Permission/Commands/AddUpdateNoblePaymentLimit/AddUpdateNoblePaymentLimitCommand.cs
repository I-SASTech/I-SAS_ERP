using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.Permission.Commands.AddUpdateNoblePaymentLimit
{
    public class AddUpdateNoblePaymentLimitCommand : IRequest<Guid>
    {
        //Get all variable in entity
        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateNoblePaymentLimitCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;
            public IConfiguration Configuration { get; }
            private readonly IUserHttpContextProvider ContextProvider;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateNoblePaymentLimitCommand> logger,
                IConfiguration configuration, IUserHttpContextProvider contextProvider, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                Configuration = configuration;
                ContextProvider = contextProvider;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateNoblePaymentLimitCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    //var company =
                    //    Context.CompanyLicences.FirstOrDefault(x => x.CompanyId == EF.Property<Guid>(x, "ClientId"));
                    //Context.DisableTenantFilter = true;
                    var companyId = ContextProvider.GetCompanyId().ToString();
                    var query = "select * from PaymentLimits where CompanyId = '" + companyId + "'";
                    //connection.Open();
                    var connection = new SqlConnection(Configuration.GetConnectionString("PermissionConnection"));
                    var paymentLimit = connection.Query<PaymentLimitLookUp>(query).ToList();
                    

                    if (paymentLimit.Count > 0)
                    {
                        var currentCompanyPaymentData = Context.PaymentLimits.ToList();
                        if (paymentLimit.Count == currentCompanyPaymentData.Count)
                        {
                            var lastRecord = Context.PaymentLimits.LastOrDefault();
                            var lastLiveLimit = paymentLimit.LastOrDefault();
                            if (lastRecord != null && lastLiveLimit != null)
                            {
                                lastRecord.FromDate = lastLiveLimit.FromDate;
                                lastRecord.ToDate = lastLiveLimit.ToDate;
                                lastRecord.Message = lastLiveLimit.Message;
                                lastRecord.IsActive = lastLiveLimit.IsActive;
                            }
                        }
                        else
                        {
                            Context.PaymentLimits.RemoveRange(currentCompanyPaymentData);
                            foreach (var data in paymentLimit)
                            {
                                var payment = new PaymentLimit()
                                {
                                    IsActive = data.IsActive,
                                    FromDate = data.FromDate,
                                    ToDate = data.ToDate,
                                    Message = data.Message
                                };
                                Context.PaymentLimits.Add(payment);
                            }
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        Context.SaveChanges();
                        return Guid.NewGuid();
                    }

                    return Guid.Empty;
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
