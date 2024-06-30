using AutoMapper;
using Focus.Business.Common;
using Focus.Business.ContactCustomerGroup.Model;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Focus.Business.ContactCustomerGroup.Queries
{
    public class CustomerGroupListQuery : PagedRequest, IRequest<PagedResult<List<CustomerGroupLookupModel>>>
    {
        public bool IsDropdown { get; set; }
        public string SearchTerm { get; set; }
        public class Handler : IRequestHandler<CustomerGroupListQuery, PagedResult<List<CustomerGroupLookupModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<CustomerGroupListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PagedResult<List<CustomerGroupLookupModel>>> Handle(CustomerGroupListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.CustomerGroups
                        .AsNoTracking()
                        .OrderBy(x => x.Code)
                        .Select(x => new CustomerGroupLookupModel
                        {
                            Id = x.Id,
                            Code = x.Code,
                            Name = x.Name,
                            NameArabic = x.NameArabic,
                            Status = x.Status,
                            Description = x.Description,                       
                        })
                        .AsQueryable();

                    if (request.IsDropdown)
                    {
                        query = query.Where(x => x.Status);

                        return new PagedResult<List<CustomerGroupLookupModel>>
                        {
                            Results = query.ToList(),
                        };
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;

                            query = query.Where(x => x.Code.ToLower().Contains(searchTerm.ToLower()) ||
                                                     x.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                                     x.NameArabic.ToLower().Contains(searchTerm.ToLower()));
                        }

                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);


                        return new PagedResult<List<CustomerGroupLookupModel>>
                        {
                            Results = query.ToList(),
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = query.ToList().Count / request.PageSize
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
