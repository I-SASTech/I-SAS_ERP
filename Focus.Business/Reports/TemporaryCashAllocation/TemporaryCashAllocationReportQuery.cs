using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Reports.TemporaryCashAllocation
{
    public class TemporaryCashAllocationReportQuery : PagedRequest, IRequest<PagedResult<List<TemporaryCashAllocationReportLookUp>>>
    {
        public int Month { get; set; }
        public int Year { get; set; }

        public class Handler : IRequestHandler<TemporaryCashAllocationReportQuery, PagedResult<List<TemporaryCashAllocationReportLookUp>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<TemporaryCashAllocationReportQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<PagedResult<List<TemporaryCashAllocationReportLookUp>>> Handle(TemporaryCashAllocationReportQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.TemporaryCashAllocations.AsNoTracking()
                        .Include(x => x.Account)
                        .Where(x => x.ApprovalStatus == ApprovalStatus.Approved && x.Date.Date.Year== request.Year && x.Date.Date.Month==request.Month).AsQueryable();

                    
                    query = query.OrderByDescending(x => x.VoucherNumber);
                    

                    var employees = _context.EmployeeRegistrations.AsNoTracking().AsQueryable();


                    var pvouchers = new List<TemporaryCashAllocationReportLookUp>();
                    foreach (var employee in employees)
                    {
                        if (employee.TemporaryCashIssuer)
                        {
                            pvouchers.Add(new TemporaryCashAllocationReportLookUp
                            {
                                Id = employee.Id,
                                UserName = employee.EnglishName,
                                UserNameAr = employee.ArabicName,
                                Amount = query.Where(x => x.UserEmployeeId == employee.Id).Sum(x => x.Amount),
                            });
                        }
                    }
                    

                    return new PagedResult<List<TemporaryCashAllocationReportLookUp>>
                    {
                        Results = pvouchers
                    };
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }

            }
        }
    }
}
