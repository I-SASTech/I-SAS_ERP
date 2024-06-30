using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;

namespace Focus.Business.PromotionOffersFolder.Queries.GetPromotionOffersList
{
    public class GetPromotionOffersListQuery : PagedRequest, IRequest<PagedResult<List<PromotionOffersDetailListModel>>>
    {
        public string SearchTerm { get; set; }
        public string Status { get; set; }
        public bool IsDropdown { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? BundleBranches { get; set; }

        public class Handler : IRequestHandler<GetPromotionOffersListQuery, PagedResult<List<PromotionOffersDetailListModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetPromotionOffersListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<PromotionOffersDetailListModel>>> Handle(GetPromotionOffersListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var promotionOffers = await _context.PromotionOffers
                        .AsNoTracking()
                        .Include(x => x.Product)
                        .Include(x => x.ProductGroup)
                        .ToListAsync();

                    if (request.BundleBranches != null)
                    {
                        promotionOffers = promotionOffers.Where(x => x.BundleOfferBranches.Any(b => b.BranchId == request.BundleBranches)).ToList();
                    }
                    else if (request.BranchId != null)
                    {
                        promotionOffers = promotionOffers.Where(x => x.BranchId == request.BranchId).ToList();
                    }

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;
                        promotionOffers = promotionOffers.Where(x => x.Offer.ToLower().Contains(searchTerm) || x.StockLimit.ToString("0.00").Contains(searchTerm)).ToList();

                    }
                    var activeBundle = promotionOffers.Count(x => x.IsActive);

                    if (request.Status == "Active")
                    {
                        promotionOffers = promotionOffers.Where(x => x.IsActive).ToList();
                    }
                    if (request.Status == "History")
                    {
                        promotionOffers = promotionOffers.Where(x => !x.IsActive).ToList();
                    }


                    var count = promotionOffers.Count();
                    promotionOffers = promotionOffers.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();


                    var saleList = new List<PromotionOffersDetailListModel>();

                    foreach (var promotion in promotionOffers)
                    {
                        saleList.Add(new PromotionOffersDetailListModel
                        {
                            Id = promotion.Id,
                            Offer = promotion.Offer,
                            FromDate = promotion.FromDate?.ToString("d"),
                            ToDate = promotion.ToDate?.ToString("d"),
                            IsActive = promotion.IsActive,
                            BaseQuantity = promotion.BaseQuantity,
                            Discount = promotion.Discount.ToString("0.00"),
                            DiscountType = promotion.DiscountType,
                            IncludingBaseQuantity = promotion.IncludingBaseQuantity,
                            StockLimit = promotion.StockLimit,
                            QuantityOut = promotion.QuantityOut,
                            ProductName = promotion.Product != null ? promotion.Product?.EnglishName : promotion.ProductGroup?.Name,
                            //GroupName = promotion.ProductGroup?.Name,
                        });
                    }
                    return new PagedResult<List<PromotionOffersDetailListModel>>
                    {
                        Results = saleList,
                        RowCount = count,
                        PageSize = request.PageSize,
                        ActiveBundle = activeBundle,
                        CurrentPage = request.PageNumber,
                        PageCount = saleList.Count / request.PageSize
                    };
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
