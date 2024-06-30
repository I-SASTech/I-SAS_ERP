using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.HR.Payroll.EmployeeSalaries.Commands;
using Focus.Business.HR.Payroll.SaveSalaryTaxSlab.Commands;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.EmployeeSalaries.Queries
{
    public class GetEmployeeSalaryDetailQuery : IRequest<EmployeeSalaryLookUpModel>
    {
        public Guid? Id { get; set; }
        public class Handler : IRequestHandler<GetEmployeeSalaryDetailQuery, EmployeeSalaryLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetEmployeeSalaryDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<EmployeeSalaryLookUpModel> Handle(GetEmployeeSalaryDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var employeeSalary = await _context.EmployeeSalaries
                        .AsNoTracking()
                        .Include(x => x.EmployeeSalaryDetails)
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                    if (employeeSalary != null)
                    {
                        var allowances = _context.Allowances.AsNoTracking().Include(x=>x.AllowanceType).AsQueryable();
                        var deductions = _context.Deductions.AsNoTracking().AsQueryable();
                        var contributions = _context.Contributions.AsNoTracking().AsQueryable();

                        var salaryDetail = new List<SalaryDetailLookUpModel>();
                        foreach (var item in employeeSalary.EmployeeSalaryDetails)
                        {
                            string name = null;
                            string nameArabic = null;
                            if (item.Type== "allowance")
                            {
                                name = allowances.FirstOrDefault(x => x.Id == item.ItemId)?.AllowanceType.Name;
                                nameArabic = allowances.FirstOrDefault(x => x.Id == item.ItemId)?.AllowanceType.NameArabic;
                            }
                            if (item.Type== "deduction")
                            {
                                name = deductions.FirstOrDefault(x => x.Id == item.ItemId)?.NameInPayslip;
                                nameArabic = deductions.FirstOrDefault(x => x.Id == item.ItemId)?.NameInPayslipArabic;
                            }
                            if (item.Type== "contribution")
                            {
                                name = contributions.FirstOrDefault(x => x.Id == item.ItemId)?.NameInPayslip;
                                nameArabic = contributions.FirstOrDefault(x => x.Id == item.ItemId)?.NameInPayslipArabic;
                            }
                            if (item.Type== "Income Tax")
                            {
                                name = "Income Tax";
                                nameArabic = "ضريبة الدخل";
                            }

                            

                            salaryDetail.Add(new SalaryDetailLookUpModel()
                            {
                                Id = item.Id,
                                ItemId = item.ItemId,
                                NameInPayslip = name,
                                NameInPayslipArabic = nameArabic,
                                Type = item.Type,
                                AmountType = item.AmountType,
                                TaxPlan = item.TaxPlan,
                                Percent = item.Percent,
                                Amount = item.Amount,
                                EmployeeSalaryId = item.EmployeeSalaryId,
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

                        return new EmployeeSalaryLookUpModel()
                        {
                            Id = employeeSalary.Id,
                            SalaryType = employeeSalary.SalaryType,
                            PayPeriodId = employeeSalary.PayPeriodId,
                            BaseSalary = employeeSalary.BaseSalary,
                            WeekdayHourlySalary = employeeSalary.WeekdayHourlySalary,
                            WeekendDayHourlySalary = employeeSalary.WeekendDayHourlySalary,
                            SalaryTemplateId = employeeSalary.SalaryTemplateId,
                            EmployeeId = employeeSalary.EmployeeId,
                            StartingDate = employeeSalary.StartingDate,
                            IncomeTax = employeeSalary.IncomeTax,
                            AutoIncomeTax = employeeSalary.AutoIncomeTax,
                            GosiRate = employeeSalary.GosiRate,
                            SalaryDetailList = salaryDetail,
                            SalaryTaxSlabList = salaryTaxSlab?.SalaryTaxSlabList,
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
