using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.AutoPurchasing
{
    public class AutoPurchaseSettingCommand : IRequest<bool>
    {
        public AutoPurchaseSettingLookUp AutoPurchaseSetting { get; set; }

        public class Handler : IRequestHandler<AutoPurchaseSettingCommand, bool>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<AutoPurchaseSettingCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<bool> Handle(AutoPurchaseSettingCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.AutoPurchaseSetting.Id == Guid.Empty)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }
                        var autoPurchaseSetting = new AutoPurchaseSetting()
                        {
                            Day = request.AutoPurchaseSetting.Day,
                            TaxMethod = request.AutoPurchaseSetting.TaxMethod,
                            TaxRateId = request.AutoPurchaseSetting.TaxRateId,
                            WareHouseId = request.AutoPurchaseSetting.WareHouseId,
                        };
                        await _context.AutoPurchaseSettings.AddAsync(autoPurchaseSetting, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);
                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        // Return Product id after successfully updating data

                        return true;
                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var autoPurchaseSetting = await _context.AutoPurchaseSettings.FindAsync(request.AutoPurchaseSetting.Id);
                        if (autoPurchaseSetting == null)
                            throw new NotFoundException("Setting Not Found", "");

                        autoPurchaseSetting.Day = request.AutoPurchaseSetting.Day;
                        autoPurchaseSetting.TaxMethod = request.AutoPurchaseSetting.TaxMethod;
                        autoPurchaseSetting.TaxRateId = request.AutoPurchaseSetting.TaxRateId;
                        autoPurchaseSetting.WareHouseId = request.AutoPurchaseSetting.WareHouseId;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        // Return Product id after successfully updating data
                        var message = new Message
                        {
                            Id = autoPurchaseSetting.Id,
                            IsAddUpdate = "Data has been added successfully"
                        };
                        return true;
                    }
                }

                catch (NotFoundException notFoundException)
                {
                    _logger.LogError(notFoundException.Message);
                    return false;
                }

                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    return false;
                }
            }
        }

    }
}
