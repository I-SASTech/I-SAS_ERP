using Focus.Business.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Focus.Business.Interface;
using Focus.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Products.Queries.GetProductList
{
    public class GetProductListForTableQuery : PagedRequest, IRequest<PagedResult<ProductListModel>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropdown { get; set; }
        public Guid? WareHouseId { get; set; }
        public string Status { get; set; }
        public bool IsRaw { get; set; }
        public bool IsProductList { get; set; }
        public bool? IsService { get; set; }
        public bool IsFifo { get; set; }
        public int OpenBatch { get; set; }
        public bool IsEmail { get; set; }
        public bool ColorVariants { get; set; }
        public List<Guid?> SubCategoryId { get; set; }
        public List<Guid?> ProductMasterId { get; set; }

        public List<Guid?> ColorId { get; set; }

        public List<Guid?> SizeId { get; set; }

        public List<Guid?> OriginId { get; set; }
        public List<Guid?> CategoryId { get; set; }
        public Guid? BranchId { get; set; }
        public class Handler : IRequestHandler<GetProductListForTableQuery, PagedResult<ProductListModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IConfiguration _configuration;
            private readonly IUserHttpContextProvider _contextProvider;

            public Handler(IApplicationDbContext context, ILogger<GetProductListForTableQuery> logger, IConfiguration configuration, IUserHttpContextProvider contextProvider)
            {
                _context = context;
                _logger = logger;
                _configuration = configuration;
                _contextProvider = contextProvider;
            }
            public async Task<PagedResult<ProductListModel>> Handle(GetProductListForTableQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var itemsListDisplay = await _context.ItemsListDisplayOrderSetup.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

                    var companyId = _contextProvider.GetCompanyId();


                    string parameterQuery = "products.Id, products.IsRaw, products.ServiceItem, products.ProductMasterId, products.CategoryId, products.SubCategoryId, products.SizeId, products.ColorId, products.OriginId ";

                    if (request.BranchId != null)
                    {
                        parameterQuery += ", ( select SUM(CurrentQuantity) from Stocks WHERE ProductId=Products.Id AND BranchId='" + request.BranchId + "') as CurrentQuantity ";
                    }
                    else
                    {
                        parameterQuery += ", ( select SUM(CurrentQuantity) from Stocks WHERE ProductId=Products.Id) as CurrentQuantity ";
                    }

                    foreach (var item in itemsListDisplay)
                    {
                        if (item.Name == "Item Code")
                        {
                            parameterQuery += ",products.Code ";
                        }
                        if (item.Name == "Item Name")
                        {
                            parameterQuery += ",products.DisplayName ";
                        }
                        if (item.Name == "Sale Price")
                        {
                            parameterQuery += ",products.SalePrice ";
                        }
                        if (item.Name == "Purchase Price")
                        {
                            parameterQuery += ",products.PurchasePrice as PurchasePrices ";
                        }
                        if (item.Name == "BarCode Type")
                        {
                            parameterQuery += ",products.BarCodeType ";
                        }
                        if (item.Name == "Image")
                        {
                            parameterQuery += ",products.Image ";
                        }
                        if (item.Name == "Item Style/Model")
                        {
                            parameterQuery += ",products.StyleNumber ";
                        }
                        if (item.Name == "SKU")
                        {
                            parameterQuery += ",products.SKU ";
                        }
                        if (item.Name == "Part Number")
                        {
                            parameterQuery += ",products.PartNumber ";
                        }


                        if (item.Name == "Item Category")
                        {
                            parameterQuery += ",Categories.Name as CategoryNameEn , Categories.NameArabic as CategoryNameAr ";
                        }
                        if (item.Name == "Item Brand")
                        {
                            parameterQuery += ",Brands.Name as BrandEnglish , Brands.NameArabic as BrandArabic ";
                        }
                        if (item.Name == "Item Origin")
                        {
                            parameterQuery += ",Origins.Name as OriginNameEn , Origins.NameArabic as OriginNameAr ";
                        }
                        if (item.Name == "Item Size")
                        {
                            parameterQuery += ",Sizes.Name as SizeName , Sizes.NameArabic as SizeNameArabic ";
                        }
                        if (item.Name == "Item Color")
                        {
                            parameterQuery += ",Colors.Name as ColorName , Colors.NameArabic as ColorNameArabic ";
                        }
                        if (item.Name == "Item Unit")
                        {
                            parameterQuery += ",Units.Name as UnitNameEn , Units.NameArabic as UnitNameAr ";
                        }
                    }

                    var sb = " SELECT " + parameterQuery + " FROM products " +
                          " LEFT JOIN Colors ON products.ColorId = Colors.Id " +
                         " LEFT JOIN Categories ON Products.CategoryId = Categories.Id " +
                         " LEFT JOIN Brands ON Products.BrandId = Brands.Id " +
                         " LEFT JOIN Origins ON Products.OriginId = Origins.Id " +
                         " LEFT JOIN Sizes ON Products.SizeId = Sizes.Id " +
                         " LEFT JOIN Units ON Products.UnitId = Units.Id" +
                         " Where products.CompanyId='" + companyId + "' ";

                    //Branch Id Filter
                    //if (request.BranchId != null)
                    //{
                    //    sb += " and BranchItems.BranchId=Products.BrandId  ";
                    //    //query = query.Where(x => x.BranchItems.Any(y => y.BranchId == request.BranchId)).ToList();
                    //}

                    await using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                    var query = conn.Query<ProductLookUpModel>(sb).AsQueryable();


                    if (request.ProductMasterId != null && request.ProductMasterId.Count != 0)
                    {
                        query = query.Where(x => request.ProductMasterId.Contains(x.ProductMasterId));
                    }


                    if (request.CategoryId != null && request.CategoryId.Count != 0)
                    {
                        query = query.Where(x => request.CategoryId.Contains(x.CategoryId));
                    }


                    if (request.SubCategoryId != null && request.SubCategoryId.Count != 0)
                    {
                        query = query.Where(x => request.SubCategoryId.Contains(x.SubCategoryId));
                    }


                    if (request.SizeId != null && request.SizeId.Count != 0)
                    {
                        query = query.Where(x => request.SizeId.Contains(x.SizeId));
                    }


                    if (request.ColorId != null && request.ColorId.Count != 0)
                    {
                        query = query.Where(x => request.ColorId.Contains(x.ColorId));
                    }


                    if (request.OriginId != null && request.OriginId.Count != 0)
                    {
                        query = query.Where(x => request.OriginId.Contains(x.OriginId));
                    }



                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm.ToLower();
                        query = query.Where(x => x.DisplayName.ToLower().Contains(searchTerm) || x.StyleNumber.ToLower().Contains(searchTerm) || x.Code.ToLower().Contains(searchTerm));

                    }


                    var count = query.Count();
                    var asyncList = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();


                    return new PagedResult<ProductListModel>
                    {
                        Results = new ProductListModel
                        {
                            Products = asyncList
                        },
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = count / request.PageSize
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
