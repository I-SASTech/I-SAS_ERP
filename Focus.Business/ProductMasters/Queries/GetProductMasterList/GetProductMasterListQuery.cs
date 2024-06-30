using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Focus.Business.ProductMasters.Queries.GetProductMasterList
{
    public class GetProductMasterListQuery : PagedRequest, IRequest<PagedResult<ProductMasterListModel>>
    {
        public bool isActive;
        public string SearchTerm { get; set; }
        public class Handler : IRequestHandler<GetProductMasterListQuery, PagedResult<ProductMasterListModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetProductMasterListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PagedResult<ProductMasterListModel>> Handle(GetProductMasterListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    IQueryable<ProductMaster> query;
                    if (request.isActive == false)
                    {
                        query = _context.ProductMasters.AsQueryable();

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;



                            query = query.Where(x => x.Code.ToLower().Contains(searchTerm.ToLower()) ||
                                                     x.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                                      x.NameArabic.ToLower().Contains(searchTerm.ToLower()));


                        }
                        query = query.OrderBy(x => x.Code);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);


                        var productMasterList = await query
                            .ProjectTo<ProductMasterLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new PagedResult<ProductMasterListModel>
                        {


                            Results = new ProductMasterListModel
                            {
                                ProductMasters = productMasterList
                            },
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = productMasterList.Count / request.PageSize
                        };
                    }
                    else
                    {
                        query = _context.ProductMasters.AsQueryable();

                        var productMasterList = await query
                            .OrderBy(x => x.Code)
                            .Where(x => x.isActive == true)
                            .ProjectTo<ProductMasterLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new PagedResult<ProductMasterListModel>
                        {
                            Results = new ProductMasterListModel
                            {
                                ProductMasters = productMasterList
                            },
                        };
                    }


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
