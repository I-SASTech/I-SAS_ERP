using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Products.Model;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.PromotionOffersFolder.Queries.GetPromotionOffersList
{
    public class GetPromotionOffersListForProduct : PagedRequest, IRequest<List<PromotionOfferModel>>
    {
        public class Handler : IRequestHandler<GetPromotionOffersListForProduct, List<PromotionOfferModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetPromotionOffersListForProduct> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<List<PromotionOfferModel>> Handle(GetPromotionOffersListForProduct request, CancellationToken cancellationToken)
            {
                try
                {
                    var promotionOffers = new List<PromotionOfferModel>();

                    if (DateTime.Now.ToString("dddd") == "Sunday")
                    {
                        promotionOffers = await (from offer in _context.PromotionOfferItems
                                                 where offer.PromotionOffer.Sunday && offer.IsActive && offer.PromotionOffer.FromDate.Value.Date <= DateTime.Now.Date && offer.PromotionOffer.ToDate.Value.Date >= DateTime.Now.Date
                                                 select new PromotionOfferModel
                                                 {
                                                     Id = offer.Id,
                                                     ProductId = offer.ProductId,
                                                     Offer = offer.PromotionOffer.Offer,
                                                     Discount = offer.Discount,
                                                     DiscountType = offer.DiscountType,
                                                     BaseQuantity = offer.BaseQuantity,
                                                     UpToQuantity = offer.UpToQuantity,
                                                     StockLimit = offer.StockLimit,
                                                     QuantityOut = offer.QuantityOut,
                                                     IncludingBaseQuantity = offer.IncludingBaseQuantity,
                                                     Buy = offer.Buy,
                                                     Get = offer.Get,
                                                     PromotionType = offer.PromotionType,
                                                     ProductGroupId = offer.PromotionOffer.ProductGroupId,
                                                     GetProductId = offer.GetProduct.Id,
                                                 }).ToListAsync(cancellationToken: cancellationToken);
                    }

                    if (DateTime.Now.ToString("dddd") == "Monday")
                    {
                        promotionOffers = await (from offer in _context.PromotionOfferItems
                                                 where offer.PromotionOffer.Monday && offer.IsActive && offer.PromotionOffer.FromDate.Value.Date <= DateTime.Now.Date && offer.PromotionOffer.ToDate.Value.Date >= DateTime.Now.Date
                                                 select new PromotionOfferModel
                                                 {
                                                     Id = offer.Id,
                                                     ProductId = offer.ProductId,
                                                     Offer = offer.PromotionOffer.Offer,
                                                     Discount = offer.Discount,
                                                     DiscountType = offer.DiscountType,
                                                     BaseQuantity = offer.BaseQuantity,
                                                     UpToQuantity = offer.UpToQuantity,
                                                     StockLimit = offer.StockLimit,
                                                     QuantityOut = offer.QuantityOut,
                                                     IncludingBaseQuantity = offer.IncludingBaseQuantity,
                                                     Buy = offer.Buy,
                                                     Get = offer.Get,
                                                     PromotionType = offer.PromotionType,
                                                     ProductGroupId = offer.PromotionOffer.ProductGroupId,
                                                     GetProductId = offer.GetProduct.Id,
                                                 }).ToListAsync(cancellationToken: cancellationToken);
                    }

                    if (DateTime.Now.ToString("dddd") == "Tuesday")
                    {
                        promotionOffers = await (from offer in _context.PromotionOfferItems
                                                 where offer.PromotionOffer.Tuesday && offer.IsActive && offer.PromotionOffer.FromDate.Value.Date <= DateTime.Now.Date && offer.PromotionOffer.ToDate.Value.Date >= DateTime.Now.Date
                                                 select new PromotionOfferModel
                                                 {
                                                     Id = offer.Id,
                                                     ProductId = offer.ProductId,
                                                     Offer = offer.PromotionOffer.Offer,
                                                     Discount = offer.Discount,
                                                     DiscountType = offer.DiscountType,
                                                     BaseQuantity = offer.BaseQuantity,
                                                     UpToQuantity = offer.UpToQuantity,
                                                     StockLimit = offer.StockLimit,
                                                     QuantityOut = offer.QuantityOut,
                                                     IncludingBaseQuantity = offer.IncludingBaseQuantity,
                                                     Buy = offer.Buy,
                                                     Get = offer.Get,
                                                     PromotionType = offer.PromotionType,
                                                     ProductGroupId = offer.PromotionOffer.ProductGroupId,
                                                     GetProductId = offer.GetProduct.Id,
                                                 }).ToListAsync(cancellationToken: cancellationToken);
                    }

                    if (DateTime.Now.ToString("dddd") == "Wednesday")
                    {
                        promotionOffers = await (from offer in _context.PromotionOfferItems
                                                 where offer.PromotionOffer.Wednesday && offer.IsActive && offer.PromotionOffer.FromDate.Value.Date <= DateTime.Now.Date && offer.PromotionOffer.ToDate.Value.Date >= DateTime.Now.Date
                                                 select new PromotionOfferModel
                                                 {
                                                     Id = offer.Id,
                                                     ProductId = offer.ProductId,
                                                     Offer = offer.PromotionOffer.Offer,
                                                     Discount = offer.Discount,
                                                     DiscountType = offer.DiscountType,
                                                     BaseQuantity = offer.BaseQuantity,
                                                     UpToQuantity = offer.UpToQuantity,
                                                     StockLimit = offer.StockLimit,
                                                     QuantityOut = offer.QuantityOut,
                                                     IncludingBaseQuantity = offer.IncludingBaseQuantity,
                                                     Buy = offer.Buy,
                                                     Get = offer.Get,
                                                     PromotionType = offer.PromotionType,
                                                     ProductGroupId = offer.PromotionOffer.ProductGroupId,
                                                     GetProductId = offer.GetProduct.Id,
                                                 }).ToListAsync(cancellationToken: cancellationToken);
                    }

                    if (DateTime.Now.ToString("dddd") == "Thursday")
                    {
                        promotionOffers = await (from offer in _context.PromotionOfferItems
                                                 where offer.PromotionOffer.Thursday && offer.IsActive && offer.PromotionOffer.FromDate.Value.Date <= DateTime.Now.Date && offer.PromotionOffer.ToDate.Value.Date >= DateTime.Now.Date
                                                 select new PromotionOfferModel
                                                 {
                                                     Id = offer.Id,
                                                     ProductId = offer.ProductId,
                                                     Offer = offer.PromotionOffer.Offer,
                                                     Discount = offer.Discount,
                                                     DiscountType = offer.DiscountType,
                                                     BaseQuantity = offer.BaseQuantity,
                                                     UpToQuantity = offer.UpToQuantity,
                                                     StockLimit = offer.StockLimit,
                                                     QuantityOut = offer.QuantityOut,
                                                     IncludingBaseQuantity = offer.IncludingBaseQuantity,
                                                     Buy = offer.Buy,
                                                     Get = offer.Get,
                                                     PromotionType = offer.PromotionType,
                                                     ProductGroupId = offer.PromotionOffer.ProductGroupId,
                                                     GetProductId = offer.GetProduct.Id,
                                                 }).ToListAsync(cancellationToken: cancellationToken);
                    }

                    if (DateTime.Now.ToString("dddd") == "Friday")
                    {
                        promotionOffers = await (from offer in _context.PromotionOfferItems
                                                 where offer.PromotionOffer.Friday && offer.IsActive && offer.PromotionOffer.FromDate.Value.Date <= DateTime.Now.Date && offer.PromotionOffer.ToDate.Value.Date >= DateTime.Now.Date
                                                 select new PromotionOfferModel
                                                 {
                                                     Id = offer.Id,
                                                     ProductId = offer.ProductId,
                                                     Offer = offer.PromotionOffer.Offer,
                                                     Discount = offer.Discount,
                                                     DiscountType = offer.DiscountType,
                                                     BaseQuantity = offer.BaseQuantity,
                                                     UpToQuantity = offer.UpToQuantity,
                                                     StockLimit = offer.StockLimit,
                                                     QuantityOut = offer.QuantityOut,
                                                     IncludingBaseQuantity = offer.IncludingBaseQuantity,
                                                     Buy = offer.Buy,
                                                     Get = offer.Get,
                                                     PromotionType = offer.PromotionType,
                                                     ProductGroupId = offer.PromotionOffer.ProductGroupId,
                                                     GetProductId = offer.GetProduct.Id,
                                                 }).ToListAsync(cancellationToken: cancellationToken);
                    }

                    if (DateTime.Now.ToString("dddd") == "Saturday")
                    {
                        promotionOffers = await (from offer in _context.PromotionOfferItems
                                                 where offer.PromotionOffer.Saturday && offer.IsActive && offer.PromotionOffer.FromDate.Value.Date <= DateTime.Now.Date && offer.PromotionOffer.ToDate.Value.Date >= DateTime.Now.Date
                                                 select new PromotionOfferModel
                                                 {
                                                     Id = offer.Id,
                                                     ProductId = offer.ProductId,
                                                     Offer = offer.PromotionOffer.Offer,
                                                     Discount = offer.Discount,
                                                     DiscountType = offer.DiscountType,
                                                     BaseQuantity = offer.BaseQuantity,
                                                     UpToQuantity = offer.UpToQuantity,
                                                     StockLimit = offer.StockLimit,
                                                     QuantityOut = offer.QuantityOut,
                                                     IncludingBaseQuantity = offer.IncludingBaseQuantity,
                                                     Buy = offer.Buy,
                                                     Get = offer.Get,
                                                     PromotionType = offer.PromotionType,
                                                     ProductGroupId = offer.PromotionOffer.ProductGroupId,
                                                     GetProductId = offer.GetProduct.Id,
                                                 }).ToListAsync(cancellationToken: cancellationToken);
                    }

                    return promotionOffers;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }

    }
}
