using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SaleOrders.Queries.GetSaleOrderDetails;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Focus.Business.SaleOrders.Queries.SaleOrderHistory
{
    public class GetSaleOrderHistoryDetail : IRequest<SaleOrderDetailLookUpModel>
    {
        public Guid Id { get; set; }
        public bool IsFifo { get; set; }
        public int OpenBatch { get; set; }
        public bool IsEmail { get; set; }

        public class Handler : IRequestHandler<GetSaleOrderHistoryDetail, SaleOrderDetailLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            public readonly IMediator Mediator;
            public readonly IConfiguration Configuration;

            public Handler(IApplicationDbContext context, IConfiguration configuration, ILogger<GetSaleOrderHistoryDetail> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                Mediator = mediator;
                Configuration = configuration;
            }
            public async Task<SaleOrderDetailLookUpModel> Handle(GetSaleOrderHistoryDetail request, CancellationToken cancellationToken)
            {
                try
                {
                    var saleOrderVersion = await _context.SaleOrderVersions
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                    if (request.IsEmail)
                        _context.DisableTenantFilter = true;

                    var saleOrder = await _context.SaleOrders
                        .AsNoTracking()
                        .Include(x => x.Customer)
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
                        .FirstOrDefaultAsync(x => x.Id == saleOrderVersion.SaleOrderId, cancellationToken);
                    
                    if (saleOrder == null)
                        throw new NotFoundException(nameof(saleOrder), request.Id);

                    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    string sb = "select  * FROM SaleOrderItems Where IsDeleted=1 AND SaleOrderId='"+saleOrder.Id+ "' AND SaleOrderVersionId= '" + saleOrderVersion.Id + "' ";


                    var query = conn.Query<SaleOrderItem>(sb).AsQueryable();
                    
                    var saleOrderItem = new List<SaleOrderItemLookupModel>();
                    foreach (var item in query)
                    {
                        var taxRate = await _context.TaxRates.AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == item.TaxRateId, cancellationToken: cancellationToken);

                        if (item.ProductId != null)
                        {
                            var product = await _context.Products.AsNoTracking()
                                .Include(x=>x.TaxRate)
                                .Include(x=>x.Unit)
                                .FirstOrDefaultAsync(x => x.Id == item.ProductId, cancellationToken: cancellationToken);
                            
                            saleOrderItem.Add(new SaleOrderItemLookupModel
                            {
                                Id = item.Id,
                                ProductId = item.ProductId,
                                ProductNameEn = product.EnglishName,
                                ProductNameAr = product.ArabicName,
                                Discount = item.Discount,
                                FixDiscount = Convert.ToDecimal(item.FixDiscount),
                                Quantity = item.Quantity,
                                QuantityOut = item.QuantityOut,
                                SaleOrderId = item.SaleOrderId,
                                TaxRateId = item.TaxRateId,
                                TaxRate = taxRate.Rate.ToString("0.00"),
                                UnitPrice = item.UnitPrice,
                                ExpiryDate = item.ExpiryDate,
                                BatchNo = item.BatchNo,
                                TaxMethod = product.TaxMethod,
                                IsExpire = product.IsExpire,
                                Description = item.Description,
                                ServiceItem = item.ServiceItem,
                                Serial = item.Serial,
                                GuaranteeDate = item.GuaranteeDate,
                                IsFree = item.IsFree,
                                Total = item.Quantity * item.UnitPrice,
                                UnitPerPack = product.UnitPerPack,
                                DiscountAmount = item.Discount == 0 ? item.Quantity * item.FixDiscount : (item.Quantity * item.UnitPrice * item.Discount) / 100,
                                InclusiveVat = (product.TaxMethod == "Inclusive" || product.TaxMethod == "شامل") ?
                                    ((((item.Quantity * item.UnitPrice) - (item.Discount == 0 ? item.Quantity * item.FixDiscount : (item.Quantity * item.UnitPrice * item.Discount) / 100)) * taxRate.Rate) /
                                    (taxRate.Rate + 100)) : 0,

                                IncludingVat = (product.TaxMethod == "Exclusive" || product.TaxMethod == "غير شامل") ?
                                    ((((item.Quantity * item.UnitPrice) - (item.Discount == 0 ? item.Quantity * item.FixDiscount : (item.Quantity * item.UnitPrice * item.Discount) / 100)) * taxRate.Rate) /
                                    100) : 0,

                            });

                        }
                        else
                        {
                            saleOrderItem.Add(new SaleOrderItemLookupModel
                            {
                                Id = item.Id,
                                ProductId = item.ProductId,
                                ProductNameEn = "",
                                ProductNameAr = "",
                                Discount = item.Discount,
                                FixDiscount = Convert.ToDecimal(item.FixDiscount),
                                Quantity = item.Quantity,
                                QuantityOut = item.QuantityOut,
                                SaleOrderId = item.SaleOrderId,
                                TaxRateId = item.TaxRateId,
                                TaxRate = taxRate.Rate.ToString("0.00"),
                                UnitPrice = item.UnitPrice,
                                ExpiryDate = item.ExpiryDate,
                                BatchNo = item.BatchNo,
                                TaxMethod = item.TaxMethod,
                                IsExpire = false,
                                Description = item.Description,
                                ServiceItem = item.ServiceItem,
                                Serial = item.Serial,
                                GuaranteeDate = item.GuaranteeDate,
                                IsFree = item.IsFree,
                                Total = item.Quantity * item.UnitPrice,
                                UnitPerPack = 0,
                                DiscountAmount = item.Discount == 0 ? item.Quantity * item.FixDiscount : (item.Quantity * item.UnitPrice * item.Discount) / 100,

                                InclusiveVat = ((item.ProductId == null ? item.TaxMethod : item.Product.TaxMethod) == "Inclusive" || (item.ProductId == null ? item.TaxMethod : item.Product.TaxMethod) == "شامل") ?
                                    ((((item.Quantity * item.UnitPrice) - (item.Discount == 0 ? item.Quantity * item.FixDiscount : (item.Quantity * item.UnitPrice * item.Discount) / 100)) * taxRate.Rate) /
                                    (item.TaxRate.Rate + 100)) : 0,

                                IncludingVat = ((item.ProductId == null ? item.TaxMethod : item.Product.TaxMethod) == "Exclusive" || (item.ProductId == null ? item.TaxMethod : item.Product.TaxMethod) == "غير شامل") ?
                                    ((((item.Quantity * item.UnitPrice) - (item.Discount == 0 ? item.Quantity * item.FixDiscount : (item.Quantity * item.UnitPrice * item.Discount) / 100)) * taxRate.Rate) /
                                    100) : 0,

                            });

                        }
                    }

                    
                    return new SaleOrderDetailLookUpModel
                    {
                        Id = saleOrder.Id,
                        LogisticId = saleOrder.LogisticId,
                        Mobile = saleOrder.IsQuotation ? saleOrder.Customer?.ContactNo1 : saleOrder.Mobile,
                        LogisticNameEn = saleOrder.Logistic?.EnglishName,
                        LogisticNameAr = saleOrder.Logistic?.ArabicName,
                        CustomerAddress = saleOrder.IsQuotation ? saleOrder.Customer?.Address : saleOrder.CustomerAddress,
                        Date = saleOrder.Date,
                        BarCode = saleOrder.BarCode,
                        //WareHouseName = saleOrder.SaleOrderItems.FirstOrDefault()?.WareHouse?.Name,
                        //WareHouseNameAr = saleOrder.SaleOrderItems.FirstOrDefault()?.WareHouse?.NameArabic,
                        WareHouseId = saleOrder.WareHouseId,
                        QuotationId = saleOrder.QuotationId,
                        IsQuotation = saleOrder.IsQuotation,
                        QuotationNo = saleOrder.QuotationId != null ? _context.SaleOrders.FirstOrDefault(x => x.Id == saleOrder.QuotationId)?.RegistrationNo : "",
                        ClientPurchaseNo = saleOrder.ClientPurchaseNo,
                        IsClose = saleOrder.IsClose,
                        ApprovalStatus = saleOrder.ApprovalStatus,
                        RegistrationNo = saleOrder.RegistrationNo,
                        CustomerId = saleOrder.CustomerId,
                        CustomerNameEn = saleOrder.Customer?.EnglishName,
                        CustomerNameAr = saleOrder.Customer?.ArabicName,
                        CustomerAccountId = saleOrder.Customer?.AccountId,
                        Email = saleOrder.Customer?.Email,
                        Refrence = saleOrder.Refrence,
                        Days = saleOrder.Days,
                        SheduleDelivery = saleOrder.SheduleDelivery,
                        PaymentMethod = saleOrder.PaymentMethod,
                        IsFreight = saleOrder.IsFreight,
                        IsLabour = saleOrder.IsLabour,
                        Note = saleOrder.Note,
                        NoteAr = saleOrder.NoteAr,
                        IsService = saleOrder.IsService,
                        SaleOrderItems = saleOrderItem,
                        Description = saleOrder.Description,
                        AdvanceAccountId = saleOrder.Customer?.AdvanceAccountId,
                        //ImportExport Fields
                        OrderTypeId = saleOrder.OrderTypeId,
                        OrderTypeName = saleOrder.OrderType?.Name,
                        OrderTypeNameAr = saleOrder.OrderType?.NameArabic,
                        IncotermsId = saleOrder.IncotermsId,
                        IncotermsName = saleOrder.Incoterms?.Name,
                        IncotermsNameAr = saleOrder.Incoterms?.NameArabic,
                        CommodityId = saleOrder.CommodityId,
                        Commodities = saleOrder.Commodities,
                        NatureOfCargo = saleOrder.NatureOfCargo,
                        Attn = saleOrder.Attn,
                        QuotationValidDate = saleOrder.QuotationValidDate,
                        FreeTimePOL = saleOrder.FreeTimePOL,
                        FreeTimePOD = saleOrder.FreeTimePOD,
                        SaleOrderVersion = saleOrder.SaleOrderVersions.Select(x => new SaleOrderVersion()
                        {
                            Id = x.Id,
                            Version = x.Version
                        }).ToList(),


                        ImportExportItems = saleOrder.ImportExportItems.Select(x =>
                           new ImportExportItemLookUpModel()
                           {
                               ServiceId = x.ServiceId,
                               ServiceName = x.Service?.Name,
                               ServiceNameAr = x.Service?.NameArabic,
                               StuffingLocationId = x.StuffingLocationId,
                               StuffingLocationName = x.StuffingLocation?.Name,
                               StuffingLocationNameAr = x.StuffingLocation?.NameArabic,
                               PortOfLoadingId = x.PortOfLoadingId,
                               PortOfLoadingName = x.PortOfLoading?.Name,
                               PortOfLoadingNameAr = x.PortOfLoading?.NameArabic,
                               PortOfDestinationId = x.PortOfDestinationId,
                               PortOfDestinationName = x.PortOfDestination?.Name,
                               PortOfDestinationNameAr = x.PortOfDestination?.NameArabic,
                               CarrierId = x.CarrierId,
                               CarrierName = x.Carrier?.Name,
                               CarrierNameAr = x.Carrier?.NameArabic,
                               Description = x.Description,
                               Ft = x.Ft,
                               Hc = x.Hc,
                               Tt = x.Tt,
                               Etd = x.Etd
                           }).ToList(),
                    };

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
