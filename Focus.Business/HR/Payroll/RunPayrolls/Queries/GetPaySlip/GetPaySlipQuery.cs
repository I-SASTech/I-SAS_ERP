using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.HR.Payroll.RunPayrolls.Queries.RunPayrollPrintQuery;
using Focus.Business.Interface;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.RunPayrolls.Queries.GetPaySlip
{
    public class GetPaySlipQuery : IRequest<PaySlipLookUpModel>
    {
        public Guid RunPayrollId { get; set; }
        public Guid EmployeeId { get; set; }

        public class Handler : IRequestHandler<GetPaySlipQuery, PaySlipLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetPaySlipQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PaySlipLookUpModel> Handle(GetPaySlipQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var employeeSalary = await _context.RunPayrollDetails
                        .AsNoTracking()
                        .Include(x => x.RunPayroll)
                        .ThenInclude(x => x.PaySchedule)
                        .Include(x => x.RunPayrollSalaryDetails)
                        .FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeId && x.RunPayrollId == request.RunPayrollId, cancellationToken: cancellationToken);

                    var employee = await _context.EmployeeRegistrations
                        .AsNoTracking()
                        .Include(x=>x.Department)
                        .Include(x=>x.Designation)
                        .FirstOrDefaultAsync(x=>x.Id==request.EmployeeId, cancellationToken: cancellationToken);

                    if (employeeSalary != null)
                    {
                        var allowances = _context.Allowances.AsNoTracking().Include(x => x.AllowanceType).OrderBy(x => x.Code).AsQueryable();
                        var deductions = _context.Deductions.AsNoTracking().OrderBy(x => x.Code).AsQueryable();
                        var contributions = _context.Contributions.AsNoTracking().OrderBy(x => x.Code).AsQueryable();

                        //Employee Salary Detail
                        var allowanceList = new List<AllowanceListModel>();
                        var deductionList = new List<DeductionListModel>();
                        var contributionList = new List<ContributionListModel>();

                        foreach (var item in employeeSalary.RunPayrollSalaryDetails)
                        {
                            if (item.Type == "allowance")
                            {
                                decimal amount;
                                if (item.AmountType == AmountType.PercentageOfSalary)
                                {
                                    amount = employeeSalary.BaseSalary / 100 * item.Percent;
                                }
                                else
                                {
                                    amount = item.Amount;
                                }
                                allowanceList.Add(new AllowanceListModel()
                                {
                                    Id = item.Id,
                                    ItemId = item.ItemId,
                                    NameInPayslip = allowances.FirstOrDefault(x => x.Id == item.ItemId)?.AllowanceType.Name,
                                    NameInPayslipArabic = allowances.FirstOrDefault(x => x.Id == item.ItemId)?.AllowanceType.NameArabic,
                                    Type = item.Type,
                                    Amount = amount,
                                });
                            }

                            if (item.Type == "deduction")
                            {
                                decimal amount;
                                if (item.AmountType == AmountType.PercentageOfSalary)
                                {
                                    amount = employeeSalary.BaseSalary / 100 * item.Percent;
                                }
                                else
                                {
                                    amount = item.Amount;
                                }
                                deductionList.Add(new DeductionListModel()
                                {
                                    Id = item.Id,
                                    ItemId = item.ItemId,
                                    NameInPayslip = deductions.FirstOrDefault(x => x.Id == item.ItemId)?.NameInPayslip,
                                    NameInPayslipArabic = deductions.FirstOrDefault(x => x.Id == item.ItemId)?.NameInPayslipArabic,
                                    Type = item.Type,
                                    Amount = amount,
                                });
                            }

                            if (item.Type == "contribution")
                            {
                                decimal amount;
                                if (item.AmountType == AmountType.PercentageOfSalary)
                                {
                                    amount = employeeSalary.BaseSalary / 100 * item.Percent;
                                }
                                else
                                {
                                    amount = item.Amount;
                                }
                                contributionList.Add(new ContributionListModel()
                                {
                                    Id = item.Id,
                                    ItemId = item.ItemId,
                                    NameInPayslip = contributions.FirstOrDefault(x => x.Id == item.ItemId)?.NameInPayslip,
                                    NameInPayslipArabic = contributions.FirstOrDefault(x => x.Id == item.ItemId)?.NameInPayslipArabic,
                                    Type = item.Type,
                                    Amount = amount,
                                });
                            }
                        }

                        //var paySchedule = await _context.PaySchedules.AsNoTracking()
                        //    .FirstOrDefaultAsync(x => x.Id == employeeSalary.RunPayroll.PayrollScheduleId, cancellationToken: cancellationToken);

                        return new PaySlipLookUpModel()
                        {
                            Id = employeeSalary.Id,
                            EmployeeId = employeeSalary.EmployeeId,
                            EmployeeArabicName = employee?.ArabicName,
                            EmployeeEnglishName = employee?.EnglishName,
                            EmployeeCode = employee?.Code,
                            EmployeeDepartment = employee?.Department?.Name,
                            EmployeeDesignation = employee?.Designation?.Name,
                            EmployeeGender = employee?.Gender,
                            EmployeeJoiningDate = employee?.RegistrationDate!=null? employee.RegistrationDate.Value.ToString("d") :"",
                            EmployeeIDNumber = employee?.IDNumber,



                            SalaryPeriod = employeeSalary.RunPayroll.PaySchedule?.PayPeriodEndDate.ToString("Y"),
                            SalaryType = employeeSalary.SalaryType,
                            BaseSalary = employeeSalary.BaseSalary,
                            Hour = employeeSalary.Hour,
                            HourAmount = employeeSalary.HourAmount,
                            WeekdayHourlySalary = employeeSalary.WeekdayHourlySalary,
                            AllowanceAmount = employeeSalary.AllowanceAmount,
                            GrossSalary = employeeSalary.GrossSalary,
                            DeductionAmount = employeeSalary.DeductionAmount,
                            ContributionAmount = employeeSalary.ContributionAmount,
                            IncomeTaxAmount = employeeSalary.TaxAmount,
                            LoanAmount = employeeSalary.LoanAmount,
                            NetSalary = employeeSalary.NetSalary,
                            ZeroSalary = employeeSalary.ZeroSalary,
                            Reason = employeeSalary.Reason,
                            IncomeTax = employeeSalary.IncomeTax,
                            AutoIncomeTax = employeeSalary.AutoIncomeTax,
                            AllowanceList = allowanceList,
                            DeductionList = deductionList,
                            ContributionList = contributionList,
                        };
                    }
                    throw new NotFoundException(nameof(employeeSalary), request.EmployeeId);
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
