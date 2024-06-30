using AutoMapper;
using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.ProductGroups.Model;

namespace Focus.Business.ProductGroups.Queries
{
    public class GetProductGroupListQueries : PagedRequest, IRequest<PagedResult<List<ProductGroupModel>>>
    {
        public bool IsDropdown { get; set; }
        public string SearchTerm { get; set; }

        public class Handler : IRequestHandler<GetProductGroupListQueries, PagedResult<List<ProductGroupModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetProductGroupListQueries> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PagedResult<List<ProductGroupModel>>> Handle(GetProductGroupListQueries request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.ProductGroups
                        .AsNoTracking()
                        .OrderBy(x=>x.Code)
                        .Select(x=>new ProductGroupModel
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

                        return new PagedResult<List<ProductGroupModel>>
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
                        

                        return new PagedResult<List<ProductGroupModel>>
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
