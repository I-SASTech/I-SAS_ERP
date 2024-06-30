using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.ItemViewsSetups.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Focus.Domain.Entities;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.ItemViewsSetups.Commands
{
    public class ItemViewAddUpdateCommand : IRequest<Message>
    {
        public ItemViewLookupModel ItemViews { get; set; }
        public bool IsForAllProduct { get; set; }
        public bool IsItemList { get; set; }
        public class Handler : IRequestHandler<ItemViewAddUpdateCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<ItemViewAddUpdateCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(ItemViewAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if(request.IsItemList)
                    {
                        if (request.ItemViews.ItemListViewSetup.Count > 0)
                        {
                            var itemListViewSetup = await _context.ItemsListDisplayOrderSetup.ToListAsync();

                            _context.ItemsListDisplayOrderSetup.RemoveRange(itemListViewSetup);

                            var itemsListDisplayName = request.ItemViews.ItemListViewSetup
                                                    .Select(x => new ItemsListDisplayOrderSetup()
                                                    {
                                                        DisplayName = x.DisplayName,
                                                        Name = x.Name,
                                                        Sequence = x.Sequence
                                                    }).ToList();

                            await _context.ItemsListDisplayOrderSetup.AddRangeAsync(itemsListDisplayName);
                            await _context.SaveChangesAsync();
                        }
                    }

                    if(request.ItemViews.ItemViewSetupList.Count > 0) 
                    {
                        var itemsDisplayName = await _context.ItemViewSetups.ToListAsync();

                        _context.ItemViewSetups.RemoveRange(itemsDisplayName);

                        var itemDisplayName = request.ItemViews.ItemViewSetupList
                                                .Select(x => new ItemViewSetup()
                                                {
                                                    DisplayName = x.DisplayName,
                                                    Name = x.Name,
                                                    Sequence = x.Sequence
                                                }).ToList();

                        await _context.ItemViewSetups.AddRangeAsync(itemDisplayName);
                        await _context.SaveChangesAsync();
                    }

                    if (request.ItemViews.itemViewSetupListForPrint.Count > 0)
                    {
                        var itemViewSetupForPrintList = await _context.ItemViewSetupForPrint.ToListAsync();

                        _context.ItemViewSetupForPrint.RemoveRange(itemViewSetupForPrintList);

                        var itemDisplayNameForPrint = request.ItemViews.itemViewSetupListForPrint
                                                .Select(x => new ItemViewSetupForPrint()
                                                {
                                                    DisplayName = x.DisplayName,
                                                    Name = x.Name,
                                                    Sequence = x.Sequence
                                                }).ToList();

                        await _context.ItemViewSetupForPrint.AddRangeAsync(itemDisplayNameForPrint);
                        await _context.SaveChangesAsync();
                    }


                    if(request.IsForAllProduct)
                    {
                        await _mediator.Send(new ProductsDisplayNameUpdateCommand());
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
