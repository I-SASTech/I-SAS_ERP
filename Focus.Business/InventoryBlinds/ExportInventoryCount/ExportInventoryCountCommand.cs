using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Focus.Business.Interface;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Syncfusion.XlsIO;

namespace Focus.Business.InventoryBlinds.ExportInventoryCount
{
    public class ExportInventoryCountCommand : IRequest<bool>
    {
        public Guid WarehouseId { get; set; }

        public class ProductData
        {
            public string ProductCode { get; set; }
            public string ProductNameEnglish { get; set; }
            public string ProductNameArabic { get; set; }
            public string Shelf { get; set; }
            public decimal Quantity { get; set; }
            public decimal PhysicalQunatity { get; set; }
            public decimal Balance { get; set; }
           
        }

        public class Handler : IRequestHandler<ExportInventoryCountCommand, bool>
        {
            public readonly IApplicationDbContext Context;
            public readonly IConfiguration Configuration;
            public readonly IUserHttpContextProvider Provider;
            public readonly IHostingEnvironment _Environment;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<ExportInventoryCountCommand> logger,
                IConfiguration configuration, IUserHttpContextProvider provider, IHostingEnvironment environment)
            {
                Context = context;
                Logger = logger;
                Configuration = configuration;
                Provider = provider;
                _Environment = environment;
            }

            public async Task<bool> Handle(ExportInventoryCountCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    var companyId = Provider.GetCompanyId();
                    var sqlQuery = "select p.Code as ProductCode, p.EnglishName as ProductNameEnglish, p.ArabicName as ProductNameArabic,p.Shelf as Shelf, s.CurrentQuantity as Quantity from Products as p inner join Stocks as s on p.Id = s.ProductId  " +
                        " where s.WareHouseId = '" + request.WarehouseId.ToString() + "' and p.CompanyId ='" + companyId + "'  order by ProductCode";

                    var sqlConnection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));

                    
                    var productFile = sqlConnection.Query<ProductData>(sqlQuery).ToList();
                    if (productFile.Count > 0)
                    {
                        var rootPath = _Environment.WebRootPath + "\\Template\\Inventory Count Template.xlsx";
                        if(System.IO.File.Exists(rootPath))
                            System.IO.File.Delete(rootPath);
                        //Create Error file workbook
                        var excelEngine = new ExcelEngine();
                        //Instantiate the Excel application object
                        var application = excelEngine.Excel;
                        //Assigns default application version
                        application.DefaultVersion = ExcelVersion.Excel2007;

                        IWorkbook workbook = application.Workbooks.Create(1);
                        IWorksheet worksheet = workbook.Worksheets[0];
                        worksheet.Name = "Inventory Count Template";

                        worksheet.Range["A1"].Text = "Product Code";
                        worksheet.Range["B1"].Text = "Product Name English	";
                        worksheet.Range["C1"].Text = "Product Name Arabic";
                        worksheet.Range["D1"].Text = "Aisle/Section";
                        worksheet.Range["E1"].Text = "Quantity";
                        worksheet.Range["F1"].Text = "Physical Quantity";
                        worksheet.Range["G1"].Text = "Balance";
                        
                        
                        productFile.OrderBy(x => x.ProductCode);
                        worksheet.ImportData(productFile, 2, 1, false);
                        
                        FileStream file = new FileStream(rootPath, FileMode.Create, FileAccess.ReadWrite);
                        workbook.SaveAs(file);
                        file.Dispose();
                    }
                    return true;

                }
                catch (Exception e)
                {
                    _ = e.Message;
                }

                return false;
            }


        }
    }
}
