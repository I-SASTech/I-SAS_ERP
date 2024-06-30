using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.ItemViewsSetups.Commands
{
    public class ProductsDisplayNameUpdateCommand : IRequest<bool>
    {
        public class Handler : IRequestHandler<ProductsDisplayNameUpdateCommand, bool>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<ProductsDisplayNameUpdateCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<bool> Handle(ProductsDisplayNameUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    var itemsDisplayName = await _context.ItemViewSetups.ToListAsync();
                    var itemViewSetupForPrintList = await _context.ItemViewSetupForPrint.ToListAsync();
                    var itemsBarCodeDisplayName = await _context.ListOrderSetups.Where(x => x.DocumentName == "BarCode").ToListAsync();

                    var products = await _context.Products.AsNoTracking().ToListAsync();
                    var productsList = new List<Product>();

                    var categories = await _context.Categories.ToListAsync();
                    var brand = await _context.Brands.ToListAsync();
                    var origin = await _context.Origins.ToListAsync();
                    var size = await _context.Sizes.ToListAsync();
                    var unit = await _context.Units.ToListAsync();
                    var color = await _context.Colors.ToListAsync();

                    foreach (var pro in products) 
                    {
                        string displayNameValue = "";
                        string displayNameValueForPrint = "";
                        string barCodedisplayName = "";

                        if (itemsDisplayName.Count > 0)
                        {
                            var categoryName = categories.FirstOrDefault(x => x.Id == pro.CategoryId)?.Name;

                            foreach (var item in itemsDisplayName)
                            {
                                if (item.Name == "Item Code")
                                {
                                    displayNameValue = displayNameValue + pro.Code + " ";
                                }
                                else if (item.Name == "Item Name (English)")
                                {
                                    displayNameValue = displayNameValue + pro.EnglishName + " ";
                                }
                                else if (item.Name == "Item Name (Arabic)")
                                {
                                    displayNameValue = displayNameValue + pro.ArabicName + " ";
                                }
                                else if (item.Name == "Item Description")
                                {
                                    displayNameValue = displayNameValue + pro.Description + " ";
                                }
                                else if (item.Name == "Item Category")
                                {
                                    displayNameValue = displayNameValue + categoryName + " ";
                                }
                                else if (item.Name == "Item Barcode")
                                {
                                    displayNameValue = displayNameValue + pro.BarCode + " ";
                                }
                                else if (item.Name == "Item Style/Model")
                                {
                                    displayNameValue = displayNameValue + pro.StyleNumber + " ";
                                }
                                else if (item.Name == "Item Brand")
                                {
                                    if(pro.BrandId != Guid.Empty)
                                    {
                                        var brandName = brand.FirstOrDefault(x => x.Id == pro.BrandId)?.Name;

                                        displayNameValue = displayNameValue + brandName + " ";
                                    }
                                }
                                else if (item.Name == "Item Origin")
                                {
                                    if (pro.OriginId != Guid.Empty)
                                    {
                                        var OriginName = origin.FirstOrDefault(x => x.Id == pro.OriginId)?.Name;

                                        displayNameValue = displayNameValue + OriginName + " ";
                                    }
                                }
                                else if (item.Name == "Item Size")
                                {
                                    if (pro.SizeId != Guid.Empty)
                                    {
                                        var sizeName = size.FirstOrDefault(x => x.Id == pro.SizeId)?.Name;

                                        displayNameValue = displayNameValue + sizeName + " ";
                                    }
                                }
                                else if (item.Name == "Item Color")
                                {
                                    if (pro.ColorId != Guid.Empty)
                                    {
                                        var colorName = color.FirstOrDefault(x => x.Id == pro.ColorId)?.Name;

                                        displayNameValue = displayNameValue + colorName + " ";
                                    }
                                }
                                else if (item.Name == "Item Unit")
                                {
                                    if (pro.UnitId != Guid.Empty)
                                    {
                                        var unitName = unit.FirstOrDefault(x => x.Id == pro.UnitId)?.Name;

                                        displayNameValue = displayNameValue + unitName + " ";
                                    }
                                }
                            }
                        }
                        else
                        {
                            displayNameValue = pro.Code + " " + pro.EnglishName;
                        }
                        if (itemsBarCodeDisplayName.Count > 0)
                        {
                            var categoryName = categories.FirstOrDefault(x => x.Id == pro.CategoryId)?.Name;

                            foreach (var item in itemsBarCodeDisplayName)
                            {
                                if (item.Name == "Item Code")
                                {
                                    barCodedisplayName = barCodedisplayName + pro.Code + " ";
                                }
                                else if (item.Name == "Item Name (English)")
                                {
                                    barCodedisplayName = barCodedisplayName + pro.EnglishName + " ";
                                }
                                else if (item.Name == "Item Name (Arabic)")
                                {
                                    barCodedisplayName = barCodedisplayName + pro.ArabicName + " ";
                                }
                                else if (item.Name == "Item Description")
                                {
                                    barCodedisplayName = barCodedisplayName + pro.Description + " ";
                                }
                                else if (item.Name == "Item Category")
                                {
                                    barCodedisplayName = barCodedisplayName + categoryName + " ";
                                }
                                else if (item.Name == "Item Barcode")
                                {
                                    barCodedisplayName = barCodedisplayName + pro.BarCode + " ";
                                }
                                else if (item.Name == "Item Style/Model")
                                {
                                    barCodedisplayName = barCodedisplayName + pro.StyleNumber + " ";
                                }
                                else if (item.Name == "Item Brand")
                                {
                                    if(pro.BrandId != Guid.Empty)
                                    {
                                        var brandName = brand.FirstOrDefault(x => x.Id == pro.BrandId)?.Name;

                                        barCodedisplayName = barCodedisplayName + brandName + " ";
                                    }
                                }
                                else if (item.Name == "Item Origin")
                                {
                                    if (pro.OriginId != Guid.Empty)
                                    {
                                        var OriginName = origin.FirstOrDefault(x => x.Id == pro.OriginId)?.Name;

                                        barCodedisplayName = barCodedisplayName + OriginName + " ";
                                    }
                                }
                                else if (item.Name == "Item Size")
                                {
                                    if (pro.SizeId != Guid.Empty)
                                    {
                                        var sizeName = size.FirstOrDefault(x => x.Id == pro.SizeId)?.Name;

                                        barCodedisplayName = barCodedisplayName + sizeName + " ";
                                    }
                                }
                                else if (item.Name == "Item Color")
                                {
                                    if (pro.ColorId != Guid.Empty)
                                    {
                                        var colorName = color.FirstOrDefault(x => x.Id == pro.ColorId)?.Name;

                                        barCodedisplayName = barCodedisplayName + colorName + " ";
                                    }
                                }
                                else if (item.Name == "Item Unit")
                                {
                                    if (pro.UnitId != Guid.Empty)
                                    {
                                        var unitName = unit.FirstOrDefault(x => x.Id == pro.UnitId)?.Name;

                                        barCodedisplayName = barCodedisplayName + unitName + " ";
                                    }
                                }
                            }
                        }
                        else
                        {
                            barCodedisplayName = pro.EnglishName + " " + pro.ArabicName;
                        }
                        if (itemViewSetupForPrintList.Count > 0)
                        {
                            var categoryName = _context.Categories.FirstOrDefault(x => x.Id == pro.CategoryId)?.Name;

                            foreach (var item in itemViewSetupForPrintList)
                            {
                                if (item.Name == "Item Code")
                                {
                                    displayNameValueForPrint = displayNameValueForPrint + pro.Code + " ";
                                }
                                else if (item.Name == "Item Name (English)")
                                {
                                    displayNameValueForPrint = displayNameValueForPrint + pro.EnglishName + " ";
                                }
                                else if (item.Name == "Item Name (Arabic)")
                                {
                                    displayNameValueForPrint = displayNameValueForPrint + pro.ArabicName + " ";
                                }
                                else if (item.Name == "Item Description")
                                {
                                    displayNameValueForPrint = displayNameValueForPrint + pro.Description + " ";
                                }
                                else if (item.Name == "Item Category")
                                {
                                    displayNameValueForPrint = displayNameValueForPrint + categoryName + " ";
                                }
                                else if (item.Name == "Item Barcode")
                                {
                                    displayNameValueForPrint = displayNameValueForPrint + pro.BarCode + " ";
                                }
                                else if (item.Name == "Item Style/Model")
                                {
                                    displayNameValueForPrint = displayNameValueForPrint + pro.StyleNumber + " ";
                                }
                                else if (item.Name == "Item Brand")
                                {
                                    if (pro.BrandId != Guid.Empty)
                                    {
                                        var brandName = brand.FirstOrDefault(x => x.Id == pro.BrandId)?.Name;

                                        displayNameValueForPrint = displayNameValueForPrint + brandName + " ";
                                    }
                                }
                                else if (item.Name == "Item Origin")
                                {
                                    if (pro.OriginId != Guid.Empty)
                                    {
                                        var OriginName = origin.FirstOrDefault(x => x.Id == pro.OriginId)?.Name;

                                        displayNameValueForPrint = displayNameValueForPrint + OriginName + " ";
                                    }
                                }
                                else if (item.Name == "Item Size")
                                {
                                    if (pro.SizeId != Guid.Empty)
                                    {
                                        var sizeName = size.FirstOrDefault(x => x.Id == pro.SizeId)?.Name;

                                        displayNameValueForPrint = displayNameValueForPrint + sizeName + " ";
                                    }
                                }
                                else if (item.Name == "Item Color")
                                {
                                    if (pro.ColorId != Guid.Empty)
                                    {
                                        var colorName = color.FirstOrDefault(x => x.Id == pro.ColorId)?.Name;

                                        displayNameValueForPrint = displayNameValueForPrint + colorName + " ";
                                    }
                                }
                                else if (item.Name == "Item Unit")
                                {
                                    if (pro.UnitId != Guid.Empty)
                                    {
                                        var unitName = unit.FirstOrDefault(x => x.Id == pro.UnitId)?.Name;

                                        displayNameValueForPrint = displayNameValueForPrint + unitName + " ";
                                    }
                                }
                            }
                        }
                        else
                        {
                            displayNameValueForPrint = pro.Code + " " + pro.EnglishName;
                        }

                        string characterString = barCodedisplayName.Length >= 25 ? barCodedisplayName.Substring(0, 25) : barCodedisplayName;
                        pro.BarCodeDisplayName = characterString;
                        pro.DisplayName = displayNameValue;
                        pro.DisplayNameForPrint = displayNameValueForPrint;

                        productsList.Add(pro);
                    }

                     _context.Products.UpdateRange(productsList);

                    await _context.SaveChangesAsync();

                    return true;
                }

                catch (Exception e)
                {
                    var message = new Message
                    {
                        IsSuccess = false,
                        IsAddUpdate = e.Message
                    };
                    _logger.LogError(e.Message);

                    return false;
                }
            }
        }
    }
}
