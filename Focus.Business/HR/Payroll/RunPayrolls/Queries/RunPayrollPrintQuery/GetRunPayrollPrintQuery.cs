using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.RunPayrolls.Queries.RunPayrollPrintQuery
{
    public class GetRunPayrollPrintQuery : IRequest<RunPayrollPrintLookUpModel>
    {

        public Guid? Id { get; set; }
        public string Prop { get; set; }

        public class Handler : IRequestHandler<GetRunPayrollPrintQuery, RunPayrollPrintLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetRunPayrollPrintQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<RunPayrollPrintLookUpModel> Handle(GetRunPayrollPrintQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var employeeSalary = await _context.RunPayrolls
                        .AsNoTracking()
                        .Include(x => x.PaySchedule)
                        .Include(x => x.RunPayrollDetails)
                        .ThenInclude(x => x.RunPayrollSalaryDetails)
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                    var employee = _context.EmployeeRegistrations.AsNoTracking().AsQueryable();

                    IQueryable<Guid> employeeId;
                    employeeId = request.Prop == "Bank" ? employee.Where(x => x.SalaryType == "Bank" || x.SalaryType == "بنكي").Select(x => x.Id) : employee.Where(x => x.SalaryType != "Bank" && x.SalaryType != "بنكي").Select(x => x.Id);


                    if (employeeSalary != null)
                    {
                        var runPayrollDetails = employeeSalary.RunPayrollDetails.Where(x => employeeId.Contains(x.EmployeeId));

                        var allowances = _context.Allowances.AsNoTracking().Include(x => x.AllowanceType).OrderBy(x => x.Code).AsQueryable();
                        var deductions = _context.Deductions.AsNoTracking().OrderBy(x => x.Code).AsQueryable();
                        var contributions = _context.Contributions.AsNoTracking().OrderBy(x => x.Code).AsQueryable();

                        var allowanceHeader = allowances.Select(x => new AllowanceListModel
                        {
                            Id = x.Id,
                            NameInPayslip = x.AllowanceType.Name,
                            NameInPayslipArabic = x.AllowanceType.NameArabic
                        }).ToList();

                        var deductionHeader = deductions.Select(x => new DeductionListModel
                        {
                            Id = x.Id,
                            NameInPayslip = x.NameInPayslip,
                            NameInPayslipArabic = x.NameInPayslipArabic
                        }).ToList();

                        var contributionHeader = contributions.Select(x => new ContributionListModel
                        {
                            Id = x.Id,
                            NameInPayslip = x.NameInPayslip,
                            NameInPayslipArabic = x.NameInPayslipArabic
                        }).ToList();

                        var runPayrollDetail = new List<RunPayrollDetailPrintLookUpModel>();
                        foreach (var item in runPayrollDetails)
                        {
                            //Employee Salary Detail
                            var allowanceList = new List<AllowanceListModel>();
                            var deductionList = new List<DeductionListModel>();
                            var contributionList = new List<ContributionListModel>();

                            foreach (var allowance in allowances)
                            {
                                var allowanceDetail = item.RunPayrollSalaryDetails.FirstOrDefault(x => x.ItemId == allowance.Id);
                                if (allowanceDetail != null)
                                {
                                    decimal amount;
                                    if (allowanceDetail.AmountType == AmountType.PercentageOfSalary)
                                    {
                                        amount = item.BaseSalary / 100 * allowanceDetail.Percent;
                                    }
                                    else
                                    {
                                        amount = allowanceDetail.Amount;
                                    }
                                    allowanceList.Add(new AllowanceListModel()
                                    {
                                        Id = allowance.Id,
                                        ItemId = allowanceDetail.ItemId,
                                        NameInPayslip = allowance.AllowanceType.Name,
                                        NameInPayslipArabic = allowance.AllowanceType.NameArabic,
                                        Type = allowanceDetail.Type,
                                        Amount = amount,
                                    });
                                }
                                else
                                {
                                    allowanceList.Add(new AllowanceListModel()
                                    {
                                        Id = allowance.Id,
                                        ItemId = allowance.Id,
                                        NameInPayslip = allowance.AllowanceType.Name,
                                        NameInPayslipArabic = allowance.AllowanceType.NameArabic,
                                        Type = "allowance",
                                        Amount = 0
                                    });
                                }
                            }

                            foreach (var deduction in deductions)
                            {
                                var deductionDetail = item.RunPayrollSalaryDetails.FirstOrDefault(x => x.ItemId == deduction.Id);
                                if (deductionDetail != null)
                                {
                                    decimal amount;
                                    if (deductionDetail.AmountType == AmountType.PercentageOfSalary)
                                    {
                                        amount = item.BaseSalary / 100 * deductionDetail.Percent;
                                    }
                                    else
                                    {
                                        amount = deductionDetail.Amount;
                                    }
                                    deductionList.Add(new DeductionListModel()
                                    {
                                        Id = deduction.Id,
                                        ItemId = deductionDetail.ItemId,
                                        NameInPayslip = deduction.NameInPayslip,
                                        NameInPayslipArabic = deduction.NameInPayslipArabic,
                                        Type = deductionDetail.Type,
                                        Amount = amount,
                                    });
                                }
                                else
                                {
                                    deductionList.Add(new DeductionListModel()
                                    {
                                        Id = deduction.Id,
                                        ItemId = deduction.Id,
                                        NameInPayslip = deduction.NameInPayslip,
                                        NameInPayslipArabic = deduction.NameInPayslipArabic,
                                        Type = "deduction",
                                        Amount = 0
                                    });
                                }
                            }

                            foreach (var contribution in contributions)
                            {
                                var contributionDetail = item.RunPayrollSalaryDetails.FirstOrDefault(x => x.ItemId == contribution.Id);
                                if (contributionDetail != null)
                                {
                                    decimal amount;
                                    if (contributionDetail.AmountType == AmountType.PercentageOfSalary)
                                    {
                                        amount = item.BaseSalary / 100 * contributionDetail.Percent;
                                    }
                                    else
                                    {
                                        amount = contributionDetail.Amount;
                                    }
                                    contributionList.Add(new ContributionListModel()
                                    {
                                        Id = contribution.Id,
                                        ItemId = contributionDetail.ItemId,
                                        NameInPayslip = contribution.NameInPayslip,
                                        NameInPayslipArabic = contribution.NameInPayslipArabic,
                                        Type = contributionDetail.Type,
                                        Amount = amount,
                                    });
                                }
                                else
                                {
                                    contributionList.Add(new ContributionListModel()
                                    {
                                        Id = contribution.Id,
                                        ItemId = contribution.Id,
                                        NameInPayslip = contribution.NameInPayslip,
                                        NameInPayslipArabic = contribution.NameInPayslipArabic,
                                        Type = "deduction",
                                        Amount = 0
                                    });
                                }
                            }

                            runPayrollDetail.Add(new RunPayrollDetailPrintLookUpModel()
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
                                AllowanceList = allowanceList,
                                DeductionList = deductionList,
                                ContributionList = contributionList,
                            });
                        }

                        var allowanceFooter = new List<AllowanceListModel>();
                        var deductionFooter = new List<DeductionListModel>();
                        var contributionFooter = new List<ContributionListModel>();

                        foreach (var item in runPayrollDetail)
                        {
                            foreach (var allowance in item.AllowanceList)
                            {
                                var find = allowanceFooter.FirstOrDefault(x => x.Id == allowance.Id);
                                if (find != null)
                                {
                                    find.Amount = find.Amount + allowance.Amount;
                                }
                                else
                                {
                                    allowanceFooter.Add(new AllowanceListModel()
                                    {
                                        Id = allowance.Id,
                                        ItemId = allowance.Id,
                                        Amount = allowance.Amount
                                    });
                                }
                            }

                            foreach (var deduction in item.DeductionList)
                            {
                                var find = deductionFooter.FirstOrDefault(x => x.Id == deduction.Id);
                                if (find != null)
                                {
                                    find.Amount = find.Amount + deduction.Amount;
                                }
                                else
                                {
                                    deductionFooter.Add(new DeductionListModel()
                                    {
                                        Id = deduction.Id,
                                        ItemId = deduction.Id,
                                        Amount = deduction.Amount
                                    });
                                }
                            }

                            foreach (var contribution in item.ContributionList)
                            {
                                var find = contributionFooter.FirstOrDefault(x => x.Id == contribution.Id);
                                if (find != null)
                                {
                                    find.Amount = find.Amount + contribution.Amount;
                                }
                                else
                                {
                                    contributionFooter.Add(new ContributionListModel()
                                    {
                                        Id = contribution.Id,
                                        ItemId = contribution.Id,
                                        Amount = contribution.Amount
                                    });
                                }
                            }
                        }

                        return new RunPayrollPrintLookUpModel()
                        {
                            Id = employeeSalary.Id,
                            Status = employeeSalary.Status,
                            PayrollSchedule = employeeSalary.PaySchedule?.Name + " " + employeeSalary.PaySchedule?.PayPeriodStartDate.ToString("d") + " - " + employeeSalary.PaySchedule?.PayPeriodEndDate.ToString("d"),
                            Hourly = employeeSalary.Hourly,
                            PayrollScheduleId = employeeSalary.PayrollScheduleId,
                            AllowanceHeader = allowanceHeader,
                            DeductionHeader = deductionHeader,
                            ContributionHeader = contributionHeader,

                            AllowanceFooter = allowanceFooter,
                            DeductionFooter = deductionFooter,
                            ContributionFooter = contributionFooter,

                            SalaryTemplateList = runPayrollDetail,
                            TotalBaseSalary = runPayrollDetail.Sum(x => x.BaseSalary),
                            TotalNetSalary = runPayrollDetail.Sum(x => x.NetSalary),
                            TotalLoanAmount = runPayrollDetail.Sum(x => x.LoanAmount),
                            TotalIncomeTaxAmount = runPayrollDetail.Sum(x => x.IncomeTaxAmount),
                            TotalHourAmount = runPayrollDetail.Sum(x => x.HourAmount),
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
