using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.BarCodeSetup.Commands
{
    public class ProductBarCodeDisplayNameUpdateCommand : IRequest<bool>
    {
        public class Handler : IRequestHandler<ProductBarCodeDisplayNameUpdateCommand, bool>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            public Handler(IApplicationDbContext context, ILogger<ProductBarCodeDisplayNameUpdateCommand> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<bool> Handle(ProductBarCodeDisplayNameUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var itemsDisplayName = await _context.ListOrderSetups.Where(x => x.DocumentName == "BarCode").ToListAsync();

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
                                    if (pro.BrandId != Guid.Empty)
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

                        string characterString = displayNameValue.Length >= 25 ? displayNameValue.Substring(0, 25) : displayNameValue;
                        pro.BarCodeDisplayName = characterString;

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
