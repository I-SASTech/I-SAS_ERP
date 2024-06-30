using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Focus.Business.Interface;
using Focus.Business.ItemViewsSetups.Models;
using Focus.Business.LeaveManagement.LeaveGroup.Queries;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.ItemViewsSetups.Queries
{
    public class GetItemsViewSetupDetailsQuery : IRequest<ItemViewLookupModel>
    {
        public class Handler : IRequestHandler<GetItemsViewSetupDetailsQuery, ItemViewLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<LeaveGroupDetailsQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<ItemViewLookupModel> Handle(GetItemsViewSetupDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var itemsDisplayName = await _context.ItemViewSetups.ToListAsync();
                    var itemViewSetupForPrintList = await _context.ItemViewSetupForPrint.ToListAsync();
                    var itemsListDisplayOrder = await _context.ItemsListDisplayOrderSetup.ToListAsync();

                    var itemDisplayName = itemsDisplayName.Select(x => new ItemViewSetupLookupModel()
                                            {
                                                DisplayName = x.DisplayName,
                                                Name = x.Name,
                                                Sequence = x.Sequence
                                            }).ToList();

                    var itemDisplayNameForPrint = itemViewSetupForPrintList
                                              .Select(x => new ItemViewSetupLookupModel()
                                              {
                                                  DisplayName = x.DisplayName,
                                                  Name = x.Name,
                                                  Sequence = x.Sequence
                                              }).ToList();

                    if (itemsListDisplayOrder.Count == 0)
                    {
                        var list = new List<ItemsListDisplayOrderSetup>();
                        list.Add(new ItemsListDisplayOrderSetup
                        {
                            DisplayName = "Image",
                            Name = "Image",
                            Sequence = 1
                        });
                        list.Add(new ItemsListDisplayOrderSetup
                        {
                            DisplayName = "Item Code",
                            Name = "Item Code",
                            Sequence = 2
                        });
                        list.Add(new ItemsListDisplayOrderSetup
                        {
                            DisplayName = "Item Name",
                            Name = "Item Name",
                            Sequence = 3
                        });
                        list.Add(new ItemsListDisplayOrderSetup
                        {
                            DisplayName = "ImSale Priceage",
                            Name = "Sale Price",
                            Sequence = 4
                        });
                        list.Add(new ItemsListDisplayOrderSetup
                        {
                            DisplayName = "Purchase Price",
                            Name = "Purchase Price",
                            Sequence = 5
                        });
                        list.Add(new ItemsListDisplayOrderSetup
                        {
                            DisplayName = "BarCode Type",
                            Name = "BarCode Type",
                            Sequence = 6
                        });
                        list.Add(new ItemsListDisplayOrderSetup
                        {
                            DisplayName = "Current Quantity",
                            Name = "Current Quantity",
                            Sequence = 7
                        });

                        _context.ItemsListDisplayOrderSetup.AddRange(list);
                        await _context.SaveChangesAsync();

                        var itemsListDisplayOrder1 = await _context.ItemsListDisplayOrderSetup.ToListAsync();

                        var itemListViewSetup = itemsListDisplayOrder1
                                             .Select(x => new ItemViewSetupLookupModel()
                                             {
                                                 DisplayName = x.DisplayName,
                                                 Name = x.Name,
                                                 Sequence = x.Sequence
                                             }).ToList();


                        return new ItemViewLookupModel()
                        {
                            ItemViewSetupList = itemDisplayName,
                            itemViewSetupListForPrint = itemDisplayNameForPrint,
                            ItemListViewSetup = itemListViewSetup
                        };
                    }
                    else
                    {
                        var itemListViewSetup = itemsListDisplayOrder
                                             .Select(x => new ItemViewSetupLookupModel()
                                             {
                                                 DisplayName = x.DisplayName,
                                                 Name = x.Name,
                                                 Sequence = x.Sequence
                                             }).ToList();


                        return new ItemViewLookupModel()
                        {
                            ItemViewSetupList = itemDisplayName,
                            itemViewSetupListForPrint = itemDisplayNameForPrint,
                            ItemListViewSetup = itemListViewSetup
                        };
                    }
                   
                }

                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}