using AutoMapper;
using Focus.Business.Attachments.Commands;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherList;
using Focus.Business.PurchaseOrders.Commands;
using Focus.Business.PurchaseOrders;
using Focus.Business.PurchaseOrders.Queries.GetPurchaseOrderDetails;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Focus.Business.PurchasePosts.Queries.GetPurchasePostList;

namespace Focus.Business.BomSetup.Queries
{
    public class GetSaleOrderItemsForGoodsReceivedQuery : IRequest<PurchaseOrderDetailLookUpModel>
    {
        public Guid Id { get; set; }
        public Guid SaleOrderId { get; set; }

        public class Handler : IRequestHandler<GetSaleOrderItemsForGoodsReceivedQuery, PurchaseOrderDetailLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetSaleOrderItemsForGoodsReceivedQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PurchaseOrderDetailLookUpModel> Handle(GetSaleOrderItemsForGoodsReceivedQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var purchaseOrder = await _context.SaleOrders.FirstOrDefaultAsync(x => x.Id == request.SaleOrderId);

                    var boms = await _context.Boms.FindAsync(request.Id);

                    if (purchaseOrder != null)
                    {

                        var poItems = new List<PurchaseOrderItemLookupModel>();

                        var netAmount = new TotalAmount();
                        decimal discount = 0;
                        decimal amountWithDiscount = 0;
                        decimal qty = 0;
                        foreach (var item in purchaseOrder.SaleOrderItems)
                        {
                            var forBomSaleOrderItem = boms.BomSaleOrderItems.FirstOrDefault(x => x.SaleOrderId == item.SaleOrderId && x.ProductId == item.ProductId);

                            if (forBomSaleOrderItem != null)
                            {
                                qty = qty + forBomSaleOrderItem.Quantity;
                                discount += item.Discount == 0 ? item.FixDiscount * item.Quantity : (item.UnitPrice * item.Quantity * item.Discount) / 100;
                                amountWithDiscount += discount;

                                var total = item.Quantity * item.UnitPrice;

                                var finishingItemQty = boms.BomSaleOrderItems.Where(x => x.SaleOrderId == item.SaleOrderId && x.ProductId == item.ProductId).Sum(x => x.Quantity);

                                poItems.Add(new PurchaseOrderItemLookupModel
                                {
                                    Id = item.Id,
                                    ProductId = item.ProductId,
                                    Discount = item.Discount,
                                    Code = item.Product.Code,
                                    ProductName = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Product?.EnglishName : item.Product?.DisplayNameForPrint,
                                    Description = string.IsNullOrEmpty(item.Product?.DisplayNameForPrint) ? item.Description : item.Product?.DisplayNameForPrint,
                                    ProductNameArabic = item.Product?.ArabicName,
                                    StyleNumber = item.Product?.StyleNumber,
                                    IsService = false,
                                    QuantityforCanvas = Convert.ToInt32(item.Quantity),
                                    FixDiscount = Convert.ToInt32(item.FixDiscount),
                                    Quantity = finishingItemQty,
                                    RemainingQuantity = finishingItemQty,
                                    ReceiveQuantity = finishingItemQty,
                                    TaxRateId = item.TaxRateId,
                                    TaxRate = item.TaxRate?.Rate ?? 0,
                                    UnitPrice = item.UnitPrice,
                                    ExpiryDate = item.ExpiryDate,
                                    BatchNo = item.BatchNo,
                                    TaxMethod = item.TaxMethod,
                                    IsExpire = item.Product.IsExpire,
                                    HighQty = 0,
                                    UnitPerPack = 0,
                                    SerialNo = "",
                                    GuaranteeDate = null,
                                    WarrantyTypeId = null,
                                    Total = total,
                                    DisplayName = item.Product?.DisplayName,
                                    GrossAmount = item.TotalWithOutAmount,
                                    TotalAmount = item.TotalAmount,
                                    VatAmount = item.VatAmount,

                                    AfterDiscountAmount = item.TotalWithOutAmount - item.DiscountAmount,
                                    DiscountAmount = ((purchaseOrder.TaxMethod == "Inclusive" || purchaseOrder.TaxMethod == "شامل") ? (item.Discount == 0 ? (item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100)) : (item.Quantity * item.UnitPrice) * item.Discount / 100) : (item.Discount == 0 ? item.FixDiscount : ((item.Quantity * item.UnitPrice) * item.Discount / 100))),

                                    Product = new ProductDropdownLookUpModel
                                    {
                                        Id = item.Product.Id,
                                        BarCode = item.Product.BarCode,
                                        Serial = item.Product.Serial,
                                        Guarantee = item.Product.Guarantee,
                                        LevelOneUnit = item.Product.LevelOneUnit,
                                        BasicUnit = item.Product.Unit?.Name,
                                        IsExpire = item.Product.IsExpire,
                                        ServiceItem = item.Product.ServiceItem,
                                        HighUnitPrice = item.Product.HighUnitPrice,
                                        StyleNumber = item.Product.StyleNumber,
                                        DisplayName = item.Product.DisplayName,

                                        Code = item.Product.Code,
                                        EnglishName = item.Product.EnglishName,
                                        ArabicName = item.Product.ArabicName,

                                        TaxRateId = item.Product.TaxRateId.Value
                                    }
                                });
                            }

                        }

                        return new PurchaseOrderDetailLookUpModel
                        {
                            Id = Guid.Empty,
                            SupplierName = (purchaseOrder.Customer.Code + "--" + purchaseOrder.Customer.CustomerDisplayName),
                            Date = purchaseOrder.Date,
                            VatAmount = purchaseOrder.VatAmount,
                            TotalAmount = purchaseOrder.TotalAmount,
                            SupplierVAT = "",
                            IsClose = purchaseOrder.IsClose,
                            DocumentType = "",
                            DiscountAmount = purchaseOrder.DiscountAmount,
                            ApprovalStatus = purchaseOrder.ApprovalStatus,
                            RegistrationNo = purchaseOrder.RegistrationNo,
                            SupplierId = purchaseOrder.CustomerId,
                            InvoiceNo = purchaseOrder.RegistrationNo,
                            InvoiceDate = purchaseOrder.Date,
                            Note = purchaseOrder.Note,
                            TaxMethod = purchaseOrder.TaxMethod,
                            TaxRateId = Guid.Parse(purchaseOrder.TaxRateId.ToString()),
                            IsRaw = false,
                            PurchaseOrderItems = poItems,

                            Discount = purchaseOrder.Discount,
                            TransactionLevelDiscount = purchaseOrder.TransactionLevelDiscount,
                            IsDiscountOnTransaction = purchaseOrder.IsDiscountOnTransaction,
                            IsFixed = purchaseOrder.IsFixed,
                            IsBeforeTax = purchaseOrder.IsBeforeTax,
                            NetAmount = purchaseOrder.TotalAmount,
                            Reference = "Created From" + " " + boms.Code + " " + "with Sale Order" + purchaseOrder.RegistrationNo + " " + "along with Items Quantity" + " " + qty.ToString(),
                        };
                    }




                    throw new NotFoundException(nameof(purchaseOrder), request.Id);
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
