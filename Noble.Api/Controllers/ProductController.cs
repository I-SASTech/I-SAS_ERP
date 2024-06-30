using Focus.Business.Brands.Commands.AddUpdateBrand;
using Focus.Business.Brands.Commands.DeleteBrand;
using Focus.Business.Brands.Queries;
using Focus.Business.Brands.Queries.GetBrandDetails;
using Focus.Business.Brands.Queries.GetBrandList;
using Focus.Business.BundleCategoriesItems.Commands.AddUpdateBundleCategory;
using Focus.Business.BundleCategoriesItems.Queries.GetBundleCategoryDetails;
using Focus.Business.BundleCategoriesItems.Queries.GetBundleCategoryList;
using Focus.Business.Categories.Command.AddUpdateCategory;
using Focus.Business.Categories.Command.DeleteCategory;
using Focus.Business.Categories.Queries;
using Focus.Business.Categories.Queries.GetCategoryDetails;
using Focus.Business.Categories.Queries.GetCategoryList;
using Focus.Business.Colors.Queries;
using Focus.Business.Colors.Queries.GetColorDetails;
using Focus.Business.Colors.Queries.GetColorList;
using Focus.Business.DayStarts.Commands.AddUpdateDayStarts;
using Focus.Business.DayStarts.Queries.GetDayStartList;
using Focus.Business.Currencies.Commands.AddUpdateCurrency;
using Focus.Business.Currencies.Commands.DeleteCurrency;
using Focus.Business.Currencies.Queries.GetCurrencyDetails;
using Focus.Business.Currencies.Queries.GetCurrencyList;
using Focus.Business.Origins.Queries;
using Focus.Business.Origins.Queries.GetOriginDetails;
using Focus.Business.Origins.Queries.GetOriginList;
using Focus.Business.PaymentOptions.Commands.AddUpdatePaymentOptions;
using Focus.Business.PaymentOptions.Commands.DeletePaymentOptions;
using Focus.Business.PaymentOptions.Queries.GetPaymentOptionsDetails;
using Focus.Business.PaymentOptions.Queries.GetPaymentOptionsList;
using Focus.Business.Products.Commands.AddUpdateProduct;
using Focus.Business.Products.Commands.DeleteProduct;
using Focus.Business.Products.Queries;
using Focus.Business.Products.Queries.GetProductDetails;
using Focus.Business.Products.Queries.GetProductList;
using Focus.Business.PromotionOffersFolder.Commands.AddUpdatePromotionOffer;
using Focus.Business.PromotionOffersFolder.Queries.GetPromotionOffersDetails;
using Focus.Business.PromotionOffersFolder.Queries.GetPromotionOffersList;
using Focus.Business.SizeCQ.Queries;
using Focus.Business.Sizes.Queries.GetSizeDetails;
using Focus.Business.Sizes.Queries.GetSizeList;
using Focus.Business.SubCategories.Commands.AddUpdateSubCategory;
using Focus.Business.SubCategories.Commands.DeleteSubCategory;
using Focus.Business.SubCategories.Queries.GetSubCategoryDetails;
using Focus.Business.SubCategories.Queries.GetSubCategoryList;
using Focus.Business.SubCategoryCQ.Queries;
using Focus.Business.TaxRates.Queries;
using Focus.Business.TaxRates.Queries.GetTaxRateDetails;
using Focus.Business.TaxRates.Queries.GetTaxRateList;
using Focus.Business.Units.Queries;
using Focus.Business.Units.Queries.GetUnitDetails;
using Focus.Business.Units.Queries.GetUnitList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Noble.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Focus.Business.Categories.Imports;
using Focus.Business.StockAdjustments.Queries.GetStockAdjustmentNumber;
using Focus.Business.Inventories.Queries.GetLatestInventory;
using Focus.Business.StockAdjustments.Queries.GetStockAdjustmentList;
using Focus.Domain.Entities;
using Focus.Business.StockAdjustments.Commands.AddUpdateStockAdjustment;
using Focus.Business.StockAdjustments.Commands.DeleteStockAdjustment;
using Focus.Business.StockAdjustments.Queries.GetStockAdjustmentDetails;
using Focus.Domain.Enum;
using Focus.Business.DayStarts.Queries.IsDayStart;
using Focus.Business.DayStarts.Queries.GetCounterInformation;
using Focus.Business.Interface;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Hosting;
using Focus.Business.Categories.Export;
using Focus.Business.Dashboard.Queries.GetWareHouseInventory;
using Focus.Business.DayStarts.Commands.AddUpdateDayEnd;
using Focus.Business.DayStarts.Queries.GetCounterDetails;
using Focus.Business.DayStarts.Queries.GetOpeninigBalance;
using Focus.Business.DayStarts.Queries.GetTerminalOfCurrentUser;
using Focus.Business.DayStarts.Queries.SupervisorLogin;
using Focus.Business.DenominationSetups.Commands.AddUpdateDenominationSetup;
using Focus.Business.DenominationSetups.Queries.GetDenominationSetupDetails;
using Focus.Business.DenominationSetups.Queries.GetDenominationSetupList;
using Focus.Business.Extensions;
using Focus.Business.InventoryBlinds;
using Focus.Business.InventoryBlinds.Commands.AddUpdateInventoryBlind;
using Focus.Business.InventoryBlinds.Queries.GetInventoryBlindDetail;
using Focus.Business.InventoryBlinds.Queries.GetInventoryBlindList;
using Focus.Business.InventoryBlinds.ViewModel;
using Focus.Business.Products.Queries.ViewProduct;
using Focus.Business.Products.ImportProduct;
using Focus.Business.StockAdjustments.ImportStock;
using Focus.Business.DayStarts.Commands.TransferDay;
using Focus.Business.ProductMasters.Commands.AddUpdateProductMaster;
using Focus.Business.ProductMasters.Commands.DeleteProductMaster;
using Focus.Business.ProductMasters.Queries;
using Focus.Business.ProductMasters.Queries.GetProductMasterDetails;
using Focus.Business.ProductMasters.Queries.GetProductMasterList;
using System.Drawing.Printing;
using Focus.Domain.Interface;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using Focus.Business.Colors.Commands.AddUpdateColor;
using Focus.Business.Colors.Commands.DeleteColor;
using Focus.Business.DayStarts.Commands.ReceivingAmount;
using Focus.Business.DayStarts.Commands.WholeSaleDayStartCommand;
using Focus.Business.DayStarts.Queries.DayReport;
using Focus.Business.DayStarts.Queries.DayStartReportList;
using Microsoft.Extensions.Configuration;
using Focus.Business.DayStarts.Queries.GetTotalInvoicesQuery;
using Focus.Business.DayStarts.WholeSaleQueries;
using Focus.Business.PriceLabelings.Commands.AddUpdatePriceLabeling;
using Focus.Business.PriceLabelings.Queries;
using Focus.Business.PriceLabelings.Queries.GetPriceLabelingDetails;
using Focus.Business.PriceLabelings.Commands.DeletePriceLabeling;
using Focus.Business.PriceLabelings.Queries.GetPriceLabelingList;
using Focus.Business.PriceRecords;
using Focus.Business.PriceRecords.Commands.AddPriceRecord;
using Focus.Business.PriceRecords.Queries.GetPriceRecordList;
using Focus.Business.PriceRecords.Queries.GetRecipeNoDetails;
using Focus.Business.WarrantyTypes.Commands;
using Focus.Business.WarrantyTypes.Queries;
using Focus.Business.InventoryBlinds.ExportInventoryCount;
using Focus.Business.Origins.Commands.AddUpdateOrigin;
using Focus.Business.Origins.Commands.DeleteUnit;
using Focus.Business.ReparingOrders;
using Focus.Business.Products.Commands.CreateProductsAccount;
using Focus.Business.ProductGroups.Queries;
using Focus.Business.ProductGroups.Commands;
using Focus.Business.ProductGroups.Model;
using Focus.Business.PriceRecordChanges.Queries;
using Focus.Business.PriceRecordChanges.Commands;
using Focus.Business.PriceRecordChanges.Model;
using Focus.Business.Sizes.Commands.AddUpdateSize;
using Focus.Business.Sizes.Commands.DeleteSize;
using Focus.Business.TaxRates.Commands.AddUpdateTaxRate;
using Focus.Business.TaxRates.Commands.DeleteTaxRate;
using Focus.Business.Units.Commands.AddUpdateUnit;
using Focus.Business.Units.Commands.DeleteUnit;
using Microsoft.EntityFrameworkCore;
using Focus.Business.ItemViewsSetups.Commands;
using Focus.Business.ItemViewsSetups.Models;
using Focus.Business.ItemViewsSetups.Queries;
using OfficeOpenXml;
using Focus.Business.ItemsBarCode.Queries;
using Focus.Business.ItemsBarCode.Commands;
using Focus.Business.ListOrdringSetup.Commands;
using Focus.Business.ListOrdringSetup.Models;
using Focus.Business.ListOrdringSetup.Queries;
using Focus.Business.BarCodeSetup.Commands;
using Focus.Business.BarCodeSetup.Models;
using Focus.Business.BarCodeSetup.Queries;
using Focus.Business.BomSetup.Queries;
using Focus.Business.BomSetup.Models;
using Focus.Business.BomSetup.Commands;
using Focus.Business.EmailConfigurationSetup.Model;
using Focus.Business.EmailConfigurationSetup.Commands;
using Focus.Business.EmailConfigurationSetup.Queries;

namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController

    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public readonly IApplicationDbContext Context;
        public IUserHttpContextProvider ContextProvider;
        public IConfiguration Configuration { get; }

        public ProductController(IHostingEnvironment hostingEnvironment, IApplicationDbContext context,
            IUserHttpContextProvider contextProvider, IConfiguration configuration)
        {
            Context = context;
            _hostingEnvironment = hostingEnvironment;
            ContextProvider = contextProvider;
            Configuration = configuration;

        }


        [Route("api/Product/CreateProductAccount")]
        [HttpGet("CreateProductAccount")]
        public async Task<IActionResult> CreateProductAccount(bool productAccount, string sum)
        {
            var product = await Mediator.Send(new CreateProductAccountCommand
            {
                ProductAccount = productAccount,
                Sum = sum,
            });

            return Ok(product);
        }

        #region Email Configuration

        [Route("api/Product/SaveEmailConfiguration")]
        [HttpPost("SaveEmailConfiguration")]
        public async Task<IActionResult> SaveEmailConfiguration([FromBody] EmailConfigurationLookupModel emailConfiguration)
        {
           
            var color = await Mediator.Send(new EmailConfigurationAddUpdateCommand
            {
                EmailConfig = emailConfiguration
            });
            return Ok(color);
        }
        [Route("api/Product/EmailConfigurationList")]
        [HttpGet("EmailConfigurationList")]
        public async Task<IActionResult> EmailConfigurationList(string searchTerm, int? pageNumber, bool isDropdown)
        {
            var color = await Mediator.Send(new GetEmailConfigurationList
            {
                IsDropdown = isDropdown,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(color);
        }
        
        [Route("api/Product/GetEmailConfigurationDetails")]
        [HttpGet("GetEmailConfigurationDetails")]
        public async Task<IActionResult> GetEmailConfigurationDetails(Guid id)
        {
            var color = await Mediator.Send(new GetEmailConfigurationDeatilsQuery
            {
                Id = id
            });
            return Ok(color);
        }
        
        [Route("api/Product/UpdateDefaultEmailConfiguration")]
        [HttpGet("UpdateDefaultEmailConfiguration")]
        public async Task<IActionResult> UpdateDefaultEmailConfiguration(Guid id)
        {
            var color = await Mediator.Send(new UpdateDefaultEmailConfiguration
            {
                Id = id
            });
            return Ok(color);
        }
       
        #endregion

        #region BOM

        [Route("api/Product/GetBomAutoCode")]
        [HttpGet("GetBomAutoCode")]
        public async Task<IActionResult> GetBomAutoCode()
        {
            var autoNo = await Mediator.Send(new GetBomAutoCodeQuery());
            return Ok(autoNo);
        }
        [Route("api/Product/GetSaleOrderItems")]
        [HttpGet("GetSaleOrderItems")]
        public async Task<IActionResult> GetSaleOrderItems(Guid id)
        {
            var data = await Mediator.Send(new GetSaleOrderItemsQuery()
            {
                Id = id
            });
            return Ok(data);
        }
        
        [Route("api/Product/GetBomDetails")]
        [HttpGet("GetBomDetails")]
        public async Task<IActionResult> GetBomDetails(Guid id)
        {
            var data = await Mediator.Send(new GetBomDetailsQuery()
            {
                Id = id
            });
            return Ok(data);
        } 
        [Route("api/Product/GetSaleOrderItemsForGoodsReceived")]
        [HttpGet("GetSaleOrderItemsForGoodsReceived")]
        public async Task<IActionResult> GetSaleOrderItemsForGoodsReceived(Guid id, Guid saleOrderId)
        {
            var data = await Mediator.Send(new GetSaleOrderItemsForGoodsReceivedQuery()
            {
                Id = id,
                SaleOrderId = saleOrderId
            });
            return Ok(data);
        }

        [Route("api/Product/SaveBomInformation")]
        [HttpPost("SaveBomInformation")]
        public async Task<IActionResult> SaveBomInformation([FromBody] BomsLookupModel bom, Guid wareHouseId, bool isPos)
        {
            var message = await Mediator.Send(new BomAddUpdateCommand()
            {
                BomsInfo = bom,
                WareHouseId = wareHouseId,
                IsPos = isPos
            });
            return Ok(message);
        }

        [Route("api/Product/GetBomList")]
        [HttpGet("GetBomList")]
        public async Task<IActionResult> GetBomList(string searchTerm, int? pageNumber, bool isDropdown, string approvalStatus)
        {
            var brand = await Mediator.Send(new GetBomListQuery
            {
                IsDropdown = isDropdown,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
                ApprovalStatus = approvalStatus
            });
            return Ok(brand);
        }

        #endregion

        #region Item BarCode Setup

        [Route("api/Product/SaveBarCodeSetup")]
        [HttpPost("SaveBarCodeSetup")]
        public async Task<IActionResult> SaveBarCodeSetup([FromBody] BarCodeSetupLookupModel barCodeSetup, bool isForAllProduct)
        {
            var message = await Mediator.Send(new BarCodeSetupAddUpdateCommand()
            {
                BarCodeSetup = barCodeSetup,
                DocumentName = "BarCode",
                IsForAllProduct = isForAllProduct
            });
            return Ok(message);
        }

        [Route("api/Product/GetBarCodeSetupList")]
        [HttpGet("GetBarCodeSetupList")]
        public async Task<IActionResult> GetBarCodeSetupList()
        {
            var message = await Mediator.Send(new GetBarCodeSetupQuery());
            return Ok(message);
        }

        #endregion 
        #region Item View Setup

        [Route("api/Product/SaveListOrderingSetup")]
        [HttpPost("SaveListOrderingSetup")]
        public async Task<IActionResult> SaveListOrderingSetup([FromBody] ListOrderSetupModel listOrderSetups, string documentName)
        {
            var message = await Mediator.Send(new AddListOrderingSetupAddUpdateCommand()
            {
                ListOrderSetups = listOrderSetups,
                DocumentName = documentName
            });
            return Ok(message);
        }

        [Route("api/Product/GetItemOrderList")]
        [HttpGet("GetItemOrderList")]
        public async Task<IActionResult> GetItemOrderList()
        {
            var message = await Mediator.Send(new GetItemOrderListQuery());
            return Ok(message);
        }

        #endregion

        #region Items BarCode

        [Route("api/Product/GetProductListExcelForBarCodes")]
        [HttpPost("GetProductListExcelForBarCodes")]
        public async Task<IActionResult> GetProductListExcelForBarCodes(Guid? warehouseId, Guid? produtMasterId, Guid? categoryId, Guid? subCategoryId, Guid? colorId, Guid? sizeId, string barCodeType, int? pageNumber, int pageSize)
        {

            var categoryListModel = await Mediator.Send(new GetItemsForBarCodeChangeQuery
            {
                PageNumber = pageNumber ?? 1,
                WareHouseId = warehouseId,
                CategoryId = categoryId,
                SubCategoryId = subCategoryId,
                ProdutMasterId = produtMasterId,
                PageSize = Convert.ToInt32(pageNumber),
                ColorId = colorId,
                SizeId = sizeId,
                BarCodeType = barCodeType
            });

            var list = categoryListModel.Results.Products;

            List<string> columnHeaders = new List<string>();

            columnHeaders.Add("ProductNameEnglish");
            columnHeaders.Add("ProductNameArabic");
            columnHeaders.Add("ItemNameEnglish");
            columnHeaders.Add("ItemNameArabic");
            columnHeaders.Add("CategoryNameEnglish");
            columnHeaders.Add("CategoryNameArabic");
            columnHeaders.Add("SubCategoryNameEnglish");
            columnHeaders.Add("SubCategoryNameArabic");
            columnHeaders.Add("BrandNameEnglish");
            columnHeaders.Add("BrandNameArabic");
            columnHeaders.Add("OriginNameEnglish");
            columnHeaders.Add("OriginNameArabic");
            columnHeaders.Add("SizeNameEnglish");
            columnHeaders.Add("SizeNameArabic");
            columnHeaders.Add("ColorNameEnglish");
            columnHeaders.Add("ColorNameArabic");
            columnHeaders.Add("UnitNameEnglish");
            columnHeaders.Add("UnitNameArabic");
            columnHeaders.Add("SalePrice");
            columnHeaders.Add("PackSizeLength");
            columnHeaders.Add("PackSizeWidth");
            columnHeaders.Add("MinStockLevel");
            columnHeaders.Add("Description");
            columnHeaders.Add("Shelf/Location");
            columnHeaders.Add("Assortment");
            columnHeaders.Add("Style/Model");
            columnHeaders.Add("SaleReturnDay");
            columnHeaders.Add("BarCode");
            columnHeaders.Add("ImagePath");
            columnHeaders.Add("RawMaterial");
            columnHeaders.Add("Display Name");
            columnHeaders.Add("Code");

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            // Create a new Excel package
            using (var excelPackage = new ExcelPackage())
            {
                // Add a new worksheet to the Excel package
                var worksheet = excelPackage.Workbook.Worksheets.Add("Client Report");
                // Set column headers
                for (int i = 1; i <= columnHeaders.Count; i++)
                {
                    worksheet.Cells[1, i].Value = columnHeaders[i - 1];
                    worksheet.Cells[1, i].Style.Font.Bold = true;
                    worksheet.Cells[1, i].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(i).Style.Numberformat.Format = "General";
                }
                // Populate the data
                for (var row = 0; row < list.Count; row++)
                {
                    var item = list[row];

                    // Adjust the row index accordingly (add 2 to account for header row)
                    var rowIndex = row + 2;
                    // Set the cell values
                    worksheet.Cells[rowIndex, 1].Value = (item.MasterProductEn == null) ? "" : item.MasterProductEn;
                    worksheet.Cells[rowIndex, 2].Value = (item.MasterProductAr == null) ? "" : item.MasterProductAr;
                    worksheet.Cells[rowIndex, 3].Value = (item.EnglishName == null) ? "" : item.EnglishName;
                    worksheet.Cells[rowIndex, 4].Value = (item.ArabicName == null) ? "" : item.ArabicName;
                    worksheet.Cells[rowIndex, 5].Value = (item.CategoryNameEn == null) ? "" : item.CategoryNameEn;
                    worksheet.Cells[rowIndex, 6].Value = (item.CategoryNameAr == null) ? "" : item.CategoryNameAr;
                    worksheet.Cells[rowIndex, 7].Value = (item.SubCategoryEnglish == null) ? "" : item.SubCategoryEnglish;
                    worksheet.Cells[rowIndex, 8].Value = (item.SubCategoryArabic == null) ? "" : item.SubCategoryArabic;
                    worksheet.Cells[rowIndex, 9].Value = (item.BrandEnglish == null) ? "" : item.BrandEnglish;
                    worksheet.Cells[rowIndex, 10].Value = (item.BrandArabic == null) ? "" : item.BrandArabic;
                    worksheet.Cells[rowIndex, 11].Value = (item.OriginNameEn == null) ? "" : item.OriginNameEn;
                    worksheet.Cells[rowIndex, 12].Value = (item.OriginNameAr == null) ? "" : item.OriginNameAr;
                    worksheet.Cells[rowIndex, 13].Value = (item.SizeName == null) ? "" : item.SizeName;
                    worksheet.Cells[rowIndex, 14].Value = (item.SizeNameArabic == null) ? "" : item.SizeNameArabic;
                    worksheet.Cells[rowIndex, 15].Value = (item.ColorName == null) ? "" : item.ColorName;
                    worksheet.Cells[rowIndex, 16].Value = (item.ColorNameArabic == null) ? "" : item.ColorNameArabic;
                    worksheet.Cells[rowIndex, 17].Value = (item.UnitNameEn == null) ? "" : item.UnitNameEn;
                    worksheet.Cells[rowIndex, 18].Value = (item.UnitNameAr == null) ? "" : item.UnitNameAr;
                    worksheet.Cells[rowIndex, 19].Value = item.SalePrice;
                    worksheet.Cells[rowIndex, 20].Value = item.Length;
                    worksheet.Cells[rowIndex, 21].Value = item.Width;
                    worksheet.Cells[rowIndex, 22].Value = item.StockLevel;
                    worksheet.Cells[rowIndex, 23].Value = item.Description;
                    worksheet.Cells[rowIndex, 24].Value = item.Shelf;
                    worksheet.Cells[rowIndex, 25].Value = item.Assortment;
                    worksheet.Cells[rowIndex, 26].Value = item.StyleNumber;
                    worksheet.Cells[rowIndex, 27].Value = ( item.SaleReturnDays == "0" || item.SaleReturnDays == null ) ? "false" : "true";
                    worksheet.Cells[rowIndex, 28].Value = item.BarCode;
                    worksheet.Cells[rowIndex, 29].Value = item.Image;
                    worksheet.Cells[rowIndex, 30].Value = item.IsRaw.ToString();
                    worksheet.Cells[rowIndex, 31].Value = item.DisplayName.ToString();
                    worksheet.Cells[rowIndex, 32].Value = item.Code.ToString();


                }


                using var memoryStream = new MemoryStream();
                await excelPackage.SaveAsAsync(memoryStream);
                var excelData = memoryStream.ToArray();
                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ClientReport.xlsx");
            }
        }


        [Route("api/Product/SaveItemsBarCodeList")]
        [HttpPost("SaveItemsBarCodeList")]
        public async Task<IActionResult> SaveItemsBarCodeList([FromBody] List<ProductLookUpModel> productList)
        {
            var list = await Mediator.Send(new ItemsBarCodeUpdateCommand
            {
                ItemsBarCodes = productList
            }) ;
            return Ok(list);
        }
        #endregion

        #region Item View Setup

        [Route("api/Product/SaveItemView")]
        [HttpPost("SaveItemView")]
        public async Task<IActionResult> SaveItemView([FromBody] ItemViewLookupModel itemViews, bool isForAllProduct, bool isItemList)
        {
            var message = await Mediator.Send(new ItemViewAddUpdateCommand()
            {
                ItemViews = itemViews,
                IsForAllProduct = isForAllProduct,
                IsItemList = isItemList
            });
            return Ok(message);
        }

        [Route("api/Product/GetItemsViewSetup")]
        [HttpGet("GetItemsViewSetup")]
        public async Task<IActionResult> GetItemsViewSetup()
        {
            var message = await Mediator.Send(new GetItemsViewSetupDetailsQuery());
            return Ok(message);
        } 
        
        [Route("api/Product/UpdateProductsDisplayName")]
        [HttpGet("UpdateProductsDisplayName")]
        public async Task<IActionResult> UpdateProductsDisplayName()
        {
            var message = await Mediator.Send(new ProductsDisplayNameUpdateCommand());
            return Ok(message);
        }

        #endregion

        #region For PriceRecord


        [Route("api/Product/SavePriceRecordInformation")]
        [HttpPost("SavePriceRecordInformation")]
        public async Task<IActionResult> SavePriceRecordInformation([FromBody] PriceRecordLookupModel priceRecord)
        {
            if (priceRecord.Id == Guid.Empty)
            {
                await Mediator.Send(new AddPriceRecordCommand()
                {
                    priceRecord = priceRecord
                });
            }
            else
            {
                await Mediator.Send(new AddPriceRecordCommand()
                {
                    priceRecord = priceRecord
                });
            }
            return Ok(new { IsSuccess = true });
        }

        [Route("api/Product/PriceRecordList")]
        [HttpGet("PriceRecordList")]
        public async Task<IActionResult> PriceRecordList(string searchTerm, int? pageNumber, bool isDropdown, int pageSize, Guid? subCategoryId, string isActiveValue, Guid? sizeId, Guid? originId, Guid? produtMasterId, Guid? categoryId)
        {
            var page = pageSize <= 10 ? 10 : pageSize;
            var priceRecord = await Mediator.Send(new GetPriceRecordListQuery
            {
                SearchTerm = searchTerm,
                PageSize = page,
                IsDropDown = isDropdown,
                SubCategoryId = subCategoryId,
                CategoryId = categoryId,
                IsActiveValue = isActiveValue,
                SizeId = sizeId,
                OriginId = originId,
                ProdutMasterId = produtMasterId,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(priceRecord);
        }

        [Route("api/Product/PriceRecordDetail")]
        [HttpGet("PriceRecordDetail")]
        public async Task<IActionResult> PriceRecordDetail(Guid id)
        {
            var priceRecord = await Mediator.Send(new GetPriceRecordDetailQuery()
            {
                Id = id
            });
            return Ok(priceRecord);
        }


        #endregion

        #region Price Record Change
        [Route("api/Product/SavePriceRecordChange")]
        [HttpPost("SavePriceRecordChange")]
        public async Task<IActionResult> SavePriceRecordChange([FromBody] List<PriceLabelsProuctsListLookupModel> priceRecordChange)
        {
            var priceRecord = await Mediator.Send(new SavePriceRecordChnageCommand
            {
                PriceRecordChange = priceRecordChange,
            });
            return Ok(priceRecord);
        }

        [Route("api/Product/GetPriceRecordChange")]
        [HttpGet("GetPriceRecordChange")]
        public async Task<IActionResult> GetPriceRecordChange(Guid? priceLebelId, Guid customerId)
        {
            var priceRecord = await Mediator.Send(new PriceLabelProductQuery
            {
                PriceLebelId = priceLebelId,
                CustomerId = customerId
            });
            return Ok(priceRecord);
        }
        #endregion

        #region PriceLabeling

        [DisplayName("PriceLabeling Code")]
        [Route("api/Product/PriceLabelingCode")]
        [HttpGet("PriceLabelingCode")]
        public async Task<IActionResult> PriceLabelingCode()
        {
            var autoNo = await Mediator.Send(new GetPriceLabelingCodeQuery());

            return Ok(autoNo);
        }

        [Route("api/Product/SavePriceLabeling")]
        [HttpPost("SavePriceLabeling")]
        public async Task<IActionResult> SavePriceLabeling([FromBody] PriceLabelingVm priceLabelingVm)
        {
            Guid id;
            if (priceLabelingVm.Id == Guid.Empty)
            {
                var autoNo = await Mediator.Send(new GetPriceLabelingCodeQuery());

                id = await Mediator.Send(new AddUpdatePriceLabelingCommand
                {
                    Id = new Guid(),

                    Code = autoNo,
                    Name = priceLabelingVm.Name,
                    Price = priceLabelingVm.Price,
                    NameArabic = priceLabelingVm.NameArabic,
                    Description = priceLabelingVm.Description,
                    IsActive = priceLabelingVm.isActive,



                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdatePriceLabelingCommand
                {
                    Id = priceLabelingVm.Id,
                    Code = priceLabelingVm.Code,
                    Price = priceLabelingVm.Price,
                    Name = priceLabelingVm.Name,
                    NameArabic = priceLabelingVm.NameArabic,
                    Description = priceLabelingVm.Description,
                    IsActive = priceLabelingVm.isActive,

                });
            }
            if (id != Guid.Empty)
            {
                var priceLabeling = await Mediator.Send(new GetPriceLabelingDetailQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, priceLabeling = priceLabeling });
            }

            return Ok(new { IsSuccess = false });



        }
        [Route("api/Product/PriceLabelingList")]
        [HttpGet("PriceLabelingList")]


        public async Task<IActionResult> PriceLabelingList(string searchTerm, int? pageNumber, bool isActive)
        {
            var priceLabeling = await Mediator.Send(new GetPriceLabelingListQuery
            {
                isActive = isActive,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(priceLabeling);
        }

        [Route("api/Product/PriceLabelingDelete")]
        [HttpGet("PriceLabelingDelete")]
        public async Task<IActionResult> PriceLabelingDelete(Guid id)
        {
            var priceLabeling = await Mediator.Send(new DeletePriceLabelingCommand
            {
                Id = id
            });
            return Ok(priceLabeling);

        }
        [Route("api/Product/PriceLabelingDetail")]
        [HttpGet("PriceLabelingDetail")]
        public async Task<IActionResult> PriceLabelingDetail(Guid id)
        {
            var priceLabeling = await Mediator.Send(new GetPriceLabelingDetailQuery()
            {
                Id = id
            });
            return Ok(priceLabeling);

        }
        #endregion

        #region Brand

        [DisplayName("Brand Code")]
        [Route("api/Product/BrandCode")]
        [HttpGet("BrandCode")]
        [Roles("CanAddBrand", "CanEditBrand")]
        public async Task<IActionResult> BrandCode()
        {
            var autoNo = await Mediator.Send(new GetBrandCodeQuery());

            return Ok(autoNo);
        }

        [Route("api/Product/SaveBrand")]
        [HttpPost("SaveBrand")]
        [Roles("CanAddBrand", "CanEditBrand")]
        public async Task<IActionResult> SaveBrand([FromBody] BrandVm brandVm)
        {
            Guid id;
            if (brandVm.Id == Guid.Empty)
            {
                var autoNo = await Mediator.Send(new GetBrandCodeQuery());

                id = await Mediator.Send(new AddUpdateBrandCommand
                {
                    Id = new Guid(),

                    Code = autoNo,
                    Name = brandVm.Name,
                    NameArabic = brandVm.NameArabic,
                    Description = brandVm.Description,
                    IsActive = brandVm.isActive,



                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateBrandCommand
                {
                    Id = brandVm.Id,
                    Code = brandVm.Code,
                    Name = brandVm.Name,
                    NameArabic = brandVm.NameArabic,
                    Description = brandVm.Description,
                    IsActive = brandVm.isActive,

                });
            }
            if (id != Guid.Empty)
            {
                var brand = await Mediator.Send(new GetBrandDetailQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, brand = brand });
            }

            return Ok(new { IsSuccess = false });



        }
        [Route("api/Product/BrandList")]
        [HttpGet("BrandList")]
        [Roles("CanViewBrand", "CanAddItem", "CanEditItem")]


        public async Task<IActionResult> BrandList(string searchTerm, int? pageNumber, bool isActive)
        {
            var brand = await Mediator.Send(new GetBrandListQuery
            {
                isActive = isActive,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(brand);
        }

        [Route("api/Product/BrandDelete")]
        [HttpGet("BrandDelete")]
        [Roles("CanDeleteBrand")]
        public async Task<IActionResult> BrandDelete(Guid id)
        {
            var brandId = await Mediator.Send(new DeleteBrandCommand
            {
                Id = id
            });
            return Ok(brandId);

        }
        [Route("api/Product/BrandDetail")]
        [HttpGet("BrandDetail")]
        [Roles("CanAddBrand", "CanEditBrand", "CanAddItem", "CanEditItem")]
        public async Task<IActionResult> BrandDetail(Guid id)
        {
            var brand = await Mediator.Send(new GetBrandDetailQuery()
            {
                Id = id
            });
            return Ok(brand);

        }
        #endregion

        #region ProductMaster

        [DisplayName("ProductMaster Code")]
        [Route("api/Product/ProductMasterCode")]
        [HttpGet("ProductMasterCode")]
        [Roles("CanAddProduct", "CanEditProduct")]
        public async Task<IActionResult> ProductMasterCode()
        {
            var autoNo = await Mediator.Send(new GetProductMasterCodeQuery());

            return Ok(autoNo);
        }

        [Route("api/Product/SaveProductMaster")]
        [HttpPost("SaveProductMaster")]
        [Roles("CanAddProduct", "CanEditProduct")]
        public async Task<IActionResult> SaveProductMaster([FromBody] BrandVm brandVm)
        {
            Guid id;
            if (brandVm.Id == Guid.Empty)
            {
                var autoNo = await Mediator.Send(new GetProductMasterCodeQuery());

                id = await Mediator.Send(new AddUpdateProductMasterCommand
                {
                    Id = new Guid(),

                    Code = autoNo,
                    Name = brandVm.Name,
                    NameArabic = brandVm.NameArabic,
                    Description = brandVm.Description,
                    IsActive = brandVm.isActive,



                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateProductMasterCommand
                {
                    Id = brandVm.Id,
                    Code = brandVm.Code,
                    Name = brandVm.Name,
                    NameArabic = brandVm.NameArabic,
                    Description = brandVm.Description,
                    IsActive = brandVm.isActive,

                });
            }
            if (id != Guid.Empty)
            {
                var productMaster = await Mediator.Send(new GetProductMasterDetailQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, productMaster = productMaster });
            }

            return Ok(new { IsSuccess = false });



        }
        [Route("api/Product/ProductMasterList")]
        [HttpGet("ProductMasterList")]
        [Roles("CanViewProduct", "CanAddItem", "CanEditItem")]

        public async Task<IActionResult> ProductMasterList(string searchTerm, int? pageNumber, bool isActive)
        {
            var brand = await Mediator.Send(new GetProductMasterListQuery
            {
                isActive = isActive,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(brand);
        }

        [Route("api/Product/ProductMasterDelete")]
        [HttpGet("ProductMasterDelete")]
        [Roles("CanDeleteProduct")]
        public async Task<IActionResult> ProductMasterDelete(Guid id)
        {
            var brandId = await Mediator.Send(new DeleteProductMasterCommand
            {
                Id = id
            });
            return Ok(brandId);

        }
        [Route("api/Product/ProductMasterDetail")]
        [HttpGet("ProductMasterDetail")]
        [Roles("CanAddProduct", "CanEditProduct", "CanAddItem", "CanEditItem")]
        public async Task<IActionResult> ProductMasterDetail(Guid id)
        {
            var brand = await Mediator.Send(new GetProductMasterDetailQuery()
            {
                Id = id
            });
            return Ok(brand);

        }
        #endregion

        #region Color

        [DisplayName("Color Code")]
        [Route("api/Product/ColorCode")]
        [HttpGet("ColorCode")]
        [Roles("CanAddColor", "CanEditColor")]
        public async Task<IActionResult> ColorCode()
        {
            var autoNo = await Mediator.Send(new GetColorCodeQuery());

            return Ok(autoNo);
        }

        [Route("api/Product/SaveColor")]
        [HttpPost("SaveColor")]
        [Roles("CanAddColor", "CanEditColor")]
        public async Task<IActionResult> SaveColor([FromBody] ColorVm colorVm)
        {
            Guid id;
            if (colorVm.Id == Guid.Empty)
            {
                var autoNo = await Mediator.Send(new GetColorCodeQuery());

                id = await Mediator.Send(new AddUpdateColorCommand
                {
                    Id = new Guid(),
                    Code = autoNo,
                    Name = colorVm.Name,
                    NameArabic = colorVm.NameArabic,
                    Description = colorVm.Description,
                    IsActive = colorVm.isActive
                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateColorCommand
                {
                    Id = colorVm.Id,
                    Code = colorVm.Code,
                    Name = colorVm.Name,
                    NameArabic = colorVm.NameArabic,
                    Description = colorVm.Description,
                    IsActive = colorVm.isActive
                });
            }
            if (id != Guid.Empty)
            {
                var color = await Mediator.Send(new GetProcessDetailQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, color = color });
            }
            return Ok(new { IsSuccess = false });
        }
        [Route("api/Product/ColorList")]
        [HttpGet("ColorList")]
        [Roles("CanViewColor", "CanAddItem", "CanEditItem", "User")]
        public async Task<IActionResult> ColorList(string searchTerm, int? pageNumber, bool isActive, bool isVariance)
        {
            var color = await Mediator.Send(new GetProcessListQuery
            {
                IsVariance = isVariance,
                isActive = isActive,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(color);
        }
        [Route("api/Product/ColorDelete")]
        [HttpGet("ColorDelete")]
        [Roles("CanDeleteColor")]
        public async Task<IActionResult> ColorDelete(Guid id)
        {
            var colorId = await Mediator.Send(new DeleteColorCommand
            {
                Id = id,

            });
            return Ok(colorId);

        }
        [Route("api/Product/ColorDetail")]
        [HttpGet("ColorDetail")]
        [Roles("CanAddColor", "CanEditColor", "CanAddItem", "CanEditItem", "User")]
        public async Task<IActionResult> ColorDetail(Guid id, Guid productId, bool isVariance)
        {
            var color = await Mediator.Send(new GetProcessDetailQuery()
            {
                Id = id,
                ProductId = productId,
                IsVariance = isVariance
            });
            return Ok(color);

        }
        #endregion

        #region Size

        [DisplayName("Size Code")]
        [Route("api/Product/SizeCode")]
        [HttpGet("SizeCode")]
        [Roles("CanAddSize", "CanEditSize")]
        public async Task<IActionResult> SizeCode()
        {
            var autoNo = await Mediator.Send(new GetSizeCodeQuery());

            return Ok(autoNo);
        }

        [Route("api/Product/SaveSize")]
        [HttpPost("SaveSize")]
        [Roles("CanAddSize", "CanEditSize")]
        public async Task<IActionResult> SaveSize([FromBody] SizeVm sizeVm)
        {
            Guid id;
            if (sizeVm.Id == Guid.Empty)
            {
                var autoNo = await Mediator.Send(new GetSizeCodeQuery());

                id = await Mediator.Send(new AddUpdateSizeCommand
                {
                    Id = new Guid(),

                    Code = autoNo,
                    Name = sizeVm.Name,
                    NameArabic = sizeVm.NameArabic,
                    Description = sizeVm.Description,
                    IsActive = sizeVm.isActive,



                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateSizeCommand
                {
                    Id = sizeVm.Id,
                    Code = sizeVm.Code,
                    Name = sizeVm.Name,
                    NameArabic = sizeVm.NameArabic,
                    Description = sizeVm.Description,
                    IsActive = sizeVm.isActive,

                });
            }
            if (id != Guid.Empty)
            {
                var size = await Mediator.Send(new GetSizeDetailQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, size = size });
            }

            return Ok(new { IsSuccess = false });



        }
        [Route("api/Product/SizeList")]
        [HttpGet("SizeList")]
        [Roles("CanViewSize", "CanAddItem", "CanEditItem", "User")]
        public async Task<IActionResult> SizeList(string searchTerm, int? pageNumber, bool isActive, bool isVariance)
        {
            var size = await Mediator.Send(new GetSizeListQuery
            {
                IsVariance = isVariance,
                isActive = isActive,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(size);
        }
        [Route("api/Product/SizeDelete")]
        [HttpGet("SizeDelete")]
        [Roles("CanDeleteSize")]
        public async Task<IActionResult> SizeDelete(Guid id)
        {
            var sizeId = await Mediator.Send(new DeleteSizeCommand
            {
                Id = id
            });
            return Ok(sizeId);

        }
        [Route("api/Product/SizeDetail")]
        [HttpGet("SizeDetail")]
        [Roles("CanAddSize", "CanEditSize", "CanAddItem", "CanEditItem")]
        public async Task<IActionResult> SizeDetail(Guid id)
        {
            var size = await Mediator.Send(new GetSizeDetailQuery()
            {
                Id = id
            });
            return Ok(size);

        }
        #endregion

        #region Unit

        [DisplayName("Unit Code")]
        [Route("api/Product/UnitCode")]
        [HttpGet("UnitCode")]
        [Roles("CanAddUnit", "CanEditUnit")]
        public async Task<IActionResult> UnitCode()
        {
            var autoNo = await Mediator.Send(new GetUnitCodeQuery());

            return Ok(autoNo);
        }

        [Route("api/Product/SaveUnit")]
        [HttpPost("SaveUnit")]
        [Roles("CanAddUnit", "CanEditUnit")]

        public async Task<IActionResult> SaveUnit([FromBody] UnitVm unitVm)
        {
            Guid id;
            if (unitVm.Id == Guid.Empty)
            {
                var autoNo = await Mediator.Send(new GetUnitCodeQuery());

                id = await Mediator.Send(new AddUpdateUnitCommand
                {
                    Id = new Guid(),
                    Code = autoNo,
                    Name = unitVm.Name,
                    NameArabic = unitVm.NameArabic,
                    Description = unitVm.Description,
                    IsActive = unitVm.isActive,



                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateUnitCommand
                {
                    Id = unitVm.Id,
                    Code = unitVm.Code,
                    Name = unitVm.Name,
                    NameArabic = unitVm.NameArabic,
                    Description = unitVm.Description,
                    IsActive = unitVm.isActive,

                });
            }
            if (id != Guid.Empty)
            {
                var unit = await Mediator.Send(new GetUnitDetailQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, unit = unit });
            }

            return Ok(new { IsSuccess = false });



        }
        [Route("api/Product/UnitList")]
        [HttpGet("UnitList")]
        //[Roles("CanViewUnit", "CanAddItem", "CanEditItem", "CanAddQuickItem", "CanAddTouchQuickItem")]

        public async Task<IActionResult> UnitList(string searchTerm, int? pageNumber, bool isActive)
        {
            var unit = await Mediator.Send(new GetUnitListQuery
            {
                isActive = isActive,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(unit);
        }
        [Route("api/Product/UnitDelete")]
        [HttpGet("unitDelete")]
        [Roles("CanDeleteUnit")]
        public async Task<IActionResult> UnitDelete(Guid id)
        {
            var unitId = await Mediator.Send(new DeleteUnitCommand
            {
                Id = id
            });
            return Ok(unitId);

        }
        [Route("api/Product/UnitDetail")]
        [HttpGet("UnitDetail")]
        [Roles("CanAddUnit", "CanEditUnit", "CanAddItem", "CanEditItem")]
        public async Task<IActionResult> UnitDetail(Guid id)
        {
            var Unit = await Mediator.Send(new GetUnitDetailQuery()
            {
                Id = id
            });
            return Ok(Unit);

        }
        #endregion

        #region Origin

        [DisplayName("Origin Code")]
        [Route("api/Product/OriginCode")]
        [HttpGet("OriginCode")]
        [Roles("CanAddOrigin", "CanEditOrigin")]
        public async Task<IActionResult> OriginCode()
        {
            var autoNo = await Mediator.Send(new GetOriginCodeQuery());

            return Ok(autoNo);
        }

        [Route("api/Product/SaveOrigin")]
        [HttpPost("SaveOrigin")]
        [Roles("CanAddOrigin", "CanEditOrigin")]
        public async Task<IActionResult> SaveOrigin([FromBody] OriginVm originVm)
        {
            Guid id;
            if (originVm.Id == Guid.Empty)
            {
                var autoNo = await Mediator.Send(new GetOriginCodeQuery());

                id = await Mediator.Send(new AddUpdateOriginCommand
                {
                    Id = new Guid(),

                    Code = autoNo,
                    Name = originVm.Name,
                    NameArabic = originVm.NameArabic,
                    Description = originVm.Description,
                    IsActive = originVm.isActive
                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateOriginCommand
                {
                    Id = originVm.Id,
                    Code = originVm.Code,
                    Name = originVm.Name,
                    NameArabic = originVm.NameArabic,
                    Description = originVm.Description,
                    IsActive = originVm.isActive
                });
            }
            if (id != Guid.Empty)
            {
                var origin = await Mediator.Send(new GetOriginDetailQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, origin = origin });
            }
            return Ok(new { IsSuccess = false });
        }
        [Route("api/Product/OriginList")]
        [HttpGet("OriginList")]
        [Roles("CanViewOrigin", "CanAddItem", "CanEditItem")]
        public async Task<IActionResult> OriginList(string searchTerm, int? pageNumber, bool isActive)
        {
            var origin = await Mediator.Send(new GetOriginListQuery
            {
                isActive = isActive,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(origin);
        }
        [Route("api/Product/OriginDelete")]
        [HttpGet("OriginDelete")]
        [Roles("CanDeleteOrigin")]
        public async Task<IActionResult> OriginDelete(Guid id)
        {
            var originId = await Mediator.Send(new DeleteOriginCommand
            {
                Id = id
            });
            return Ok(originId);

        }
        [Route("api/Product/OriginDetail")]
        [HttpGet("OriginDetail")]
        [Roles("CanAddOrigin", "CanEditOrigin", "CanAddItem", "CanEditItem")]
        public async Task<IActionResult> OriginDetail(Guid id)
        {
            var origin = await Mediator.Send(new GetOriginDetailQuery()
            {
                Id = id
            });
            return Ok(origin);

        }
        #endregion

        #region TaxRate

        [DisplayName("TaxRate Code")]
        [Route("api/Product/TaxRateCode")]
        [HttpGet("TaxRateCode")]
        //[Roles("Can Save Product", "Can Edit Product", "Can Save Tax Rate", "Noble Admin", "Can Save  Purchase Invoice as Draft", "Can Edit Purchase Order as Draft", "Can Save Purchase Order as Post", "Can Edit Purchase Order as Post", "Can Save  Purchase Invoice as Draft", "Can Edit Purchase Invoice as Draft", "Can Save Purchase Invoice as Post", "Can Edit Purchase Invoice as Post", "Can Save Purchase Return", "Can Edit Purchase Return", "Can Save Sale Invoice as Hold", "Can Edit Sale Invoice as Hold", "Can Save Sale Invoice as Paid", "Can Edit Sale Invoice as Paid", "Can Save  Mobile Order", "Can Edit  Mobile Order", "Can Save Touch Invoice", "Can Edit Touch Invoice")]
        public async Task<IActionResult> TaxRateCode()
        {
            var autoNo = await Mediator.Send(new GetTaxRateCodeQuery());

            return Ok(autoNo);
        }

        [Route("api/Product/SaveTaxRate")]
        [HttpPost("SaveTaxRate")]
        public async Task<IActionResult> SaveTaxRate([FromBody] TaxRateVm taxRateVm)
        {
            Guid id;
            if (taxRateVm.Id == Guid.Empty)
            {
                var autoNo = await Mediator.Send(new GetTaxRateCodeQuery());

                id = await Mediator.Send(new AddUpdateTaxRateCommand
                {
                    Id = new Guid(),

                    Code = autoNo,
                    Name = taxRateVm.Name,
                    NameArabic = taxRateVm.NameArabic,
                    Rate = taxRateVm.Rate,
                    Description = taxRateVm.Description,
                    isActive = taxRateVm.isActive,
                    Setup = taxRateVm.Setup,
                    TaxMethod = taxRateVm.TaxMethod,

                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateTaxRateCommand
                {
                    Id = taxRateVm.Id,
                    Code = taxRateVm.Code,
                    Rate = taxRateVm.Rate,
                    Name = taxRateVm.Name,
                    NameArabic = taxRateVm.NameArabic,
                    Description = taxRateVm.Description,
                    isActive = taxRateVm.isActive
                });
            }
            if (id != Guid.Empty)
            {
                var taxRate = await Mediator.Send(new GetTaxRateDetailQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, taxRate = taxRate });
            }

            return Ok(new { IsSuccess = false });



        }
        [Route("api/Product/TaxRateList")]
        [HttpGet("TaxRateList")]
        //[Roles("CanViewVatRate", "CashInvoices", "CreditInvoices", "CanHoldTouchInvoices", "TouchInvoiceTemplate1", "TouchInvoiceTemplate2", "TouchInvoiceTemplate3", "CanViewSaleReturn", "CanAddSaleReturn", "CanAddSaleOrder", "CanViewSaleOrder", "CreditPurchase", "CashPurchase", "CanPurchaseInvoiceCosting", "CanViewDraftOrder", "CanViewInProcessOrder", "CanAddPurchaseOrder", "CanAddPurchaseReturn", "CanViewDetailPurchaseReturn", "CanAddExpense", "CanEditExpense", "CanDraftExpense", "CanAddStockIn", "CanEditStockIn", "CanDraftStockIn", "CanAddStockOut", "CanEditStockOut", "CanDraftStockOut", "CanHoldServiceInvoices", "CashServiceInvoices", "CreditServiceInvoices", "CanAddServiceSaleOrder", "CanDraftServiceSaleOrder", "CanEditServiceSaleOrder", "CanDuplicateServiceSaleOrder", "CanAddServiceQuotation", "CanEditServiceQuotation", "CanDraftServiceQuotation", "CanViewServiceInvoiceDetail", "CanAddItem", "CanEditItem")]
        public async Task<IActionResult> TaxRateList(bool isActive, bool isEmail)
        {
            var taxRate = await Mediator.Send(new GetTaxRateListQuery { isActive = isActive, IsEmail = isEmail });
            return Ok(taxRate);
        }
        [Route("api/Product/TaxRateDelete")]
        [HttpGet("TaxRateDelete")]
        [Roles("Can Delete Tax Rate", "Noble Admin")]
        public async Task<IActionResult> TaxRateDelete(Guid id)
        {
            var taxRateId = await Mediator.Send(new DeleteTaxRateCommand
            {
                Id = id
            });
            return Ok(taxRateId);

        }
        [Route("api/Product/TaxRateDetail")]
        [HttpGet("TaxRateDetail")]
        [Roles("CanAddVatRate", "CanEditVatRate")]
        public async Task<IActionResult> TaxRateDetail(Guid id)
        {
            var taxRate = await Mediator.Send(new GetTaxRateDetailQuery()
            {
                Id = id
            });
            return Ok(taxRate);

        }
        #endregion

        #region For Category
        [Route("api/Product/CategoryAutoGenerateCode")]
        [HttpGet("CategoryAutoGenerateCode")]
        [Roles("CanAddCategory", "CanEditCategory", "CanAddPurchaseInvoice")]
        public async Task<IActionResult> CategoryAutoGenerateCode()
        {
            var autoNo = await Mediator.Send(new GetCategoryCodeQuery());
            return Ok(autoNo);
        }
        [Route("api/Product/GetCategoryInformation")]
        [HttpGet("GetCategoryInformation")]
        //[Roles("CanHoldTouchInvoices", "TouchInvoiceTemplate1", "TouchInvoiceTemplate2", "TouchInvoiceTemplate3", "CanAddTerminal", "CanEditTerminal", "CanAddItem", "CanEditItem", "CanViewCategory", "CanAddSubCategory", "CanEditSubCategory", "Noble Admin")]
        public async Task<IActionResult> GetCategoryInformation(string searchTerm, int? pageNumber, bool isActive, bool isTouch, Guid? companyId)
        {
            var categoryListModel = await Mediator.Send(new GetCategoryListQuery
            {
                IsTouch = isTouch,
                isActive = isActive,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
                CompanyId = companyId
            });
            return Ok(categoryListModel);
        }
        [Route("api/Product/CategoryDetailsViaId")]
        [HttpGet("CategoryDetailsViaId")]
        [Roles("CanAddCategory", "CanEditCategory", "CanAddItem", "CanEditItem", "CanAddSubCategory", "CanEditSubCategory")]
        public async Task<IActionResult> CategoryDetailsViaId(Guid id)
        {
            var category = await Mediator.Send(new GetCategoryDetailsQuery()
            {
                Id = id
            });
            return Ok(category);
        }
        [Route("api/Product/DeleteCategory")]
        [HttpGet("DeleteCategory")]
        [Roles("CanDeleteCategory")]
        public async Task<IActionResult> DeleteCategory(Guid Id)
        {
            var category = await Mediator.Send(new DeleteCategoryCommand
            {
                Id = Id
            });
            return Ok(category);

        }
        [Route("api/Product/SaveCategoryInformation")]
        [HttpPost("SaveCategoryInformation")]
        [Roles("CanAddCategory", "CanEditCategory", "CanAddPurchaseInvoice")]
        public async Task<IActionResult> SaveCategoryInformation([FromBody] CategoryVM categoryInformation)
        {
            Guid id;
            if (categoryInformation.Id == Guid.Empty)
            {
                var autoNo = await Mediator.Send(new GetCategoryCodeQuery());

                id = await Mediator.Send(new AddUpdateCategoryCommand
                {
                    Id = new Guid(),
                    Name = categoryInformation.Name,
                    NameArabic = categoryInformation.NameArabic,
                    Code = autoNo,
                    Description = categoryInformation.Description,
                    IsReturn = categoryInformation.IsReturn,
                    ReturnDays = categoryInformation.ReturnDays,
                    IsActive = categoryInformation.isActive,
                    IsService = categoryInformation.IsService,
                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateCategoryCommand
                {
                    Id = categoryInformation.Id,
                    Name = categoryInformation.Name,
                    NameArabic = categoryInformation.NameArabic,
                    Code = categoryInformation.Code,
                    Description = categoryInformation.Description,
                    PurchaseAccountId = categoryInformation.PurchaseAccountId,
                    COGSAccountId = categoryInformation.COGSAccountId,
                    InventoryAccountId = categoryInformation.InventoryAccountId,
                    IncomeAccountId = categoryInformation.IncomeAccountId,
                    SaleAccountId = categoryInformation.SaleAccountId,
                    IsActive = categoryInformation.isActive,
                    ReturnDays = categoryInformation.ReturnDays,
                    IsReturn = categoryInformation.IsReturn,
                    IsService = categoryInformation.IsService,
                });
            }
            if (id != Guid.Empty)
            {
                var category = await Mediator.Send(new GetCategoryDetailsQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, category = category });
            }
            return Ok(new { IsSuccess = false });
        }
        #endregion

        #region For SubCategory
        [Route("api/Product/SubCategoryAutoGenerateCode")]
        [HttpGet("SubCategoryAutoGenerateCode")]
        [Roles("CanAddSubCategory", "CanEditSubCategory")]
        public async Task<IActionResult> SubCategoryAutoGenerateCode()
        {
            var autoNo = await Mediator.Send(new GetSubCategoryCodeQuery());
            return Ok(autoNo);
        }
        [Route("api/Product/GetSubCategoryInformation")]
        [HttpGet("GetSubCategoryInformation")]
        [Roles("CanViewSubCategory", "CanAddItem", "CanEditItem")]
        public async Task<IActionResult> GetSubCategoryInformation(string searchTerm, int? pageNumber, bool isActive, Guid categoryId)
        {
            var categoryListModel = await Mediator.Send(new GetSubCategoryListQuery
            {
                CategoryId = categoryId,
                IsActive = isActive,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(categoryListModel);
        }
        [Route("api/Product/SubCategoryDetailsViaId")]
        [HttpGet("SubCategoryDetailsViaId")]
        [Roles("CanAddSubCategory", "CanEditSubCategory", "CanAddItem", "CanEditItem")]
        public async Task<IActionResult> SubCategoryDetailsViaId(Guid id)
        {
            var subCategory = await Mediator.Send(new GetSubCategoryDetailsQuery()
            {
                Id = id
            });
            return Ok(subCategory);
        }
        [Route("api/Product/DeleteSubCategory")]
        [HttpGet("DeleteSubCategory")]
        [Roles("CanDeleteSubCategories")]
        public async Task<IActionResult> DeleteSubCategory(Guid id)
        {
            var category = await Mediator.Send(new DeleteSubCategoryCommand
            {
                Id = id
            });
            return Ok(category);

        }
        [Route("api/Product/SaveSubCategoryInformation")]
        [HttpPost("SaveSubCategoryInformation")]
        [Roles("CanAddSubCategory", "CanEditSubCategory")]
        public async Task<IActionResult> SaveSubCategoryInformation([FromBody] SubCategoryVM categoryInformation)
        {
            Guid id;
            if (categoryInformation.Id == Guid.Empty)
            {
                var autoNo = await Mediator.Send(new GetSubCategoryCodeQuery());

                id = await Mediator.Send(new AddUpdateSubCategoryCommand
                {
                    Id = new Guid(),
                    Name = categoryInformation.Name,
                    NameArabic = categoryInformation.NameArabic,
                    Code = autoNo,
                    Description = categoryInformation.Description,
                    CategoryId = categoryInformation.CategoryId,
                    IsActive = categoryInformation.isActive,
                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateSubCategoryCommand
                {
                    Id = categoryInformation.Id,
                    Name = categoryInformation.Name,
                    NameArabic = categoryInformation.NameArabic,
                    Code = categoryInformation.Code,
                    Description = categoryInformation.Description,
                    CategoryId = categoryInformation.CategoryId,
                    IsActive = categoryInformation.isActive,
                });
            }
            if (id != Guid.Empty)
            {
                var subCategory = await Mediator.Send(new GetSubCategoryDetailsQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, subCategory = subCategory });
            }
            return Ok(new { IsSuccess = false });
        }
        #endregion

        #region For Products
        [Route("api/Product/ProductAutoGenerateCode")]
        [HttpGet("ProductAutoGenerateCode")]
        [Roles("CanAddTouchQuickItem", "CanAddQuickItem", "CanAddItem", "CanEditItem", "CanAddPurchaseInvoice")]
        public async Task<IActionResult> ProductAutoGenerateCode()
        {
            var autoNo = await Mediator.Send(new GetProductsCodeQuery());
            return Ok(autoNo);
        }

        [Route("api/Product/GetProductByCategoryId")]
        [HttpGet("GetProductByCategoryId")]
        [Roles("CanPrintRackBarcode")]
        public async Task<IActionResult> GetProductByCategoryId(Guid categoryId)
        {
            var productList = await Mediator.Send(new GetProductListQuery
            {
                IsProductList = true,
                IsRaw = false,
                //CategoryId = categoryId,

            });
            return Ok(productList);
        }
        [Route("api/Product/GetProductFilterInformation")]
        [HttpPost("GetProductFilterInformation")]
        [Roles("CanViewSuppliersWiseProductPurchase", "CanPrintSuppliersWiseProductPurchase", "CanViewProductDiscountCustomer", "CanViewProductDiscountSupplier", "CanPrintProductDiscountCustomer", "CanPrintProductDiscountSupplier", "CanAddProductionRecipe", "CanViewItem", "CanDraftStockIn", "CanEditStockIn", "CanAddStockIn", "CanAddStockOut", "CanEditStockOut", "CanDraftStockOut", "CanAddStockTransfer", "CanEditStockTransfer", "CanDraftStockTransfer", "CanHoldServiceInvoices", "CashServiceInvoices", "CreditServiceInvoices", "CanAddServiceSaleOrder", "CanDraftServiceSaleOrder", "CanEditServiceSaleOrder", "CanDuplicateServiceSaleOrder", "CanAddServiceQuotation", "CanEditServiceQuotation", "CanDraftServiceQuotation", "CanAddDispatchNote", "CanHoldTouchInvoices")]
        public async Task<IActionResult> GetProductFilterInformation([FromBody] ProductFilterVm productFilter)
        {
            var page = productFilter.pageSize <= 10 ? 10 : productFilter.pageSize;
            if (productFilter.pageNumber == 0)
            {
                productFilter.pageNumber = 1;
            }
            var categoryListModel = await Mediator.Send(new GetProductListForTableQuery
            {
                SearchTerm = productFilter.searchTerm,
                PageNumber = productFilter.pageNumber ?? 1,
                IsDropdown = productFilter.isDropdown,
                IsRaw = productFilter.isRaw,
                IsFifo = productFilter.isFifo,
                OpenBatch = productFilter.openBatch,
                WareHouseId = productFilter.warehouseId,
                Status = productFilter.status,
                CategoryId = productFilter.categoryId,
                SubCategoryId = productFilter.subCategoryId,
                ProductMasterId = productFilter.productMasterId,
                OriginId = productFilter.originId,
                PageSize = page,
                ColorId = productFilter.colorId,
                SizeId = productFilter.sizeId,
                IsProductList = productFilter.isProductList,
                ColorVariants = productFilter.colorVariants,
                IsService = productFilter.isService,
                BranchId = productFilter.branchId,
            });
            return Ok(categoryListModel);
        }
        [Route("api/Product/GetItemsForBarCodeChange")]
        [HttpPost("GetItemsForBarCodeChange")]
        public async Task<IActionResult> GetItemsForBarCodeChange(Guid? warehouseId, Guid? produtMasterId, Guid? categoryId, Guid? subCategoryId, Guid? colorId, Guid? sizeId, string barCodeType, int? pageNumber, int pageSize)
        {
            
            var categoryListModel = await Mediator.Send(new GetItemsForBarCodeChangeQuery
            {
                PageNumber = pageNumber ?? 1,
                WareHouseId = warehouseId,
                CategoryId = categoryId,
                SubCategoryId = subCategoryId,
                ProdutMasterId = produtMasterId,
                PageSize = Convert.ToInt32(pageNumber),
                ColorId = colorId,
                SizeId = sizeId,
                BarCodeType = barCodeType
            });
            return Ok(categoryListModel);
        }

        [Route("api/Product/GetProductListExcel")]
        [HttpPost("GetProductListExcel")]
        public async Task<IActionResult> GetProductListExcel([FromBody] ProductFilterVm productFilter)
        {
            var page = productFilter.pageSize <= 10 ? 10 : productFilter.pageSize;
            if (productFilter.pageNumber == 0)
            {
                productFilter.pageNumber = 1;
            }

            var categoryListModel = await Mediator.Send(new GetProductListQuery
            {
                SearchTerm = productFilter.searchTerm,
                PageNumber = productFilter.pageNumber ?? 1,
                IsDropdown = productFilter.isDropdown,
                IsRaw = productFilter.isRaw,
                IsFifo = productFilter.isFifo,
                OpenBatch = productFilter.openBatch,
                WareHouseId = productFilter.warehouseId,
                Status = productFilter.status,
                Categories = productFilter.categoryId,
                SubCategoryId = productFilter.subCategoryId,
                ProductMasterId = productFilter.productMasterId,
                OriginId = productFilter.originId,
                PageSize = productFilter.pageSize,
                ColorId = productFilter.colorId,
                SizeId = productFilter.sizeId,
                IsProductList = productFilter.isProductList,
                ColorVariants = productFilter.colorVariants,
                IsService = productFilter.isService,
                BranchId = productFilter.branchId,
            });
            
            var list = categoryListModel.Results.Products;

            List<string> columnHeaders = new List<string>();

            columnHeaders.Add("#");
            columnHeaders.Add("Code");
            columnHeaders.Add("Name English");
            columnHeaders.Add("Name Other Language");
            columnHeaders.Add("Description");
            columnHeaders.Add("DisplayName");
            columnHeaders.Add("Category");
            columnHeaders.Add("Vat / Tax Method");
            columnHeaders.Add("Tax Rate");
            columnHeaders.Add("Sale Price");
            columnHeaders.Add("Purchase Price");
            columnHeaders.Add("Barcode");
            columnHeaders.Add("Style / Model ");
            columnHeaders.Add("Unit");

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            // Create a new Excel package
            using (var excelPackage = new ExcelPackage())
            {
                // Add a new worksheet to the Excel package
                var worksheet = excelPackage.Workbook.Worksheets.Add("Client Report");
                // Set column headers
                for (int i = 1; i <= columnHeaders.Count; i++)
                {
                    worksheet.Cells[1, i].Value = columnHeaders[i - 1];
                    worksheet.Cells[1, i].Style.Font.Bold = true;
                    worksheet.Cells[1, i].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(i).Style.Numberformat.Format = "General";
                }
                // Populate the data
                for (var row = 0; row < list.Count; row++)
                {
                    var item = list[row];
                    
                    // Adjust the row index accordingly (add 2 to account for header row)
                    var rowIndex = row + 2;
                    // Set the cell values
                    worksheet.Cells[rowIndex, 1].Value = row + 1;
                    worksheet.Cells[rowIndex, 2].Value = item.Code;
                    worksheet.Cells[rowIndex, 3].Value = (item.EnglishName == null) ? "" : item.EnglishName;
                    worksheet.Cells[rowIndex, 4].Value = (item.ArabicName == null) ? "" : item.ArabicName;
                    worksheet.Cells[rowIndex, 5].Value = item.Description != null ? item.Description : "";
                    worksheet.Cells[rowIndex, 6].Value = item.DisplayName;
                    worksheet.Cells[rowIndex, 7].Value = item.Category == null ? "" : item.Category.Name;
                    worksheet.Cells[rowIndex, 8].Value = item.TaxMethod;
                    worksheet.Cells[rowIndex, 9].Value = item.TaxRateValue;
                    worksheet.Cells[rowIndex, 10].Value = item.SalePrice;
                    worksheet.Cells[rowIndex, 11].Value = item.PurchasePrice;  // Corrected the index from 12 to 11
                    worksheet.Cells[rowIndex, 12].Value = item.BarCode;
                    worksheet.Cells[rowIndex, 13].Value = item.StyleNumber;
                    worksheet.Cells[rowIndex, 14].Value = item.Unit;
                }
                // Auto-fit columns for better visibility
                //worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                // Save the Excel package to a MemoryStream

                using var memoryStream = new MemoryStream();
                await excelPackage.SaveAsAsync(memoryStream);
                // Return the Excel file as a stream in the response
                var excelData = memoryStream.ToArray();
                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ClientReport.xlsx");
            }
        }

        [Route("api/Product/GetProductDetailForInvoiceQuery")]
        [HttpGet("GetProductDetailForInvoiceQuery")]
        public async Task<IActionResult> GetProductDetailForInvoiceQuery(Guid id, bool isFifo, bool colorVariants, Guid? wareHouseId, int openBatch, Guid? branchId, string barCode)
        {
            var categoryListModel = await Mediator.Send(new GetProductDetailForInvoiceQuery
            {
                Id = id,
                IsFifo = isFifo,
                ColorVariants = colorVariants,
                WareHouseId = wareHouseId,
                OpenBatch = openBatch,
                BranchId = branchId,
                BarCode = barCode,
            });
            return Ok(categoryListModel);
        }

        [Route("api/Product/GetProductListForDropdown")]
        [HttpGet("GetProductListForDropdown")]
        public async Task<IActionResult> GetProductListForDropdown(string searchTerm, int? pageNumber, Guid? categoryId,bool isRaw, bool isService, Guid? branchId, bool isForRaw)
        {
            var categoryListModel = await Mediator.Send(new GetProductListForDropdownQuery
            {
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
                IsRaw = isRaw,
                CategoryId = categoryId,
                IsService = isService,
                BranchId = branchId,
                IsForRaw = isForRaw,
            });
            return Ok(categoryListModel);
        }

        [Route("api/Product/GetProductInformation")]
        [HttpGet("GetProductInformation")]
        [Roles("CreditInvoices", "CashInvoices", "CanAddSaleOrder", "CanViewSaleOrder", "CashPurchase", "CreditPurchase", "CanPurchaseInvoiceCosting", "CanViewDraftOrder", "CanViewInProcessOrder", "CanAddPurchaseOrder", "CanViewCustomersWiseProductSale", "CanPrintCustomersWiseProductSale", "CanViewSuppliersWiseProductPurchase", "CanPrintSuppliersWiseProductPurchase", "CanViewProductDiscountCustomer", "CanViewProductDiscountSupplier", "CanPrintProductDiscountCustomer", "CanPrintProductDiscountSupplier", "CanAddProductionRecipe", "CanViewItem", "CanDraftStockIn", "CanEditStockIn", "CanAddStockIn", "CanAddStockOut", "CanEditStockOut", "CanDraftStockOut", "CanAddStockTransfer", "CanEditStockTransfer", "CanDraftStockTransfer", "CanHoldServiceInvoices", "CashServiceInvoices", "CreditServiceInvoices", "CanAddServiceSaleOrder", "CanDraftServiceSaleOrder", "CanEditServiceSaleOrder", "CanDuplicateServiceSaleOrder", "CanAddServiceQuotation", "CanEditServiceQuotation", "CanDraftServiceQuotation", "CanAddDispatchNote", "CanHoldTouchInvoices", "CanAddReparingOrder", "CanEditReparingOrder", "Simple")]
        public async Task<IActionResult> GetProductInformation(string searchTerm, int? pageNumber, Guid? wareHouseId, bool isDropdown,bool isActive, string status, Guid? categoryId, int pageSize, bool isRaw, bool isProductList, bool? isService, bool isFifo, int openBatch, bool colorVariants, Guid? productMasterId, Guid? colorId, Guid? sizeId, Guid? branchId)
        {
            var page = pageSize <= 10 ? 10 : pageSize;
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }
            var categoryListModel = await Mediator.Send(new GetProductListQuery
            {
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
                IsDropdown = isDropdown,
                IsRaw = isRaw,
                IsFifo = isFifo,
                OpenBatch = openBatch,
                WareHouseId = wareHouseId,
                Status = status,
                CategoryId = categoryId,
                PageSize = page,
                IsProductList = isProductList,
                ColorVariants = colorVariants,
                IsService = isService,
                BranchId = branchId,
                IsActive = isActive
            });
            return Ok(categoryListModel);
        }

        [Route("api/Product/GetProductInformationForTouch")]
        [HttpGet("GetProductInformationForTouch")]
        [Roles("TouchInvoiceTemplate1", "TouchInvoiceTemplate2", "TouchInvoiceTemplate3", "CanHoldTouchInvoices", "CanViewItem", "CashServiceInvoices", "CreditServiceInvoices")]
        public async Task<IActionResult> GetProductInformationForTouch(string searchTerm, Guid? wareHouseId, Guid? categoryId, bool isRaw)
        {
            var categoryListModel = await Mediator.Send(new GetProductListQueryForTouchScreen
            {
                SearchTerm = searchTerm,
                WareHouseId = wareHouseId,
                CategoryId = categoryId,
                IsRaw = isRaw,
            });
            return Ok(categoryListModel);
        }

        [Route("api/Product/GetProductForPromotionAndBundleList")]
        [HttpGet("GetProductForPromotionAndBundleList")]
        public async Task<IActionResult> GetProductForPromotionAndBundleList()
        {
            var categoryListModel = await Mediator.Send(new GetProductForPromotionAndBundleListQuery
            {
            });
            return Ok(categoryListModel);
        }

        [Route("api/Product/ProductDetailsViaId")]
        [HttpGet("ProductDetailsViaId")]
        [Roles("CanEditItem", "CanViewDetailItem")]
        public async Task<IActionResult> ProductDetailsViaId(Guid id, string barCode)
        {
            var size = await Mediator.Send(new GetProductDetailQuery()
            {
                Id = id,
                BarCode = barCode
            });
            return Ok(size);
        }

        [Route("api/Product/ProductCurrentQuantity")]
        [HttpGet("ProductCurrentQuantity")]
        //[Roles("CanEditItem", "CanViewDetailItem")]
        public async Task<IActionResult> ProductCurrentQuantity(Guid? branchId, Guid? wareHouseId)
        {
            if (wareHouseId!=null)
            {
                var item = await Context.Stocks.AsNoTracking().Where(x => x.BranchId==branchId&& x.WareHouseId==wareHouseId).ToListAsync();
                return Ok(item);
            }
            else
            {
                var item = await Context.Stocks.AsNoTracking().Where(x => x.BranchId==branchId).ToListAsync();
                return Ok(item);
            }
        }

        [Route("api/Product/ProductViewDetail")]
        [HttpGet("ProductViewDetail")]
        [Roles("CanEditItem", "CanViewDetailItem")]
        public async Task<IActionResult> ProductViewDetail(Guid id)
        {
            var size = await Mediator.Send(new ViewProductDetailQuery()
            {
                Id = id,
            });
            return Ok(size);
        }

        [Route("api/Product/DeleteProduct")]
        [HttpGet("DeleteProduct")]
        [Roles("CanDeleteProduct")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var category = await Mediator.Send(new DeleteProductCommand
            {
                Id = id
            });
            return Ok(category);

        }
        [Route("api/Product/GetChangeBarCodeType")]
        [HttpGet("GetChangeBarCodeType")]
        public async Task<IActionResult> GetChangeBarCodeType()
        {
            var proBarcodeType = await Mediator.Send(new GetChangeBarCodeTypeQuery());
            return Ok(proBarcodeType);
        }
        [Route("api/Product/SaveProductInformation")]
        [HttpPost("SaveProductInformation")]
        [Roles("CanAddItem", "CanEditItem", "CanAddReparingOrder", "CanEditReparingOrder")]
        public async Task<IActionResult> SaveProductInformation([FromBody] ProductVm categoryInformation, bool isBarCodeGenerated)
        {
            //var id = Guid.Empty;
            if (categoryInformation.Id == Guid.Empty)
            {
                var autoNo = await Mediator.Send(new GetProductsCodeQuery());
                var message = await Mediator.Send(new AddUpdateProductCommand
                {
                    Id = new Guid(),
                    Code = autoNo,
                    ProductMasterId = categoryInformation.ProductMasterId,
                    EnglishName = categoryInformation.EnglishName,
                    HsCode = categoryInformation.HsCode,
                    ArabicName = categoryInformation.ArabicName,
                    CategoryId = categoryInformation.CategoryId,
                    SubCategoryId = categoryInformation.SubCategoryId,
                    BrandId = categoryInformation.BrandId,
                    IsBarCodeGenerated = isBarCodeGenerated,
                    UnitId = categoryInformation.UnitId,
                    SizeId = categoryInformation.SizeId,
                    ColorId = categoryInformation.ColorId,
                    BarCode = categoryInformation.BarCode,
                    Length = categoryInformation.Length,
                    Width = categoryInformation.Width,
                    TaxRateId = categoryInformation.TaxRateId,
                    SalePrice = categoryInformation.SalePrice,
                    OriginId = categoryInformation.OriginId,
                    SaleReturnDays = categoryInformation.SaleReturnDays,
                    Description = categoryInformation.Description,
                    IsExpire = categoryInformation.IsExpire,
                    StockLevel = categoryInformation.StockLevel,
                    TaxMethod = categoryInformation.TaxMethod,
                    Shelf = categoryInformation.Shelf,
                    IsActive = categoryInformation.IsActive,
                    Image = categoryInformation.Image,
                    IsRaw = categoryInformation.IsRaw,
                    LevelOneUnit = categoryInformation.LevelOneUnit,
                    BasicUnit = categoryInformation.BasicUnit,
                    UnitPerPack = categoryInformation.UnitPerPack,
                    SalePriceUnit = categoryInformation.SalePriceUnit,
                    Assortment = categoryInformation.Assortment,
                    StyleNumber = categoryInformation.StyleNumber,
                    Serial = categoryInformation.Serial,
                    Guarantee = categoryInformation.Guarantee,
                    ServiceItem = categoryInformation.ServiceItem,
                    HighUnitPrice = categoryInformation.HighUnitPrice,
                    SizeIdList = categoryInformation.SizeIdList,
                    WholesalePrice = categoryInformation.WholesalePrice,
                    WholesaleQuantity = categoryInformation.WholesaleQuantity,
                    SchemeQuantity = categoryInformation.SchemeQuantity,
                    Scheme = categoryInformation.Scheme,
                    PurchasePrice = categoryInformation.PurchasePrice,
                    ProductGroupId = categoryInformation.ProductGroupId,
                    CostPrice = categoryInformation.CostPrice,
                    CostValue = categoryInformation.CostValue,
                    CostSign = categoryInformation.CostSign,
                    PriceRecords = categoryInformation.PriceRecords,
                    BranchesIdList = categoryInformation.BranchesIdList,
                    DisplayName = categoryInformation.DisplayName,
                    SKU = categoryInformation.SKU,
                    PartNumber = categoryInformation.PartNumber,
                    DisplayNameForPrint = categoryInformation.DisplayNameForPrint
                });
                if (message.Id != Guid.Empty)
                {
                    return Ok(new { message = message, Action = "Add" });
                }
                else
                {
                    return Ok(new { Message = message, Action = "Error" });
                }
            }
            else
            {
                var message = await Mediator.Send(new AddUpdateProductCommand
                {
                    Id = categoryInformation.Id,
                    ProductMasterId = categoryInformation.ProductMasterId,
                    Code = categoryInformation.Code,
                    HsCode = categoryInformation.HsCode,
                    EnglishName = categoryInformation.EnglishName,
                    ArabicName = categoryInformation.ArabicName,
                    CategoryId = categoryInformation.CategoryId,
                    SubCategoryId = categoryInformation.SubCategoryId,
                    BrandId = categoryInformation.BrandId,
                    UnitId = categoryInformation.UnitId,
                    SizeId = categoryInformation.SizeId,
                    ColorId = categoryInformation.ColorId,
                    BarCode = categoryInformation.BarCode,
                    Length = categoryInformation.Length,
                    Width = categoryInformation.Width,
                    TaxRateId = categoryInformation.TaxRateId,
                    SalePrice = categoryInformation.SalePrice,
                    OriginId = categoryInformation.OriginId,
                    IsBarCodeGenerated = isBarCodeGenerated,
                    StockLevel = categoryInformation.StockLevel,
                    TaxMethod = categoryInformation.TaxMethod,
                    SaleReturnDays = categoryInformation.SaleReturnDays,
                    Description = categoryInformation.Description,
                    Shelf = categoryInformation.Shelf,
                    IsExpire = categoryInformation.IsExpire,
                    IsActive = categoryInformation.IsActive,
                    Image = categoryInformation.Image,
                    DisplayNameForPrint = categoryInformation.DisplayNameForPrint,
                    IsRaw = categoryInformation.IsRaw,
                    LevelOneUnit = categoryInformation.LevelOneUnit,
                    BasicUnit = categoryInformation.BasicUnit,
                    UnitPerPack = categoryInformation.UnitPerPack,
                    SalePriceUnit = categoryInformation.SalePriceUnit,
                    Assortment = categoryInformation.Assortment,
                    StyleNumber = categoryInformation.StyleNumber,
                    Serial = categoryInformation.Serial,
                    Guarantee = categoryInformation.Guarantee,
                    ServiceItem = categoryInformation.ServiceItem,
                    HighUnitPrice = categoryInformation.HighUnitPrice,
                    SizeIdList = categoryInformation.SizeIdList,
                    WholesalePrice = categoryInformation.WholesalePrice,
                    WholesaleQuantity = categoryInformation.WholesaleQuantity,
                    SchemeQuantity = categoryInformation.SchemeQuantity,
                    Scheme = categoryInformation.Scheme,
                    PurchasePrice = categoryInformation.PurchasePrice,
                    ProductGroupId = categoryInformation.ProductGroupId,
                    CostPrice = categoryInformation.CostPrice,
                    CostValue = categoryInformation.CostValue,
                    CostSign = categoryInformation.CostSign,
                    PriceRecords = categoryInformation.PriceRecords,
                    BranchesIdList = categoryInformation.BranchesIdList,
                    DisplayName = categoryInformation.DisplayName,
                    SKU = categoryInformation.SKU,
                    PartNumber = categoryInformation.PartNumber
                });
                if (message.Id != Guid.Empty)
                {
                    return Ok(new { message = message, Action = "Update" });

                }
                else
                {
                    return Ok(new { Message = message, Action = "Error" });
                }
            }

        }

        [Route("api/Product/SaveQuickProduct")]
        [HttpPost("SaveQuickProduct")]
        [Roles("CanAddTouchQuickItem", "CanAddQuickItem", "CanAddReparingOrder", "CanEditReparingOrder", "CanViewItem", "CanAddPurchaseInvoice")]
        public async Task<IActionResult> SaveQuickProduct([FromBody] QuickProductVm quickProduct)
        {

            var autoNo = await Mediator.Send(new GetProductsCodeQuery());
            var message = await Mediator.Send(new AddUpdateProductCommand
            {
                Id = new Guid(),
                Code = autoNo,
                EnglishName = quickProduct.EnglishName,
                ArabicName = quickProduct.ArabicName,
                SalePrice = quickProduct.SalePrice,
                TaxMethod = quickProduct.TaxMethod,
                TaxRateId = quickProduct.TaxRateId,
                CategoryIdQuick = quickProduct.CategoryIdQuick,
                UnitId = quickProduct.UnitId,
                LevelOneUnit = quickProduct.LevelOneUnit,
                BarCode = quickProduct.Barcode,
                ServiceItem = quickProduct.ServiceItem,
                QuickProduct = true
            });
            var categoryListModel = await Mediator.Send(new GetProductListQuery
            {
                SearchTerm = autoNo,
            });
            if (message.Id != Guid.Empty)
            {
                return Ok(new { message = message, categoryList = categoryListModel, Action = "Add" });
            }
            else
            {
                return Ok(new { Message = message, Action = "Error" });
            }

        }
        #endregion

        #region Day Start

        [Route("api/Product/IsDayStart")]
        [HttpGet("IsDayStart")]
        public async Task<IActionResult> IsDayStart(Guid? userId, Guid? employeeId, bool isSupervisor)
        {

            var dayStart = await Mediator.Send(new IsDayStartQuery()
            {
                UserId = userId,
                EmployeeId = employeeId,
                IsSupervisor = isSupervisor,
            });

            return Ok(dayStart);
        }
        [Route("api/Product/GetDayStartDetail")]
        [HttpGet("GetDayStartDetail")]
        public async Task<IActionResult> GetDayStartDetail()
        {

            var dayStart = Context.DayStarts.ToList().LastOrDefault();
            var countername = Context.Terminals.ToList().Find(x => x.Id == dayStart.CounterId)?.Code;
            var lookup = new Focus.Business.DayStarts.Queries.IsDayStart.DayStartLookUpModel()
            {
                Date = dayStart.Date.ToString(),
                ToTime = dayStart.FromTime.ToString(),
                CounterName = countername,
                OpeningCash = dayStart.OpeningCash,
                TotalCash = dayStart.TotalCash,
                ExpenseCash = dayStart.ExpenseCash,
                DayEndUser = dayStart.DayEndUser,
                CarryCash = dayStart.CarryCash,
                BankAmount = dayStart.BankAmount,
                VerifyCash = dayStart.VerifyCash,
                CashInHand = dayStart.CashInHand,



            };
            return Ok(lookup);
        }
        [Route("api/Product/ReceivingCash")]
        [HttpPost("ReceivingCash")]
        [Roles("StartDay", "Noble Admin", "CloseDay", "User")]
        public async Task<IActionResult> ReceivingCash([FromBody] ReceivingCashModel rc)
        {

            var receivingAmount = await Mediator.Send(new ReceivingAmountCommand()
            {
                Id = rc.Id,
                Amount = rc.Amount,
                Reason = rc.Reason,
                Password = rc.Password,
                UserName = rc.UserName
            });

            return Ok(receivingAmount);
        }
        [Route("api/Product/GetDetailOfInvoices")]
        [HttpGet("GetDetailOfInvoices")]
        [Roles("StartDay", "Noble Admin", "CloseDay", "User")]
        public async Task<IActionResult> GetDetailOfInvoices()
        {

            var invoicesDetail = await Mediator.Send(new GetTotalInvoicesQuery()
            {

            });

            return Ok(invoicesDetail);
        }
        [Route("api/Product/GetDayStartReport")]
        [HttpGet("GetDetaGetDayStartReportilOfInvoices")]
        [Roles("StartDay", "Noble Admin", "CloseDay", "User")]
        public async Task<IActionResult> GetDayStartReport()
        {

            var invoicesDetail = await Mediator.Send(new GetTotalInvoicesQuery()
            {

            });

            return Ok(invoicesDetail);
        }

        [Route("api/Product/DayStartReport")]
        [HttpGet("DayStartReport")]
        [Roles("StartDay", "Noble Admin", "CloseDay", "User")]
        public async Task<IActionResult> DayStartReport(DateTime from, DateTime to)
        {
            var companyId = ContextProvider.GetCompanyId();

            var data = new IsDayStartLookupModel();
            var cashInHand = "";
            var bank = "";
            var expense = "";

            SqlCommand cmd;
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var tra = connection.BeginTransaction();
                var query = "declare @dayStartId uniqueidentifier " +
                    "declare @cashAccountId uniqueidentifier " +
                    "declare @bankId uniqueidentifier " +
                    "select top 1 @cashAccountId = CashAccountId, @bankId = accountId from terminals trm where companyId = '" + companyId + "' " +
                    "select Sum(debit -credit) cashInHand from transactions where companyId = '" + companyId + "' and [date] >=  Cast('" + from.ToString("yyyy-MM-dd", new CultureInfo("en-US").DateTimeFormat) + "' as datetime)  and [date] <=  Cast('" + to.ToString("yyyy-MM-dd", new CultureInfo("en-US").DateTimeFormat) + "'  as datetime) and AccountId = @cashAccountId " +
                    "select Sum(debit -credit) bank from transactions where companyId = '" + companyId + "' and [date] >=  Cast('" + from.ToString("yyyy-MM-dd", new CultureInfo("en-US").DateTimeFormat) + "' as datetime)  and [date] <=  Cast('" + to.ToString("yyyy-MM-dd", new CultureInfo("en-US").DateTimeFormat) + "'  as datetime)  and AccountId = @bankId " +
                    "select sum(ded.Amount) expense from DailyExpenses de join dailyexpensedetails ded on ded.dailyExpenseId = de.Id where de.companyId = '" + companyId + "' and [date] >=  Cast('" + from.ToString("yyyy-MM-dd", new CultureInfo("en-US").DateTimeFormat) + "' as datetime)  and [date] <=  Cast('" + to.ToString("yyyy-MM-dd", new CultureInfo("en-US").DateTimeFormat) + "'  as datetime) ";
                cmd = new SqlCommand(query, connection, tra);
                da.SelectCommand = (SqlCommand)cmd;

                da.Fill(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    data.CashInHand = Convert.ToInt32(dr["cashInHand"]);
                }

                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    data.Bank = Convert.ToInt32(dr["bank"]);
                }

                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    data.Expense = Convert.ToInt32(dr["expense"]);
                }
                tra.Commit();
                connection.Close();
            }

            return Ok(data);
        }

        [Route("api/Product/GetCounterInformation")]
        [HttpGet("GetCounterInformation")]
        [Roles("StartDay", "Noble Admin", "CloseDay", "User")]
        public async Task<IActionResult> GetCounterInformation(Guid? userId, Guid? employeeId, bool isDayStart, bool isDayEnd)
        {
            var dayStart = await Mediator.Send(new GetCounterInformationQuery()
            {
                UserId = userId,
                EmployeeId = employeeId,
                IsDayStart = isDayStart,
                IsDayEnd = isDayEnd
            });

            return Ok(dayStart);
        }

        [Route("api/Product/DayCounterInformation")]
        [HttpPost("DayCounterInformation")]
        [Roles("Day Counter", "Noble Admin", "User")]
        public async Task<IActionResult> DayCounterInformation([FromBody] List<CounterVm> counters)
        {
            var counterList = counters.Select(x => new CounterLookupModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            var result = await Mediator.Send(new GetCounterDetailsQuery
            {
                Counters = counterList
            });

            return Ok(result);
        }

        [Route("api/Product/SaveDayStart")]
        [HttpPost("SaveDayStart")]
        [Roles("StartDay", "Noble Admin", "CloseDay")]
        public async Task<IActionResult> SaveDayStart([FromBody] DayStartVm dyStartVm, bool isDayStart)
        {
            DayStartLookupModel result;
            if (isDayStart)

            {
                if (dyStartVm.IsTransfer)
                {
                    var result3 = await Mediator.Send(new TransferDayCommand
                    {
                        Id = new Guid(),
                        Date = dyStartVm.Date,
                        SaleId = dyStartVm.SaleId,
                        CounterId = dyStartVm.CounterId,
                        LocationId = dyStartVm.LocationId,
                        OpeningCash = dyStartVm.OpeningCash,
                        VerifyCash = dyStartVm.VerifyCash,
                        FromTime = dyStartVm.FromTime,
                        ToTime = dyStartVm.ToTime,
                        IsActive = dyStartVm.IsActive,
                        User = dyStartVm.User,
                        Password = dyStartVm.Password,
                        ToUser = dyStartVm.ToUser,
                        ToPassword = dyStartVm.ToPassword,
                        CarryCash = dyStartVm.CarryCash,
                        CashInHand = dyStartVm.CashInHand,
                        IsExpenseDay = dyStartVm.IsExpenseDay,
                        IsOpenDay = dyStartVm.IsOpenDay,
                        CreditReason = dyStartVm.CreditReason
                    });
                    return Ok(new { IsSuccess = true, result3 });

                }
                else if (dyStartVm.Id == Guid.Empty)
                {
                    result = await Mediator.Send(new AddUpdateDayStartsCommand
                    {
                        Id = new Guid(),
                        Date = dyStartVm.Date,
                        SaleId = dyStartVm.SaleId,
                        CounterId = dyStartVm.CounterId,
                        LocationId = dyStartVm.LocationId,
                        OpeningCash = dyStartVm.OpeningCash,
                        VerifyCash = dyStartVm.VerifyCash,
                        FromTime = dyStartVm.FromTime,
                        ToTime = dyStartVm.ToTime,
                        IsActive = dyStartVm.IsActive,
                        User = dyStartVm.User,
                        Password = dyStartVm.Password,
                        IsDayStart = isDayStart,
                        Reason = dyStartVm.Reason,
                        CarryCash = dyStartVm.CarryCash,
                        CashInHand = dyStartVm.CashInHand,
                        IsExpenseDay = dyStartVm.IsExpenseDay,
                        IsFirstUser = dyStartVm.IsFirstUser,
                        SupervisorCash = dyStartVm.SupervisorCash,
                        ExpenseCash = dyStartVm.ExpenseCash,
                        IsOpenDay = dyStartVm.IsOpenDay,
                        IsSupervisorLogin = dyStartVm.IsSupervisorLogin,


                    });
                    return Ok(new { IsSuccess = true, result });

                }
                else
                {
                    result = await Mediator.Send(new AddUpdateDayStartsCommand
                    {
                        Id = dyStartVm.Id,
                        Date = dyStartVm.Date,
                        SaleId = dyStartVm.SaleId,
                        CounterId = dyStartVm.CounterId,
                        LocationId = dyStartVm.LocationId,
                        OpeningCash = dyStartVm.OpeningCash,
                        VerifyCash = dyStartVm.VerifyCash,
                        SupervisorCash = dyStartVm.SupervisorCash,
                        ExpenseCash = dyStartVm.ExpenseCash,
                        FromTime = dyStartVm.FromTime,
                        ToTime = dyStartVm.ToTime,
                        IsActive = dyStartVm.IsActive,
                        User = dyStartVm.User,
                        Password = dyStartVm.Password,
                        IsDayStart = isDayStart,
                        Reason = dyStartVm.Reason,
                        CarryCash = dyStartVm.CarryCash,
                        CashInHand = dyStartVm.CashInHand,
                        IsExpenseDay = dyStartVm.IsExpenseDay,
                        IsOpenDay = dyStartVm.IsOpenDay,
                        IsSupervisorLogin = dyStartVm.IsSupervisorLogin,


                    });
                    return Ok(new { IsSuccess = true, result });

                }
            }
            else
            {
                var result1 = await Mediator.Send(new AddUpdateDayEndCommand
                {
                    Id = dyStartVm.Id,
                    Date = dyStartVm.Date,
                    SaleId = dyStartVm.SaleId,
                    CounterId = dyStartVm.CounterId,
                    LocationId = dyStartVm.LocationId,
                    OpeningCash = dyStartVm.OpeningCash,
                    VerifyCash = dyStartVm.VerifyCash,
                    Expense = dyStartVm.ExpenseCash,
                    FromTime = dyStartVm.FromTime,
                    ToTime = dyStartVm.ToTime,
                    IsActive = dyStartVm.IsActive,
                    User = dyStartVm.User,
                    Password = dyStartVm.Password,
                    Bank = dyStartVm.BankAmount,
                    CarryCash = dyStartVm.CarryCash,
                    CashInHand = dyStartVm.CashInHand,
                    EndTerminalBy = dyStartVm.EndTerminalBy,
                    SupervisorName = dyStartVm.SupervisorName,
                    SupervisorPassword = dyStartVm.SupervisorPassword,
                    IsSupervisor = dyStartVm.IsSupervisor,
                    CreditReason = dyStartVm.CreditReason,
                    IsOpenDay = dyStartVm.IsOpenDay,
                    RemainingAmount = dyStartVm.RemainingAmount,

                });

            }

            return Ok(new { IsSuccess = true });
        }

        [Route("api/Product/OpeningBalance")]
        [HttpGet("OpeningBalance")]
        [Roles("StartDay", "Noble Admin", "CloseDay", "User")]
        public async Task<IActionResult> OpeningBalance(Guid counterId, Guid? dayId, bool isReport, bool isOpeningCash, DateTime? fromTime, DateTime? toTime)
        {
            var result = await Mediator.Send(new GetOpeningBalanceQuery
            {
                Id = counterId,
                IsOpeningCash = isOpeningCash,
                FromTime = fromTime,
                ToTime = toTime,
                DayId = dayId,
                IsReport = isReport

            });

            return Ok(result);
        }

        [Route("api/Product/GetTerminalByUserId")]
        [HttpGet("GetTerminalByUserId")]
        [Roles("StartDay", "Noble Admin", "CloseDay", "User")]
        public async Task<IActionResult> GetTerminalByUserId()
        {
            var user = User.Identity.UserId();
            var result = await Mediator.Send(new GetTerminalOfCurrentUserQuery()
            {
                Id = user
            });

            return Ok(result);
        }


        [Route("api/Product/SupervisorLogin")]
        [HttpPost("SupervisorLogin")]
        //[Roles("StartDay", "Noble Admin", "CloseDay", "User")]
        public async Task<IActionResult> SupervisorLogin([FromBody] SupervisorLoginModel loginModel)
        {

            var result = await Mediator.Send(new SupervisorLoginQuery()
            {
                Email = loginModel.Email,
                Password = loginModel.Password,
                IsFlushData = loginModel.IsFlushData
            });

            return Ok(result);
        }

        [Route("api/Product/DayStartList")]
        [HttpPost("DayStartList")]
        [Roles("StartDay", "Noble Admin", "CloseDay", "User")]

        public async Task<IActionResult> DayStartList()
        {

            var dayStart = await Mediator.Send(new GetDayStartListQuery());
            return Ok(dayStart);
        }

        [Route("api/Product/WholeSaleDayStart")]
        [HttpGet("WholeSaleDayStart")]
        [Roles("StartDay", "Noble Admin", "CloseDay", "User")]

        public async Task<IActionResult> WholeSaleDayStart()
        {

            var dayStart = await Mediator.Send(new GetTransactionDetail());
            return Ok(dayStart);
        }



        [Route("api/Product/AddWholeSaleDayStart")]
        [HttpGet("AddWholeSaleDayStart")]
        [Roles("StartDay", "Noble Admin", "CloseDay")]
        public async Task<IActionResult> AddWholeSaleDayStart(Guid? id)
        {
            var dayStart = await Mediator.Send(new AddUpdateWholeSaleDay()
            {
                Id = id
            });
            return Ok(dayStart);

        }

        [Route("api/Product/IsWholeSaleDayStart")]
        [HttpGet("IsWholeSaleDayStart")]
        [Roles("StartDay", "Noble Admin", "CloseDay")]
        public async Task<IActionResult> IsWholeSaleDayStart()
        {
            var dayStart = await Mediator.Send(new IsWholeSaleDayStart());
            return Ok(dayStart);

        }
        [Route("api/Product/GetTransactionDetailWholeSale")]
        [HttpGet("GetTransactionDetailWholeSale")]
        [Roles("StartDay", "Noble Admin", "CloseDay")]
        public async Task<IActionResult> GetTransactionDetailWholeSale(Guid id)
        {
            var dayStart = await Mediator.Send(new GetTransactionDetail()
            {
                Id = id
            });
            return Ok(dayStart);

        }
        #endregion

        #region For Promotion-Discount Offers

        [Route("api/Product/GetPromotionProducts")]
        [HttpGet("GetPromotionProducts")]
        [Roles("CanViewPromotionOffer", "Noble Admin")]
        public async Task<IActionResult> GetPromotionProducts(bool status, bool type, string searchTerm, int? pageNumber)
        {
            if (status == true)
            {
                var promotionOffersListModel = await Mediator.Send(new GetPromotionProductListQuery()
                {
                    SearchTerm = searchTerm,
                    PageNumber = pageNumber ?? 1,
                    Type = type
                });
                return Ok(promotionOffersListModel);
            }
            if (status == false)
            {
                var promotionOffersListModel = await Mediator.Send(new GetBundleCategoryProductListQuery()
                {
                    SearchTerm = searchTerm,
                    PageNumber = pageNumber ?? 1,
                    Type = type
                });
                return Ok(promotionOffersListModel);
            }
            return Ok(null);
        }
        [Route("api/Product/GetPromotion")]
        [HttpGet("GetPromotion")]
        [Roles("CanViewPromotionOffer")]
        public async Task<IActionResult> GetPromotion(string status, string searchTerm, int? pageNumber, bool isDropdown, Guid? branchId, Guid? bundleBranches)
        {
            var promotionOffersListModel = await Mediator.Send(new GetPromotionOffersListQuery()
            {
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
                Status = status,
                IsDropdown = isDropdown,
                BranchId = branchId,
                BundleBranches = bundleBranches
            });
            return Ok(promotionOffersListModel);
        }
        [Route("api/Product/PromotionDetailsViaId")]
        [HttpGet("PromotionDetailsViaId")]
        [Roles("CanEditPromotionOffer")]
        public async Task<IActionResult> PromotionDetailsViaId(Guid id)
        {
            var promotionOffers = await Mediator.Send(new GetPromotionOffersDetailQuery()
            {
                Id = id
            });
            return Ok(promotionOffers);
        }
        [Route("api/Product/SavePromotionOffer")]
        [HttpPost("SavePromotionOffer")]
        [Roles("CanAddPromotionOffer", "CanEditPromotionOffer")]
        public async Task<IActionResult> SavePromotionOffer([FromBody] PromotionOfferVm promotion)
        {
            if (promotion.Id == Guid.Empty)
            {
                var message = await Mediator.Send(new AddUpdatePromotionOfferCommand
                {
                    Id = new Guid(),
                    Offer = promotion.Offer,
                    Discount = promotion.Discount,
                    DiscountType = promotion.DiscountType,
                    FromDate = promotion.FromDate,
                    ToDate = promotion.ToDate,
                    BaseQuantity = promotion.BaseQuantity,
                    UpToQuantity = promotion.UpToQuantity,
                    IncludingBaseQuantity = promotion.IncludingBaseQuantity,
                    StockLimit = promotion.StockLimit,
                    ProductId = promotion.ProductId,
                    IsActive = promotion.IsActive,

                    Sunday = promotion.Sunday,
                    Monday = promotion.Monday,
                    Tuesday = promotion.Tuesday,
                    Wednesday = promotion.Wednesday,
                    Thursday = promotion.Thursday,
                    Friday = promotion.Friday,
                    Saturday = promotion.Saturday,
                    Buy = promotion.Buy,
                    Get = promotion.Get,
                    PromotionType = promotion.PromotionType,
                    ProductGroupId = promotion.ProductGroupId,
                    GetProductId = promotion.GetProductId,
                    BranchId = promotion.BranchId,
                    BranchesIdList = promotion.BranchesIdList,
                });
                if (message.Id != Guid.Empty)
                {
                    return Ok(new { message = message, Action = "Add" });

                }
                else
                {
                    return Ok(new { Message = message, Action = "Error" });
                }
            }
            else
            {
                var message = await Mediator.Send(new AddUpdatePromotionOfferCommand
                {
                    Id = promotion.Id,
                    Offer = promotion.Offer,
                    Discount = promotion.Discount,
                    DiscountType = promotion.DiscountType,
                    FromDate = promotion.FromDate,
                    ToDate = promotion.ToDate,
                    BaseQuantity = promotion.BaseQuantity,
                    UpToQuantity = promotion.UpToQuantity,
                    IncludingBaseQuantity = promotion.IncludingBaseQuantity,
                    StockLimit = promotion.StockLimit,
                    ProductId = promotion.ProductId,
                    IsActive = promotion.IsActive,

                    Sunday = promotion.Sunday,
                    Monday = promotion.Monday,
                    Tuesday = promotion.Tuesday,
                    Wednesday = promotion.Wednesday,
                    Thursday = promotion.Thursday,
                    Friday = promotion.Friday,
                    Saturday = promotion.Saturday,
                    Buy = promotion.Buy,
                    Get = promotion.Get,
                    PromotionType = promotion.PromotionType,
                    ProductGroupId = promotion.ProductGroupId,
                    GetProductId = promotion.GetProductId,
                    BranchId = promotion.BranchId,
                    BranchesIdList = promotion.BranchesIdList,
                });
                if (message.Id != Guid.Empty)
                {
                    return Ok(new { message = message, Action = "Update" });

                }
                else
                {
                    return Ok(new { Message = message, Action = "Error" });
                }
            }
        }
        #endregion

        #region For Currency

        [Route("api/Product/SaveCurrency")]
        [HttpPost("SaveCurrency")]


        public async Task<IActionResult> SaveCurrency([FromBody] CurrencyVm currencyVm)
        {
            Guid id;
            if (currencyVm.Id == Guid.Empty)
            {
                id = await Mediator.Send(new AddUpdateCurrencyCommand
                {
                    Id = new Guid(),
                    Name = currencyVm.Name,
                    NameArabic = currencyVm.NameArabic,
                    Sign = currencyVm.Sign,
                    ArabicSign = currencyVm.ArabicSign,
                    IsActive = currencyVm.IsActive,
                    Image = currencyVm.Image,
                    Setup = currencyVm.Setup
                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateCurrencyCommand
                {
                    Id = currencyVm.Id,
                    Name = currencyVm.Name,
                    NameArabic = currencyVm.NameArabic,
                    Sign = currencyVm.Sign,
                    ArabicSign = currencyVm.ArabicSign,
                    IsActive = currencyVm.IsActive,
                    Image = currencyVm.Image
                });
            }
            if (id != Guid.Empty)
            {
                var currency = await Mediator.Send(new GetCurrencyDetailQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, currency });
            }

            return Ok(new { IsSuccess = false });
        }
        [Route("api/Product/CurrencyList")]
        [HttpGet("CurrencyList")]
        [Roles("CanViewCurrency", "TouchInvoiceTemplate1", "TouchInvoiceTemplate2", "TouchInvoiceTemplate3", "CanHoldServiceInvoices", "CashServiceInvoices", "CreditServiceInvoices", "CreditInvoices", "CashInvoices", "CanHoldInvoices", "CanEditHoldInvoices", "CanAddSupplier", "CanEditSupplier")]

        public async Task<IActionResult> CurrencyList()
        {
            var currency = await Mediator.Send(new GetCurrencyListQuery());
            return Ok(currency);
        }
        [Route("api/Product/CurrencyDelete")]
        [HttpGet("CurrencyDelete")]
        [Roles("Can Delete Currency", "Noble Admin")]
        public async Task<IActionResult> CurrencyDelete(Guid id)
        {
            var currencyId = await Mediator.Send(new DeleteCurrencyCommand
            {
                Id = id
            });
            return Ok(currencyId);

        }
        [Route("api/Product/CurrencyDetail")]
        [HttpGet("CurrencyDetail")]
        [Roles("CanEditCurrency", "CanAddCurrency")]
        public async Task<IActionResult> CurrencyDetail(Guid id)
        {
            var currency = await Mediator.Send(new GetCurrencyDetailQuery()
            {
                Id = id
            });
            return Ok(currency);

        }
        #endregion

        #region For Payment Options

        [Route("api/Product/SavePaymentOptions")]
        [HttpPost("SavePaymentOptions")]
        [Roles("CanEditPaymentOption", "CanAddPaymentOption")]
        public async Task<IActionResult> SavePaymentOptions([FromBody] PaymentOptionsVm paymentOptionsVm, string image)
        {
            Guid id;
            if (paymentOptionsVm.Id == Guid.Empty)
            {
                id = await Mediator.Send(new AddUpdatePaymentOptionsCommand
                {
                    Id = new Guid(),
                    Name = paymentOptionsVm.Name,
                    NameArabic = paymentOptionsVm.NameArabic,
                    Image = image,
                    IsActive = paymentOptionsVm.IsActive
                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdatePaymentOptionsCommand
                {
                    Id = paymentOptionsVm.Id,
                    Name = paymentOptionsVm.Name,
                    NameArabic = paymentOptionsVm.NameArabic,
                    Image = image,
                    IsActive = paymentOptionsVm.IsActive
                });
            }
            if (id != Guid.Empty)
            {
                var paymentOptions = await Mediator.Send(new GetPaymentOptionsDetailQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, paymentOptions = paymentOptions });
            }

            return Ok(new { IsSuccess = false });
        }
        [Route("api/Product/PaymentOptionsList")]
        [HttpGet("PaymentOptionsList")]
        [Roles("CanViewPaymentOption", "CashInvoices", "CreditInvoices", "CanHoldTouchInvoices", "TouchInvoiceTemplate1", "TouchInvoiceTemplate2", "TouchInvoiceTemplate3", "CanHoldServiceInvoices", "CashServiceInvoices", "CreditServiceInvoices")]
        public async Task<IActionResult> PaymentOptionsList(bool isActive)
        {
            var paymentOptions = await Mediator.Send(new GetPaymentOptionsListQuery
            { IsActive = isActive }
            );
            return Ok(paymentOptions);
        }
        [Route("api/Product/PaymentOptionsDelete")]
        [HttpGet("PaymentOptionsDelete")]
        [Roles("Can Delete Payment Option", "Noble Admin")]
        public async Task<IActionResult> PaymentOptionsDelete(Guid id)
        {
            var paymentOptionsId = await Mediator.Send(new DeletePaymentOptionsCommand
            {
                Id = id
            });
            return Ok(paymentOptionsId);
        }
        [Route("api/Product/PaymentOptionsDetail")]
        [HttpGet("PaymentOptionsDetail")]
        [Roles("CanEditPaymentOption", "CanAddPaymentOption")]
        public async Task<IActionResult> PaymentOptionsDetail(Guid id)
        {
            var paymentOptions = await Mediator.Send(new GetPaymentOptionsDetailQuery()
            {
                Id = id
            });
            return Ok(paymentOptions);

        }
        #endregion

        #region For Promotion-Bundles Offers

        [Route("api/Product/GetBundleCategoryItemsList")]
        [HttpGet("GetBundleCategoryItemsList")]
        [Roles("CanViewBundleOffer")]
        public async Task<IActionResult> GetBundleCategoryItemsList(string status, string searchTerm, int? pageNumber, bool isDropdown, Guid? branchId, Guid? bundleBranches)
        {
            var promotionOffersListModel = await Mediator.Send(new GetBundleCategoryListQuery()
            {
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
                Status = status,
                IsDropdown = isDropdown,
                BranchId = branchId,
                BundleBranches = bundleBranches,
            });
            return Ok(promotionOffersListModel);
        }
        [Route("api/Product/BundleCategoryItemsDetailsViaId")]
        [HttpGet("BundleCategoryItemsDetailsViaId")]
        [Roles("CanEditBundleOffer")]
        public async Task<IActionResult> BundleCategoryItemsDetailsViaId(Guid Id)
        {
            var promotionOffers = await Mediator.Send(new GetBundleCategoryDetailQuery()
            {
                Id = Id
            });
            return Ok(promotionOffers);
        }
        [Route("api/Product/SaveBundleCategoryItems")]
        [HttpPost("SaveBundleCategoryItems")]
        [Roles("CanAddBundleOffer", "CanEditBundleOffer")]

        public async Task<IActionResult> SaveBundleCategoryItems([FromBody] BundleCategoryVM promotion)
        {
            if (promotion.Id == Guid.Empty)
            {
                var message = await Mediator.Send(new AddUpdateBundleCategoryCommand
                {
                    Id = new Guid(),
                    Offer = promotion.Offer,
                    Buy = promotion.Buy,
                    Get = promotion.Get,
                    ProductId = promotion.ProductId,
                    isActive = promotion.isActive,
                    quantityLimit = promotion.quantityLimit,
                    StockLimit = promotion.StockLimit,
                    FromDate = promotion.FromDate,
                    ToDate = promotion.ToDate,
                    BranchId = promotion.BranchId,
                    BranchesIdList = promotion.BranchesIdList,
                });
                if (message.Id != Guid.Empty)
                {
                    return Ok(new { message = message, Action = "Add" });

                }
                else
                {
                    return Ok(new { Message = message, Action = "Error" });
                }
            }
            else
            {
                var message = await Mediator.Send(new AddUpdateBundleCategoryCommand
                {
                    Id = promotion.Id,
                    Offer = promotion.Offer,
                    Buy = promotion.Buy,
                    Get = promotion.Get,
                    ProductId = promotion.ProductId,
                    isActive = promotion.isActive,
                    FromDate = promotion.FromDate,
                    ToDate = promotion.ToDate,
                    StockLimit = promotion.StockLimit,
                    quantityLimit = promotion.quantityLimit,
                    BranchId = promotion.BranchId,
                    BranchesIdList = promotion.BranchesIdList,
                });
                if (message.Id != Guid.Empty)
                {
                    return Ok(new { message = message, Action = "Update" });

                }
                else
                {
                    return Ok(new { Message = message, Action = "Error" });
                }
            }
        }
        #endregion

        #region For Stock Adjustment
        [DisplayName("StockAdjustment Code")]
        [Route("api/Product/StockAdjustmentCode")]
        [HttpGet("StockAdjustmentCode")]
        [Roles("CanAddStockIn", "CanDraftStockIn", "CanAddStockOut", "CanDraftStockOut")]

        public async Task<IActionResult> StockAdjustmentCode(StockAdjustmentType stockAdjustmentType, Guid? branchId)
        {
            var autoNo = await Mediator.Send(new GetStockAdjustmentNumberQuery
            {
                StockAdjustmentType = stockAdjustmentType,
                BranchId = branchId,
            });
            return Ok(autoNo);
        }
        [DisplayName("Inventory Last Price")]
        [Route("api/Product/InventoryLastPrice")]
        [HttpGet("InventoryLastPrice")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> InventoryLastPrice(Guid productId)
        {
            var lastPrice = await Mediator.Send(new GetLatestInventoryQuery
            {
                ProductId = productId,
                //TransactionType = 0
                WareHouseId = Guid.Empty
            });
            return Ok(lastPrice);
        }
        [DisplayName("StockAdjustment List")]
        [Route("api/Product/StockAdjustmentList")]
        [HttpGet("StockAdjustmentList")]
        [Roles("CanDraftStockIn", "CanViewStockIn", "CanDraftStockOut", "CanViewStockOut")]

        public async Task<IActionResult> StockAdjustmentList(string searchTerm, int? pageNumber, bool status, bool isDropdown, StockAdjustmentType stockAdjustmentType, DateTime? fromDate, DateTime? toDate, Guid? branchId)
        {
            var list = await Mediator.Send(new GetStockAdjustmentListQuery
            {
                SearchTerm = searchTerm,
                Status = status,
                IsDropDown = isDropdown,
                StockAdjustmentType = stockAdjustmentType,
                PageNumber = pageNumber ?? 1,
                FromDate = fromDate,
                ToDate = toDate,
                BranchId = branchId,
            });

            return Ok(list);
        }
        [DisplayName("Add StockAdjustment")]
        [Route("api/Product/AddStockAdjustment")]
        [HttpPost("AddStockAdjustment")]
        [Roles("CanAddStockIn", "CanEditStockIn", "CanDraftStockIn", "CanAddStockOut", "CanEditStockOut", "CanDraftStockOut")]
        public async Task<IActionResult> AddStockAdjustment([FromBody] StockAdjustmentVm stockAdjustment)
        {
            var id = stockAdjustment.Id;
            if (id == Guid.Empty)
            {
                var autoNo = await Mediator.Send(new GetStockAdjustmentNumberQuery
                {
                    StockAdjustmentType = stockAdjustment.stockAdjustmentType,
                    BranchId = stockAdjustment.BranchId
                });

                var batchPaymentItems = new List<StockAdjustmentDetail>();
                if (stockAdjustment.stockAdjustmentType == StockAdjustmentType.StockIn)
                {
                    foreach (var item in stockAdjustment.StockAdjustmentDetails)
                    {
                        if (item.IsSerial && !string.IsNullOrEmpty(item.SerialNo))
                        {
                            for (int i = 0; i < item.Quantity; i++)
                            {
                                string serialNo = item.SerialNo;
                                string lastFragment = serialNo.Split('-').Last();
                                string firstFragment = serialNo.Substring(0, serialNo.Length - lastFragment.Length);
                                Int32 autoNos = Convert.ToInt32(lastFragment) + i;
                                var format = "";

                                for (int j = 0; j < lastFragment.Length; j++)
                                {
                                    format += "0";
                                }

                                var prefix = firstFragment;
                                var newCode = prefix + autoNos.ToString(format);

                                var pbItem = new StockAdjustmentDetail()
                                {
                                    Id = item.Id,
                                    WarehouseId = stockAdjustment.WarehouseId,
                                    Price = item.Price,
                                    Quantity = 1,
                                    ProductId = item.ProductId,
                                    Serial = item.Serial,
                                    WarrantyTypeId = item.WarrantyTypeId,
                                    GuaranteeDate = item.GuaranteeDate,
                                    SerialNo = Convert.ToString(newCode),
                                    IsSerial = item.IsSerial,
                                };
                                batchPaymentItems.Add(pbItem);
                            }

                        }
                        else
                        {
                            var pbItem = new StockAdjustmentDetail()
                            {
                                Id = item.Id,
                                WarehouseId = stockAdjustment.WarehouseId,
                                Price = item.Price,
                                Quantity = item.Quantity,
                                ProductId = item.ProductId,
                                Serial = item.Serial,
                                GuaranteeDate = item.GuaranteeDate,
                                IsSerial = item.IsSerial,
                                BatchExpiry = item.BatchExpiry,
                                BatchNo = item.BatchNo,
                            };
                            batchPaymentItems.Add(pbItem);
                        }
                    }
                }
                else
                {
                    foreach (var item in stockAdjustment.StockAdjustmentDetails)
                    {
                        var pbItem = new StockAdjustmentDetail()
                        {
                            Id = item.Id,
                            WarehouseId = stockAdjustment.WarehouseId,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            ProductId = item.ProductId,
                            Serial = item.Serial,
                            GuaranteeDate = item.GuaranteeDate,
                            IsSerial = item.IsSerial,
                            BatchExpiry = item.BatchExpiry,
                            BatchNo = item.BatchNo,
                        };
                        batchPaymentItems.Add(pbItem);
                    }
                }

                var message = await Mediator.Send(new AddUpdateStockAdjustmentCommand
                {
                    Date = stockAdjustment.Date,
                    Code = autoNo,
                    Reason = stockAdjustment.Reason,
                    Narration = stockAdjustment.Narration,
                    WarehouseId = stockAdjustment.WarehouseId,
                    StockAdjustmentType = stockAdjustment.stockAdjustmentType,
                    IsDraft = stockAdjustment.isDraft,
                    TaxMethod = stockAdjustment.TaxMethod,
                    TaxRateId = stockAdjustment.TaxRateId,
                    IsSerial = stockAdjustment.IsSerial,
                    BankCashAccountId = stockAdjustment.BankCashAccountId,
                    AttachmentList = stockAdjustment.AttachmentList,
                    BranchId = stockAdjustment.BranchId,
                    StockAdjustmentDetails = batchPaymentItems,
                });
                if (message.Id != Guid.Empty)
                {
                    return Ok(new { message = message, Action = "Add" });

                }
                else
                {
                    return Ok(new { Message = message, Action = "Error" });
                }
            }
            else
            {
                var batchPaymentItems = new List<StockAdjustmentDetail>();
                foreach (var item in stockAdjustment.StockAdjustmentDetails)
                {
                    if (stockAdjustment.stockAdjustmentType == StockAdjustmentType.StockIn)
                    {
                        var pbItem = new StockAdjustmentDetail()
                        {
                            Id = item.Id,
                            WarehouseId = stockAdjustment.WarehouseId,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            ProductId = item.ProductId,
                            Serial = item.Serial,
                            WarrantyTypeId = item.WarrantyTypeId,
                            GuaranteeDate = item.GuaranteeDate,
                            SerialNo = item.SerialNo,
                            IsSerial = item.IsSerial,
                            BatchExpiry = item.BatchExpiry,
                            BatchNo = item.BatchNo,
                        };
                        batchPaymentItems.Add(pbItem);
                    }
                    else
                    {
                        var pbItem = new StockAdjustmentDetail()
                        {
                            Id = item.Id,
                            WarehouseId = stockAdjustment.WarehouseId,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            ProductId = item.ProductId,
                            Serial = item.Serial,
                            GuaranteeDate = item.GuaranteeDate,
                            IsSerial = item.IsSerial,
                            BatchExpiry = item.BatchExpiry,
                            BatchNo = item.BatchNo,
                        };
                        batchPaymentItems.Add(pbItem);
                    }
                }
                var message = await Mediator.Send(new AddUpdateStockAdjustmentCommand
                {
                    Id = stockAdjustment.Id,
                    Date = stockAdjustment.Date,
                    Code = stockAdjustment.Code,
                    Reason = stockAdjustment.Reason,
                    Narration = stockAdjustment.Narration,
                    WarehouseId = stockAdjustment.WarehouseId,
                    StockAdjustmentType = stockAdjustment.stockAdjustmentType,
                    IsDraft = stockAdjustment.isDraft,
                    TaxMethod = stockAdjustment.TaxMethod,
                    TaxRateId = stockAdjustment.TaxRateId,
                    IsSerial = stockAdjustment.IsSerial,
                    BankCashAccountId = stockAdjustment.BankCashAccountId,
                    AttachmentList = stockAdjustment.AttachmentList,
                    StockAdjustmentDetails = batchPaymentItems
                });
                if (message.Id != Guid.Empty)
                {
                    return Ok(new { message = message, Action = "Update" });

                }
                else
                {
                    return Ok(new { Message = message, Action = "Error" });
                }
            }
        }
        [DisplayName("StockAdjustment Delete")]
        [Route("api/Product/StockAdjustmentDelete")]
        [HttpGet("StockAdjustmentDelete")]
        [Roles("Can Delete Stock In as Draft")]

        public async Task<IActionResult> StockAdjustmentDelete(Guid id)
        {
            var stockAdjustment = await Mediator.Send(new DeleteStockAdjustmentCommand
            {
                Id = id
            });
            return Ok(new { Message = stockAdjustment });
        }
        [DisplayName("StockAdjustment Detail Query")]
        [Route("api/Product/StockAdjustmentDetails")]
        [Route("StockAdjustmentDetails")]
        [Roles("CanViewDetailStockIn", "CanPrintStockIn", "CanEditStockIn", "CanViewDetailStockOut", "CanEditStockOut", "CanPrintStockOut")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> StockAdjustmentDetails(Guid id, bool isFifo, int openBatch)
        {
            var stockAdjustmentId = await Mediator.Send(new StockAdjustmentDetailQuery
            {
                Id = id,
                IsFifo = isFifo,
                OpenBatch = openBatch
            });
            return Ok(stockAdjustmentId);
        }
        #endregion

        #region Import

        [Route("api/Product/UploadFilesForImportCategory")]
        [HttpPost("UploadFilesForImportCategory")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UploadFilesForImportCategory([FromForm] IFormFile file)
        {
            var result = await Mediator.Send(new ImportCategoryCommand { File = file });
            return Ok(result);
        }

        [Route("api/Product/UploadFilesForImportProduct")]
        [HttpPost("UploadFilesForImportProduct")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UploadFilesForImportProduct([FromBody] List<ImportProductLookUp> productLookUp)
        {
            //var path = _hostingEnvironment.WebRootPath + "\\Template\\Template Error File.xlsx";
            //if (System.IO.File.Exists(path))
            //{
            //    System.IO.File.Delete(path);
            //}
            var result = await Mediator.Send(new ImportProductCommand() { ProductLookUp = productLookUp });
            return Ok(result);
        }
        [Route("api/Product/UploadFilesForImportProductBarCode")]
        [HttpPost("UploadFilesForImportProductBarCode")]
        public async Task<IActionResult> UploadFilesForImportProductBarCode([FromBody] List<ImportProductLookUp> productLookUp)
        {
            var result = await Mediator.Send(new ImportProductCommand() 
            { 
                ProductLookUp = productLookUp,
                IsProductBarCode = true
            });
            return Ok(result);
        }


        [Route("api/Product/UploadFilesForImportReparingOrder")]
        [HttpPost("UploadFilesForImportReparingOrder")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UploadFilesForImportReparingOrder([FromForm] IFormFile file)
        {
            var path = _hostingEnvironment.WebRootPath + "\\Template\\Template Error File.xlsx";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            var result = await Mediator.Send(new ImportReparingOrder() { File = file });
            return Ok(result);
        }




        [Route("api/Product/DownloadFileAsync")]
        [HttpGet("DownloadFileAsync")]
        //[Roles("CanImportProduct")]
        public async Task<IActionResult> DownloadFileAsync(string filePath)
        {
            // ... code for validation and get the file
            var path = Path.Combine(_hostingEnvironment.WebRootPath) + filePath;
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(path);
            return File(bytes, "application/xlsx" /*"image/png"*/, Path.GetFileName(path));


            //var path = Path.Combine(
            //               Directory.GetCurrentDirectory(),
            //               "wwwroot", filePath);

            //var memory = new MemoryStream();
            //using (var stream = new FileStream(path, FileMode.Open))
            //{
            //    await stream.CopyToAsync(memory);
            //}
            //memory.Position = 0;
            //return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        [Route("api/Product/DownloadErrorFileAsync")]
        [HttpGet("DownloadErrorFileAsync")]
        [Roles("CanImportProduct")]
        public async Task<IActionResult> DownloadErrorFileAsync(string filePath)
        {
            // ... code for validation and get the file
            var path = Path.Combine(_hostingEnvironment.WebRootPath) + filePath;
            if (System.IO.File.Exists(path))
            {
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(filePath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                var bytes = await System.IO.File.ReadAllBytesAsync(path);

                return File(bytes, "application/xlsx" /*"image/png"*/, Path.GetFileName(path));
            }
            else
            {
                return Ok("File not exist");
            }

        }




        [Route("api/Product/DeleteErrorFileAsync")]
        [HttpGet("DeleteErrorFileAsync")]
        [Roles("CanImportProduct")]
        public IActionResult DeleteErrorFileAsync(string filePath)
        {
            // ... code for validation and get the file
            var path = Path.Combine(_hostingEnvironment.WebRootPath) + filePath;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return Ok("File Deleted Successfully");
            }
            else
            {
                return Ok("File Not Deleted Successfully");
            }

        }


        #endregion

        #region Export

        [Route("api/Product/ExportData")]
        [HttpGet("ExportData")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ExportData(string filePath)
        {
            var path = Path.Combine(_hostingEnvironment.WebRootPath) + filePath;

            var result = await Mediator.Send(new ExportCategoryData()
            {
                filePath = path
            });


            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(path);
            return File(bytes, "application/csv" /*"image/png"*/, Path.GetFileName(path));




        }


        [Route("api/Product/DownloadInventoryCountAsync")]
        [HttpGet("DownloadInventoryCountAsync")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DownloadInventoryCountAsync(string filePath, Guid warehouseId)
        {
            var d = await Mediator.Send(new ExportInventoryCountCommand()
            {
                WarehouseId = warehouseId
            });
            // ... code for validation and get the file
            var path = Path.Combine(_hostingEnvironment.WebRootPath) + filePath;
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(path);
            return File(bytes, "application/csv" /*"image/png"*/, Path.GetFileName(path));


        }
        #endregion

        #region Denomination Setup

        [DisplayName("Save Denomination Setup")]
        [Route("api/Product/SaveDenominationSetup")]
        [HttpPost("SaveDenominationSetup")]
        [Roles("CanAddDenomination", "CanEditDenomination")]
        public async Task<IActionResult> SaveDenominationSetup([FromBody] DenominationSetupVm denominationSetupVm)
        {
            var id = Guid.Empty;
            if (denominationSetupVm.Id == Guid.Empty)
            {
                id = await Mediator.Send(new AddUpdateDenominationSetupCommand
                {
                    Id = Guid.Empty,
                    Number = denominationSetupVm.Number,
                    isActive = denominationSetupVm.isActive,

                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateDenominationSetupCommand
                {
                    Id = denominationSetupVm.Id,
                    Number = denominationSetupVm.Number,
                    isActive = denominationSetupVm.isActive,

                });
            }
            if (id != Guid.Empty)
            {
                var DenominationSetup = await Mediator.Send(new GetDenominationSetupDetailQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, DenominationSetup = DenominationSetup });
            }

            return Ok(new { IsSuccess = false });



        }
        [Route("api/Product/DenominationSetupList")]
        [HttpGet("DenominationSetupList")]
        [Roles("CanViewDenomination", "TouchInvoiceTemplate1", "TouchInvoiceTemplate2", "TouchInvoiceTemplate3")]
        public async Task<IActionResult> DenominationSetupList(bool isActive)
        {
            var denominationSetup = await Mediator.Send(new GetDenominationSetupListQuery { isActive = isActive });
            return Ok(denominationSetup);
        }

        [Route("api/Product/DenominationSetupDetail")]
        [HttpGet("DenominationSetupDetail")]
        [Roles("CanAddDenomination", "CanEditDenomination")]
        public async Task<IActionResult> DenominationSetupDetail(Guid Id)
        {
            var DenominationSetup = await Mediator.Send(new GetDenominationSetupDetailQuery()
            {
                Id = Id
            });
            return Ok(DenominationSetup);

        }
        #endregion


        #region Import Stock

        [Route("api/Product/DownloadStockFileAsync")]
        [HttpGet("DownloadStockFileAsync")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DownloadStockFileAsync()
        {
            var data = await Mediator.Send(new ExportStockCommand()
            {
            });
            return Ok(data);


        }


        [Route("api/Product/UploadFilesForImportStock")]
        [HttpPost("UploadFilesForImportStock")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UploadFilesForImportStock([FromBody] List<ImportExportStockLookUp> importStockVm, Guid warehouseId)
        {
            var result = await Mediator.Send(new ImportStockCommand()
            {
                stockLookUps = importStockVm,
                WareHouseId = warehouseId
            });

            return Ok(result);
        }

        public class UploadFilesForImportStockVm
        {
            public IFormFile file { get; set; }
            public Guid WareHouseId { get; set; }
        }

        #endregion

        #region Barcode Scanner

        [Route("api/Product/GetProductBarcode")]
        [HttpGet("GetProductBarcode")]
        //[Roles("TouchInvoiceTemplate1", "TouchInvoiceTemplate2", "TouchInvoiceTemplate3", "CanHoldTouchInvoices", "CanAddSaleOrder", "CanViewSaleOrder", "CreditPurchase", "CashPurchase", "CanViewPurchaseDraft", "CanViewPurchasePost", "CanPurchaseInvoiceCosting", "CanViewDraftOrder", "CanViewInProcessOrder", "CanAddPurchaseOrder", "CanAddPurchaseReturn", "CanViewDetailPurchaseReturn", "CanAddProductionRecipe", "CanPrintItemBarcode", "CanAddStockIn", "CanEditStockIn", "CanDraftStockIn", "CanAddStockOut", "CanEditStockOut", "CanDraftStockOut", "CanAddStockTransfer", "CanEditStockTransfer", "CanDraftStockTransfer", "CanHoldServiceInvoices", "CashServiceInvoices", "CreditServiceInvoices", "CanAddServiceSaleOrder", "CanDraftServiceSaleOrder", "CanEditServiceSaleOrder", "CanDuplicateServiceSaleOrder", "CanAddServiceQuotation", "CanEditServiceQuotation", "CanDraftServiceQuotation", "CanAddDispatchNote", "CanAddAutoTemplate", "CanEditAutoTemplate","Simple")]
        public async Task<IActionResult> GetProductBarcode(bool isRaw, Guid? wareHouseId, bool isEmail)
        {

            var categoryListModel = await Mediator.Send(new GetProductListQuery
            {

                IsRaw = isRaw,
                IsProductList = true,
                WareHouseId = wareHouseId,
                IsEmail = isEmail
            });
            return Ok(categoryListModel);
        }

        #endregion

        #region ProductInventoryDetailQuery

        [Route("api/Product/ProductInventoryDetailQuery")]
        [HttpGet("ProductInventoryDetailQuery")]
        [Roles("CanViewItem")]
        public async Task<IActionResult> ProductInventoryDetailQuery(Guid id)
        {
            var result = await Mediator.Send(new ProductInventoryDetailQuery
            {
                ProductId = id
            });
            return Ok(result);

        }

        #endregion

        #region BlindInventory
        [Route("api/Product/AddInventoryBlind")]
        [HttpPost("AddInventoryBlind")]
        [Roles("CanAddInventoryCount", "CanEditInventoryCount")]
        public async Task<IActionResult> AddInventoryBlind([FromBody] InventoryBlindVm inventoryBlind)
        {
            var blind = await Mediator.Send(new AddUpdateInventoryBlindCommand()
            {
                InventoryBlindVm = inventoryBlind
            });
            if (blind != Guid.Empty)
            {
                return Ok("Success");
            }
            return Ok(blind);
        }


        [Route("api/Product/GetBlindInventoryCode")]
        [HttpGet("GetBlindInventoryCode")]
        [Roles("CanAddInventoryCount")]
        public async Task<IActionResult> GetBlindInventoryCode()
        {
            var code = await Mediator.Send(new BlindInventoryAutoNumber());
            return Ok(code);
        }


        [Route("api/Product/GetBlindInventoryDetail")]
        [HttpGet("GetBlindInventoryDetail")]
        [Roles("CanEditInventoryCount")]
        public async Task<IActionResult> GetBlindInventoryDetail(Guid id)
        {
            var detail = await Mediator.Send(new GetInventoryBlindDetailQuery()
            {
                Id = id
            });
            return Ok(detail);
        }


        [Route("api/Product/GetBlindInventoryList")]
        [HttpGet("GetBlindInventoryList")]
        [Roles("CanViewInventoryCount")]
        public async Task<IActionResult> GetBlindInventoryList(string searchTerm, int? pageNumber, bool isCounted)
        {

            if (pageNumber == 0)
            {
                pageNumber = 1;
            }
            var list = await Mediator.Send(
               new GetInventoryBlindListQuery()
               {
                   SearchTerm = searchTerm,
                   PageNumber = pageNumber ?? 1,
                   IsCounted = isCounted
               });
            return Ok(list);
        }
        #endregion

        #region Installed Printer List

        [Route("api/Product/GetPrinterList")]
        [HttpGet("GetPrinterList")]
        //[Roles("CanEditTerminal", "CanAddTerminal", "CanPrintItemBarcode", "Noble Admin")]
        public IActionResult GetPrinterList()
        {
            var printerList = new List<string>();
            foreach (string printName in PrinterSettings.InstalledPrinters)
            {
                printerList.Add(printName);
            }
            return Ok(printerList);
        }

        #endregion


        [Route("api/Product/RetailReport")]
        [HttpGet("RetailReport")]
        [Roles("StartDay", "Noble Admin", "CloseDay", "User")]
        public async Task<IActionResult> RetailReport(Guid counterId, bool isOpeningCash)
        {
            var result = await Mediator.Send(new GetOpeningBalanceQuery
            {
                Id = counterId,
                IsOpeningCash = isOpeningCash

            });

            return Ok(result);
        }
        [Route("api/Product/DayStartReportList")]
        [HttpGet("DayStartReportList")]
        [Roles("Noble Admin", "User")]
        public async Task<IActionResult> DayStartReportList()
        {
            var result = await Mediator.Send(new DayStartReportListQuery());

            return Ok(result);
        }

        [Route("api/Product/InActiveTerminalReportList")]
        [HttpGet("InActiveTerminalReportList")]
        [Roles("Noble Admin", "User")]
        public async Task<IActionResult> InActiveTerminalReportList(DateTime? fromDay, DateTime? toDay)
        {
            var result = await Mediator.Send(new InActiveTerminalReport()
            {
                FromDay = fromDay,
                ToDay = toDay
            });

            return Ok(result);
        }

        [Route("api/Product/SaveWarrantyType")]
        [HttpPost("SaveWarrantyType")]
        [Roles("CanAddWarrantyType", "CanEditWarrantyType")]
        public async Task<IActionResult> SaveWarrantyType([FromBody] WarrantyTypeLookUpModel warrantyType)
        {
            if (warrantyType.Id == Guid.Empty)
            {
                await Mediator.Send(new AddUpdateWarrantyTypeCommand
                {
                    WarrantyTypeLook = warrantyType
                });
                return Ok(new { IsSuccess = true });
            }
            else
            {
                await Mediator.Send(new AddUpdateWarrantyTypeCommand
                {
                    WarrantyTypeLook = warrantyType
                });
                return Ok(new { IsSuccess = true });
            }
        }

        [Route("api/Product/WarrantyTypeList")]
        [HttpGet("WarrantyTypeList")]
        [Roles("CanViewWarrantyType", "CanAddStockIn", "CanEditStockIn", "CanDraftStockIn", "CanAddStockOut", "CanEditStockOut", "CanDraftStockOut", "CanViewPurchaseDraft", "CanAddPurchaseInvoice", "CanEditPurchaseInvoice")]
        public async Task<IActionResult> WarrantyTypeList(string searchTerm, int? pageNumber, bool isDropdown)
        {
            var result = await Mediator.Send(new GetWarrantyTypeListQuery()
            {
                IsDropdown = isDropdown,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1
            });

            return Ok(result);
        }

        [Route("api/Product/WarrantyTypeDetail")]
        [HttpGet("WarrantyTypeDetail")]
        [Roles("CanEditWarrantyType")]
        public async Task<IActionResult> WarrantyTypeDetail(Guid id)
        {
            var color = await Mediator.Send(new GetWarrantyTypeDetailQuery()
            {
                Id = id
            });
            return Ok(color);
        }

        #region ProductGroup
        [Route("api/Product/ProductGroupAutoGenerateCode")]
        [HttpGet("ProductGroupAutoGenerateCode")]
        public async Task<IActionResult> ProductGroupAutoGenerateCode()
        {
            var autoNo = await Mediator.Send(new GetProductGroupCode());
            return Ok(autoNo);
        }

        [Route("api/Product/SaveProductGroup")]
        [HttpPost("SaveProductGroup")]
        public async Task<IActionResult> SaveProductGroup([FromBody] ProductGroupModel productGroup)
        {
            await Mediator.Send(new AddUpdateProductGroupCommand
            {
                ProductGroup = productGroup
            });
            return Ok(new { IsSuccess = true });
        }

        [Route("api/Product/ProductGroupList")]
        [HttpGet("ProductGroupList")]
        public async Task<IActionResult> ProductGroupList(string searchTerm, int? pageNumber, bool isDropdown)
        {
            var result = await Mediator.Send(new GetProductGroupListQueries()
            {
                IsDropdown = isDropdown,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1
            });

            return Ok(result);
        }

        [Route("api/Product/ProductGroupDetail")]
        [HttpGet("ProductGroupDetail")]
        public async Task<IActionResult> ProductGroupDetail(Guid id)
        {
            var color = await Mediator.Send(new GetProductGroupDetailQueries()
            {
                Id = id
            });
            return Ok(color);

        }
        #endregion
    }
}



