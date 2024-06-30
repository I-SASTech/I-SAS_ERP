using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.HR.Payroll.ShiftAssigns.Model;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.HR.Payroll.ShiftAssigns.Queries
{
    public class GetShiftAssignListQuery : PagedRequest, IRequest<PagedResult<List<ShiftAssignModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }

        public class Handler : IRequestHandler<GetShiftAssignListQuery, PagedResult<List<ShiftAssignModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetShiftAssignListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<ShiftAssignModel>>> Handle(GetShiftAssignListQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var query =await (from shift in _context.ShiftAssigns
                                 select new
                                 {
                                     shift.Id,
                                     shift.Code,
                                     shift.ShiftName,
                                     EmployeeName = shift.Employee.EnglishName,
                                     shift.FromDate,
                                     shift.ToDate,
                                     shift.IsActive,
                                     shift.ReasonOfClosingShift,
                                 }).ToListAsync(cancellationToken: cancellationToken);

                    if (request.IsDropDown)
                    {
                        var deduction = new List<ShiftAssignModel>();
                        foreach (var item in query)
                        {
                            deduction.Add(new ShiftAssignModel()
                            {
                                Id = item.Id,
                                ShiftName = item.ShiftName,
                                EmployeeName = item.EmployeeName,
                                IsActive = item.IsActive,
                                FromDate = item.FromDate,
                                ToDate = item.ToDate,
                                ReasonOfClosingShift = item.ReasonOfClosingShift,
                            });
                        }

                        return new PagedResult<List<ShiftAssignModel>>
                        {
                            Results = deduction
                        };
                    }

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;
                        query = query.Where(x => x.ShiftName.Contains(searchTerm) || x.Code.Contains(searchTerm)).ToList();
                    }

                    var count =  query.Count();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();

                    var list = new List<ShiftAssignModel>();
                    foreach (var item in query)
                    {
                        list.Add(new ShiftAssignModel()
                        {
                            Id = item.Id,
                            Code = item.Code,
                            ShiftName = item.ShiftName,
                            EmployeeName = item.EmployeeName,
                            IsActive = item.IsActive,
                            FromDate = item.FromDate,
                            ToDate = item.ToDate,
                            ReasonOfClosingShift = item.ReasonOfClosingShift,
                        });
                    }

                    return new PagedResult<List<ShiftAssignModel>>
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
