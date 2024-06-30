using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.LeaveManagement.LeavePeriod.Model;
using Focus.Business.LeaveManagement.LeavePeriod.Queries;
using Focus.Business.LeaveManagement.WorkWeek.Model;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.WorkWeek.Queries
{
    public class WorkWeekDetailQuery : IRequest<WorkWeekLookupModel>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<WorkWeekDetailQuery, WorkWeekLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<WorkWeekDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<WorkWeekLookupModel> Handle(WorkWeekDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var leave = await _context.WorkWeeks.FindAsync(request.Id);

                    if (leave != null)
                    {
                        return new WorkWeekLookupModel
                        {
                            Id = leave.Id,
                            Day = leave.Day,
                            Status = leave.Status,
                            Country = leave.Country,
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
