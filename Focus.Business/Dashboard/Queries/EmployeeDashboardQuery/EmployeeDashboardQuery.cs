
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using AutoMapper;
using Focus.Business.Interface;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.Dashboard.Queries.EmployeeDashboardQuery
{
   public class EmployeeDashboardQuery : IRequest<EmployeeDashboardLookUpModel>
    {
        public string OverViewFilter { get; set; }

        public class Handler : IRequestHandler<EmployeeDashboardQuery, EmployeeDashboardLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                 ILogger<EmployeeDashboardLookUpModel> logger,
                 IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<EmployeeDashboardLookUpModel> Handle(EmployeeDashboardQuery request, CancellationToken cancellationToken)

            {
                try
                {

                    var employees = _context.EmployeeRegistrations
                        .AsNoTracking()
                        .Include(x=>x.EmployeeSalaries)
                        .Include(x=>x.Department)
                        .Where(x => !x.IsActive)
                        .ToList();
                    var departmentWiseSalaryList = new List<DepartmentWiseSalary>();
                    var departmentList = _context.Departments.AsNoTracking()
                        .Include(x=>x.EmployeeRegistrations)
                        .ThenInclude(x=>x.EmployeeSalaries)
                        .Include(x=>x.EmployeeRegistrations)
                        .ThenInclude(x => x.Department)
                        .Where(x=>x.IsActive).ToList();
                    var department = departmentList.Count(x=>x.IsActive);
                    var grossSalary = employees.Sum(x => x.EmployeeSalaries.Sum(x => x.BaseSalary));
                    var departmentEmployee = employees.Select(x => x.Department);
                    var remainingLoan = _context.LoanPayments.Sum(x => x.RemainingLoan);
                    var date = DateTime.Now;

                    if (departmentList != null)
                    {
                        var groupByDepartment = departmentList.GroupBy(x => x.EmployeeRegistrations);

                        foreach( var dep in groupByDepartment)
                        {
                            departmentWiseSalaryList.Add(new DepartmentWiseSalary{

                                CountEmployee = dep.Key.Count(),
                                Name= dep.FirstOrDefault().Name,
                                Salary=dep.Sum(x=>x.EmployeeRegistrations.Sum(x=>x.EmployeeSalaries.Sum(x=>x.BaseSalary)))
                            });
                        }
                        //var result = departmentEmployee.Except(departmentEmployee.Where(o => employees.Select(s => s.DepartmentId).ToList().Contains(o.Id))).ToList();
                    }



                        var attendances = _context.ManualAttendances.AsNoTracking().AsQueryable();
                    var totalEmployee = employees.Count();
                    var maleEmployee = employees.Count(x=>x.Gender=="Male");
                    var femaleEmployee = employees.Count(x => x.Gender == "Fe-Male");
                    var otherEmployee = employees.Count(x => x.Gender == "Other");
                    var todayPresentEmployee = attendances.Count(x => x.Date.Date == date.Date && !x.IsOnLeave);
                    var employeeOnLeave = attendances.Count(x => x.Date.Date == date.Date && x.IsOnLeave);

                    var gender = new List<int>();
                    gender.Add(maleEmployee);
                    gender.Add(femaleEmployee);
                    gender.Add(otherEmployee);

                    return new EmployeeDashboardLookUpModel
                    {

                        TotalEmployee = totalEmployee,
                        RemainingLoan = remainingLoan,
                        GrossSalary = grossSalary,
                        TotalDepartment = department,
                        TotalPresentEmployee = todayPresentEmployee,
                        TotalAbsentEmployee = totalEmployee - (todayPresentEmployee + employeeOnLeave),
                        EmployeeOnLeave = employeeOnLeave,
                        GenderList = gender,
                        DepartmentWiseSalaryList = departmentWiseSalaryList,


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
