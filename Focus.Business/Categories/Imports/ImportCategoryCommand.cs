using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CsvHelper;
using CsvHelper.Configuration;
using Focus.Business.Categories.Command.AddUpdateCategory;
using Focus.Business.Categories.Queries;

namespace Focus.Business.Categories.Imports
{
    public class ImportCategoryCommand : IRequest<bool>
    {
        public IFormFile File { get; set; }

        public class Handler : IRequestHandler<ImportCategoryCommand, bool>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<ImportCategoryCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }

            public async Task<bool> Handle(ImportCategoryCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    
                   
                    var reader = new StreamReader(request.File.OpenReadStream());
                    var csvReader = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);
                    csvReader.Configuration.RegisterClassMap<CategoryMapper>();
                    var fileData = csvReader.GetRecords<Category>().ToList();
                    var errorInvoice = new List<CategoryImportModel>();
                    foreach (var category in fileData)
                    {
                        if (category.Name == "")
                        {
                            throw new ApplicationException ("Please Add Category Name");
                        }
                        else
                        {
                            var code = await _mediator.Send(new GetCategoryCodeQuery(), cancellationToken);

                            if (string.IsNullOrEmpty(code))
                            {
                                throw new ApplicationException("Code no is not found");
                            }

                            var result= await _mediator.Send(new AddUpdateCategoryCommand
                            {
                                Id = new Guid(),
                                Name=category.Name,
                                Code = code,
                                IsActive=true,
                                Description=category.Description
                            }, cancellationToken);

                        }
                      
                         
                    }
                    return true;

                }
                catch (Exception e)
                {
                    _ = e.Message;
                    throw;
                }
            }


        }
        public class Category
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public sealed class CategoryMapper:ClassMap<Category>
        {
            public CategoryMapper()
            {
                Map(x => x.Name).Name("Name");
                Map(x => x.Description).Name("Description");
            }
        }
    }
}