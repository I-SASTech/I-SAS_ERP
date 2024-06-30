using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.ReparingOrderTypes.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ReparingOrderTypes.Queries
{
    public class GetReparingOrderTypeDetailQuery : IRequest<ReparingOrderTypeLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetReparingOrderTypeDetailQuery, ReparingOrderTypeLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetReparingOrderTypeDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<ReparingOrderTypeLookUpModel> Handle(GetReparingOrderTypeDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var reparingOrdertType = await _context.ReparingOrderTypes.FindAsync(request.Id);

                    if (reparingOrdertType != null)
                    {
                        return new ReparingOrderTypeLookUpModel()
                        {
                            Id = reparingOrdertType.Id,
                            Name = reparingOrdertType.Name,
                            NameArabic = reparingOrdertType.NameArabic,
                            ReparingOrderTypes = reparingOrdertType.ReparingOrderTypeEnums,
                            isActive = reparingOrdertType.isActive,
                        };
                    }
                    throw new NotFoundException(nameof(reparingOrdertType), request.Id);
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
