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

namespace Focus.Business.BundleCategoriesItems.Queries.GetBundleCategoryList
{
    public class GetBundleCategoryListQuery : PagedRequest, IRequest<PagedResult<List<BundleCategoryDetailListModel>>>
    {
        public string SearchTerm { get; set; }
        public string Status { get; set; }
        public bool IsDropdown { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? BundleBranches { get; set; }

        public class Handler : IRequestHandler<GetBundleCategoryListQuery, PagedResult<List<BundleCategoryDetailListModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetBundleCategoryListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<BundleCategoryDetailListModel>>> Handle(GetBundleCategoryListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var promotionOffers = _context.BundleCategories
                        .AsNoTracking()
                        .Include(x => x.Product)
                        .Include(x => x.BundleOfferBranches)
                        .AsQueryable();

                    if(request.BundleBranches != null)
                    {
                        promotionOffers = promotionOffers.Where(x => x.BundleOfferBranches.Any(b => b.BranchId == request.BundleBranches));
                    }
                    else if (request.BranchId != null)
                    {
                        promotionOffers = promotionOffers.Where(x => x.BranchId == request.BranchId);
                    }

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;

                        promotionOffers = promotionOffers.Where(x =>
                            x.Offer.Contains(searchTerm)
                            || x.Buy.ToString().Contains(searchTerm)
                            || x.Get.ToString().Contains(searchTerm)
                            );

                    }

                    var activeBundle = promotionOffers.Count(x => x.IsActive);
                    if (request.Status == "Active")
                    {
                        promotionOffers = promotionOffers.Where(x => x.IsActive);
                    }
                    if (request.Status == "History")
                    {
                        promotionOffers = promotionOffers.Where(x => !x.IsActive);
                    }
                    var count = await promotionOffers.CountAsync(cancellationToken: cancellationToken);
                    promotionOffers = promotionOffers.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var saleList = new List<BundleCategoryDetailListModel>();

                    foreach (var promotion in promotionOffers)
                    {
                        saleList.Add(new BundleCategoryDetailListModel
                        {
                            Id = promotion.Id,
                            Offer = promotion.Offer,
                            Buy = promotion.Buy,
                            Get = promotion.Get,
                            FromDate = promotion.FromDate?.ToString("d"),
                            ToDate = promotion.ToDate?.ToString("d"),
                            isActive = promotion.IsActive,
                            ProductName = promotion.Product.DisplayName,

                        });
                    }

                    return new PagedResult<List<BundleCategoryDetailListModel>>
                    {
                        Results = saleList,
                        RowCount = count,
                        ActiveBundle = activeBundle,
                        PageSize = request.PageSize,
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
