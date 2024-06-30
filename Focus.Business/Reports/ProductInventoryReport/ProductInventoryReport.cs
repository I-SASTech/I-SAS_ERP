using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Reports.ProductInventoryReport
{
    public class ProductInventoryReport : IRequest<List<ProductList>>
    {
        public Guid WareHouseId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<ProductInventoryReport, List<ProductList>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<ProductInventoryReport> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<List<ProductList>> Handle(ProductInventoryReport request, CancellationToken cancellationToken)
            {
                try
                {
                    


                    if (request.FromDate == null || request.ToDate == null) return new List<ProductList>();

                    var query = await _context.Inventories
                        .AsNoTracking()
                        .Where(x => x.WareHouseId == request.WareHouseId)
                        .Select(x => new
                        {
                            x.TransactionType,
                            x.BranchId,
                            x.Date,
                            x.ProductId,
                            x.Quantity,
                            x.WareHouseId,
                            x.Price,
                            x.AveragePrice,
                            x.SalePrice,
                            x.CurrentQuantity,
                            x.DocumentNumber,
                            x.CurrentStockValue,
                        }).ToListAsync(cancellationToken: cancellationToken);

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        query = query.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                    }

                    var inventoriesList = query.Where(x => x.Date.Date >= request.FromDate.Value.Date && x.Date.Date <= request.ToDate.Value.Date).ToList();

                    var stock = await _context.Stocks
                        .AsNoTracking()
                        .Select(z => new
                        {
                            z.ProductId,
                            z.Product.Code,
                            z.Product.EnglishName,
                            z.Product.ArabicName,
                            z.Product.DisplayNameForPrint
                        }).ToListAsync(cancellationToken: cancellationToken);

                    var wareHouse = _context.Warehouses.AsNoTracking().ToList();


                    var productLists = new List<ProductList>();
                    foreach (var item in stock)
                    {
                        var inventories = inventoriesList.Where(x => x.ProductId == item.ProductId).OrderBy(x => x.Date);

                        var opening = query.Where(x => x.ProductId == item.ProductId && x.Date.Date < request.FromDate.Value.Date).ToList();

                        var saleSum = opening.Where(x => x.TransactionType == TransactionType.SaleInvoice || x.TransactionType == TransactionType.StockOut
                            || x.TransactionType == TransactionType.PurchaseReturn || x.TransactionType == TransactionType.WareHouseTransferFrom).Sum(x => x.Quantity);

                        var purchaseSum = opening.Where(x => x.TransactionType == TransactionType.PurchasePost || x.TransactionType == TransactionType.StockIn
                            || x.TransactionType == TransactionType.SaleReturn || x.TransactionType == TransactionType.WareHouseTransferTo).Sum(x => x.Quantity);


                        var currentQuantity = purchaseSum - saleSum;
                        var productInventoryReport = new List<ProductLookUpModel>();
                        foreach (var inv in inventories)
                        {
                            if (inv.TransactionType == TransactionType.SaleInvoice || inv.TransactionType == TransactionType.StockOut
                                || inv.TransactionType == TransactionType.PurchaseReturn || inv.TransactionType == TransactionType.WareHouseTransferFrom)
                            {
                                currentQuantity -= inv.Quantity;
                            }
                            else
                            {
                                currentQuantity += inv.Quantity;
                            }
                            productInventoryReport.Add(new ProductLookUpModel()
                            {
                                Date = inv.Date.ToString("MM/dd/yyyy hh:mm tt"),
                                TransactionType = inv.TransactionType.ToString(),
                                WareHouseName = wareHouse.FirstOrDefault(z => z.Id == inv.WareHouseId)?.Name.ToString(),
                                Price = inv.Price,
                                AveragePrice = inv.AveragePrice,
                                Quantity = inv.Quantity,
                                CurrentQuantity = currentQuantity,
                                CurrentStockValue = inv.CurrentStockValue,
                                SalePrice = inv.SalePrice,
                                WareHouseNameArabic = wareHouse.FirstOrDefault(z => z.Id == inv.WareHouseId)?.NameArabic,
                                DocumentNumber = inv.DocumentNumber
                            });
                        }

                        if (productInventoryReport.Count > 0)
                        {
                            productLists.Add(new ProductList()
                            {
                                Code = item.Code,
                                ProductName = string.IsNullOrEmpty(item.DisplayNameForPrint) ? item.EnglishName + " " + item.ArabicName : item.DisplayNameForPrint,
                                ProductNameAr = string.IsNullOrEmpty(item.DisplayNameForPrint) ? item.ArabicName : item.DisplayNameForPrint,
                                ProductInventoryReport = productInventoryReport
                            });
                        }
                    }

                    return productLists.OrderBy(x => x.Code).ToList();

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }
        }
    }
}