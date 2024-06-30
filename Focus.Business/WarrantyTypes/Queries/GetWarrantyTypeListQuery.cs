using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.WarrantyTypes.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.WarrantyTypes.Queries
{
    public class GetWarrantyTypeListQuery : PagedRequest, IRequest<PagedResult<List<WarrantyTypeLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropdown { get; set; }

        public class Handler : IRequestHandler<GetWarrantyTypeListQuery, PagedResult<List<WarrantyTypeLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetWarrantyTypeListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<WarrantyTypeLookUpModel>>> Handle(GetWarrantyTypeListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.WarrantyTypes.AsQueryable();

                    if (request.IsDropdown)
                    {
                        query = query.Where(x => x.IsActive);
                        var warrantyTypeLists = new List<WarrantyTypeLookUpModel>();
                        foreach (var warrantyType in query)
                        {
                            warrantyTypeLists.Add(new WarrantyTypeLookUpModel()
                            {
                                Id = warrantyType.Id,
                                Code = warrantyType.Code,
                                Name = warrantyType.Name,
                                NameArabic = warrantyType.NameArabic,
                                IsActive = warrantyType.IsActive,
                            });
                        }


                        return new PagedResult<List<WarrantyTypeLookUpModel>>
                        {
                            Results = warrantyTypeLists
                        };
                    }


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

                    var warrantyTypeList = new List<WarrantyTypeLookUpModel>();
                    foreach (var warrantyType in query)
                    {
                        warrantyTypeList.Add(new WarrantyTypeLookUpModel()
                        {
                            Id = warrantyType.Id,
                            Code = warrantyType.Code,
                            Name = warrantyType.Name,
                            NameArabic = warrantyType.NameArabic,
                            IsActive = warrantyType.IsActive,
                        });
                    }


                    return new PagedResult<List<WarrantyTypeLookUpModel>>
                    {
                        Results = warrantyTypeList,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = warrantyTypeList.Count / request.PageSize
                    };
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
