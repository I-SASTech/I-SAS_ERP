using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Inventories.Queries.GetLatestInventory;
using Focus.Business.PurchaseTemplate.Command;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.PurchaseTemplate.Queries
{
    public class AutoPurchaseTemplateDetail : IRequest<PurchaseTemplateLookUpModel>
    {
        public Guid Id { get; set; }
        public bool IsMultiUnit { get; set; }

        public class Handler : IRequestHandler<AutoPurchaseTemplateDetail, PurchaseTemplateLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<AutoPurchaseTemplateDetail> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<PurchaseTemplateLookUpModel> Handle(AutoPurchaseTemplateDetail request, CancellationToken cancellationToken)
            {
                try
                {
                    var purchaseOrder = await _context.AutoPurchaseTemplates.FindAsync(request.Id);
                    if (purchaseOrder != null)
                    {

                        var poItems = new List<PurchaseTemplateItemLookupModel>();
                        foreach (var item in purchaseOrder.AutoPurchaseTemplateItems)
                        {
                            poItems.Add(new PurchaseTemplateItemLookupModel
                            {
                                Id = item.Id,
                                ProductId = item.ProductId,
                                Discount = item.Discount,
                                FixDiscount = Convert.ToInt32(item.FixDiscount),
                                Quantity = request.IsMultiUnit ? Convert.ToInt32(Convert.ToInt32(item.Quantity) % Convert.ToInt32(item.UnitPerPack)) : item.Quantity,

                                TaxRateId = item.TaxRateId,
                                UnitPrice = item.UnitPrice,
                                TaxMethod = purchaseOrder.TaxMethod,
                                HighQty = request.IsMultiUnit ? Convert.ToInt32(Convert.ToInt32(item.Quantity) / Convert.ToInt32(item.UnitPerPack)) : 0,
                                UnitPerPack = item.UnitPerPack ?? 0,
                                Product = new ProductDropdownLookUpModel
                                {
                                    Id = item.Product.Id,
                                    BarCode = item.Product.BarCode,
                                    Serial = item.Product.Serial,
                                    Guarantee = item.Product.Guarantee,
                                    LevelOneUnit = item.Product.LevelOneUnit,
                                    BasicUnit = item.Product.Unit?.Name,
                                    StockLevel = item.Product.StockLevel,
                                    
                                    Code = item.Product.Code,
                                    EnglishName = item.Product.EnglishName,
                                    ArabicName = item.Product.ArabicName,
                                    //PromotionOffer = item.Product.PromotionOffer == null
                                    //    ? null
                                    //    : new PromotionOffer
                                    //    {
                                    //        FromDate = item.Product.PromotionOffer.FromDate,
                                    //        isActive = item.Product.PromotionOffer.isActive,
                                    //        Offer = item.Product.PromotionOffer.Offer,
                                    //        //DiscountPercentage = item.Product.PromotionOffer.DiscountPercentage,
                                    //        //FixedDiscount = item.Product.PromotionOffer.FixedDiscount,
                                    //        //Quantity = item.Product.PromotionOffer.Quantity,
                                    //        ToDate = item.Product.PromotionOffer.ToDate
                                    //    },
                                    //BundleCategory = item.Product.BundleCategory == null
                                    //    ? null
                                    //    : new BundleCategory
                                    //    {
                                    //        Buy = item.Product.BundleCategory.Buy,
                                    //        Get = item.Product.BundleCategory.Get,
                                    //        Offer = item.Product.BundleCategory.Offer,
                                    //        isActive = item.Product.BundleCategory.isActive
                                    //    },
                                    TaxRateId = item.Product.TaxRateId.Value,
                                    Inventory = item.Product.Inventories == null
                                        ? null
                                        : new Inventory()
                                        {
                                            CurrentQuantity = _mediator.Send(new GetLatestInventoryQuery
                                            {
                                                ProductId = item.ProductId,
                                                WareHouseId = purchaseOrder.WareHouseId
                                            }, cancellationToken).Result.CurrentQuantity,
                                        }
                                }
                            });

                        }

                        return new PurchaseTemplateLookUpModel
                        {
                            Id = purchaseOrder.Id,
                            Date = purchaseOrder.Date,
                            Day = purchaseOrder.Day,
                            IsActive = purchaseOrder.IsActive,
                            RegistrationNo = purchaseOrder.RegistrationNo,
                            SupplierId = purchaseOrder.SupplierId,
                            Note = purchaseOrder.Note,
                            TaxMethod = purchaseOrder.TaxMethod,
                            TaxRateId = purchaseOrder.TaxRateId,
                            IsRaw = purchaseOrder.Raw,
                            WareHouseId = purchaseOrder.WareHouseId,
                            PurchaseOrderItems = poItems,
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
