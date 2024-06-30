using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.HR.Payroll.EmployeeSalaries.Commands;
using Focus.Business.HR.Payroll.EmployeeSalaries.Queries;
using Focus.Business.HR.Payroll.SaveSalaryTaxSlab.Commands;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.RunPayrolls.Queries
{
    public class EmployeePayRunListQuery : PagedRequest, IRequest<PagedResult<List<EmployeeSalaryListLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public int PageNumber { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? DesignationId { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? PayrollScheduleId { get; set; }

        public class Handler : IRequestHandler<EmployeePayRunListQuery, PagedResult<List<EmployeeSalaryListLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<EmployeePayRunListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<EmployeeSalaryListLookUpModel>>> Handle(EmployeePayRunListQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var query = _context.EmployeeSalaries
                        .AsNoTracking()
                        .Include(x => x.Employee)
                        .ThenInclude(x => x.LoanPayments)
                        //.Include(x => x.Employee)
                        //.ThenInclude(x => x.EmployeeSalaries)
                        //.ThenInclude(x => x.EmployeeSalaryDetails)
                        .Include(x => x.EmployeeSalaryDetails)
                        .AsQueryable();


                    PaySchedule schedule = null;
                    if (request.PayrollScheduleId != null)
                    {
                        schedule = await _context.PaySchedules
                               .AsNoTracking()
                               .FirstOrDefaultAsync(x => x.Id == request.PayrollScheduleId, cancellationToken: cancellationToken);
                        if (schedule != null)
                        {
                            query = schedule.IsHourlyPay ? query.Where(x => x.SalaryType == "Hourly Based" || x.SalaryType == "بالساعة") : query.Where(x => x.SalaryType == "Salary Based" || x.SalaryType == "على أساس الراتب");
                        }
                    }

                    if (request.DesignationId != null)
                    {
                        query = query.Where(x => x.Employee.DesignationId == request.DesignationId);
                    }

                    if (request.DepartmentId != null)
                    {
                        query = query.Where(x => x.Employee.DepartmentId == request.DepartmentId);
                    }

                    if (request.EmployeeId != null)
                    {
                        query = query.Where(x => x.EmployeeId == request.EmployeeId);
                    }


                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;

                        query = query.Where(x => x.Employee.ArabicName.Contains(searchTerm)
                                                 || x.Employee.EnglishName.Contains(searchTerm)
                                                 || x.SalaryType.Contains(searchTerm));

                    }

                    var count = await query.CountAsync(cancellationToken: cancellationToken);
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var allowances = _context.Allowances.AsNoTracking().Include(x => x.AllowanceType).AsQueryable();
                    var deductions = _context.Deductions.AsNoTracking().AsQueryable();
                    var contributions = _context.Contributions.AsNoTracking().AsQueryable();

                    var list = new List<EmployeeSalaryListLookUpModel>();
                    foreach (var item in query)
                    {

                        //Employee Loan
                        var loan = item.Employee?.LoanPayments.FirstOrDefault(x => !x.IsActive && x.RecoveryMethod == RecoveryMethod.Salary);
                        decimal loanAmount = 0;
                        if (loan != null && schedule != null && loan.PaymentStartDate.Date <= schedule.PayPeriodStartDate.Date)
                        {
                            if (loan.InstallmentMethod == AmountType.Fixed)
                            {
                                loanAmount = loan.RemainingLoan < loan.DeductionValue ? loan.RemainingLoan : loan.DeductionValue;
                            }

                            if (loan.InstallmentMethod == AmountType.PercentageOfSalary)
                            {
                                loanAmount = item.BaseSalary / 100 * loan.DeductionValue;
                                loanAmount = loan.RemainingLoan < loanAmount ? loan.RemainingLoan : loanAmount;
                            }
                        }

                        //Employee Salary Detail
                        var employeeSalaryLookUp = new EmployeeSalaryLookUpModel();

                        var salaryDetail = new List<SalaryDetailLookUpModel>();
                        foreach (var salary in item.EmployeeSalaryDetails)
                        {
                            string name = null;
                            string nameArabic = null;
                            if (salary.Type == "allowance")
                            {
                                name = allowances.FirstOrDefault(x => x.Id == salary.ItemId)?.AllowanceType.Name;
                                nameArabic = allowances.FirstOrDefault(x => x.Id == salary.ItemId)?.AllowanceType.NameArabic;
                            }
                            if (salary.Type == "deduction")
                            {
                                name = deductions.FirstOrDefault(x => x.Id == salary.ItemId)?.NameInPayslip;
                                nameArabic = deductions.FirstOrDefault(x => x.Id == salary.ItemId)?.NameInPayslipArabic;
                            }
                            if (salary.Type == "contribution")
                            {
                                name = contributions.FirstOrDefault(x => x.Id == salary.ItemId)?.NameInPayslip;
                                nameArabic = contributions.FirstOrDefault(x => x.Id == salary.ItemId)?.NameInPayslipArabic;
                            }
                            if (salary.Type == "Income Tax")
                            {
                                name = "Income Tax";
                                nameArabic = "ضريبة الدخل";
                            }



                            salaryDetail.Add(new SalaryDetailLookUpModel()
                            {
                                Id = salary.Id,
                                ItemId = salary.ItemId,
                                NameInPayslip = name,
                                NameInPayslipArabic = nameArabic,
                                Type = salary.Type,
                                AmountType = salary.AmountType,
                                TaxPlan = salary.TaxPlan,
                                Percent = salary.Percent,
                                Amount = salary.Amount,
                                EmployeeSalaryId = salary.EmployeeSalaryId,
                            });
                        }



                        var taxSalary = await _context.TaxSalaries
                            .AsNoTracking()
                            .Include(x => x.SalaryTaxSlabs)
                            .FirstOrDefaultAsync(x => x.FromDate.Date <= DateTime.UtcNow.Date && x.ToDate.Date >= DateTime.UtcNow.Date, cancellationToken: cancellationToken);

                        SalaryTaxSlabLookUpModel salaryTaxSlab = null;
                        if (taxSalary != null)
                        {
                            salaryTaxSlab = new SalaryTaxSlabLookUpModel()
                            {
                                Id = taxSalary.Id,
                                FromDate = taxSalary.FromDate,
                                ToDate = taxSalary.ToDate,
                                SalaryTaxSlabList = taxSalary.SalaryTaxSlabs.Select(x => new SalaryTaxSlab
                                {
                                    Id = x.Id,
                                    IncomeFrom = x.IncomeFrom,
                                    IncomeTo = x.IncomeTo,
                                    FixedTax = x.FixedTax,
                                    Rate = x.Rate
                                }).ToList()
                            };
                        }

                        //Employee Salary
                        list.Add(new EmployeeSalaryListLookUpModel()
                        {
                            Id = item.Id,
                            EmployeeId = item.EmployeeId,
                            EmployeeArabicName = item.Employee?.ArabicName,
                            EmployeeEnglishName = item.Employee?.EnglishName,
                            SalaryType = item.SalaryType,
                            BaseSalary = item.BaseSalary,
                            WeekdayHourlySalary = item.WeekdayHourlySalary,
                            WeekendDayHourlySalary = item.WeekendDayHourlySalary,
                            IncomeTax = item.IncomeTax,
                            AutoIncomeTax = item.AutoIncomeTax,
                            LoanAmount = loanAmount,
                            AllowanceAmount = item.EmployeeSalaryDetails.Where(x => x.Type == "allowance").Sum(x => x.Amount),
                            GrossSalary = item.BaseSalary + item.EmployeeSalaryDetails.Where(x => x.Type == "allowance").Sum(x => x.Amount),
                            DeductionAmount = item.EmployeeSalaryDetails.Where(x => x.Type == "deduction").Sum(x => x.Amount),
                            ContributionAmount = item.EmployeeSalaryDetails.Where(x => x.Type == "contribution").Sum(x => x.Amount),
                            IncomeTaxAmount = item.EmployeeSalaryDetails.Where(x => x.Type == "Income Tax").Sum(x => x.Amount),
                            NetSalary = item.BaseSalary + item.EmployeeSalaryDetails.Where(x => x.Type == "allowance").Sum(x => x.Amount) - item.EmployeeSalaryDetails.Where(x => x.Type == "deduction" || x.Type == "contribution" || x.Type == "Income Tax").Sum(x => x.Amount) - loanAmount,
                            SalaryTaxSlabList = salaryTaxSlab?.SalaryTaxSlabList,
                            SalaryDetailList = salaryDetail,

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
