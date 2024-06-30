using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Domain.Entities;

namespace Focus.Business.Sizes.Queries.GetSizeList
{
    public class GetSizeListQuery : PagedRequest, IRequest<PagedResult<SizeListModel>>
    {
        public bool isActive;
        public string SearchTerm { get; set; }
        public bool IsVariance { get; set; }

        public class Handler : IRequestHandler<GetSizeListQuery, PagedResult<SizeListModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetSizeListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<SizeListModel>> Handle(GetSizeListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    IQueryable<Size> query;
                    if (request.isActive == false)
                    {
                        query = _context.Sizes.AsQueryable();

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


                        var sizeList = await query.Select(x => new SizeLookUpModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            NameArabic = x.NameArabic,
                            Description = x.Description,
                            Code = x.Code,
                            isActive = x.isActive
                        }).ToListAsync(cancellationToken: cancellationToken);


                        return new PagedResult<SizeListModel>
                        {


                            Results = new SizeListModel
                            {
                                Sizes = sizeList
                            },
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = sizeList.Count / request.PageSize
                        };
                    }
                    else
                    {
                        query = _context.Sizes.AsNoTracking().Where(x => x.isActive).AsQueryable();

                        query = request.IsVariance ? query.OrderBy(x => x.Name) : query.OrderBy(x => x.Code);

                        var sizeList = await query.Select(x => new SizeLookUpModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            NameArabic = x.NameArabic,
                            Description = x.Description,
                            Code = x.Code,
                            isActive = x.isActive
                        }).ToListAsync(cancellationToken: cancellationToken);


                        return new PagedResult<SizeListModel>
                        {
                            Results = new SizeListModel
                            {
                                Sizes = sizeList
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
