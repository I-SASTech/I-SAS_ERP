using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.EmployeeSalaries.Queries
{
    public class GetEmployeeSalaryListQuery : PagedRequest, IRequest<PagedResult<List<EmployeeSalaryListLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public int PageNumber { get; set; }

        public class Handler : IRequestHandler<GetEmployeeSalaryListQuery, PagedResult<List<EmployeeSalaryListLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetEmployeeSalaryListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<EmployeeSalaryListLookUpModel>>> Handle(GetEmployeeSalaryListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.EmployeeSalaries
                        .AsNoTracking()
                        .Include(x=>x.Employee)
                        .Include(x=>x.EmployeeSalaryDetails)
                        .AsQueryable();
                    

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;

                        query = query.Where(x => x.Employee.ArabicName.Contains(searchTerm) 
                                                 || x.Employee.EnglishName.Contains(searchTerm) 
                                                 || x.SalaryType.Contains(searchTerm));

                    }

                    var count = await query.CountAsync(cancellationToken: cancellationToken);
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var list = new List<EmployeeSalaryListLookUpModel>();
                    foreach (var item in query)
                    {
                        list.Add(new EmployeeSalaryListLookUpModel()
                        {
                            Id = item.Id,
                            EmployeeArabicName = item.Employee?.ArabicName,
                            EmployeeEnglishName = item.Employee?.EnglishName,
                            SalaryType = item.SalaryType,
                            BaseSalary = item.BaseSalary,
                            AllowanceAmount = item.EmployeeSalaryDetails.Where(x=>x.Type== "allowance").Sum(x=>x.Amount),
                            GrossSalary = item.BaseSalary + item.EmployeeSalaryDetails.Where(x=>x.Type== "allowance").Sum(x=>x.Amount),
                            DeductionAmount = item.EmployeeSalaryDetails.Where(x=>x.Type== "deduction").Sum(x=>x.Amount),
                            ContributionAmount = item.EmployeeSalaryDetails.Where(x=>x.Type== "contribution").Sum(x=>x.Amount),
                            IncomeTaxAmount = item.EmployeeSalaryDetails.Where(x=>x.Type== "Income Tax").Sum(x=>x.Amount),
                            NetSalary = item.BaseSalary + item.EmployeeSalaryDetails.Where(x => x.Type == "allowance").Sum(x => x.Amount) - item.EmployeeSalaryDetails.Where(x => x.Type == "deduction" || x.Type == "contribution" || x.Type == "Income Tax").Sum(x => x.Amount),
                        });
                    }

                    return new PagedResult<List<EmployeeSalaryListLookUpModel>>
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
