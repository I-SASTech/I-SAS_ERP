using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Categories.Queries.GetCategoryList;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.InventoryBlinds.Queries.GetInventoryBlindDetail;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.InventoryBlinds.Queries.GetInventoryBlindList
{
    public class GetInventoryBlindListQuery : PagedRequest, IRequest<PagedResult<InventoryBlindListLookUpModel>>
    {
        public string SearchTerm { get; set; }
        public bool IsCounted { get; set; }

        public class Handler : IRequestHandler<GetInventoryBlindListQuery, PagedResult<InventoryBlindListLookUpModel>>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            public readonly IMapper Mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetCategoryListQuery> logger,
                IMapper mapper)
            {
                Context = context;
                Logger = logger;
                Mapper = mapper;
            }
            public async Task<PagedResult<InventoryBlindListLookUpModel>> Handle(GetInventoryBlindListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    
                    var query = Context.InventoryBlinds.AsNoTracking().Include(x=>x.Warehouse).Where(x=>x.IsCounted == request.IsCounted).AsQueryable();

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;



                        query = query.Where(x => x.DocumentId.ToLower().Contains(searchTerm.ToLower()) ||
                                                 x.Warehouse.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                                 x.Warehouse.NameArabic.ToLower().Contains(searchTerm.ToLower()));


                    }
                    query = query.OrderBy(x => x.DocumentId);
                    var count = await query.CountAsync(cancellationToken: cancellationToken);
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var inventoryBlindList = new List<InventoryBlindListModel>();
                    foreach (var blindDetail in query)
                    {
                        var list = new InventoryBlindListModel()
                        {
                            Id = blindDetail.Id,
                            IsCounted = blindDetail.IsCounted,
                            DocumentId = blindDetail.DocumentId,
                            DateTime = blindDetail.DateTime.ToString("dd-MM-yyyy hh:mm tt"),
                            WarehouseName = blindDetail.Warehouse.Name,
                            WarehouseNameArabic = blindDetail.Warehouse.NameArabic,
                            WarehouseId = blindDetail.WarehouseId
                        };
                        inventoryBlindList.Add(list);
                    }
                        
                    return new PagedResult<InventoryBlindListLookUpModel>
                    {


                        Results = new InventoryBlindListLookUpModel()
                        {
                            InventoryBlindList = inventoryBlindList
                        },
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = inventoryBlindList.Count / request.PageSize
                    };
                }
                
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
    }
}
