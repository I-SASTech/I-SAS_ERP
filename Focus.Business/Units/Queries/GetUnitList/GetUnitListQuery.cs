using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Focus.Business.Units.Queries.GetUnitList
{
    public class GetUnitListQuery : PagedRequest, IRequest<PagedResult<UnitListModel>>
    {
        public bool isActive;
        public string SearchTerm { get; set; }
        public class Handler : IRequestHandler<GetUnitListQuery, PagedResult<UnitListModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetUnitListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PagedResult<UnitListModel>> Handle(GetUnitListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    IQueryable<Domain.Entities.Unit> query;
                    if (request.isActive == false)
                    {
                        query = _context.Units.AsQueryable();

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


                        var unitList = await query
                            .ProjectTo<UnitLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new PagedResult<UnitListModel>
                        {


                            Results = new UnitListModel
                            {
                                Units = unitList
                            },
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = unitList.Count / request.PageSize
                        };
                    }
                    else
                    {
                        query = _context.Units.AsQueryable();

                        var unitList = await query
                            .OrderBy(x => x.Code)
                            .Where(x => x.isActive == true)
                            .ProjectTo<UnitLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new PagedResult<UnitListModel>
                        {
                            Results = new UnitListModel
                            {
                                Units = unitList
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
