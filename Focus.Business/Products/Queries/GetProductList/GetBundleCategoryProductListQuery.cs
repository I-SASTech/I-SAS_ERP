using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Products.Queries.GetProductList
{
    public class GetBundleCategoryProductListQuery : PagedRequest, IRequest<PagedResult<ProductListModel>>
    {
        public bool Type { get; set; }
        public string SearchTerm { get; set; }

        public class Handler : IRequestHandler<GetBundleCategoryProductListQuery, PagedResult<ProductListModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetBundleCategoryProductListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PagedResult<ProductListModel>> Handle(GetBundleCategoryProductListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.Products.AsNoTracking()
                        .Include(x => x.Inventories).AsQueryable();

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;

                        query = query.Where(x => x.EnglishName.Contains(searchTerm) ||
                        x.BarCode.Contains(searchTerm) ||
                        x.Code.Contains(searchTerm));
                    }

                    //if (request.Type == false)
                    //{
                    //    query = query
                    //        .Where(x => x.BundleCategoryId == null).AsQueryable();
                    //}

                    query = query.OrderByDescending(x => x.EnglishName);
                    var count = await query.CountAsync(cancellationToken: cancellationToken);
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);


                    var productList = await query
                           .ProjectTo<ProductLookUpModel>(_mapper.ConfigurationProvider)
                           .ToListAsync(cancellationToken);

                    return new PagedResult<ProductListModel>
                    {
                        Results = new ProductListModel
                        {
                            Products = productList
                        },
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = productList.Count / request.PageSize
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
