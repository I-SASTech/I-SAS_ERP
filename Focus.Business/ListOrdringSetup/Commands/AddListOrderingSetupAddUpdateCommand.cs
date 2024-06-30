using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.ListOrdringSetup.Models;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.ListOrdringSetup.Commands
{
    public class AddListOrderingSetupAddUpdateCommand : IRequest<Message>
    {
        public ListOrderSetupModel ListOrderSetups { get; set; }
        public string DocumentName { get; set; }
        public class Handler : IRequestHandler<AddListOrderingSetupAddUpdateCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<AddListOrderingSetupAddUpdateCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(AddListOrderingSetupAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if(request.DocumentName == "Customer")
                    {
                        if (request.ListOrderSetups.CustomerListView.Count > 0)
                        {
                            var listOrderSetup = await _context.ListOrderSetups.Where(x => x.DocumentName == "Customer").ToListAsync();

                            _context.ListOrderSetups.RemoveRange(listOrderSetup);

                            var customerListOrder = request.ListOrderSetups.CustomerListView.Select(x => new ListOrderSetup
                            {
                                DisplayName = x.DisplayName,
                                Name = x.Name,
                                Sequence = x.Sequence,
                                DocumentName = "Customer"
                            }).ToList();

                            await _context.ListOrderSetups.AddRangeAsync(customerListOrder);
                        }
                    }
                    if(request.DocumentName == "Supplier")
                    {
                        if (request.ListOrderSetups.CustomerListView.Count > 0)
                        {
                            var listOrderSetup = await _context.ListOrderSetups.Where(x => x.DocumentName == "Supplier").ToListAsync();

                            _context.ListOrderSetups.RemoveRange(listOrderSetup);

                            var customerListOrder = request.ListOrderSetups.SupplierListView.Select(x => new ListOrderSetup
                            {
                                DisplayName = x.DisplayName,
                                Name = x.Name,
                                Sequence = x.Sequence,
                                DocumentName = "Supplier"
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
