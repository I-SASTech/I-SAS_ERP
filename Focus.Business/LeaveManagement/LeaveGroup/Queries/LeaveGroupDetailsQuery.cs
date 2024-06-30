using DocumentFormat.OpenXml.Office.CustomUI;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.LeaveManagement.Holiday.Model;
using Focus.Business.LeaveManagement.Holiday.Queries;
using Focus.Business.LeaveManagement.LeaveGroup.Model;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.LeaveGroup.Queries
{
    public class LeaveGroupDetailsQuery : IRequest<LeaveGroupLookupModel>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<LeaveGroupDetailsQuery, LeaveGroupLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<LeaveGroupDetailsQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<LeaveGroupLookupModel> Handle(LeaveGroupDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var item = await _context.LeaveGroups.FindAsync(request.Id);

                    if (item != null)
                    {
                        var leaveGroupEmployees = new List<LeaveGroupEmployeesLookupModel>();

                        foreach (var employee in item.LeaveGroupEmployees)
                        {
                            leaveGroupEmployees.Add(new LeaveGroupEmployeesLookupModel
                            {
                                Id= employee.Id,
                                EmployeeName = employee.EmployeeRegistration?.EnglishName,
                                EmployeeId = employee.EmployeeId,
                                LeaveGroupId = employee.LeaveGroupId,
                            });
                        }

                        return new LeaveGroupLookupModel
                        {
                            Id = item.Id,
                            Name = item.Name,
                            LeaveGroupEmployees = leaveGroupEmployees,
                            Details = item.Details
                        };
                    }

                    throw new NotFoundException(nameof(item), request.Id);
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
