using AutoMapper;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Focus.Business.BundleCategoriesItems.Models;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.PromotionOffersFolder.Queries.GetPromotionOffersDetails
{
    public class GetPromotionOffersDetailQuery : IRequest<PromotionOffersDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetPromotionOffersDetailQuery, PromotionOffersDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetPromotionOffersDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PromotionOffersDetailsLookUpModel> Handle(GetPromotionOffersDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var promotionOffer = await _context.PromotionOffers.FindAsync(request.Id);
                    var branchesList = await _context.Branches.AsNoTracking().ToListAsync();

                    if (promotionOffer == null)
                        throw new NotFoundException(nameof(promotionOffer), request.Id);

                    var promotion = new PromotionOffersDetailsLookUpModel
                    {
                        Id = promotionOffer.Id,
                        Offer = promotionOffer.Offer,
                        Name = promotionOffer.ProductId == null ? promotionOffer.ProductGroup.Name : promotionOffer.Product.DisplayName,
                        Discount = promotionOffer.Discount,
                        DiscountType = promotionOffer.DiscountType,
                        BaseQuantity = promotionOffer.BaseQuantity,
                        IncludingBaseQuantity = promotionOffer.IncludingBaseQuantity,
                        UpToQuantity = promotionOffer.UpToQuantity,
                        StockLimit = promotionOffer.StockLimit,
                        FromDate = promotionOffer.FromDate,
                        ToDate = promotionOffer.ToDate,
                        IsActive = promotionOffer.IsActive,
                        ProductId = promotionOffer.ProductId,
                        Sunday = promotionOffer.Sunday,
                        Monday = promotionOffer.Monday,
                        Tuesday = promotionOffer.Tuesday,
                        Wednesday = promotionOffer.Wednesday,
                        Thursday = promotionOffer.Thursday,
                        Friday = promotionOffer.Friday,
                        Saturday = promotionOffer.Saturday,
                        Buy = promotionOffer.Buy,
                        Get = promotionOffer.Get,
                        PromotionType = promotionOffer.PromotionType,
                        ProductGroupId = promotionOffer.ProductGroupId,
                        GetProductId = promotionOffer.GetProductId,
                        BranchesIdList = promotionOffer.BundleOfferBranches.Count > 0 ? promotionOffer.BundleOfferBranches.Select(x => new BundlesOffersBranchesLookupModel
                        {
                            Id = Guid.Parse(x.BranchId.ToString()),
                            Name = branchesList.Count > 0 ? branchesList.FirstOrDefault(y => y.Id == x.BranchId).Code + " " + branchesList.FirstOrDefault(y => y.Id == x.BranchId).BranchName : null,
                        }).ToList() : new List<BundlesOffersBranchesLookupModel>(),
                    };

                    
                    return promotion;
                    
                    
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
