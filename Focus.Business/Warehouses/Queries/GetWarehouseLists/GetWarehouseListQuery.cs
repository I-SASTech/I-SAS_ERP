using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Interface;
using Microsoft.Extensions.Caching.Memory;

namespace Focus.Business.Warehouses.Queries.GetWarehouseLists
{
    public class GetWarehouseListQuery : IRequest<WarehousesListModel>
    {
        public bool IsDropdown { get; set; }
        public Guid CompanyId { get; set; }
        public string SearchTerm { get; set; }

        public class Handler : IRequestHandler<GetWarehouseListQuery, WarehousesListModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMapper _mapper;
            private readonly IMemoryCache _memoryCache;
            private readonly IUserHttpContextProvider _httpContextProvider;
            public Handler(IApplicationDbContext context, ILogger<GetWarehouseListQuery> logger, IMemoryCache memoryCache,
                IMapper mapper, IUserHttpContextProvider httpContextProvider)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _memoryCache = memoryCache;
                _httpContextProvider = httpContextProvider;
            }
            public async Task<WarehousesListModel> Handle(GetWarehouseListQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    //var cacheKey = "WareHouse" + _httpContextProvider.GetUserId();
                    //if (!_memoryCache.TryGetValue(cacheKey, out List<WarehouseLookModel> warehousesInformationList))
                    //{
                    //var warehousesInformationList  = new List<WarehouseLookModel>();

                    var warehousesInformation = _context.Warehouses.AsQueryable();
                    if (request.CompanyId != Guid.Empty)
                    {
                        _context.DisableTenantFilter = true;
                        warehousesInformation = _context.Warehouses
                            .Where(x => EF.Property<Guid>(x, "CompanyId") == request.CompanyId).AsQueryable();
                    }

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;



                        warehousesInformation = warehousesInformation.Where(x => x.StoreID.ToLower().Contains(searchTerm.ToLower()) ||
                                                 x.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                                  x.NameArabic.ToLower().Contains(searchTerm.ToLower()));


                    }



                    if (request.IsDropdown)
                    {
                        warehousesInformation = warehousesInformation.Where(x => x.isActive).AsQueryable();
                    }

                    var warehousesInformationList = await warehousesInformation
                       .OrderBy(x => x.StoreID)
                       .ProjectTo<WarehouseLookModel>(_mapper.ConfigurationProvider)
                       .ToListAsync(cancellationToken);

                    //var cacheEntryOptions = new MemoryCacheEntryOptions()
                    //    // Keep in cache for this time, reset time if accessed.
                    //    .SetSlidingExpiration(TimeSpan.FromSeconds(120));

                    //// Save data in cache only if it exists
                    //if (warehousesInformationList.Any())
                    //{
                    //    _memoryCache.Set(cacheKey, warehousesInformationList, cacheEntryOptions);
                    //}
                    //}

                    return new WarehousesListModel
                    {
                        warehousesListModels = warehousesInformationList
                    };

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
                finally
                {
                    _context.DisableTenantFilter = false;
                }
            }
        }

    }
}
