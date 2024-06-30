using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.LeaveManagement.LeavePeriod.Model;
using Focus.Business.LeaveManagement.LeaveType.Model;
using Focus.Business.LeaveManagement.LeaveType.Quries;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.LeavePeriod.Queries
{
    public class GetLeavePeriodDetailQuery : IRequest<LeavePeriodLookupModel>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<GetLeavePeriodDetailQuery, LeavePeriodLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetLeavePeriodDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<LeavePeriodLookupModel> Handle(GetLeavePeriodDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var leave = await _context.LeavePeriods.FindAsync(request.Id);

                    if (leave != null)
                    {
                        return new LeavePeriodLookupModel
                        {
                            Id = leave.Id,
                            Name = leave.Name,
                            PeriodStart = leave.PeriodStart,
                            PeriodEnd  = leave.PeriodEnd,
                        };
                    }

                    throw new NotFoundException(nameof(leave), request.Id);
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
