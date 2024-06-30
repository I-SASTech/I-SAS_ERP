using Focus.Business.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.PurchaseOrders.Queries.PurchaseOrderAction
{
    public class PurchaseOrderActionListQuery : IRequest<List<PurchaseOrderActionLookUp>>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<PurchaseOrderActionListQuery, List<PurchaseOrderActionLookUp>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<PurchaseOrderActionListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<List<PurchaseOrderActionLookUp>> Handle(PurchaseOrderActionListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.PurchaseOrderActions
                        .AsNoTracking()
                        .Include(x=>x.CompanyProcess)
                        .Where(x=>x.PurchaseOrderId==request.Id)
                        .AsQueryable();

                   
                    query = query.OrderBy(x => x.Id);
                   
                    var process = await query.Select(x =>
                        new PurchaseOrderActionLookUp()
                        {
                            Id = x.Id,
                            ProcessId = x.ProcessId,
                            ProcessName = x.CompanyProcess.ProcessName,
                            ProcessNameArabic = x.CompanyProcess.ProcessNameArabic,
                            PurchaseOrderId = x.PurchaseOrderId,
                            Date = x.Date,
                            Description = x.Description,
                        }).ToListAsync(cancellationToken: cancellationToken);

                    return process;


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
