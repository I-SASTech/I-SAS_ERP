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
    public class GetProductListForDropdownQuery : PagedRequest, IRequest<PagedResult<ProductListModel>>
    {
        public string SearchTerm { get; set; }
        public Guid? CategoryId { get; set; }
        public bool IsRaw { get; set; }
        public bool IsService { get; set; }
        public bool IsForRaw { get; set; }
      
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetProductListForDropdownQuery, PagedResult<ProductListModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetProductListForDropdownQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<ProductListModel>> Handle(GetProductListForDropdownQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    List<ProductLookUpModel> query;
                  
                    if(request.BranchId != null)
                    {
                        query = await _context.Products
                            .Where(x => x.ServiceItem == request.IsService && x.IsActive && x.BranchItems.Any(y => y.BranchId == request.BranchId))
                            .Select(product => new ProductLookUpModel
                            {
                                Id = product.Id,
                                BarCode = product.BarCode ?? "",
                                CategoryId = product.CategoryId,
                                TaxRateId = product.TaxRateId,
                                TaxMethod = product.TaxMethod,
                                SalePrice = product.SalePrice,
                                DisplayName = product.DisplayName ?? ""
                            }).ToListAsync(cancellationToken: cancellationToken);
                    }
                    else
                    {
                        query = await _context.Products
                            .Where(x => x.ServiceItem == request.IsService  && x.IsActive)
                            .Select(product => new ProductLookUpModel
                            {
                                Id = product.Id,
                                BarCode = product.BarCode ?? "",
                                CategoryId = product.CategoryId,
                                TaxRateId = product.TaxRateId,
                                TaxMethod = product.TaxMethod,
                                SalePrice = product.SalePrice,
                                DisplayName = product.DisplayName ?? ""
                            }).ToListAsync(cancellationToken: cancellationToken);
                    }

                    if (!request.IsForRaw)
                    {
                        query = query.Where(x => x.IsRaw == request.IsRaw).ToList();
                    }
                    
                    if (request.CategoryId != Guid.Empty && request.CategoryId != null)
                    {
                        query = query.Where(x => x.CategoryId == request.CategoryId).ToList();
                    }

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm.ToLower();
                        query = query.Where(x => x.DisplayName.ToLower().Contains(searchTerm) || x.BarCode.ToLower().Contains(searchTerm)).ToList();

                    }

                    return new PagedResult<ProductListModel>
                    {
                        Results = new ProductListModel
                        {
                            Products = query
                        }
                    };
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }
        }
    }
}
