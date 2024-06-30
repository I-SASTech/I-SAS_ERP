using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Focus.Business.Logistics.Queries.GetLogisticList
{
    public class GetLogisticListQuery : PagedRequest, IRequest<PagedResult<LogisticListModel>>
    {
        public bool isActive;
        public string SearchTerm { get; set; }
        public Domain.Enum.Logistics LogisticsForm { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetLogisticListQuery, PagedResult<LogisticListModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetLogisticListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PagedResult<LogisticListModel>> Handle(GetLogisticListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.isActive == false)
                    {
                        var query = _context.Logistics.AsNoTracking()
                            .Include(x => x.Account)
                            .Where(x => x.LogisticsForm == request.LogisticsForm )
                            .AsQueryable();

                        if (request.BranchId!=null)
                        {
                            query = query.Where(x => x.BranchId == request.BranchId);
                        }

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;



                            query = query.Where(x => x.Code.ToLower().Contains(searchTerm.ToLower()) ||
                                                     x.EnglishName.ToLower().Contains(searchTerm.ToLower()) ||
                                                     x.ContactName.ToLower().Contains(searchTerm.ToLower()) ||
                                                      x.ArabicName.ToLower().Contains(searchTerm.ToLower()));


                        }
                        query = query.OrderBy(x => x.Code);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);


                        var logisticList = await query
                            .ProjectTo<LogisticLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new PagedResult<LogisticListModel>
                        {


                            Results = new LogisticListModel
                            {
                                Logistics = logisticList
                            },
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = logisticList.Count / request.PageSize
                        };
                    }
                    else
                    {
                        var query = _context.Logistics.AsNoTracking()
                            .Include(x=>x.Account)
                            .Where(x => x.LogisticsForm == request.LogisticsForm && x.IsActive )
                            .AsQueryable();

                        if (request.BranchId != null)
                        {
                            query = query.Where(x => x.BranchId == request.BranchId);
                        }

                        var logisticList = await query
                            .OrderBy(x => x.Code)
                            .ProjectTo<LogisticLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new PagedResult<LogisticListModel>
                        {
                            Results = new LogisticListModel
                            {
                                Logistics = logisticList
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
