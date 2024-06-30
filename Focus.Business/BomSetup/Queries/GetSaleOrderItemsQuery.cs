using Focus.Business.BomSetup.Models;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SaleOrders;
using Focus.Business.SaleOrders.Queries.GetSaleOrderDetails;
using Focus.Business.Sales.Queries.GetSaleDetail;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.BomSetup.Queries
{
    public class GetSaleOrderItemsQuery : IRequest<BomSaleOrderItemsLookupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetSaleOrderItemsQuery, BomSaleOrderItemsLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;



            public Handler(IApplicationDbContext context,
                ILogger<GetSaleOrderDetailQuery> logger)
            {
                _context = context;
                _logger = logger;

            }
            public async Task<BomSaleOrderItemsLookupModel> Handle(GetSaleOrderItemsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    

                    var saleOrder = await _context.SaleOrders
                        .AsNoTracking()
                        .Include(x => x.TaxRate)
                        .Include(x => x.SaleOrderItems)
                        .ThenInclude(x => x.TaxRate)
                        .Include(x => x.SaleOrderItems)
                        .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.Unit)
                        .Include(x => x.SaleOrderItems)
                        .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.Unit)
                        .Include(x => x.SaleOrderItems)
                        .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.Unit)
                        .Include(x => x.SaleOrderItems)
                        .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.Inventories)
                        .Include(x => x.SaleOrderItems)
                        .ThenInclude(x => x.WareHouse)
                        .Include(x => x.Customer)
                        .Include(x => x.SaleOrderItems)
                        .Include(x => x.SaleOrderPayments)
                        .Include(x => x.Logistic)
                        .Include(x => x.OrderType)
                        .Include(x => x.Incoterms)
                        .Include(x => x.Commodity)
                        .Include(x => x.Commodity)
                        .Include(x => x.ImportExportItems)
                        .ThenInclude(x => x.Service)
                         .Include(x => x.ImportExportItems)
                        .ThenInclude(x => x.StuffingLocation)
                         .Include(x => x.ImportExportItems)
                        .ThenInclude(x => x.PortOfLoading)
                        .Include(x => x.ImportExportItems)
                        .ThenInclude(x => x.PortOfLoading)
                        .Include(x => x.ImportExportItems)
                        .ThenInclude(x => x.PortOfDestination)
                        .Include(x => x.ImportExportItems)
                        .ThenInclude(x => x.Carrier)
                        .Include(x => x.SaleOrderVersions)
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);




                    
                    var stocks = await _context.Stocks.AsNoTracking().Where(x => x.BranchId == saleOrder.BranchId).ToListAsync(cancellationToken: cancellationToken);

                    var bomSaleOrderItems = await _context.BomSaleOrderItems.AsNoTracking().Include(x => x.Boms).Where(x => x.Boms.ApprovalStatus == "Post").ToListAsync();

                    if (saleOrder != null)
                    {
                       


                        var poItems = new List<SaleOrderItemLookupModel>();
                        var bowRawProducts = new List<BomRawProductsLookupModel>();
                        foreach (var item in saleOrder.SaleOrderItems)
                        {
                            var highUnit = item.Product?.HighUnitPrice != null && ((!saleOrder.IsService && (bool)item.Product?.HighUnitPrice));

                            var quantityOfBomSaleOrders = bomSaleOrderItems.Where(x => x.SaleOrderId== item.SaleOrderId && x.ProductId == item.ProductId).ToList();
                            decimal bomSaleOrdersQty = 0;
                            if(quantityOfBomSaleOrders.Count > 0)
                            {
                                bomSaleOrdersQty = quantityOfBomSaleOrders.Sum(x => x.Quantity);
                            }

                            if (item.ProductId != null && !item.IsUsedForBom)
                            {
                                poItems.Add(new SaleOrderItemLookupModel
                                {
                                    Id = item.Id,
                                    ProductId = item.ProductId,
                                    Code = item.Product.Code,
                                    ProductName = string.IsNullOrEmpty(item.Product.EnglishName) ? item.Product.ArabicName : item.Product.EnglishName,
                                    ProductNameEn = item.Product.EnglishName,
                                    ProductNameAr = item.Product.ArabicName,
                                    Discount = item.Discount,
                                    FixDiscount = Convert.ToDecimal(item.FixDiscount),
                                    Quantity = item.Quantity - bomSaleOrdersQty,
                                    CurrentQuantity = item.Quantity - bomSaleOrdersQty,
                                    QuantityOut = item.QuantityOut,
                                    SaleOrderId = item.SaleOrderId,
                                    TaxRateId = item.TaxRateId,
                                    TaxRate = item.TaxRate.Rate.ToString("0.00"),
                                    UnitPrice = item.UnitPrice,
                                    ExpiryDate = item.ExpiryDate,
                                    BatchNo = item.BatchNo,
                                    TaxMethod = item.TaxMethod,
                                    IsExpire = item.Product.IsExpire,
                                    Description = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Description : item.Product?.DisplayNameForPrint,
                                    ServiceItem = item.ServiceItem,
                                    Serial = item.Serial,
                                    GuaranteeDate = item.GuaranteeDate,
                                    StyleNumber = item.StyleNumber,
                                    IsFree = item.IsFree,
                                    Scheme = item.Scheme,
                                    SchemeQuantity = item.SchemeQuantity,
                                    SchemePhysicalQuantity = item.SchemePhysicalQuantity,
                                    Total = highUnit ? (item.Quantity / item.Product.UnitPerPack ?? 0) * item.UnitPrice : item.Quantity * item.UnitPrice,
                                    UnitPerPack = item.Product.UnitPerPack,
                                    TotalAmount = item.TotalAmount,
                                    VatAmount = item.VatAmount,
                                    AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                    GrossAmount = item.TotalWithOutAmount,
                                    DisplayName = item.Product?.DisplayName,
                                    BomRawProducts = bowRawProducts,
                                    UnitName = item.Product?.Unit?.Name,
                                    Product = new ProductDropdownLookUpModel
                                    {
                                        Id = item.Product.Id,
                                        BarCode = item.Product.BarCode,
                                        StyleNumber = item.Product.StyleNumber,
                                        Width = item.Product.Width,
                                        UnitPerPack = item.Product.UnitPerPack,
                                        ServiceItem = item.Product.ServiceItem,
                                        WholesaleQuantity = item.Product.WholesaleQuantity,
                                        WholesalePrice = item.Product.WholesalePrice,
                                        Code = item.Product.Code,
                                        EnglishName = item.Product.EnglishName,
                                        ArabicName = item.Product.ArabicName,
                                        SalePrice = item.Product.SalePrice,
                                        LevelOneUnit = item.Product.LevelOneUnit,
                                        BasicUnit = item.Product.Unit?.Name,
                                       
                                        TaxRateId = item.Product.TaxRateId.Value,
                                        TaxMethod = item.Product.TaxMethod,
                                        Serial = item.Product.Serial,
                                        HighUnitPrice = item.Product.HighUnitPrice,
                                        Guarantee = item.Product.Guarantee,
                                    }
                                });

                            }
                            
                        }
                        return new BomSaleOrderItemsLookupModel
                        {
                           
                            SaleOrderId = saleOrder.SaleOrderId,
                            Id = saleOrder.Id,
                            SaleOrderItems = poItems,
                           
                        };
                    }
                    throw new NotFoundException(nameof(saleOrder), request.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
                finally
                {
                    _context.DisableTenantFilter = false;
                }
            }
        }
    }
}
