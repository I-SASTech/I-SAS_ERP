using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.Products.Queries.GetProductList
{
    public class GetProductForPromotionAndBundleListQuery : PagedRequest, IRequest<PagedResult<ProductListModel>>
    {
        public class Handler : IRequestHandler<GetProductForPromotionAndBundleListQuery, PagedResult<ProductListModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetProductForPromotionAndBundleListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<ProductListModel>> Handle(GetProductForPromotionAndBundleListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = await (from product in _context.Products
                            where product.IsActive
                                       select new
                                       {
                                           product.Id,
                                           product.Code,
                                           product.EnglishName,
                                           product.ArabicName,
                                           product.ProductGroupId,
                                           product.DisplayName,
                                           product.PromotionOfferItems,
                                       }).ToListAsync(cancellationToken: cancellationToken);


                    var productLookUpModel = new List<ProductLookUpModel>();

                    query = query.Where(x => !x.PromotionOfferItems.Any(z => z.ProductId == x.Id && z.IsActive)).ToList();




                    foreach (var product in query)
                    {
                        productLookUpModel.Add(new ProductLookUpModel()
                        {
                            ArabicName = product.ArabicName,
                            Code = product.Code,
                            EnglishName = product.EnglishName,
                            DisplayName = product.DisplayName,
                            Id = product.Id,
                        });
                    }

                    return new PagedResult<ProductListModel>
                    {
                        Results = new ProductListModel
                        {
                            Products = productLookUpModel
                        },
                    };

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
                finally
                {
                    _context.DisableTenantFilter = false;
                }
            }
        }

    }
}
