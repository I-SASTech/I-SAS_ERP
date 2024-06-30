using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Dapper;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Syncfusion.XlsIO;
using UnitM = MediatR.Unit;
using Unit = Focus.Domain.Entities.Unit;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.Products.ImportProduct
{
    public class ImportProductCommand : IRequest<List<ImportProductLookUp>>
    {
        // delegate int NumberChanger(int n);
        public IFormFile File { get; set; }
        public bool IsProductBarCode { get; set; }
        public List<ImportProductLookUp> ProductLookUp { get; set; }

        public class Handler : IRequestHandler<ImportProductCommand, List<ImportProductLookUp>>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            private readonly IMediator _mediator;
            private readonly IConfiguration _configuration;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly IHostingEnvironment _environment;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<ImportProductCommand> logger,
                IMediator mediator, IConfiguration configuration, IUserHttpContextProvider contextProvider, IHostingEnvironment environment)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
                _configuration = configuration;
                _contextProvider = contextProvider;
                _environment = environment;
            }

            public async Task<List<ImportProductLookUp>> Handle(ImportProductCommand request, CancellationToken cancellationToken)
            {


                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }



                    var path = _environment.WebRootPath + "\\Template\\Template Error File.xlsx";
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);


                    if (request.IsProductBarCode)
                    {
                        var products = await Context.Products.ToListAsync(cancellationToken:cancellationToken);
                        var errorProductFileDataList = new List<ImportProductLookUp>();
                        var updateBarCodes = request.ProductLookUp;
                        var productList = new List<Product>();

                        if(updateBarCodes.Count > 0)
                        {
                            foreach (var item in updateBarCodes)
                            {
                                var singleProduct = products.FirstOrDefault(x => x.Code == item.Code);
                                if(singleProduct != null)
                                {
                                    singleProduct.BarCode= item.BarCode;
                                    productList.Add(singleProduct);
                                }
                            }
                        }

                        Context.Products.UpdateRange(productList);
                        var log = Context.SyncLog();
                        var branches = await Context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                        var list = new List<SyncRecordModel>();
                        foreach (var branch in branches)
                        {
                            var syncRecords = log.Select(x => new SyncRecordModel
                            {
                                Table = x.Table,
                                ColumnId = x.ColumnId,
                                CompanyId = x.CompanyId,
                                ColumnType = x.ColumnType,
                                Push = x.Push,
                                Pull = x.Pull,
                                Action = x.Action,
                                ChangeDate = DateTime.Now,
                                PullDate = x.PullDate,
                                PushDate = x.PushDate,
                                ColumnName = x.ColumnName,
                                BranchId = branch.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer = true
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);

                        return errorProductFileDataList;
                    }
                    else
                    {
                        var companyId = _contextProvider.GetCompanyId().ToString();
                        var sqlQuery = "select Id,Code, EnglishName, ArabicName  from Products where CompanyId ='" +
                                       companyId + "';" +
                                       "select  Id,Code, Name, NameArabic  from Categories where CompanyId ='" + companyId +
                                       "';" +
                                       "select  Id,Code, Name, NameArabic from SubCategories where CompanyId ='" +
                                       companyId + "';" +
                                       "select  Id,Code, Name, NameArabic from Brands where CompanyId ='" + companyId +
                                       "';" +
                                       "select  Id,Code, Name, NameArabic from Origins where CompanyId ='" + companyId +
                                       "';" +
                                       "select  Id,Code, Name, NameArabic from Sizes where CompanyId ='" + companyId +
                                       "';" +
                                       "select  Id,Code, Name, NameArabic from Colors where CompanyId ='" + companyId +
                                       "';" +
                                       "select  Id,Code, Name, NameArabic from Units where CompanyId ='" + companyId +
                                       "';" +
                                       "select  a.Code as AccountCode, a.CostCenterId as CostCenterId, c.code as CostCenterCode from Accounts as a  inner join CostCenters as c on a.CostCenterId = c.Id  where a.CompanyId ='" +
                                       companyId + "';" +
                                       "select TaxMethod, TaxRateId from CompanyAccountSetups where CompanyId ='" +
                                       companyId + "';" +
                                       "select  Id,Code, Name, NameArabic from ProductMasters where CompanyId ='" + companyId + "';";

                        var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));


                        using (var multi = connection.QueryMultiple(sqlQuery, null))
                        {
                            DropDownCodeModel.Product = multi.Read<Product>().ToList();
                            DropDownCodeModel.Category = multi.Read<Category>().ToList();
                            DropDownCodeModel.SubCategory = multi.Read<SubCategory>().ToList();
                            DropDownCodeModel.Brand = multi.Read<Brand>().ToList();
                            DropDownCodeModel.Origin = multi.Read<Origin>().ToList();
                            DropDownCodeModel.Size = multi.Read<Size>().ToList();
                            DropDownCodeModel.Color = multi.Read<Color>().ToList();
                            DropDownCodeModel.Unit = multi.Read<Unit>().ToList();
                            DropDownCodeModel.AccountsDataForCategories = multi.Read<AccountsDataForCategory>().ToList();
                            DropDownCodeModel.CompanySetupForm = multi.Read<CompanySetupFormData>().SingleOrDefault();
                            DropDownCodeModel.ProductMaster = multi.Read<ProductMaster>().ToList();
                        }

                        // Get Last Code of all drop down from upper list
                        DropDownCodeModel.CategoryCode = DropDownCodeModel.Category.Count > 0 ? DropDownCodeModel.Category.ElementAt(DropDownCodeModel.Category.Count - 1).Code : String.Empty;
                        DropDownCodeModel.SubCategoryCode = DropDownCodeModel.SubCategory.Count > 0 ? DropDownCodeModel.SubCategory.ElementAt(DropDownCodeModel.SubCategory.Count - 1).Code : String.Empty;
                        DropDownCodeModel.BrandCode = DropDownCodeModel.Brand.Count > 0 ? DropDownCodeModel.Brand.ElementAt(DropDownCodeModel.Brand.Count - 1).Code : String.Empty;
                        DropDownCodeModel.OriginCode = DropDownCodeModel.Origin.Count > 0 ? DropDownCodeModel.Origin.ElementAt(DropDownCodeModel.Origin.Count - 1).Code : String.Empty;
                        DropDownCodeModel.SizeCode = DropDownCodeModel.Size.Count > 0 ? DropDownCodeModel.Size.ElementAt(DropDownCodeModel.Size.Count - 1).Code : String.Empty;
                        DropDownCodeModel.ColorCode = DropDownCodeModel.Color.Count > 0 ? DropDownCodeModel.Color.ElementAt(DropDownCodeModel.Color.Count - 1).Code : String.Empty;
                        DropDownCodeModel.UnitCode = DropDownCodeModel.Unit.Count > 0 ? DropDownCodeModel.Unit.ElementAt(DropDownCodeModel.Unit.Count - 1).Code : String.Empty;
                        DropDownCodeModel.ProductCode = DropDownCodeModel.Product.Count > 0 ? DropDownCodeModel.Product.ElementAt(DropDownCodeModel.Product.Count - 1).Code : String.Empty;
                        DropDownCodeModel.ProductMasterCode = DropDownCodeModel.ProductMaster.Count > 0 ? DropDownCodeModel.ProductMaster.ElementAt(DropDownCodeModel.ProductMaster.Count - 1).Code : String.Empty;

                        //Accounts for category
                        var accountCount =
                            DropDownCodeModel.AccountsDataForCategories.OrderBy(x => x.AccountCode).LastOrDefault(x => x.CostCenterCode == "111000");
                        var accountCogs = DropDownCodeModel.AccountsDataForCategories.OrderBy(x => x.AccountCode).LastOrDefault(x => x.CostCenterCode == "600001");
                        var accountSale =
                            DropDownCodeModel.AccountsDataForCategories.OrderBy(x => x.AccountCode).LastOrDefault(x => x.CostCenterCode == "420000");




                        var accountCode = 1;
                        var errorProductFileDataList = new List<ImportProductLookUp>();
                        //Accounts for category

                        foreach (var productData in request.ProductLookUp)
                        {
                            //Get employee by EmployeeNo
                            //var employee = _context.Employees.FirstOrDefault(x => x.EmployeeNo == row.Columns[1].Value);
                            string error = String.Empty;
                            if (!string.IsNullOrEmpty(productData.ItemNameEnglish) || !string.IsNullOrEmpty(productData.ItemNameArabic))
                            {
                                if (!string.IsNullOrEmpty(productData.CategoryNameEnglish) || !string.IsNullOrEmpty(productData.CategoryNameArabic))
                                {
                                    //Category Working

                                    var categoryObj = (Category)null;
                                    var categoryLangName = string.IsNullOrEmpty(productData.CategoryNameEnglish)
                                        ? "Arabic" : "English";
                                    if (categoryLangName == "English")
                                    {
                                        categoryObj = DropDownCodeModel.Category.FirstOrDefault(x =>
                                            x.Name.ToLower() == productData.CategoryNameEnglish.ToLower());

                                    }
                                    else
                                    {
                                        categoryObj = DropDownCodeModel.Category.FirstOrDefault(x =>
                                            x.NameArabic == productData.CategoryNameArabic);
                                    }

                                    if (categoryObj == null)
                                    {

                                        if (accountCount != null && accountCogs != null && accountSale != null)
                                        {
                                            var accounts = new List<Account>();
                                            var inventoryAccount = new Account
                                            {
                                                Name = categoryLangName == "English" ? productData.CategoryNameEnglish + " Inventory" : productData.CategoryNameArabic + " Inventory",
                                                Description = categoryLangName == "English" ? productData.CategoryNameEnglish + " " + DropDownCodeModel.CategoryCode : productData.CategoryNameArabic + " " + DropDownCodeModel.CategoryCode,
                                                Code = (int.Parse(accountCount.AccountCode) + accountCode).ToString(),
                                                CostCenterId = accountCount.CostCenterId,
                                                IsActive = true
                                            };

                                            var cogsAccount = new Account
                                            {
                                                Name = categoryLangName == "English" ? productData.CategoryNameEnglish + " COGS" : productData.CategoryNameArabic + " COGS",
                                                Description = categoryLangName == "English" ? productData.CategoryNameEnglish + " " + DropDownCodeModel.CategoryCode : productData.CategoryNameArabic + " " + DropDownCodeModel.CategoryCode,
                                                Code = (int.Parse(accountCogs.AccountCode) + accountCode).ToString(),
                                                CostCenterId = accountCogs.CostCenterId,
                                                IsActive = true
                                            };

                                            var saleAccount = new Account
                                            {
                                                Name = categoryLangName == "English" ? productData.CategoryNameEnglish + " Sale Account" : productData.CategoryNameArabic + " Sale Account",
                                                Description = categoryLangName == "English" ? productData.CategoryNameEnglish + " " + DropDownCodeModel.CategoryCode : productData.CategoryNameArabic + " " + DropDownCodeModel.CategoryCode,
                                                Code = (int.Parse(accountSale.AccountCode) + accountCode).ToString(),
                                                CostCenterId = accountSale.CostCenterId,
                                                IsActive = true
                                            };

                                            accounts.Add(inventoryAccount);
                                            accounts.Add(cogsAccount);
                                            accounts.Add(saleAccount);
                                            await Context.Accounts.AddRangeAsync(accounts, cancellationToken);

                                            DropDownCodeModel.CategoryCode = string.IsNullOrEmpty(DropDownCodeModel.CategoryCode) ? GenerateCodeFirstTime("CA") : GenerateNewCode(DropDownCodeModel.CategoryCode, "CA");


                                            var category = new Category
                                            {
                                                Name = productData.CategoryNameEnglish,
                                                NameArabic = productData.CategoryNameArabic,
                                                Code = DropDownCodeModel.CategoryCode,
                                                SaleAccountId = saleAccount.Id,
                                                COGSAccountId = cogsAccount.Id,
                                                InventoryAccountId = inventoryAccount.Id,
                                                isActive = true
                                            };


                                            await Context.Categories.AddAsync(category, cancellationToken);
                                            DropDownCodeModel.CategoryId = category.Id;
                                            DropDownCodeModel.Category.Add(category);
                                            accountCode++;
                                        }

                                    }
                                    else
                                    {
                                        DropDownCodeModel.CategoryId = categoryObj.Id;
                                    }



                                    // SubCategory working

                                    if (!string.IsNullOrEmpty(productData.SubCategoryNameEnglish) || !string.IsNullOrEmpty(productData.SubCategoryNameArabic))
                                    {

                                        var subCategoryObj = (SubCategory)null;
                                        var subCategoryLangName = string.IsNullOrEmpty(productData.SubCategoryNameEnglish)
                                            ? "Arabic" : "English";
                                        if (subCategoryLangName == "English")
                                        {
                                            subCategoryObj = DropDownCodeModel.SubCategory.FirstOrDefault(x =>
                                                x.Name.ToLower() == productData.SubCategoryNameEnglish.ToLower());

                                        }
                                        else
                                        {
                                            subCategoryObj = DropDownCodeModel.SubCategory.FirstOrDefault(x =>
                                                x.NameArabic == productData.SubCategoryNameArabic);
                                        }


                                        if (subCategoryObj == null)
                                        {

                                            DropDownCodeModel.SubCategoryCode = string.IsNullOrEmpty(DropDownCodeModel.SubCategoryCode) ? GenerateCodeFirstTime("SC") : GenerateNewCode(DropDownCodeModel.SubCategoryCode, "SC");

                                            var subCategory = new SubCategory
                                            {
                                                Name = productData.SubCategoryNameEnglish,
                                                NameArabic = productData.SubCategoryNameArabic,
                                                Code = DropDownCodeModel.SubCategoryCode,
                                                CategoryId = DropDownCodeModel.CategoryId,
                                                isActive = true
                                            };


                                            await Context.SubCategories.AddAsync(subCategory, cancellationToken);

                                            DropDownCodeModel.SubCategoryId = subCategory.Id;
                                            DropDownCodeModel.SubCategory.Add(subCategory);
                                        }
                                        else
                                        {
                                            DropDownCodeModel.SubCategoryId = subCategoryObj.Id;
                                        }
                                    }
                                    //Product Master
                                    if (!string.IsNullOrEmpty(productData.ProductNameEnglish) || !string.IsNullOrEmpty(productData.ProductNameArabic))
                                    {

                                        var brandObj = (ProductMaster)null;
                                        var brandLangName = string.IsNullOrEmpty(productData.ProductNameEnglish)
                                            ? "Arabic" : "English";
                                        if (brandLangName == "English")
                                        {
                                            brandObj = DropDownCodeModel.ProductMaster.FirstOrDefault(x =>
                                                x.Name.ToLower() == productData.ProductNameEnglish.ToLower());

                                        }
                                        else
                                        {
                                            brandObj = DropDownCodeModel.ProductMaster.FirstOrDefault(x =>
                                                x.NameArabic == productData.ProductNameArabic);
                                        }


                                        if (brandObj == null)
                                        {

                                            DropDownCodeModel.ProductMasterCode = string.IsNullOrEmpty(DropDownCodeModel.ProductMasterCode) ? GenerateCodeFirstTime("PR") : GenerateNewCode(DropDownCodeModel.ProductMasterCode, "PR");

                                            var peMaster = new ProductMaster()
                                            {
                                                Name = productData.ProductNameEnglish,
                                                NameArabic = productData.ProductNameArabic,
                                                Code = DropDownCodeModel.ProductMasterCode,
                                                isActive = true
                                            };

                                            await Context.ProductMasters.AddAsync(peMaster, cancellationToken);

                                            DropDownCodeModel.ProductMasterId = peMaster.Id;
                                            DropDownCodeModel.ProductMaster.Add(peMaster);
                                        }
                                        else
                                        {
                                            DropDownCodeModel.ProductMasterId = brandObj.Id;
                                        }
                                    }

                                    // Brand Working

                                    if (!string.IsNullOrEmpty(productData.BrandNameEnglish) || !string.IsNullOrEmpty(productData.BrandNameArabic))
                                    {

                                        var brandObj = (Brand)null;
                                        var brandLangName = string.IsNullOrEmpty(productData.BrandNameEnglish)
                                            ? "Arabic" : "English";
                                        if (brandLangName == "English")
                                        {
                                            brandObj = DropDownCodeModel.Brand.FirstOrDefault(x =>
                                                x.Name.ToLower() == productData.BrandNameEnglish.ToLower());

                                        }
                                        else
                                        {
                                            brandObj = DropDownCodeModel.Brand.FirstOrDefault(x =>
                                                x.NameArabic == productData.BrandNameArabic);
                                        }


                                        if (brandObj == null)
                                        {

                                            DropDownCodeModel.BrandCode = string.IsNullOrEmpty(DropDownCodeModel.BrandCode) ? GenerateCodeFirstTime("BR") : GenerateNewCode(DropDownCodeModel.BrandCode, "BR");

                                            var brand = new Brand
                                            {
                                                Name = productData.BrandNameEnglish,
                                                NameArabic = productData.BrandNameArabic,
                                                Code = DropDownCodeModel.BrandCode,
                                                isActive = true
                                            };

                                            await Context.Brands.AddAsync(brand, cancellationToken);

                                            DropDownCodeModel.BrandId = brand.Id;
                                            DropDownCodeModel.Brand.Add(brand);
                                        }
                                        else
                                        {
                                            DropDownCodeModel.BrandId = brandObj.Id;
                                        }
                                    }

                                    //Origin working
                                    if (!string.IsNullOrEmpty(productData.OriginNameEnglish) || !string.IsNullOrEmpty(productData.OriginNameArabic))
                                    {
                                        var originObj = (Origin)null;
                                        var originLangName = string.IsNullOrEmpty(productData.OriginNameEnglish)
                                            ? "Arabic" : "English";
                                        if (originLangName == "English")
                                        {
                                            originObj = DropDownCodeModel.Origin.FirstOrDefault(x =>
                                                x.Name.ToLower() == productData.OriginNameEnglish.ToLower());

                                        }
                                        else
                                        {
                                            originObj = DropDownCodeModel.Origin.FirstOrDefault(x =>
                                                x.NameArabic == productData.OriginNameArabic);
                                        }

                                        if (originObj == null)
                                        {

                                            DropDownCodeModel.OriginCode =
                                                string.IsNullOrEmpty(DropDownCodeModel.OriginCode)
                                                    ? GenerateCodeFirstTime("OR")
                                                    : GenerateNewCode(DropDownCodeModel.OriginCode, "OR");

                                            var origin = new Origin
                                            {
                                                Name = productData.OriginNameEnglish,
                                                NameArabic = productData.OriginNameArabic,
                                                Code = DropDownCodeModel.OriginCode,
                                                isActive = true
                                            };


                                            await Context.Origins.AddAsync(origin, cancellationToken);

                                            DropDownCodeModel.OriginId = origin.Id;
                                            DropDownCodeModel.Origin.Add(origin);
                                        }
                                        else
                                        {
                                            DropDownCodeModel.OriginId = originObj.Id;
                                        }

                                    }

                                    //Size working

                                    if (!string.IsNullOrEmpty(productData.SizeNameEnglish) || !string.IsNullOrEmpty(productData.SizeNameArabic))
                                    {
                                        var sizeObj = (Size)null;
                                        var sizeLangName = string.IsNullOrEmpty(productData.SizeNameEnglish)
                                            ? "Arabic" : "English";
                                        if (sizeLangName == "English")
                                        {
                                            sizeObj = DropDownCodeModel.Size.FirstOrDefault(x =>
                                                x.Name.ToLower() == productData.SizeNameEnglish.ToLower());

                                        }
                                        else
                                        {
                                            sizeObj = DropDownCodeModel.Size.FirstOrDefault(x =>
                                                x.NameArabic == productData.SizeNameArabic);
                                        }


                                        if (sizeObj == null)
                                        {

                                            DropDownCodeModel.SizeCode =
                                                string.IsNullOrEmpty(DropDownCodeModel.SizeCode)
                                                    ? GenerateCodeFirstTime("SZ")
                                                    : GenerateNewCode(DropDownCodeModel.SizeCode, "SZ");

                                            var size = new Size
                                            {
                                                Name = productData.SizeNameEnglish,
                                                NameArabic = productData.SizeNameArabic,
                                                Code = DropDownCodeModel.SizeCode,
                                                isActive = true
                                            };


                                            await Context.Sizes.AddAsync(size, cancellationToken);

                                            DropDownCodeModel.SizeId = size.Id;
                                            DropDownCodeModel.Size.Add(size);
                                        }
                                        else
                                        {
                                            DropDownCodeModel.SizeId = sizeObj.Id;
                                        }

                                    }

                                    //Color working

                                    if (!string.IsNullOrEmpty(productData.ColorNameEnglish) || !string.IsNullOrEmpty(productData.ColorNameArabic))
                                    {
                                        var colorObj = (Color)null;
                                        var colorLangName = string.IsNullOrEmpty(productData.ColorNameEnglish)
                                            ? "Arabic" : "English";
                                        if (colorLangName == "English")
                                        {
                                            colorObj = DropDownCodeModel.Color.FirstOrDefault(x =>
                                                x.Name.ToLower() == productData.ColorNameEnglish.ToLower());

                                        }
                                        else
                                        {
                                            colorObj = DropDownCodeModel.Color.FirstOrDefault(x =>
                                                x.NameArabic == productData.ColorNameArabic);
                                        }

                                        if (colorObj == null)
                                        {

                                            DropDownCodeModel.ColorCode =
                                                string.IsNullOrEmpty(DropDownCodeModel.ColorCode)
                                                    ? GenerateCodeFirstTime("CR")
                                                    : GenerateNewCode(DropDownCodeModel.ColorCode, "CR");

                                            var color = new Color
                                            {
                                                Name = productData.ColorNameEnglish,
                                                NameArabic = productData.ColorNameArabic,
                                                Code = DropDownCodeModel.ColorCode,
                                                isActive = true
                                            };


                                            await Context.Colors.AddAsync(color, cancellationToken);

                                            DropDownCodeModel.ColorId = color.Id;
                                            DropDownCodeModel.Color.Add(color);
                                        }
                                        else
                                        {
                                            DropDownCodeModel.ColorId = colorObj.Id;
                                        }
                                    }

                                    //Unit working
                                    if (!string.IsNullOrEmpty(productData.UnitNameEnglish) || !string.IsNullOrEmpty(productData.UnitNameArabic))
                                    {

                                        var unitObj = (Unit)null;
                                        var unitLangName = string.IsNullOrEmpty(productData.UnitNameEnglish)
                                            ? "Arabic" : "English";
                                        if (unitLangName == "English")
                                        {
                                            unitObj = DropDownCodeModel.Unit.FirstOrDefault(x =>
                                                x.Name.ToLower() == productData.UnitNameEnglish.ToLower());

                                        }
                                        else
                                        {
                                            unitObj = DropDownCodeModel.Unit.FirstOrDefault(x =>
                                                x.NameArabic == productData.UnitNameArabic);
                                        }


                                        if (unitObj == null)
                                        {

                                            DropDownCodeModel.UnitCode =
                                                string.IsNullOrEmpty(DropDownCodeModel.UnitCode)
                                                    ? GenerateCodeFirstTime("UT")
                                                    : GenerateNewCode(DropDownCodeModel.UnitCode, "UT");

                                            var unit = new Unit
                                            {
                                                Name = productData.UnitNameEnglish,
                                                NameArabic = productData.UnitNameArabic,
                                                Code = DropDownCodeModel.UnitCode,
                                                isActive = true
                                            };


                                            await Context.Units.AddAsync(unit, cancellationToken);

                                            DropDownCodeModel.UnitId = unit.Id;
                                            DropDownCodeModel.Unit.Add(unit);
                                        }
                                        else
                                        {
                                            DropDownCodeModel.UnitId = unitObj.Id;
                                        }
                                    }

                                    //product working


                                    DropDownCodeModel.ProductCode =
                                        string.IsNullOrEmpty(DropDownCodeModel.ProductCode)
                                            ? GenerateCodeFirstTime("PR")
                                            : GenerateNewCode(DropDownCodeModel.ProductCode, "PR");
                                    var product = new Product()
                                    {
                                        Code = DropDownCodeModel.ProductCode,
                                        EnglishName = productData.ItemNameEnglish,
                                        ArabicName = productData.ItemNameArabic,
                                        SalePrice = string.IsNullOrEmpty(productData.SalePrice) || string.IsNullOrWhiteSpace(productData.SalePrice) ? 0M : decimal.TryParse(productData.SalePrice, out decimal numericValue) ? numericValue : 0M,
                                        Length = productData.PackSizeLength,
                                        Width = productData.PackSizeWidth,
                                        StockLevel = productData.MinStockLevel,
                                        Description = productData.Description,
                                        Shelf = productData.Shelf,
                                        Assortment = productData.Assortment,
                                        StyleNumber = productData.Style,
                                        SaleReturnDays = productData.SaleReturnDay,
                                        BarCode = productData.BarCode,
                                        UnitPerPack = string.IsNullOrEmpty(productData.PackSizeWidth) ? 0 : decimal.Parse(productData.PackSizeWidth),
                                        CategoryId = DropDownCodeModel.CategoryId,
                                        ProductMasterId = DropDownCodeModel.ProductMasterId == Guid.Empty ? (Guid?)null : DropDownCodeModel.ProductMasterId,
                                        SubCategoryId = DropDownCodeModel.SubCategoryId == Guid.Empty ? (Guid?)null : DropDownCodeModel.SubCategoryId,
                                        BrandId = DropDownCodeModel.BrandId == Guid.Empty ? (Guid?)null : DropDownCodeModel.BrandId,
                                        OriginId = DropDownCodeModel.OriginId == Guid.Empty ? (Guid?)null : DropDownCodeModel.OriginId,
                                        SizeId = DropDownCodeModel.SizeId == Guid.Empty ? (Guid?)null : DropDownCodeModel.SizeId,
                                        ColorId = DropDownCodeModel.ColorId == Guid.Empty ? (Guid?)null : DropDownCodeModel.ColorId,
                                        UnitId = DropDownCodeModel.UnitId == Guid.Empty ? (Guid?)null : DropDownCodeModel.UnitId,
                                        TaxMethod = DropDownCodeModel.CompanySetupForm.TaxMethod,
                                        TaxRateId = DropDownCodeModel.CompanySetupForm.TaxRateId,
                                        Image = productData.ImagePath,
                                        IsRaw = productData.RawMaterial == "TRUE" ? true : false,
                                        BarCodeType = (productData.BarCode != "" || productData.BarCode != null) ? productData.BarCode : "None",
                                        IsActive = true
                                    };
                                    await Context.Products.AddAsync(product, cancellationToken);

                                    var log = Context.SyncLog();
                                    var branches = await Context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                                    var list = new List<SyncRecordModel>();
                                    foreach (var branch in branches)
                                    {
                                        var syncRecords = log.Select(x => new SyncRecordModel
                                        {
                                            Table = x.Table,
                                            ColumnId = x.ColumnId,
                                            CompanyId = x.CompanyId,
                                            ColumnType = x.ColumnType,
                                            Push = x.Push,
                                            Pull = x.Pull,
                                            Action = x.Action,
                                            ChangeDate = DateTime.Now,
                                            PullDate = x.PullDate,
                                            PushDate = x.PushDate,
                                            ColumnName = x.ColumnName,
                                            BranchId = branch.Id,
                                            Code = _code,
                                        }).ToList();

                                        list.AddRange(syncRecords);
                                    }

                                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                                    {
                                        SyncRecordModels = list,
                                        IsServer = true
                                    }, cancellationToken);

                                    await Context.SaveChangesAsync(cancellationToken);
                                }
                                else
                                {
                                    productData.ErrorDescription = "Product category not exist";
                                    errorProductFileDataList.Add(productData);

                                }
                            }
                            else
                            {
                                productData.ErrorDescription = "Product name not exist";
                                errorProductFileDataList.Add(productData);
                            }

                        }
                        return errorProductFileDataList;
                    }
                }
                catch (Exception e)
                {
                    throw new ApplicationException("Something Went wrong. Error is: " + e.Message.ToString());

                }



            }


        }



        public static string GenerateCodeFirstTime(string headerName)
        {
            return headerName + "-00001";
        }

        public static string GenerateNewCode(string soNumber, string headerName)
        {
            string fetchNo = soNumber.Substring(3);
            Int32 autoNo = Convert.ToInt32((fetchNo));
            var format = headerName + "-00000";
            autoNo++;
            var newCode = autoNo.ToString(format);
            return newCode;
        }



    }

    public static class DropDownCodeModel
    {
        public static ICollection<Product> Product { get; set; }
        public static ICollection<Category> Category { get; set; }
        public static ICollection<SubCategory> SubCategory { get; set; }
        public static ICollection<Brand> Brand { get; set; }
        public static ICollection<Origin> Origin { get; set; }
        public static ICollection<Size> Size { get; set; }
        public static ICollection<Color> Color { get; set; }
        public static ICollection<Unit> Unit { get; set; }
        public static ICollection<ProductMaster> ProductMaster { get; set; }
        public static ICollection<AccountsDataForCategory> AccountsDataForCategories { get; set; }
        public static CompanySetupFormData CompanySetupForm { get; set; }
        public static string ProductCode { get; set; }
        public static string CategoryCode { get; set; }
        public static string SubCategoryCode { get; set; }
        public static string BrandCode { get; set; }
        public static string OriginCode { get; set; }
        public static string SizeCode { get; set; }
        public static string ProductMasterCode { get; set; }
        public static string ColorCode { get; set; }
        public static string UnitCode { get; set; }
        public static Guid CategoryId { get; set; }
        public static Guid SubCategoryId { get; set; }
        public static Guid BrandId { get; set; }
        public static Guid OriginId { get; set; }
        public static Guid SizeId { get; set; }
        public static Guid ColorId { get; set; }
        public static Guid UnitId { get; set; }
        public static Guid ProductMasterId { get; set; }
    }


    public class AccountsDataForCategory
    {
        public string AccountCode { get; set; }
        public string CostCenterCode { get; set; }
        public Guid CostCenterId { get; set; }
    }

    public class CompanySetupFormData
    {
        public string TaxMethod { get; set; }
        public Guid TaxRateId { get; set; }
    }
}
