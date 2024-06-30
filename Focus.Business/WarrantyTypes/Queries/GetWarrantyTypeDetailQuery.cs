using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.WarrantyTypes.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.WarrantyTypes.Queries
{
    public class GetWarrantyTypeDetailQuery : IRequest<WarrantyTypeLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetWarrantyTypeDetailQuery, WarrantyTypeLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context,
                ILogger<GetWarrantyTypeDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<WarrantyTypeLookUpModel> Handle(GetWarrantyTypeDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var warrantyType = await _context.WarrantyTypes.FindAsync(request.Id);

                    if (warrantyType != null)
                    {
                        return new WarrantyTypeLookUpModel
                        {
                            Id = warrantyType.Id,
                            Code = warrantyType.Code,
                            Name = warrantyType.Name,
                            NameArabic = warrantyType.NameArabic,
                            IsActive = warrantyType.IsActive,
                        };
                    }
                    throw new NotFoundException(nameof(warrantyType), request.Id);
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
