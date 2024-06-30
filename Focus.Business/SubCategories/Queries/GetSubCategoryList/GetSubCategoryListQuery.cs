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
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace Focus.Business.SubCategories.Queries.GetSubCategoryList
{
    public class GetSubCategoryListQuery : PagedRequest, IRequest<PagedResult<SubCategoryListModel>>
    {
        public bool IsActive;
        public string SearchTerm { get; set; }
        public Guid CategoryId { get; set; }

        public class Handler : IRequestHandler<GetSubCategoryListQuery, PagedResult<SubCategoryListModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetSubCategoryListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PagedResult<SubCategoryListModel>> Handle(GetSubCategoryListQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    IQueryable<SubCategory> query;
                    if (request.IsActive == false)
                    {
                        query = _context.SubCategories.AsQueryable();

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

                        var subCategoryList = new List<SubCategoryLookUpModel>();
                        foreach(var item in query)
                        {
                            subCategoryList.Add(new SubCategoryLookUpModel
                            {
                                Id=item.Id,
                                Name=item.Name,
                                NameArabic=item.NameArabic,
                                Description=item.Description,
                                Code=item.Code,
                                isActive=item.isActive,
                                CategoryId=item.CategoryId,
                                CategoryName = item.Category?.Name,
                                CategoryNameArabic = item.Category?.NameArabic,
                            });
                        }

                        return new PagedResult<SubCategoryListModel>
                        {


                            Results = new SubCategoryListModel
                            {
                                SubCategories = subCategoryList
                            },
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = subCategoryList.Count / request.PageSize
                        };
                    }
                    else
                    {
                        query = _context.SubCategories.AsQueryable();

                        if (request.CategoryId!=Guid.Empty)
                        {
                            query = query.Where(x => x.CategoryId == request.CategoryId);
                        }
                        query = query
                            .OrderBy(x => x.Code)
                            .Where(x => x.isActive == true);

                        var subCategoryList = new List<SubCategoryLookUpModel>();
                        foreach (var item in query)
                        {
                            subCategoryList.Add(new SubCategoryLookUpModel
                            {
                                Id = item.Id,
                                Name = item.Name,
                                NameArabic = item.NameArabic,
                                Description = item.Description,
                                Code = item.Code,
                                isActive = item.isActive,
                                CategoryId = item.CategoryId,
                                CategoryName = item.Category?.Name,
                                CategoryNameArabic = item.Category?.NameArabic,
                            });
                        }

                        return new PagedResult<SubCategoryListModel>
                        {
                            Results = new SubCategoryListModel
                            {
                                SubCategories = subCategoryList
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
