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
using Dapper;

namespace Focus.Business.PriceLabelings.Queries.GetPriceLabelingList
{
    public class GetPriceLabelingListQuery : PagedRequest, IRequest<PagedResult<PriceLabelingListModel>>
    {
        public bool isActive;
        public string SearchTerm { get; set; }
        public class Handler : IRequestHandler<GetPriceLabelingListQuery, PagedResult<PriceLabelingListModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetPriceLabelingListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PagedResult<PriceLabelingListModel>> Handle(GetPriceLabelingListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.isActive == false)
                    {
                      var query = await _context.PriceLabelings.ToListAsync();

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;



                            query = query.Where(x => x.Code.ToLower().Contains(searchTerm.ToLower()) ||
                                                     x.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                                      x.NameArabic.ToLower().Contains(searchTerm.ToLower())).ToList();


                        }
                        query = query.OrderBy(x => x.Code).ToList();
                        var count = query.Count();
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();


                        var priceLabelingList = query.Select(x => new PriceLabelingLookUpModel
                        {
                            Id = x.Id,
                            isActive = x.IsActive,
                            Code= x.Code,
                            NameArabic = x.NameArabic,
                            Description = x.Description,
                            Name = x.Name,
                            Price= x.Price,
                        }).ToList();

                        return new PagedResult<PriceLabelingListModel>
                        {
                            Results = new PriceLabelingListModel
                            {
                                PriceLabelings = priceLabelingList
                            },
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = priceLabelingList.Count / request.PageSize
                        };
                    }
                    else
                    {
                       var query = await _context.PriceLabelings.ToListAsync();

                        query = query.OrderBy(x => x.Code).Where(x => x.IsActive == true).ToList();


                         var priceLabelingList = query.Select(x => new PriceLabelingLookUpModel
                         {
                             Id = x.Id,
                             isActive = x.IsActive,
                             Code = x.Code,
                             NameArabic = x.NameArabic,
                             Description = x.Description,
                             Name = x.Name,
                             Price = x.Price,
                         }).ToList();

                        return new PagedResult<PriceLabelingListModel>
                        {
                            Results = new PriceLabelingListModel
                            {
                                PriceLabelings = priceLabelingList
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
