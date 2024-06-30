using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.PurchaseOrders.Queries.PurchaseOrderAction
{
    public class PurchaseOrderAttachment : IRequest<List<PurchaseAttachment>>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<PurchaseOrderAttachment, List<PurchaseAttachment>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<PurchaseOrderAttachment> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<List<PurchaseAttachment>> Handle(PurchaseOrderAttachment request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.PurchaseAttachments
                        .AsNoTracking()
                        .Where(x => x.PurchaseOrderId == request.Id)
                        .AsQueryable();


                    query = query.OrderBy(x => x.Id);

                    var process = await query.Select(x =>
                        new PurchaseAttachment()
                        {
                            Id = x.Id,
                            PurchaseOrderId = x.PurchaseOrderId,
                            Description = x.Description,
                            Date = x.Date,
                            Path = x.Path
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
