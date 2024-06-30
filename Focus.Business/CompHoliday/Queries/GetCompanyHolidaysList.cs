using AutoMapper;
using Focus.Business.Categories.Queries.GetCategoryList;
using Focus.Business.Common;
using Focus.Business.CompHoliday.Models;
using Focus.Business.Exceptions;
using Focus.Business.Holidays;
using Focus.Business.Holidays.Queries.GetHolidaysList;
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

namespace Focus.Business.CompHoliday.Queries
{
    public class GetCompanyHolidaysList : PagedRequest, IRequest<PagedResult<List<CompanyHolidaysLookupModel>>>
    {
        public string SearchTerm { get; set; }

        public class Handler : IRequestHandler<GetCompanyHolidaysList, PagedResult<List<CompanyHolidaysLookupModel>>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, ILogger<GetCompanyHolidaysList> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<PagedResult<List<CompanyHolidaysLookupModel>>> Handle(GetCompanyHolidaysList request, CancellationToken cancellationToken)
            {
                try
                {
                    var query =  (from item in _context.CompanyHolidays
                                       select new CompanyHolidaysLookupModel
                                       {
                                           Id = item.Id,
                                           HolidayType = item.HolidayType,
                                           Date = item.Date,
                                           Year = item.Year,
                                           Description = item.Description,
                                           PaidStatus = item.PaidStatus,
                                           IsActive = item.IsActive,
                                       }).AsQueryable();

                    

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;
                        
                        query = query.Where(x => x.Description.Contains(request.SearchTerm));


                    }

                    var count = await query.CountAsync(cancellationToken: cancellationToken);
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);
                    
                    return new PagedResult<List<CompanyHolidaysLookupModel>>
                    {
                        Results = query.ToList(),
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = query.ToList().Count / request.PageSize
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
