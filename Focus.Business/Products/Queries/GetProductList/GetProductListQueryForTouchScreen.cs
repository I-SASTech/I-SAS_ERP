using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Focus.Business.Categories.Queries.GetCategoryDetails;
using Focus.Business.Common;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Products.Queries.GetProductList
{
    public class GetProductListQueryForTouchScreen : PagedRequest, IRequest<PagedResult<ProductListModel>>
    {
        public string SearchTerm { get; set; }
        public Guid? WareHouseId { get; set; }
        public Guid? CategoryId { get; set; }
        public bool IsRaw { get; set; }

        public class Handler : IRequestHandler<GetProductListQueryForTouchScreen, PagedResult<ProductListModel>>
        {
            private readonly ILogger _logger;
            public readonly IConfiguration Configuration;
            private readonly IUserHttpContextProvider _contextProvider;

            public Handler(ILogger<GetProductListQueryForTouchScreen> logger,
                IConfiguration configuration, IUserHttpContextProvider contextProvider)
            {
                _logger = logger;
                Configuration = configuration;
                _contextProvider = contextProvider;
            }
            public async Task<PagedResult<ProductListModel>> Handle(GetProductListQueryForTouchScreen request, CancellationToken cancellationToken)
            {
                try
                {
                    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    var categories = conn.Query<Category>("select * from Categories where Categories.CompanyId = '" + _contextProvider.GetCompanyId() + "'").ToList();
                    var taxRates = conn.Query<TaxRate>("select * from TaxRates where TaxRates.CompanyId = '" + _contextProvider.GetCompanyId() + "'").ToList();
                    var terminalCategories = conn.Query<TerminalCategory>("select * from TerminalCategories where TerminalCategories.CompanyId = '" + _contextProvider.GetCompanyId() + "' AND TerminalCategories.TerminalId = '" + _contextProvider.GetCounterId() + "'").Select(x => x.CategoryId).ToList();
                    var terminalIdString = string.Join("','", terminalCategories.Count()==0? categories.Select(x=>x.Id).ToList(): terminalCategories);
                    request.WareHouseId ??= Guid.Empty;

                    var searchString = request.SearchTerm != null ? "select TOP 30 * " : "select  TOP 30  * ";


                    string querystring;
                    if (request.CategoryId != Guid.Empty && request.CategoryId != null)
                    {
                        querystring = searchString + " ,Products.Code As PCode" +
                                      " FROM Products" +
                                      " left  JOIN Units ON Units.Id = Products.UnitId" +
                                      " left JOIN Inventories inv ON Products.Id = inv.ProductId" +
                                      " left join PromotionOffers  PromotionOffers on  Products.PromotionOfferId =PromotionOffers.Id" +
                                      " left join BundleCategories BundleCategories on Products.BundleCategoryId= BundleCategories.Id" +
                                      " left join Sizes on Products.SizeId= Sizes.Id" +
                                      " left join Colors on Products.ColorId= Colors.Id" +
                                      " WHERE  (CASE" +
                                      " WHEN (inv.Id is NULL" +
                                      " or inv.Id In(select top 1 i.Id" +
                                      " from Inventories as i	" +
                                      " where Products.Id = i.ProductId  " +
                                      " order by i.CreatedOn desc))" +
                                      " AND (ProductId IN (SELECT ProductId" +
                                      " FROM Products" +
                                      " WHERE WarehouseId = '" +
                                      request.WareHouseId + "'" +
                                      " ))" +
                                      " THEN 1" +
                                      " WHEN (inv.Id is NULL" +
                                      " or inv.Id In(select top 1 i.Id" +
                                      "  from Inventories as i	" +
                                      "    where Products.Id = i.ProductId  " +
                                      "  order by i.CreatedOn desc))" +
                                      " OR (ProductId IN (SELECT ProductId" +
                                      " FROM Products" +
                                      " WHERE WarehouseId IS NULL " +
                                      " ))" +
                                      " THEN " + (request.WareHouseId == Guid.Empty ? 1 : 0) +
                                      " END  = 1) " +
                                      " and Products.CompanyId= '" +
                                      _contextProvider.GetCompanyId() + "' and Products.IsActive=1 and Products.CategoryId='" + request.CategoryId + "'  and Products.IsRaw='" + request.IsRaw + "'" +
                                      "and Products.CategoryId in (" + "'" + terminalIdString + "'" + ")" +
                                      "and (Products.Code LIKE '%" + request.SearchTerm + "%' OR Products.EnglishName LIKE '%" + request.SearchTerm + "%' OR Products.ArabicName LIKE '%" + request.SearchTerm + "%' OR Products.BarCode LIKE '%" + request.SearchTerm + "%')" +
                                      " order by Products.Code ";
                    }
                    else
                    {
                        querystring = searchString + " ,Products.Code As PCode" +
                                      " FROM Products" +
                                      " left  JOIN Units ON Units.Id = Products.UnitId" +
                                      " left JOIN Inventories inv ON Products.Id = inv.ProductId" +
                                      " left join PromotionOffers  PromotionOffers on  Products.PromotionOfferId =PromotionOffers.Id" +
                                      " left join BundleCategories BundleCategories on Products.BundleCategoryId= BundleCategories.Id" +
                                      " left join Sizes on Products.SizeId= Sizes.Id" +
                                      " left join Colors on Products.ColorId= Colors.Id" +
                                      " WHERE  (CASE" +
                                      " WHEN (inv.Id is NULL" +
                                      " or inv.Id In(select top 1 i.Id" +
                                      " from Inventories as i	" +
                                      " where Products.Id = i.ProductId  " +
                                      " order by i.CreatedOn desc))" +
                                      " AND (ProductId IN (SELECT ProductId" +
                                      " FROM Products" +
                                      " WHERE WarehouseId = '" +
                                      request.WareHouseId + "'" +
                                      " ))" +
                                      " THEN 1" +
                                      " WHEN (inv.Id is NULL" +
                                      " or inv.Id In(select top 1 i.Id" +
                                      "  from Inventories as i	" +
                                      "    where Products.Id = i.ProductId  " +
                                      "  order by i.CreatedOn desc))" +
                                      " OR (ProductId IN (SELECT ProductId" +
                                      " FROM Products" +
                                      " WHERE WarehouseId IS NULL " +
                                      " ))" +
                                      " THEN " + (request.WareHouseId == Guid.Empty ? 1 : 0) +
                                      " END  = 1) " +
                                      " and Products.CompanyId= '" +
                                      _contextProvider.GetCompanyId() + "' and Products.IsActive=1 and Products.IsRaw='"+ request.IsRaw+"'" +
                                      "and Products.CategoryId in (" + "'" + terminalIdString + "'" + ")" +
                                      "and (Products.Code LIKE '%" + request.SearchTerm + "%' OR Products.EnglishName LIKE '%" + request.SearchTerm + "%' OR Products.ArabicName LIKE '%" + request.SearchTerm + "%' OR Products.BarCode LIKE '%" + request.SearchTerm + "%')" +
                                      " order by Products.Code ";
                    }



                    var productsWithCategories = conn.Query<Product, Domain.Entities.Unit, Inventory, PromotionOffer, BundleCategory, Size, Color, Product>(querystring,
                        map: (product, unit, inv, promotionOffer, bundleCategory, size, color) =>
                        {
                            product.Unit = unit;
                            product.Id = product.Id;
                            //product.PromotionOffer = promotionOffer;
                            //product.BundleCategory = bundleCategory;
                            product.Size = size;
                            product.Category = categories.FirstOrDefault(x => x.Id == product.CategoryId);
                            product.Color = color;

                            if (inv != null)
                            {
                                var list = new List<Inventory> { inv };
                                product.Inventories = list;
                            }

                            return product;
                        }, splitOn: "Id").ToList();

                    var query = productsWithCategories.AsQueryable();

                    //if (request.WareHouseId != Guid.Empty && request.WareHouseId != null)
                    //{
                    //    query = query.Where(x =>
                    //        x.Inventories != null && x.Inventories.Any(y => y.WareHouseId == request.WareHouseId.Value));
                    //}

                    
                    var productLookUpModel = new List<ProductLookUpModel>();

                    foreach (var product in query)
                    {
                        productLookUpModel.Add(new ProductLookUpModel()
                        {
                            ArabicName = product.ArabicName,
                            Assortment = product.Assortment,
                            BarCode = product.BarCode,
                            BasicUnit = product.Unit?.Name,
                            BrandId = product.BrandId,
                            //BundleCategory = product.BundleCategory,
                            //PromotionOffer = product.PromotionOffer,
                            CategoryId = product.CategoryId,
                            Code = product.Code,
                            Category = new CategoryDetailLookUpModel()
                            {
                                Name = product.Category.Name,
                                NameArabic = product.Category.NameArabic,
                            },
                            ColorId = product.ColorId,
                            ColorName = product.Color?.Name,
                            ColorNameArabic = product.Color?.NameArabic,
                            Description = product.Description,
                            EnglishName = product.EnglishName,
                            Inventory = product.Inventories?.FirstOrDefault(),
                            IsActive = product.IsActive,
                            Id = product.Id,
                            Image = product.Image,
                            IsExpire = product.IsExpire,
                            IsRaw = product.IsRaw,
                            Length = product.Length,
                            LevelOneUnit = product.LevelOneUnit,
                            StockLevel = product.StockLevel,
                            OriginId = product.OriginId,
                            PurchasePrice = product.Inventories?.FirstOrDefault(),
                            SalePrice = product.SalePrice,
                            SalePriceUnit = product.SalePriceUnit,
                            SaleReturnDays = product.SaleReturnDays,
                            Shelf = product.Shelf,
                            SizeId = product.SizeId,
                            SizeName = product.Size?.Name,
                            SizeNameArabic = product.Size?.NameArabic,
                            StyleNumber = product.StyleNumber,
                            SubCategoryId = product.SubCategoryId,
                            TaxMethod = product.TaxMethod,
                            TaxRate = product.TaxRate?.Name,
                            TaxRateValue = taxRates.FirstOrDefault(x => x.Id == product.TaxRateId)?.Rate,
                            TaxRateId = product.TaxRateId,
                            Unit = product.Unit,
                            UnitId = product.UnitId,
                            UnitPerPack = product.UnitPerPack,
                            Width = product.Width,
                            Guarantee = product.Guarantee,
                            Serial = product.Serial,
                            HighUnitPrice = product.HighUnitPrice,
                        });
                    }

                    return new PagedResult<ProductListModel>
                    {
                        Results = new ProductListModel
                        {
                            Products = productLookUpModel
                        }
                    };


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
