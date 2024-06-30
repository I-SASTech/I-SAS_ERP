using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.LeaveManagement.Holiday.Model;
using Focus.Business.LeaveManagement.WorkWeek.Model;
using Focus.Business.LeaveManagement.WorkWeek.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.Holiday.Queries
{
    public class GetLeaveHolidayDetailQuery : IRequest<LeaveHolidayLookupModel>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<GetLeaveHolidayDetailQuery, LeaveHolidayLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetLeaveHolidayDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<LeaveHolidayLookupModel> Handle(GetLeaveHolidayDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var leave = await _context.LeaveHolidays.FindAsync(request.Id);

                    if (leave != null)
                    {
                        return new LeaveHolidayLookupModel
                        {
                            Id = leave.Id,
                            Name = leave.Name,
                            Date = leave.Date,
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
