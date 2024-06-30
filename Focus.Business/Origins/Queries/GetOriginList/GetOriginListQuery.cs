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

namespace Focus.Business.Origins.Queries.GetOriginList
{
    public class GetOriginListQuery : PagedRequest, IRequest<PagedResult<OriginListModel>>
    {
        public bool isActive;
        public string SearchTerm { get; set; }
        public class Handler : IRequestHandler<GetOriginListQuery, PagedResult<OriginListModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetOriginListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PagedResult<OriginListModel>> Handle(GetOriginListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    IQueryable<Origin> query;
                    if (request.isActive == false)
                    {
                        query = _context.Origins.AsQueryable();

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;



                            query = query.Where(x => x.Code.ToLower().Contains(searchTerm.ToLower()) ||
                                                     x.Name.ToLower().Contains(searchTerm.ToLower())  ||
                                                     x.NameArabic.ToLower().Contains(searchTerm.ToLower()));


                        }
                        query = query.OrderBy(x => x.Code);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);


                        var originList = await query
                            .ProjectTo<OriginLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new PagedResult<OriginListModel>
                        {


                            Results = new OriginListModel
                            {
                                Origins = originList
                            },
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = originList.Count / request.PageSize
                        };
                    }
                    else
                    {
                        query = _context.Origins.AsQueryable();

                        var originList = await query
                            .OrderBy(x => x.Code)
                            .Where(x => x.isActive == true)
                            .ProjectTo<OriginLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new PagedResult<OriginListModel>
                        {
                            Results = new OriginListModel
                            {
                                Origins = originList
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
