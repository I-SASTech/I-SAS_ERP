using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Focus.Business.Colors.Queries.GetColorList
{
    public class GetProcessListQuery : PagedRequest, IRequest<PagedResult<ColorListModel>>
    {
        public bool isActive;
        public string SearchTerm { get; set; }
        public bool IsVariance { get; set; }

        public class Handler : IRequestHandler<GetProcessListQuery, PagedResult<ColorListModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetProcessListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PagedResult<ColorListModel>> Handle(GetProcessListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    IQueryable<Color> query;
                    if (request.isActive == false)
                    {
                        query = _context.Colors.AsNoTracking().AsQueryable();

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
                        
                        var colorList = new List<ColorLookUpModel>();
                        foreach (var color in query)
                        {
                            colorList.Add(new ColorLookUpModel()
                            {
                                Id = color.Id,
                                Code = color.Code,
                                Name = color.Name,
                                NameArabic = color.NameArabic,
                                Description = color.Description,
                                isActive = color.isActive
                            });
                        }

                        return new PagedResult<ColorListModel>
                        {
                            Results = new ColorListModel
                            {
                                Colors = colorList
                            },
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = colorList.Count / request.PageSize
                        };
                    }
                    else
                    {
                        query = _context.Colors
                            .AsNoTracking()
                            .Where(x => x.isActive)
                            .OrderBy(x => x.Code)
                            .AsQueryable();

                        var colorList = new List<ColorLookUpModel>();
                        foreach (var color in query)
                        {
                            colorList.Add(new ColorLookUpModel()
                            {
                                Id = color.Id,
                                Code = color.Code,
                                Name = color.Name,
                                NameArabic = color.NameArabic,
                                Description = color.Description,
                                isActive = color.isActive
                            });
                        }
                        

                        return new PagedResult<ColorListModel>
                        {
                            Results = new ColorListModel
                            {
                                Colors = colorList
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
