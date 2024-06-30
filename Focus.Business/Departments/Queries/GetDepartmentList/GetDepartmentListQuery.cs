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

namespace Focus.Business.Departments.Queries.GetDepartmentList
{
    public class GetDepartmentListQuery : PagedRequest, IRequest<PagedResult<DepartmentListModel>>
    {
        public bool isActive;
        public string SearchTerm { get; set; }
        public class Handler : IRequestHandler<GetDepartmentListQuery, PagedResult<DepartmentListModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetDepartmentListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PagedResult<DepartmentListModel>> Handle(GetDepartmentListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    IQueryable<Department> query;
                    if (request.isActive == false)
                    {
                        query = _context.Departments.AsQueryable();

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
                            .ProjectTo<DepartmentLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new PagedResult<DepartmentListModel>
                        {


                            Results = new DepartmentListModel
                            {
                                Departments = originList
                            },
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = originList.Count / request.PageSize
                        };
                    }
                    else
                    {
                        query = _context.Departments.AsQueryable();

                        var originList = await query
                            .OrderBy(x => x.Code)
                            .Where(x => x.IsActive == true)
                            .ProjectTo<DepartmentLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new PagedResult<DepartmentListModel>
                        {
                            Results = new DepartmentListModel
                            {
                                Departments = originList
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
