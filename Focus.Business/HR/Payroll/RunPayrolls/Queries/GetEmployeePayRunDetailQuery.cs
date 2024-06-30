using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.HR.Payroll.EmployeeSalaries.Queries;
using Focus.Business.HR.Payroll.RunPayrolls.Commands;
using Focus.Business.HR.Payroll.SaveSalaryTaxSlab.Commands;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.RunPayrolls.Queries
{
    public class GetEmployeePayRunDetailQuery : IRequest<RunPayrollLookUpModel>
    {
        public Guid? Id { get; set; }
        public class Handler : IRequestHandler<GetEmployeePayRunDetailQuery, RunPayrollLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetEmployeePayRunDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<RunPayrollLookUpModel> Handle(GetEmployeePayRunDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var employeeSalary = await _context.RunPayrolls
                        .AsNoTracking()
                        .Include(x => x.RunPayrollDetails)
                        .ThenInclude(x => x.RunPayrollSalaryDetails)
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                    var employee = _context.EmployeeRegistrations.AsNoTracking().AsQueryable();
                    if (employeeSalary != null)
                    {
                        var allowances = _context.Allowances.AsNoTracking().Include(x => x.AllowanceType).AsQueryable();
                        var deductions = _context.Deductions.AsNoTracking().AsQueryable();
                        var contributions = _context.Contributions.AsNoTracking().AsQueryable();

                        var runPayrollDetail = new List<RunPayrollDetailLookUpModel>();
                        foreach (var item in employeeSalary.RunPayrollDetails)
                        {
                            //Employee Salary Detail
                            var salaryDetail = new List<SalaryDetailLookUpModel>();
                            foreach (var salary in item.RunPayrollSalaryDetails)
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
                            

                            runPayrollDetail.Add(new RunPayrollDetailLookUpModel()
                            {
                                Id = item.Id,
                                EmployeeId = item.EmployeeId,
                                EmployeeArabicName = employee.FirstOrDefault(z => z.Id == item.EmployeeId)?.ArabicName,
                                EmployeeEnglishName = employee.FirstOrDefault(z => z.Id == item.EmployeeId)?.EnglishName,
                                SalaryType = item.SalaryType,
                                BaseSalary = item.BaseSalary,
                                Hour = item.Hour,
                                HourAmount = item.HourAmount,
                                WeekdayHourlySalary = item.WeekdayHourlySalary,
                                AllowanceAmount = item.AllowanceAmount,
                                GrossSalary = item.GrossSalary,
                                DeductionAmount = item.DeductionAmount,
                                ContributionAmount = item.ContributionAmount,
                                IncomeTaxAmount = item.TaxAmount,
                                LoanAmount = item.LoanAmount,
                                NetSalary = item.NetSalary,
                                ZeroSalary = item.ZeroSalary,
                                Reason = item.Reason,
                                IncomeTax = item.IncomeTax,
                                AutoIncomeTax = item.AutoIncomeTax,
                                SalaryTaxSlabList = salaryTaxSlab?.SalaryTaxSlabList,
                                SalaryDetailList = salaryDetail,
                            });
                        }

                        return new RunPayrollLookUpModel()
                        {
                            Id = employeeSalary.Id,
                            Status = employeeSalary.Status,
                            Hourly = employeeSalary.Hourly,
                            PayrollScheduleId = employeeSalary.PayrollScheduleId,
                            SalaryTemplateList = runPayrollDetail
                        };
                    }
                    throw new NotFoundException(nameof(employeeSalary), request.Id);
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
