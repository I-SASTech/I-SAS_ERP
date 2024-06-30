using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Designations.Queries.GetDesignationList
{
    public class GetDesignationListQuery : PagedRequest, IRequest<PagedResult<DesignationListModel>>
    {
        public bool isActive;
        public string SearchTerm { get; set; }
        public class Handler : IRequestHandler<GetDesignationListQuery, PagedResult<DesignationListModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetDesignationListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PagedResult<DesignationListModel>> Handle(GetDesignationListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    IQueryable<Designation> query;
                    if (request.isActive == false)
                    {
                        query = _context.Designations.AsQueryable();

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


                        var originList = await query
                            .ProjectTo<DesignationLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new PagedResult<DesignationListModel>
                        {


                            Results = new DesignationListModel
                            {
                                Designations = originList
                            },
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = originList.Count / request.PageSize
                        };
                    }
                    else
                    {
                        query = _context.Designations.AsQueryable();

                        var originList = await query
                            .OrderBy(x => x.Code)
                            .Where(x => x.IsActive == true)
                            .ProjectTo<DesignationLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new PagedResult<DesignationListModel>
                        {
                            Results = new DesignationListModel
                            {
                                Designations = originList
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
