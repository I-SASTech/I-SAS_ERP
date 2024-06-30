using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Categories.Export
{
    public class ExportCategoryData : IRequest<bool>
    {
        public string filePath;

        public class Category
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Code { get; set; }
        }

        public class Handler : IRequestHandler<ExportCategoryData, bool>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<ExportCategoryData> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<bool> Handle(ExportCategoryData request, CancellationToken cancellationToken)
            {
                try
                {

                    var query = await Context.Categories.ToListAsync(cancellationToken);

              

                    var writer = new StreamWriter(request.filePath);
                    var csvWriter = new CsvWriter(writer, CultureInfo.CurrentCulture);
                 


                    csvWriter.WriteField("Name");
                    csvWriter.WriteField("Description");
                    csvWriter.WriteField("Code");
                    await csvWriter.NextRecordAsync();

                    foreach (var project in query)
                    {
                        csvWriter.WriteField(project.Name);
                        csvWriter.WriteField(project.Description);
                        csvWriter.WriteField(project.Code);
                        await csvWriter.NextRecordAsync();
                    }

                    writer.Close();
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
