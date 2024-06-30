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
using Focus.Business.Categories.Export;
using Focus.Business.Interface;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Syncfusion.XlsIO;

namespace Focus.Business.StockAdjustments.ImportStock
{
    public class ExportStockCommand : IRequest<List<ImportExportStockLookUp>>
    {
        public class Handler : IRequestHandler<ExportStockCommand, List<ImportExportStockLookUp>>
        {
            public readonly IApplicationDbContext Context;
            public readonly IConfiguration Configuration;
            public readonly IUserHttpContextProvider Provider;
            public readonly IHostingEnvironment _Environment;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<ExportStockCommand> logger,
                IConfiguration configuration, IUserHttpContextProvider provider, IHostingEnvironment environment)
            {
                Context = context;
                Logger = logger;
                Configuration = configuration;
                Provider = provider;
                _Environment = environment;
            }

            public async Task<List<ImportExportStockLookUp>> Handle(ExportStockCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    var companyId = Provider.GetCompanyId();

                    var products = await Context.Products.AsNoTracking().Select(x => new ImportExportStockLookUp
                    {
                        ProductCode = x.Code,
                        ProductNameEnglish = x.EnglishName,
                        ProductNameArabic = x.ArabicName,
                        DisplayName = x.DisplayName,
                    }).OrderBy(x => x.ProductCode).ToListAsync();

                    //var sqlQuery = "select p.Code as ProductCode, p.EnglishName as ProductNameEnglish, p.ArabicName as ProductNameArabic, p.DisplayName as DisplayName" +
                    //    "from Products as p where p.CompanyId ='" + companyId + "'  order by ProductCode";

                    //var sqlConnection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));

                    
                    //var productFile = sqlConnection.Query<ImportExportStockLookUp>(sqlQuery).ToList();
                    
                    return products;

                }
                catch (Exception e)
                {
                    throw new ApplicationException("Something went wrong. Please contact to Support");
                }

            }


        }
    }
}
