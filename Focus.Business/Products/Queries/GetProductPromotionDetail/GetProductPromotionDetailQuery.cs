

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Interface;
using Focus.Business.Products.Queries.GetProductList;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Products.Queries.GetProductPromotionDetail
{
    public class GetProductPromotionDetailQuery : IRequest<ProductLookUpModel>
    {
        public Guid Id { get; set; }
        public Guid? WareHouseId { get; set; }

        public class Handler : IRequestHandler<GetProductPromotionDetailQuery, ProductLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, ILogger<GetProductPromotionDetailQuery> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<ProductLookUpModel> Handle(GetProductPromotionDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var productDetail = await (from product in _context.Products
                                               select new
                                               {
                                                   product.Id,
                                                   //PromotionOffer = product.PromotionOffer == null
                                                   //                   ? null
                                                   //                   : new PromotionOffer
                                                   //                   {
                                                   //                       FromDate = product.PromotionOffer.FromDate,
                                                   //                       isActive = product.PromotionOffer.isActive,
                                                   //                       Offer = product.PromotionOffer.Offer,
                                                   //                       StockLimit = product.PromotionOffer.StockLimit,
                                                   //                       QuantityOut = product.PromotionOffer.QuantityOut,
                                                   //                       Discount = product.PromotionOffer.Discount,
                                                   //                       DiscountType = product.PromotionOffer.DiscountType,
                                                   //                       BaseQuantity = product.PromotionOffer.BaseQuantity,
                                                   //                       UpToQuantity = product.PromotionOffer.UpToQuantity,
                                                   //                       IncludingBaseQuantity = product.PromotionOffer.IncludingBaseQuantity,
                                                   //                       ToDate = product.PromotionOffer.ToDate
                                                   //                   },
                                                   //BundleCategory = product.BundleCategory == null
                                                   //                   ? null
                                                   //                   : new BundleCategory
                                                   //                   {
                                                   //                       Buy = product.BundleCategory.Buy,
                                                   //                       Get = product.BundleCategory.Get,
                                                   //                       Offer = product.BundleCategory.Offer,
                                                   //                       isActive = product.BundleCategory.isActive,
                                                   //                       QuantityLimit = product.BundleCategory.QuantityLimit,
                                                   //                       StockLimit = product.BundleCategory.StockLimit
                                                   //                   },
                                               }).FirstOrDefaultAsync(cancellationToken: cancellationToken);


                    decimal currentQuantity = 0;
                    if (request.WareHouseId != null || request.WareHouseId != Guid.Empty)
                    {
                        var stocks = await (from stock in _context.Stocks
                                            where stock.ProductId == productDetail.Id && stock.WareHouseId == request.WareHouseId
                                            select new
                                            {
                                                stock.CurrentQuantity,
                                            }).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                        currentQuantity = stocks.CurrentQuantity;
                    }



                    return new ProductLookUpModel()
                    {

                        
                        //PromotionOffer = productDetail.PromotionOffer,
                        //BundleCategory = productDetail.BundleCategory,
                        Inventory = request.WareHouseId == null || request.WareHouseId == Guid.Empty ? null : new Inventory()
                        {
                            CurrentQuantity = currentQuantity
                        },
                    };

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
