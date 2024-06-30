using Focus.Business.Common;
using Focus.Business.EmployeeRegistrations.Queries.GetEmployeeRegistrationList;
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
    public class SearchEmployeeListQuery : PagedRequest, IRequest<PagedResult<List<EmployeeRegistrationLookUpModel>>>
    {

        public string SearchTerm { get; set; }
        public EmployeeSearchModel EmployeeSearch { get; set; }

        public class Handler : IRequestHandler<SearchEmployeeListQuery, PagedResult<List<EmployeeRegistrationLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context,
                ILogger<SearchEmployeeListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<EmployeeRegistrationLookUpModel>>> Handle(SearchEmployeeListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query =await (from employee in _context.EmployeeRegistrations
                                 where employee.DepartmentId != null && employee.DepartmentId != null
                                 select new EmployeeRegistrationLookUpModel
                                 {
                                     Id = employee.Id,
                                     Code = employee.Code,
                                     EnglishName = employee.EnglishName,
                                     ArabicName = employee.ArabicName,
                                     DepartmentId = employee.DepartmentId,
                                     DesignationId = employee.DesignationId,
                                 }).OrderBy(x => x.Code).ToListAsync(cancellationToken: cancellationToken);


                    //if (!string.IsNullOrEmpty(request.SearchTerm))
                    //{
                    //    var searchTerm = request.SearchTerm;
                    //    query = query.Where(x =>
                    //        x.EnglishName.Contains(searchTerm) || x.ArabicName.Contains(searchTerm) || x.Code.Contains(searchTerm));

                    //}

                    if (request.EmployeeSearch.DepartmentIds != null)
                    {
                        query = query.Where(x =>request.EmployeeSearch.DepartmentIds.Contains((Guid)x.DepartmentId)).ToList();
                    }
                    if (request.EmployeeSearch.DesignationIds != null)
                    {
                        query = query.Where(x => request.EmployeeSearch.DesignationIds.Contains((Guid)x.DesignationId)).ToList();
                    }



                    var count =  query.Count();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();




                    return new PagedResult<List<EmployeeRegistrationLookUpModel>>
                    {
                        Results = query,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = query.Count / request.PageSize
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
