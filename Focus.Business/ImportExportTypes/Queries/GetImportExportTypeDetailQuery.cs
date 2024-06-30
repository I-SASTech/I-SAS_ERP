using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.ImportExportTypes.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ImportExportTypes.Queries
{
    public class GetImportExportTypeDetailQuery : IRequest<ImportExportTypeLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetImportExportTypeDetailQuery, ImportExportTypeLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetImportExportTypeDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<ImportExportTypeLookUpModel> Handle(GetImportExportTypeDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var importExportType = await _context.ImportExportTypes.FindAsync(request.Id);

                    if (importExportType != null)
                    {
                        return new ImportExportTypeLookUpModel()
                        {
                            Id = importExportType.Id,
                            Name = importExportType.Name,
                            NameArabic = importExportType.NameArabic,
                            Code = importExportType.Code,
                            Description = importExportType.Description,
                            ImportExportTypes = importExportType.ImportExportTypes,
                            isActive = importExportType.isActive,
                        };
                    }
                    throw new NotFoundException(nameof(importExportType), request.Id);
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
