using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.ImportExportTypes.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ImportExportTypes.Queries
{
    public class ImportExportTypeListQuery : PagedRequest, IRequest<PagedResult<List<ImportExportTypeLookUpModel>>>
    {
        public Domain.Enum.ImportExportTypes ImportExportTypes { get; set; }

        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public int PageNumber { get; set; }

        public class Handler : IRequestHandler<ImportExportTypeListQuery, PagedResult<List<ImportExportTypeLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<ImportExportTypeListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<ImportExportTypeLookUpModel>>> Handle(ImportExportTypeListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    
                    var query = _context.ImportExportTypes.AsNoTracking().Where(x=>x.ImportExportTypes==request.ImportExportTypes).AsQueryable();

                    if (request.IsDropDown)
                    {
                        query = query.Where(x => x.isActive);
                        var importExportType = new List<ImportExportTypeLookUpModel>();
                        foreach (var item in query)
                        {
                            importExportType.Add(new ImportExportTypeLookUpModel()
                            {
                                Id = item.Id,
                                Code = item.Code,
                                Name = item.Name,
                                NameArabic = item.NameArabic,
                                Description = item.Description,
                                isActive = item.isActive,
                                ImportExportTypes = item.ImportExportTypes,
                            });
                        }

                        return new PagedResult<List<ImportExportTypeLookUpModel>>
                        {
                            Results = importExportType
                        };
                    }

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;


                        query = query.Where(x =>
                            x.Code.Contains(searchTerm) 
                            || x.Name.Contains(searchTerm)
                            || x.NameArabic.Contains(searchTerm));

                    }

                    query = query.OrderBy(x => x.Code);
                    var count = await query.CountAsync(cancellationToken: cancellationToken);
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var list = new List<ImportExportTypeLookUpModel>();
                    foreach (var item in query)
                    {
                        list.Add(new ImportExportTypeLookUpModel()
                        {
                            Id = item.Id,
                            Code = item.Code,
                            Name = item.Name,
                            NameArabic = item.NameArabic,
                            Description = item.Description,
                            isActive = item.isActive,
                            ImportExportTypes = item.ImportExportTypes,
                        });
                    }

                    return new PagedResult<List<ImportExportTypeLookUpModel>>
                    {
                        Results = list,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = list.Count / request.PageSize
                    };





                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
    }
}
