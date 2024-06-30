using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Warehouses.Queries.GetWarehouseDetails
{

    public class GetWarehousesQuery : IRequest<WarehouseLookUpModel>
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }

        public class Handler : IRequestHandler<GetWarehousesQuery, WarehouseLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetWarehousesQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<WarehouseLookUpModel> Handle(GetWarehousesQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var warehouse = await _context.Warehouses.FindAsync(request.Id);
                    if (request.CompanyId != Guid.Empty)
                    {
                        _context.DisableTenantFilter = true;
                        warehouse= await _context.Warehouses.FindAsync(request.Id);
                    }
                    if (warehouse != null)
                    {
                        return WarehouseLookUpModel.Create(warehouse);
                    }
                    return null;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
                finally
                {
                    _context.DisableTenantFilter = false;
                }


            }
        }
    }
}
