using Focus.Business.BarCodeSetup.Models;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.ItemViewsSetups.Commands;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.BarCodeSetup.Commands
{
    public class BarCodeSetupAddUpdateCommand : IRequest<Message>
    {
        public BarCodeSetupLookupModel BarCodeSetup { get; set; }
        public string DocumentName { get; set; }
        public bool IsForAllProduct { get; set; }
        public class Handler : IRequestHandler<BarCodeSetupAddUpdateCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<BarCodeSetupAddUpdateCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(BarCodeSetupAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.DocumentName == "BarCode")
                    {
                        if (request.BarCodeSetup.ItemViewSetupList.Count > 0)
                        {
                            var barCodeSetup = await _context.ListOrderSetups.Where(x => x.DocumentName == "BarCode").ToListAsync();

                            _context.ListOrderSetups.RemoveRange(barCodeSetup);

                            var customerListOrder = request.BarCodeSetup.ItemViewSetupList.Select(x => new ListOrderSetup
                            {
                                DisplayName = x.DisplayName,
                                Name = x.Name,
                                Sequence = x.Sequence,
                                DocumentName = "BarCode"
                            }).ToList();

                            await _context.ListOrderSetups.AddRangeAsync(customerListOrder);
                        }
                    }

                   

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = _context.SyncLog(),
                        Code = _code,

                    }, cancellationToken);

                    await _context.SaveChangesAsync();


                    return new Message
                    {
                        IsSuccess = true,
                        IsAddUpdate = "Data has been updated successfully"
                    };
                }

                catch (Exception e)
                {
                    var message = new Message
                    {
                        IsSuccess = false,
                        IsAddUpdate = e.Message
                    };
                    _logger.LogError(e.Message);

                    return message;
                }
            }
        }
    }
}
