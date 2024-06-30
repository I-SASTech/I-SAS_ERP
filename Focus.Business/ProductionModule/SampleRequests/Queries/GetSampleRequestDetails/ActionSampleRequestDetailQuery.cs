using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ProductionModule.SampleRequests.Queries.GetSampleRequestDetails
{
    public class ActionSampleRequestDetailQuery : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public ApprovalStatus Status { get; set; }
        public class Handler : IRequestHandler<ActionSampleRequestDetailQuery, Guid>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<ActionSampleRequestDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<Guid> Handle(ActionSampleRequestDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var sampleRequest = await _context.SampleRequests
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x=>x.Id==request.Id,cancellationToken);
                    if (sampleRequest != null)
                    {
                        sampleRequest.ApprovalStatus = request.Status;
                        _context.SampleRequests.Update(sampleRequest);
                        await _context.SaveChangesAsync(cancellationToken);

                        return sampleRequest.Id;
                    }
                    return Guid.Empty;
                   
                    
                   
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
