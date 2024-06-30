using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Dashboard.Queries.GetInventoryList
{
    public class GetInventoryListQuery : PagedRequest, IRequest<PagedResult<List<InventoryLookUpModel>>>
    {
        public bool isInventory { get; set; }
        public bool isProductFilter { get; set; }
        public bool isProductStockValue { get; set; }
        public bool isProductAverageStockValue { get; set; }
        public bool isTransactionTypeStockValue { get; set; }
        public bool isCustomerWiseProducts { get; set; }
        public bool isCustomersWiseProduct { get; set; }
        public bool isSupplierWiseProducts { get; set; }
        public bool isSuppliersWiseProduct { get; set; }
        public bool isSuppliersDiscount { get; set; }
        public bool isCustomersDiscount { get; set; }
        public bool isDiscount { get; set; }
        public bool isDashboard { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
        public int TransactionType { get; set; }
        public Guid productId { get; set; }
        public DateTime SearchTerm { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid SupplierId { get; set; }
        public bool isProductCustomersDiscount { get; set; }
        public bool isProductSuppliersDiscount { get; set; }
        public bool isfocSale { get; set; }
        public bool isfocPurchase { get; set; }
        public bool IsStock { get; set; }
        public string Search { get; set; }

        public class Handler : IRequestHandler<GetInventoryListQuery, PagedResult<List<InventoryLookUpModel>>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IMapper _mapper;
            public Handler(IApplicationDbContext context, ILogger<GetInventoryListQuery> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<PagedResult<List<InventoryLookUpModel>>> Handle(GetInventoryListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.isDashboard)
                    {
                        var store = _context.Warehouses.AsNoTracking().ToList();

                        var query = _context.Inventories.AsNoTracking()
                            //.Where(x=> x.Date == DateTime.UtcNow.Date)

                            .Include(x => x.Product).AsQueryable();

                        //var date = request.SearchTerm;
                        if (request.productId != Guid.Empty)
                        {
                            query = query.Where(x =>
                                x.Product.Id == request.productId && x.Date.Date == request.SearchTerm.Date
                            );
                        }
                        else
                        {
                            query = query.Where(x =>
                                x.Date.Date == request.SearchTerm.Date
                            );
                        }

                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var inventories = new List<InventoryLookUpModel>();
                        foreach (var item in query)
                        {
                            var wareHouseName = store.Where(x => x.Id == item.WareHouseId)?.FirstOrDefault();
                            inventories.Add(new InventoryLookUpModel
                            {
                                Id = item.Id,
                                WareHouseName = wareHouseName.Name,
                                ProductName = string.IsNullOrEmpty(item.Product.DisplayNameForPrint) ? item.Product.EnglishName : item.Product.DisplayNameForPrint,
                                UnitPerPack = item.Product.UnitPerPack,
                                Date = item.Date,
                                QuantityIn = item.Quantity,
                                QuantityOut = item.Quantity,
                                ProductId = item.Product.Id,
                                TransactionType = item.TransactionType.ToString()
                            });
                        }

                        return new PagedResult<List<InventoryLookUpModel>>
                        {
                            Results = inventories,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = inventories.Count / request.PageSize
                        };
                    }
                    else if (request.isInventory)
                    {
                        var store = _context.Warehouses.AsNoTracking().ToList();

                        var query = _context.Inventories.AsNoTracking()
                            .Include(x => x.Product).AsQueryable();
                        if (!string.IsNullOrEmpty(request.Search))
                        {
                            var searchTerm = request.Search;



                            query = query.Where(x =>
                                                         (x.Product.EnglishName == null || x.Product.EnglishName == "" ? x.Product.ArabicName.ToLower().Contains(searchTerm.ToLower()) : x.Product.EnglishName.ToLower().Contains(searchTerm.ToLower())) ||
                                                         (x.Product.ArabicName == null || x.Product.ArabicName == "" ? x.Product.EnglishName.ToLower().Contains(searchTerm.ToLower()) : x.Product.ArabicName.ToLower().Contains(searchTerm.ToLower())) ||
                                                         x.Product.Code.ToLower().Contains(searchTerm.ToLower())
                                                         );


                        }


                        if (request.WarehouseId != Guid.Empty)
                        {
                            query = query.Where(x => x.WareHouseId == request.WarehouseId && x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date);
                        }
                        else
                        {
                            query = query.Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date);
                        }

                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var inventories = new List<InventoryLookUpModel>();
                        foreach (var item in query)
                        {
                            var wareHouseName = store.FirstOrDefault(x => x.Id == item.WareHouseId);
                            inventories.Add(new InventoryLookUpModel
                            {
                                Id = item.Id,
                                ProductName = string.IsNullOrEmpty(item.Product.DisplayNameForPrint) ? item.Product.EnglishName : item.Product.DisplayNameForPrint,
                                ProductNameArabic = string.IsNullOrEmpty(item.Product.DisplayNameForPrint) ? item.Product.ArabicName : item.Product.DisplayNameForPrint,
                                UnitPerPack = item.Product.UnitPerPack,
                                Date = item.Date,
                                QuantityIn = item.Quantity,
                                QuantityOut = item.Quantity,
                                PurchasePrice = item.Price,
                                AvgPurchasePrice = item.AveragePrice,
                                SalePrice = item.Price,
                                AvgSalePrice = item.AveragePrice,
                                WareHouseName = wareHouseName.Name,
                                WareHouseNameArabic = wareHouseName.NameArabic,
                                WarehouseId = item.WareHouseId,
                                ProductId = item.Product.Id,
                                TransactionType = item.TransactionType.ToString()
                            });
                        }

                        return new PagedResult<List<InventoryLookUpModel>>
                        {
                            Results = inventories,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = inventories.Count / request.PageSize
                        };
                    }
                    else if (request.isProductFilter)
                    {
                        var store = _context.Warehouses.AsNoTracking().ToList();

                        var query = _context.Inventories.AsNoTracking()
                            .Include(x => x.Product).AsQueryable();
                        

                        if (request.productId != Guid.Empty)
                        {
                            query = query.Where(x => x.ProductId == request.productId && x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date);
                        }
                        if (request.WarehouseId != Guid.Empty)
                        {
                            query = query.Where(x => x.WareHouseId == request.WarehouseId && x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date);
                        }
                        else
                        {
                            query = query.Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date);
                        }

                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var inventories = new List<InventoryLookUpModel>();
                        foreach (var item in query)
                        {
                            var wareHouseName = store.FirstOrDefault(x => x.Id == item.WareHouseId);
                            inventories.Add(new InventoryLookUpModel
                            {
                                Id = item.Id,
                                ProductName = string.IsNullOrEmpty(item.Product.DisplayNameForPrint) ? item.Product.EnglishName : item.Product.DisplayNameForPrint,
                                ProductNameArabic = string.IsNullOrEmpty(item.Product.DisplayNameForPrint) ? item.Product.ArabicName : item.Product.DisplayNameForPrint,
                                Date = item.Date,
                                QuantityIn = item.Quantity,
                                QuantityOut = item.Quantity,
                                PurchasePrice = item.Price,
                                AvgPurchasePrice = item.AveragePrice,
                                SalePrice = item.Price,
                                AvgSalePrice = item.AveragePrice,
                                WareHouseName = wareHouseName.Name,
                                WarehouseId = item.WareHouseId,
                                ProductId = item.Product.Id,
                                TransactionType = item.TransactionType.ToString(),
                                UnitPerPack = item.Product.UnitPerPack
                            });
                        }

                        return new PagedResult<List<InventoryLookUpModel>>
                        {
                            Results = inventories,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = inventories.Count / request.PageSize
                        };
                    }
                    else if (request.isProductStockValue)
                    {
                        var store = _context.Warehouses.AsNoTracking().ToList();

                        var query = _context.Inventories.AsNoTracking()
                            .Include(x => x.Product).AsQueryable();
                        

                        if (request.productId != Guid.Empty)
                        {
                            query = query.Where(x => x.ProductId == request.productId && x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date);
                        }
                        if (request.WarehouseId != Guid.Empty)
                        {
                            query = query.Where(x => x.WareHouseId == request.WarehouseId && x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date);
                        }
                        else
                        {
                            query = query.Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date);
                        }

                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var inventories = new List<InventoryLookUpModel>();
                        foreach (var item in query)
                        {
                            var wareHouseName = store.FirstOrDefault(x => x.Id == item.WareHouseId);
                            inventories.Add(new InventoryLookUpModel
                            {
                                Id = item.Id,
                                ProductName = string.IsNullOrEmpty(item.Product.DisplayNameForPrint) ? item.Product.EnglishName : item.Product.DisplayNameForPrint,
                                ProductNameArabic = string.IsNullOrEmpty(item.Product.DisplayNameForPrint) ? item.Product.ArabicName : item.Product.DisplayNameForPrint,
                                Date = item.Date,
                                QuantityIn = item.Quantity,
                                QuantityOut = item.Quantity,
                                PurchasePrice = item.Price,
                                AvgPurchasePrice = item.AveragePrice,
                                SalePrice = item.Price,
                                AvgSalePrice = item.AveragePrice,
                                WareHouseName = wareHouseName.Name,
                                WarehouseId = item.WareHouseId,
                                ProductId = item.Product.Id,
                                TransactionType = item.TransactionType.ToString(),
                                UnitPerPack = item.Product.UnitPerPack
                            });
                        }

                        return new PagedResult<List<InventoryLookUpModel>>
                        {
                            Results = inventories,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = inventories.Count / request.PageSize
                        };
                    }
                    else if (request.isProductAverageStockValue)
                    {
                        var store = _context.Warehouses.AsNoTracking().ToList();

                        var query = _context.Inventories.AsNoTracking()
                            .Include(x => x.Product).AsQueryable();
                        

                        if (request.productId != Guid.Empty)
                        {
                            query = query.Where(x => x.ProductId == request.productId && x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date);
                        }
                        if (request.WarehouseId != Guid.Empty)
                        {
                            query = query.Where(x => x.WareHouseId == request.WarehouseId && x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date);
                        }
                        else
                        {
                            query = query.Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date);
                        }

                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var inventories = new List<InventoryLookUpModel>();
                        foreach (var item in query)
                        {
                            var wareHouseName = store.FirstOrDefault(x => x.Id == item.WareHouseId);
                            inventories.Add(new InventoryLookUpModel
                            {
                                Id = item.Id,
                                ProductName = string.IsNullOrEmpty(item.Product.DisplayNameForPrint) ? item.Product.EnglishName : item.Product.DisplayNameForPrint,
                                ProductNameArabic = string.IsNullOrEmpty(item.Product.DisplayNameForPrint) ? item.Product.ArabicName : item.Product.DisplayNameForPrint,
                                Date = item.Date,
                                QuantityIn = item.Quantity,
                                QuantityOut = item.Quantity,
                                PurchasePrice = item.Price,
                                AvgPurchasePrice = item.AveragePrice,
                                SalePrice = item.Price,
                                AvgSalePrice = item.AveragePrice,
                                WareHouseName = wareHouseName.Name,
                                WarehouseId = item.WareHouseId,
                                ProductId = item.Product.Id,
                                TransactionType = item.TransactionType.ToString(),
                                UnitPerPack = item.Product.UnitPerPack
                            });
                        }

                        return new PagedResult<List<InventoryLookUpModel>>
                        {
                            Results = inventories,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = inventories.Count / request.PageSize
                        };
                    }
                    else if (request.isTransactionTypeStockValue)
                    {
                        var store = _context.Warehouses.AsNoTracking().ToList();

                        var query = _context.Inventories.AsNoTracking()
                            .Include(x => x.Product).AsQueryable();
                        

                        if (request.productId != Guid.Empty)
                        {
                            query = query.Where(x => x.ProductId == request.productId && x.TransactionType == (TransactionType)request.TransactionType && x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date);
                        }
                        if (request.WarehouseId != Guid.Empty)
                        {
                            query = query.Where(x => x.WareHouseId == request.WarehouseId && x.TransactionType == (TransactionType)request.TransactionType && x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date);
                        }
                        else
                        {
                            query = query.Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date && x.TransactionType == (TransactionType)request.TransactionType);
                        }

                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var inventories = new List<InventoryLookUpModel>();
                        foreach (var item in query)
                        {
                            var wareHouseName = store.FirstOrDefault(x => x.Id == item.WareHouseId);
                            inventories.Add(new InventoryLookUpModel
                            {
                                Id = item.Id,
                                ProductName = string.IsNullOrEmpty(item.Product.DisplayNameForPrint) ? item.Product.EnglishName : item.Product.DisplayNameForPrint,
                                ProductNameArabic = string.IsNullOrEmpty(item.Product.DisplayNameForPrint) ? item.Product.ArabicName : item.Product.DisplayNameForPrint,
                                Date = item.Date,
                                QuantityIn = item.Quantity,
                                QuantityOut = item.Quantity,
                                PurchasePrice = item.Price,
                                AvgPurchasePrice = item.AveragePrice,
                                SalePrice = item.Price,
                                AvgSalePrice = item.AveragePrice,
                                WareHouseName = wareHouseName.Name,
                                WarehouseId = item.WareHouseId,
                                ProductId = item.Product.Id,
                                TransactionType = item.TransactionType.ToString(),
                                UnitPerPack = item.Product.UnitPerPack
                            });
                        }

                        return new PagedResult<List<InventoryLookUpModel>>
                        {
                            Results = inventories,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = inventories.Count / request.PageSize
                        };
                    }
                    else if (request.isCustomerWiseProducts)
                    {

                        var results = from ss in _context.Sales
                                      join inv in _context.Inventories on ss.Id equals inv.DocumentId
                                      join pr in _context.Products on inv.ProductId equals pr.Id
                                      join ct in _context.Contacts on ss.CustomerId equals ct.Id
                                      where inv.Date.Date >= request.FromDate.Date
                                          && inv.Date.Date <= request.ToDate.Date
                                          && ct.Id == request.CustomerId
                                      select new InventoryLookUpModel
                                      {
                                          Id = inv.Id,
                                          ProductName = string.IsNullOrEmpty(pr.DisplayNameForPrint) ? pr.EnglishName : pr.DisplayNameForPrint,
                                          ProductId = pr.Id,
                                          Date = inv.Date,
                                          QuantityOut = inv.Quantity,
                                          SalePrice = inv.Price,
                                          CustomerName = ct.EnglishName,
                                          CustomerId = ct.Id,
                                          TransactionType = inv.TransactionType.ToString(),
                                          UnitPerPack = pr.UnitPerPack
                                      };

                        var count = await results.CountAsync(cancellationToken: cancellationToken);
                        results = results.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        return new PagedResult<List<InventoryLookUpModel>>
                        {
                            Results = results.ToList(),
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = results.ToList().Count / request.PageSize
                        };
                    }
                    else if (request.isCustomersWiseProduct)
                    {

                        var results = from ss in _context.Sales
                                      join inv in _context.Inventories on ss.Id equals inv.DocumentId
                                      join pr in _context.Products on inv.ProductId equals pr.Id
                                      join ct in _context.Contacts on ss.CustomerId equals ct.Id
                                      where inv.Date.Date >= request.FromDate.Date
                                          && inv.Date.Date <= request.ToDate.Date
                                          && pr.Id == request.productId
                                      select new InventoryLookUpModel
                                      {
                                          Id = inv.Id,
                                          ProductName = string.IsNullOrEmpty(pr.DisplayNameForPrint) ? pr.EnglishName : pr.DisplayNameForPrint,
                                          ProductId = pr.Id,
                                          Date = inv.Date,
                                          QuantityOut = inv.Quantity,
                                          SalePrice = inv.Price,
                                          CustomerName = ct.EnglishName,
                                          CustomerId = ct.Id,
                                          TransactionType = inv.TransactionType.ToString(),
                                          UnitPerPack = pr.UnitPerPack
                                      };

                        var count = await results.CountAsync(cancellationToken: cancellationToken);
                        results = results.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        return new PagedResult<List<InventoryLookUpModel>>
                        {
                            Results = results.ToList(),
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = results.ToList().Count / request.PageSize
                        };
                    }
                    else if (request.isSupplierWiseProducts)
                    {

                        var results = from pp in _context.PurchasePosts
                                      join inv in _context.Inventories on pp.Id equals inv.DocumentId
                                      join pr in _context.Products on inv.ProductId equals pr.Id
                                      join ct in _context.Contacts on pp.SupplierId equals ct.Id
                                      where inv.Date.Date >= request.FromDate.Date
                                          && inv.Date.Date <= request.ToDate.Date
                                          && ct.Id == request.SupplierId
                                      select new InventoryLookUpModel
                                      {
                                          Id = inv.Id,
                                          ProductName = string.IsNullOrEmpty(pr.DisplayNameForPrint) ? pr.EnglishName : pr.DisplayNameForPrint,
                                          ProductId = pr.Id,
                                          Date = inv.Date,
                                          QuantityIn = inv.Quantity,
                                          PurchasePrice = inv.Price,
                                          SupplierName = ct.EnglishName,
                                          SupplierId = ct.Id,
                                          TransactionType = inv.TransactionType.ToString(),
                                          UnitPerPack = pr.UnitPerPack
                                      };

                        var count = await results.CountAsync(cancellationToken: cancellationToken);
                        results = results.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        return new PagedResult<List<InventoryLookUpModel>>
                        {
                            Results = results.ToList(),
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = results.ToList().Count / request.PageSize
                        };
                    }
                    else if (request.isSuppliersWiseProduct)
                    {
                        

                        var results = from pp in _context.PurchasePosts
                                      join inv in _context.Inventories on pp.Id equals inv.DocumentId
                                      join pr in _context.Products on inv.ProductId equals pr.Id
                                      join ct in _context.Contacts on pp.SupplierId equals ct.Id
                                      where inv.Date.Date >= request.FromDate.Date
                                          && inv.Date.Date <= request.ToDate.Date
                                          && pr.Id == request.productId
                                      select new InventoryLookUpModel
                                      {
                                          Id = inv.Id,
                                          ProductName = string.IsNullOrEmpty(pr.DisplayNameForPrint) ? pr.EnglishName : pr.DisplayNameForPrint,
                                          ProductId = pr.Id,
                                          Date = inv.Date,
                                          QuantityIn = inv.Quantity,
                                          PurchasePrice = inv.Price,
                                          SupplierName = ct.EnglishName,
                                          SupplierId = ct.Id,
                                          TransactionType = inv.TransactionType.ToString(),
                                          UnitPerPack = pr.UnitPerPack
                                      };

                        var count = await results.CountAsync(cancellationToken: cancellationToken);
                        results = results.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        return new PagedResult<List<InventoryLookUpModel>>
                        {
                            Results = results.ToList(),
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = results.ToList().Count / request.PageSize
                        };
                    }
                    else if (request.isCustomersDiscount)
                    {
                        if (request.isDiscount)
                        {
                            var results = from ss in _context.Sales
                                          join ssi in _context.SaleItems on ss.Id equals ssi.SaleId
                                          join pr in _context.Products on ssi.ProductId equals pr.Id
                                          join ct in _context.Contacts on ss.CustomerId equals ct.Id
                                          where ss.Date.Date >= request.FromDate.Date
                                              && ss.Date.Date <= request.ToDate.Date
                                              && ct.Id == request.CustomerId && ssi.FixDiscount != 0
                                          select new InventoryLookUpModel
                                          {
                                              Id = ssi.Id,
                                              ProductName = string.IsNullOrEmpty(pr.DisplayNameForPrint) ? pr.EnglishName : pr.DisplayNameForPrint,
                                              ProductId = pr.Id,
                                              Date = ss.Date,
                                              DiscountFixed = ssi.FixDiscount,
                                              DiscountPercentage = ssi.Discount,
                                              QuantityOut = ssi.Quantity,
                                              SalePrice = ssi.UnitPrice,
                                              CustomerName = ct.EnglishName,
                                              CustomerId = ct.Id,
                                              UnitPerPack = pr.UnitPerPack
                                          };

                            var count = await results.CountAsync(cancellationToken: cancellationToken);
                            results = results.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                            return new PagedResult<List<InventoryLookUpModel>>
                            {
                                Results = results.ToList(),
                                RowCount = count,
                                PageSize = request.PageSize,
                                CurrentPage = request.PageNumber,
                                PageCount = results.ToList().Count / request.PageSize
                            };
                        }
                        else
                        {
                            var results = from ss in _context.Sales
                                          join ssi in _context.SaleItems on ss.Id equals ssi.SaleId
                                          join pr in _context.Products on ssi.ProductId equals pr.Id
                                          join ct in _context.Contacts on ss.CustomerId equals ct.Id
                                          where ss.Date.Date >= request.FromDate.Date
                                              && ss.Date.Date <= request.ToDate.Date
                                              && ct.Id == request.CustomerId && ssi.Discount != 0
                                          select new InventoryLookUpModel
                                          {
                                              Id = ssi.Id,
                                              ProductName = string.IsNullOrEmpty(pr.DisplayNameForPrint) ? pr.EnglishName : pr.DisplayNameForPrint,
                                              ProductId = pr.Id,
                                              Date = ss.Date,
                                              DiscountFixed = ssi.FixDiscount,
                                              DiscountPercentage = ssi.Discount,
                                              QuantityOut = ssi.Quantity,
                                              SalePrice = ssi.UnitPrice,
                                              CustomerName = ct.EnglishName,
                                              CustomerId = ct.Id,
                                              UnitPerPack = pr.UnitPerPack
                                          };

                            var count = await results.CountAsync(cancellationToken: cancellationToken);
                            results = results.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                            return new PagedResult<List<InventoryLookUpModel>>
                            {
                                Results = results.ToList(),
                                RowCount = count,
                                PageSize = request.PageSize,
                                CurrentPage = request.PageNumber,
                                PageCount = results.ToList().Count / request.PageSize
                            };
                        }
                    }
                    else if (request.isSuppliersDiscount)
                    {
                        if (request.isDiscount)
                        {
                            var results = from pp in _context.PurchasePosts
                                          join ppi in _context.PurchasePostItems on pp.Id equals ppi.PurchasePostId
                                          join pr in _context.Products on ppi.ProductId equals pr.Id
                                          join ct in _context.Contacts on pp.SupplierId equals ct.Id
                                          where pp.Date.Date >= request.FromDate.Date
                                              && pp.Date.Date <= request.ToDate.Date
                                              && ct.Id == request.SupplierId && ppi.FixDiscount != 0
                                          select new InventoryLookUpModel
                                          {
                                              Id = ppi.Id,
                                              ProductName = string.IsNullOrEmpty(pr.DisplayNameForPrint) ? pr.EnglishName : pr.DisplayNameForPrint,
                                              ProductId = pr.Id,
                                              Date = pp.Date,
                                              DiscountFixed = ppi.FixDiscount,
                                              DiscountPercentage = ppi.Discount,
                                              QuantityIn = ppi.Quantity,
                                              PurchasePrice = ppi.UnitPrice,
                                              SupplierName = ct.EnglishName,
                                              SupplierId = ct.Id,
                                              UnitPerPack = pr.UnitPerPack
                                          };

                            var count = await results.CountAsync(cancellationToken: cancellationToken);
                            results = results.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                            return new PagedResult<List<InventoryLookUpModel>>
                            {
                                Results = results.ToList(),
                                RowCount = count,
                                PageSize = request.PageSize,
                                CurrentPage = request.PageNumber,
                                PageCount = results.ToList().Count / request.PageSize
                            };
                        }
                        else
                        {
                            var results = from pp in _context.PurchasePosts
                                          join ppi in _context.PurchasePostItems on pp.Id equals ppi.PurchasePostId
                                          join pr in _context.Products on ppi.ProductId equals pr.Id
                                          join ct in _context.Contacts on pp.SupplierId equals ct.Id
                                          where pp.Date.Date >= request.FromDate.Date
                                              && pp.Date.Date <= request.ToDate.Date
                                              && ct.Id == request.SupplierId && ppi.Discount != 0
                                          select new InventoryLookUpModel
                                          {
                                              Id = ppi.Id,
                                              ProductName = string.IsNullOrEmpty(pr.DisplayNameForPrint) ? pr.EnglishName : pr.DisplayNameForPrint,
                                              ProductId = pr.Id,
                                              Date = pp.Date,
                                              DiscountFixed = ppi.FixDiscount,
                                              DiscountPercentage = ppi.Discount,
                                              QuantityIn = ppi.Quantity,
                                              PurchasePrice = ppi.UnitPrice,
                                              SupplierName = ct.EnglishName,
                                              SupplierId = ct.Id,
                                              UnitPerPack = pr.UnitPerPack
                                          };

                            var count = await results.CountAsync(cancellationToken: cancellationToken);
                            results = results.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                            return new PagedResult<List<InventoryLookUpModel>>
                            {
                                Results = results.ToList(),
                                RowCount = count,
                                PageSize = request.PageSize,
                                CurrentPage = request.PageNumber,
                                PageCount = results.ToList().Count / request.PageSize
                            };
                        }
                    }
                    else if (request.isProductCustomersDiscount)
                    {
                        if (request.isDiscount)
                        {
                            var results = from ss in _context.Sales
                                          join ssi in _context.SaleItems on ss.Id equals ssi.SaleId
                                          join pr in _context.Products on ssi.ProductId equals pr.Id
                                          join ct in _context.Contacts on ss.CustomerId equals ct.Id
                                          where ss.Date.Date >= request.FromDate.Date
                                              && ss.Date.Date <= request.ToDate.Date
                                              && pr.Id == request.productId && ssi.FixDiscount != 0
                                          select new InventoryLookUpModel
                                          {
                                              Id = ssi.Id,
                                              ProductName = string.IsNullOrEmpty(pr.DisplayNameForPrint) ? pr.EnglishName : pr.DisplayNameForPrint,
                                              ProductId = pr.Id,
                                              Date = ss.Date,
                                              DiscountFixed = ssi.FixDiscount,
                                              DiscountPercentage = ssi.Discount,
                                              QuantityOut = ssi.Quantity,
                                              SalePrice = ssi.UnitPrice,
                                              CustomerName = ct.EnglishName,
                                              CustomerId = ct.Id,
                                              UnitPerPack = pr.UnitPerPack
                                          };

                            var count = await results.CountAsync(cancellationToken: cancellationToken);
                            results = results.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                            return new PagedResult<List<InventoryLookUpModel>>
                            {
                                Results = results.ToList(),
                                RowCount = count,
                                PageSize = request.PageSize,
                                CurrentPage = request.PageNumber,
                                PageCount = results.ToList().Count / request.PageSize
                            };
                        }
                        else
                        {
                            var results = from ss in _context.Sales
                                          join ssi in _context.SaleItems on ss.Id equals ssi.SaleId
                                          join pr in _context.Products on ssi.ProductId equals pr.Id
                                          join ct in _context.Contacts on ss.CustomerId equals ct.Id
                                          where ss.Date.Date >= request.FromDate.Date
                                              && ss.Date.Date <= request.ToDate.Date
                                              && pr.Id == request.productId && ssi.FixDiscount != 0
                                          select new InventoryLookUpModel
                                          {
                                              Id = ssi.Id,
                                              ProductName = string.IsNullOrEmpty(pr.DisplayNameForPrint) ? pr.EnglishName : pr.DisplayNameForPrint,
                                              ProductId = pr.Id,
                                              Date = ss.Date,
                                              DiscountFixed = ssi.FixDiscount,
                                              DiscountPercentage = ssi.Discount,
                                              QuantityOut = ssi.Quantity,
                                              SalePrice = ssi.UnitPrice,
                                              CustomerName = ct.EnglishName,
                                              CustomerId = ct.Id,
                                              UnitPerPack = pr.UnitPerPack
                                          };

                            var count = await results.CountAsync(cancellationToken: cancellationToken);
                            results = results.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                            return new PagedResult<List<InventoryLookUpModel>>
                            {
                                Results = results.ToList(),
                                RowCount = count,
                                PageSize = request.PageSize,
                                CurrentPage = request.PageNumber,
                                PageCount = results.ToList().Count / request.PageSize
                            };
                        }
                    }
                    else if (request.isProductSuppliersDiscount)
                    {
                        if (request.isDiscount)
                        {
                            var results = from pp in _context.PurchasePosts
                                          join ppi in _context.PurchasePostItems on pp.Id equals ppi.PurchasePostId
                                          join pr in _context.Products on ppi.ProductId equals pr.Id
                                          join ct in _context.Contacts on pp.SupplierId equals ct.Id
                                          where pp.Date.Date >= request.FromDate.Date
                                              && pp.Date.Date <= request.ToDate.Date
                                              && pr.Id == request.productId && ppi.FixDiscount != 0
                                          select new InventoryLookUpModel
                                          {
                                              Id = ppi.Id,
                                              ProductName = string.IsNullOrEmpty(pr.DisplayNameForPrint) ? pr.EnglishName : pr.DisplayNameForPrint,
                                              ProductId = pr.Id,
                                              Date = pp.Date,
                                              DiscountFixed = ppi.FixDiscount,
                                              DiscountPercentage = ppi.Discount,
                                              QuantityIn = ppi.Quantity,
                                              PurchasePrice = ppi.UnitPrice,
                                              SupplierName = ct.EnglishName,
                                              SupplierId = ct.Id,
                                              UnitPerPack = pr.UnitPerPack
                                          };

                            var count = await results.CountAsync(cancellationToken: cancellationToken);
                            results = results.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                            return new PagedResult<List<InventoryLookUpModel>>
                            {
                                Results = results.ToList(),
                                RowCount = count,
                                PageSize = request.PageSize,
                                CurrentPage = request.PageNumber,
                                PageCount = results.ToList().Count / request.PageSize
                            };
                        }
                        else
                        {
                            var results = from pp in _context.PurchasePosts
                                          join ppi in _context.PurchasePostItems on pp.Id equals ppi.PurchasePostId
                                          join pr in _context.Products on ppi.ProductId equals pr.Id
                                          join ct in _context.Contacts on pp.SupplierId equals ct.Id
                                          where pp.Date.Date >= request.FromDate.Date
                                              && pp.Date.Date <= request.ToDate.Date
                                              && pr.Id == request.productId && ppi.FixDiscount != 0
                                          select new InventoryLookUpModel
                                          {
                                              Id = ppi.Id,
                                              ProductName = pr.EnglishName,
                                              ProductId = pr.Id,
                                              Date = pp.Date,
                                              DiscountFixed = ppi.FixDiscount,
                                              DiscountPercentage = ppi.Discount,
                                              QuantityIn = ppi.Quantity,
                                              PurchasePrice = ppi.UnitPrice,
                                              SupplierName = ct.EnglishName,
                                              SupplierId = ct.Id,
                                              UnitPerPack = pr.UnitPerPack
                                          };

                            var count = await results.CountAsync(cancellationToken: cancellationToken);
                            results = results.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                            return new PagedResult<List<InventoryLookUpModel>>
                            {
                                Results = results.ToList(),
                                RowCount = count,
                                PageSize = request.PageSize,
                                CurrentPage = request.PageNumber,
                                PageCount = results.ToList().Count / request.PageSize
                            };
                        }
                    }
                    else if (request.isfocPurchase)
                    {
                        var results = from pp in _context.PurchasePosts
                                      join ppi in _context.PurchasePostItems on pp.Id equals ppi.PurchasePostId
                                      join pr in _context.Products on ppi.ProductId equals pr.Id
                                      join ct in _context.Contacts on pp.SupplierId equals ct.Id
                                      where pp.Date.Date >= request.FromDate.Date
                                          && pp.Date.Date <= request.ToDate.Date
                                          && ct.Id == request.SupplierId && ppi.UnitPrice == 0
                                      select new InventoryLookUpModel
                                      {
                                          Id = ppi.Id,
                                          ProductName = string.IsNullOrEmpty(pr.DisplayNameForPrint) ? pr.EnglishName : pr.DisplayNameForPrint,
                                          ProductId = pr.Id,
                                          Date = pp.Date,
                                          DiscountFixed = ppi.FixDiscount,
                                          DiscountPercentage = ppi.Discount,
                                          QuantityIn = ppi.Quantity,
                                          PurchasePrice = ppi.UnitPrice,
                                          SupplierName = ct.EnglishName,
                                          SupplierId = ct.Id,
                                          UnitPerPack = pr.UnitPerPack
                                      };

                        var count = await results.CountAsync(cancellationToken: cancellationToken);
                        results = results.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        return new PagedResult<List<InventoryLookUpModel>>
                        {
                            Results = results.ToList(),
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = results.ToList().Count / request.PageSize
                        };
                    }
                    else if (request.isfocSale == true)
                    {
                        var results = from ss in _context.Sales
                                      join ssi in _context.SaleItems on ss.Id equals ssi.SaleId
                                      join pr in _context.Products on ssi.ProductId equals pr.Id
                                      join ct in _context.Contacts on ss.CustomerId equals ct.Id
                                      where ss.Date.Date >= request.FromDate.Date
                                          && ss.Date.Date <= request.ToDate.Date
                                          && ct.Id == request.CustomerId && ssi.Buy != 0 && ssi.Get != 0
                                      select new InventoryLookUpModel
                                      {
                                          Id = ssi.Id,
                                          ProductName = string.IsNullOrEmpty(pr.DisplayNameForPrint) ? pr.EnglishName : pr.DisplayNameForPrint,
                                          ProductId = pr.Id,
                                          Date = ss.Date,
                                          DiscountFixed = ssi.FixDiscount,
                                          DiscountPercentage = ssi.Discount,
                                          QuantityOut = ssi.Quantity,
                                          SalePrice = ssi.UnitPrice,
                                          CustomerName = ct.EnglishName,
                                          CustomerId = ct.Id,
                                          UnitPerPack = pr.UnitPerPack
                                      };

                        var count = await results.CountAsync(cancellationToken: cancellationToken);
                        results = results.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        return new PagedResult<List<InventoryLookUpModel>>
                        {
                            Results = results.ToList(),
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = results.ToList().Count / request.PageSize
                        };
                    }
                    else
                    {
                        return null;
                    }
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
