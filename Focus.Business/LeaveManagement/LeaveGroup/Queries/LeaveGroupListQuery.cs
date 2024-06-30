using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.LeaveManagement.Holiday.Model;
using Focus.Business.LeaveManagement.Holiday.Queries;
using Focus.Business.LeaveManagement.LeaveGroup.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.LeaveGroup.Queries
{
    public class LeaveGroupListQuery : PagedRequest, IRequest<PagedResult<List<LeaveGroupLookupModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public class Handler : IRequestHandler<LeaveGroupListQuery, PagedResult<List<LeaveGroupLookupModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<LeaveGroupListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<LeaveGroupLookupModel>>> Handle(LeaveGroupListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = await _context.LeaveGroups.AsNoTracking().Include(x => x.LeaveGroupEmployees).ThenInclude(x => x.EmployeeRegistration).ToListAsync();

                    if (request.IsDropDown)
                    {
                        var leave = new List<LeaveGroupLookupModel>();

                        foreach (var item in query)
                        {   var leaveGroupEmployees = new List<LeaveGroupEmployeesLookupModel>();

                            foreach (var employee in item.LeaveGroupEmployees)
                            {
                                leaveGroupEmployees.Add(new LeaveGroupEmployeesLookupModel
                                {
                                    Id = employee.Id,
                                    EmployeeName = employee.EmployeeRegistration?.EnglishName,
                                    EmployeeId = employee.EmployeeId,
                                    LeaveGroupId = employee.LeaveGroupId,
                                });
                            }
                            leave.Add(new LeaveGroupLookupModel
                            {
                                Id = item.Id,
                                Name = item.Name,
                                LeaveGroupEmployees = leaveGroupEmployees,
                                Details = item.Details
                            });
                        }

                        return new PagedResult<List<LeaveGroupLookupModel>>
                        {
                            Results = leave
                        };
                    }

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;
                        query = query.Where(x => x.Name.Contains(searchTerm)).ToList();
                    }

                    var count = query.Count();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();

                    var list = new List<LeaveGroupLookupModel>();
                    foreach (var item in query)
                    {
                        var leaveGroupEmployees = new List<LeaveGroupEmployeesLookupModel>();

                        foreach (var employee in item.LeaveGroupEmployees)
                        {
                            leaveGroupEmployees.Add(new LeaveGroupEmployeesLookupModel
                            {
                                Id = employee.Id,
                                EmployeeName = employee.EmployeeRegistration?.EnglishName,
                                EmployeeId = employee.EmployeeId,
                                LeaveGroupId = employee.LeaveGroupId,
                            });
                        }

                        list.Add(new LeaveGroupLookupModel
                        {
                            Id = item.Id,
                            Name = item.Name,
                            LeaveGroupEmployees = leaveGroupEmployees,
                            Details = item.Details
                        });
                    }

                    return new PagedResult<List<LeaveGroupLookupModel>>
                    {
                        Results = list,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = list.Count / request.PageSize
                    };
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
