using CsvHelper.Configuration;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using Dapper;
using Focus.Business.Interface;
using Focus.Domain.Enum;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Syncfusion.XlsIO;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.StockAdjustments.ImportStock
{
    public class ImportStockCommand : IRequest<List<ImportExportStockLookUp>>
    {
        // delegate int NumberChanger(int n);
        public List<ImportExportStockLookUp> stockLookUps { get; set; }
        public Guid WareHouseId { get; set; }


        public class Handler : IRequestHandler<ImportStockCommand, List<ImportExportStockLookUp>>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            private readonly IMediator _mediator;
            private readonly IConfiguration _configuration;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly IHostingEnvironment _environment;
            private string _adjustmentCode = null;
            private string _adjustmentCodeOut = null;
            private bool IsFirstTime = true;
            private bool IsFirstTimeOut = true;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<ImportStockCommand> logger,
                IMediator mediator, IConfiguration configuration, IUserHttpContextProvider contextProvider,
                IHostingEnvironment environment)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
                _configuration = configuration;
                _contextProvider = contextProvider;
                _environment = environment;
            }

            public async Task<List<ImportExportStockLookUp>> Handle(ImportStockCommand request, CancellationToken cancellationToken)
            {


                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    var companyId = _contextProvider.GetCompanyId().ToString();
                    var sqlQuery =
                        "select Id, Code , EnglishName , ArabicName  from Products where CompanyId = '" +
                        companyId + "';" +
                        " select top 1 code from StockAdjustments where (CompanyId = '" + companyId +
                        "' and StockAdjustmentType = 5) order by code desc;" +
                        " select top 1 code from StockAdjustments where (CompanyId = '" + companyId +
                        "' and StockAdjustmentType = 6) order by code desc;" +
                        " select TaxMethod, TaxRateId from CompanyAccountSetups where CompanyId ='" + companyId + "';";

                    var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));


                    using (var multi = connection.QueryMultiple(sqlQuery, null))
                    {
                        DropDownCodeModel.Product = multi.Read<Product>().ToList();
                        _adjustmentCode = multi.Read<StockAdjustment>().FirstOrDefault()?.Code;
                        _adjustmentCodeOut = multi.Read<StockAdjustment>().FirstOrDefault()?.Code;
                        DropDownCodeModel.CompanySetupForm =
                            multi.Read<CompanySetupFormData>().SingleOrDefault();
                    }

                    _adjustmentCode = _adjustmentCode == null
                      ? GenerateCodeFirstTime("STI")
                      : GenerateNewCode(_adjustmentCode, "STI");

                    _adjustmentCodeOut = _adjustmentCodeOut == null
                      ? GenerateCodeFirstTime("STO")
                      : GenerateNewCode(_adjustmentCodeOut, "STO");

                    var stockAdjustment = new StockAdjustment();
                    var stockAdjustmentOut = new StockAdjustment();
                    if (request.stockLookUps[0].IsEqualStock)
                    {
                        // StockIn
                        stockAdjustment.Date = DateTime.UtcNow;
                        stockAdjustment.Code = _adjustmentCode;
                        stockAdjustment.isDraft = true;
                        stockAdjustment.StockAdjustmentType = StockAdjustmentType.StockIn;
                        stockAdjustment.WarehouseId = request.WareHouseId;
                        stockAdjustment.Narration = "Quantity Adjustment In";

                        // StockOut
                        stockAdjustmentOut.Date = DateTime.UtcNow;
                        stockAdjustmentOut.Code = _adjustmentCodeOut;
                        stockAdjustmentOut.isDraft = true;
                        stockAdjustmentOut.StockAdjustmentType = StockAdjustmentType.StockOut;
                        stockAdjustmentOut.WarehouseId = request.WareHouseId;
                        stockAdjustmentOut.Narration = "Quantity Adjustment Out";
                    }
                    else
                    {
                        stockAdjustment.Date = DateTime.UtcNow;
                        stockAdjustment.Code = _adjustmentCode;
                        stockAdjustment.isDraft = true;
                        stockAdjustment.StockAdjustmentType = StockAdjustmentType.StockIn;
                        stockAdjustment.WarehouseId = request.WareHouseId;
                        stockAdjustment.Narration = "Import from csv file";
                        await Context.StockAdjustments.AddAsync(stockAdjustment, cancellationToken);
                    }
                    
                    var detailedList = new List<StockAdjustmentDetail>();
                    var detailedListOut = new List<StockAdjustmentDetail>();
                    var ErrorStockFiles = new List<ImportExportStockLookUp>();
                    foreach (var stock in request.stockLookUps)
                    {
                        if (!string.IsNullOrEmpty(stock.ProductCode))
                        {
                            var product =
                                DropDownCodeModel.Product.FirstOrDefault(x => x.Code == stock.ProductCode);
                            if (product != null)
                            {
                                if (stock.IsEqualStock)
                                {
                                    var quantity = string.IsNullOrEmpty(stock.Quantity) || string.IsNullOrWhiteSpace(stock.Quantity) ? 0M : decimal.TryParse(stock.Quantity, out decimal numericValue) ? numericValue : 0M;

                                    if(quantity > 0)
                                    {
                                        if (IsFirstTime)
                                        {
                                            await Context.StockAdjustments.AddAsync(stockAdjustment, cancellationToken);
                                            IsFirstTime = false;
                                        }
                                        var stockAdjustmentDetail = new StockAdjustmentDetail()
                                        {
                                            ProductId = product.Id,
                                            Quantity = quantity,
                                            Price = 0,
                                            StockAdjustmentId = stockAdjustment.Id,
                                            WarehouseId = stockAdjustment.WarehouseId,

                                        };
                                        detailedList.Add(stockAdjustmentDetail);
                                    }
                                    if(quantity < 0)
                                    {
                                        if (IsFirstTimeOut)
                                        {
                                            await Context.StockAdjustments.AddAsync(stockAdjustmentOut, cancellationToken);
                                            IsFirstTimeOut = false;
                                        }
                                        var stockAdjustmentDetail = new StockAdjustmentDetail()
                                        {
                                            ProductId = product.Id,
                                            Quantity = quantity*(-1),
                                            Price = 0,
                                            StockAdjustmentId = stockAdjustmentOut.Id,
                                            WarehouseId = stockAdjustment.WarehouseId,

                                        };
                                        detailedListOut.Add(stockAdjustmentDetail);
                                    }
                                   
                                }
                                else
                                {
                                    var stockAdjustmentDetail = new StockAdjustmentDetail()
                                    {
                                        ProductId = product.Id,
                                        Quantity = string.IsNullOrEmpty(stock.Quantity) || string.IsNullOrWhiteSpace(stock.Quantity) ? 0M : decimal.TryParse(stock.Quantity, out decimal numericValue) ? numericValue : 0M,
                                        Price = string.IsNullOrEmpty(stock.UnitPrice) || string.IsNullOrWhiteSpace(stock.UnitPrice) ? 0M : decimal.TryParse(stock.UnitPrice, out decimal numeric) ? numeric : 0M,
                                        StockAdjustmentId = stockAdjustment.Id,
                                        WarehouseId = stockAdjustment.WarehouseId,

                                    };
                                    detailedList.Add(stockAdjustmentDetail);
                                }
                            }
                            else
                            {
                                stock.ErrorDescription = "Product not exist";
                                ErrorStockFiles.Add(stock);

                            }
                        }
                        else
                        {
                            stock.ErrorDescription = "Product code not empty";
                            ErrorStockFiles.Add(stock);

                            
                        }
                        
                    }

                    if(detailedList.Count() > 0 || detailedListOut.Count() > 0)
                    {
                        if (detailedList.Count > 0) await Context.StockAdjustmentDetails.AddRangeAsync(detailedList, cancellationToken);
                        if(detailedListOut.Count > 0) await Context.StockAdjustmentDetails.AddRangeAsync(detailedListOut, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                          
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                    }
                    return ErrorStockFiles;
                    

                }
                catch (Exception e)
                {

                    throw new ApplicationException("Something went wrong. Please Contact to support");

                    
                }



            }

            public string GenerateCodeFirstTime(string header)
            {
                return header + "-00001";
            }

            public string GenerateNewCode(string soNumber, string header)
            {
                string fetchNo = soNumber.Substring(4);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                //string prefix = "STI-";
                var newCode = header + "-" + autoNo.ToString(format);
                return newCode;
            }


        }

    }

    public static class DropDownCodeModel
    {
        public static ICollection<Product> Product { get; set; }

        public static CompanySetupFormData CompanySetupForm { get; set; }
        public static string ProductCode { get; set; }
        public static string StockCode { get; set; }
    }


    public class CompanySetupFormData
    {
        public string TaxMethod { get; set; }
        public Guid TaxRateId { get; set; }
    }

}

