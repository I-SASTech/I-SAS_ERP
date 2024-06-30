using Focus.Business.Interface;
using Focus.Business.Sales.Queries.GetSaleDetail;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Purchases.Queries.GetPurchaseDetail
{
   public class PurchaseDetailQuery : IRequest<PurchaseLookupModel>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<PurchaseDetailQuery, PurchaseLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            
            public Handler(IApplicationDbContext context, ILogger<PurchaseDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<PurchaseLookupModel> Handle(PurchaseDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var po = await _context.Purchases
                        .AsNoTracking()
                        .Include(x => x.PurchaseItems)
                        .ThenInclude(x=>x.TaxRate)
                        .Include(x => x.PurchaseItems)
                        .ThenInclude(x => x.Product)
                        .Include(x => x.PurchaseItems)
                        .ThenInclude(x => x.Product)
                        .ThenInclude(x=>x.Unit)
                        .Include(x=>x.Supplier)
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);


                    var wareHouseName = _context.Warehouses.FirstOrDefault(x => x.Id == po.WareHouseId)?.Name;
                    var taxRates = _context.TaxRates.FirstOrDefault(x => x.Id == po.TaxRateId);
                    var items = new List<PurchaseItemLookupModel>();
                    foreach (var item in po.PurchaseItems)
                    {
                        if (item.Product.TaxRateId != null)
                            items.Add(new PurchaseItemLookupModel
                            {
                                Id = item.Id,
                                ProductId = item.ProductId,
                                TaxRateId = item.TaxRateId,
                                Discount = item.Discount,
                                FixDiscount = item.FixDiscount,
                                Quantity = item.Quantity,
                                ExpiryDate = item.ExpiryDate,
                                BatchNo = item.BatchNo,
                                UnitPrice = item.UnitPrice,
                                PurchaseId = item.PurchaseId,
                                TaxMethod = po.TaxMethod,
                                IsExpire = item.Product.IsExpire,
                                HighQty = item.HighQty ?? 0,
                                UnitPerPack = item.UnitPerPack ?? 0,
                                SerialNo = item.SerialNo,
                                GuaranteeDate = item.GuaranteeDate,
                                Product = new ProductDropdownLookUpModel
                                {
                                    Id = item.Product.Id,
                                    BarCode = item.Product.BarCode,
                                    Code = item.Product.Code,
                                    EnglishName = item.Product.EnglishName,
                                    ArabicName = item.Product.ArabicName,
                                    IsExpire = item.Product.IsExpire,
                                    TaxRateId = item.Product.TaxRateId.Value,
                                    Serial = item.Product.Serial,
                                    Guarantee = item.Product.Guarantee,
                                    LevelOneUnit = item.Product.LevelOneUnit,
                                    BasicUnit = item.Product.Unit?.Name
                                }
                            });
                    }
                    return new PurchaseLookupModel
                    {
                        Id = po.Id,
                        PurchaseOrderNo = po.PurchaseOrder?.RegistrationNo,
                        TaxRatesName = taxRates?.Name + ' ' + taxRates?.Rate + '%',
                        SupplierName = (po.Supplier.Code + "--" + po.Supplier.EnglishName),
                        WareHouseName = wareHouseName,
                        PurchaseOrderId = po.PurchaseOrderId,
                        RegistrationNo = po.RegistrationNo,
                        Date = po.Date,
                        SupplierId = po.SupplierId,
                        InvoiceNo = po.InvoiceNo,
                        InvoiceDate = po.InvoiceDate,
                        WareHouseId = po.WareHouseId,
                        TaxMethod = po.TaxMethod,
                        TaxRateId = po.TaxRateId,
                        IsRaw = po.Raw,
                        PurchaseItems = items
                    };
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}