using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Transaction = Focus.Domain.Entities.Transaction;

namespace Focus.Business.HR.Payroll.RunPayrolls.Commands
{
    public class AddUpdateRunPayrollCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public RunPayrollLookUpModel RunPayroll { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateRunPayrollCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateRunPayrollCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateRunPayrollCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.RunPayroll.Id != Guid.Empty)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }


                        var runPayroll = await Context.RunPayrolls.FindAsync(request.RunPayroll.Id);
                        if (runPayroll == null)
                            throw new NotFoundException("Run Payroll", "");

                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        foreach (var item in runPayroll.RunPayrollDetails)
                        {
                            Context.RunPayrollSalaryDetails.RemoveRange(item.RunPayrollSalaryDetails);
                        }

                        Context.RunPayrollDetails.RemoveRange(runPayroll.RunPayrollDetails);

                        runPayroll.Date = DateTime.UtcNow;
                        runPayroll.PayrollScheduleId = request.RunPayroll.PayrollScheduleId;
                        runPayroll.Status = request.RunPayroll.Status;
                        runPayroll.Hourly = request.RunPayroll.Hourly;


                        CompanySubmissionPeriod period = null;
                        List<Account> accounts = null;
                        IQueryable<EmployeeRegistration> employeeAccountIds = null;
                        Guid vatAccountId = default;

                        if (request.RunPayroll.Status)
                        {
                            //Transaction
                            period = await Context.CompanySubmissionPeriods
                                .FirstOrDefaultAsync(x => x.PeriodStart.Date <= DateTime.UtcNow.Date
                                                          && x.PeriodEnd.Date >= DateTime.UtcNow.Date, cancellationToken: cancellationToken);

                            accounts = await Context.Accounts.AsNoTracking().Where(x =>
                                    x.Code == "10100001" ||             //Cash in Hand
                                    x.Code == "10500001" ||             //Banks
                                    x.Code == "60300101" ||             //Payroll Expense
                                    x.Name == "VAT Payable on Payroll"  //Payroll Vat
                            ).ToListAsync(cancellationToken: cancellationToken);

                            employeeAccountIds = Context.EmployeeRegistrations.AsNoTracking().AsQueryable();

                            var vatAccount = accounts.FirstOrDefault(x => x.Name == "VAT Payable on Payroll");
                            if (vatAccount == null)
                            {
                                var costCenter = await Context.CostCenters
                                    .AsNoTracking()
                                    .Include(x => x.Accounts)
                                    .FirstOrDefaultAsync(x => x.Code == "250000", cancellationToken: cancellationToken);

                                var code = costCenter.Accounts.OrderBy(x => x.Code).LastOrDefault()?.Code;
                                var account = new Account()
                                {
                                    Code = (Convert.ToInt64(code) + 1).ToString(),
                                    Name = "VAT Payable on Payroll",
                                    NameArabic = "ضريبة القيمة المضافة المستحقة على كشوف المرتبات",
                                    Description = "VAT Payable on Payroll",
                                    IsActive = true,
                                    CostCenterId = costCenter.Id
                                };
                                await Context.Accounts.AddAsync(account, cancellationToken);
                                vatAccountId = account.Id;
                            }
                            else
                            {
                                vatAccountId = vatAccount.Id;
                            }
                        }

                        foreach (var salary in request.RunPayroll.SalaryTemplateList)
                        {
                            var runPayrollDetail = new RunPayrollDetail()
                            {
                                RunPayrollId = runPayroll.Id,
                                EmployeeId = salary.EmployeeId,
                                BaseSalary = salary.BaseSalary,
                                SalaryType = salary.SalaryType,
                                Hour = salary.Hour,
                                HourAmount = salary.HourAmount,
                                WeekdayHourlySalary = salary.WeekdayHourlySalary,
                                AllowanceAmount = salary.AllowanceAmount,
                                GrossSalary = salary.GrossSalary,
                                DeductionAmount = salary.DeductionAmount,
                                ContributionAmount = salary.ContributionAmount,
                                TaxAmount = salary.IncomeTaxAmount,
                                LoanAmount = salary.LoanAmount,
                                NetSalary = salary.NetSalary,
                                ZeroSalary = salary.ZeroSalary,
                                Reason = salary.Reason,
                                IncomeTax = salary.IncomeTax,
                                AutoIncomeTax = salary.AutoIncomeTax,
                            };
                            await Context.RunPayrollDetails.AddAsync(runPayrollDetail, cancellationToken);

                            if (salary.LoanAmount > 0)
                            {
                                var empLoan = await Context.LoanPayments
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.EmployeeRegistrationId == salary.EmployeeId && x.RecoveryMethod == RecoveryMethod.Salary && !x.IsActive, cancellationToken: cancellationToken);

                                if (empLoan != null)
                                {
                                    var loanPay = new LoanPay()
                                    {
                                        PaymentDate = DateTime.UtcNow,
                                        Amount = salary.LoanAmount,
                                        RemainingLoan = empLoan.RemainingLoan - salary.LoanAmount,
                                        Comments = "Pay by salary",
                                        LoanPaymentId = empLoan.Id,
                                    };
                                    await Context.LoanPays.AddAsync(loanPay, cancellationToken);

                                    empLoan.RemainingLoan = empLoan.RemainingLoan - salary.LoanAmount;
                                    if (empLoan.RemainingLoan <= 0)
                                    {
                                        empLoan.IsActive = true;
                                    }
                                    Context.LoanPayments.Update(empLoan);
                                }
                            }


                            if (!salary.ZeroSalary)
                            {
                                foreach (var item in salary.SalaryDetailList)
                                {
                                    var runPayrollSalaryDetail = new RunPayrollSalaryDetail()
                                    {
                                        RunPayrollDetailId = runPayrollDetail.Id,
                                        ItemId = item.ItemId,
                                        Type = item.Type,
                                        AmountType = item.AmountType,
                                        TaxPlan = item.TaxPlan,
                                        Percent = item.Percent,
                                        Amount = item.Amount,
                                    };
                                    await Context.RunPayrollSalaryDetails.AddAsync(runPayrollSalaryDetail, cancellationToken);
                                }
                            }

                            if (request.RunPayroll.Status)
                            {

                                //Transaction

                                var transactions = new List<Transaction>
                            {
                                //Employee Transaction Cr
                                new Transaction
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.RunPayroll.Date,
                                    ApprovalDate = DateTime.UtcNow,
                                    AccountId = employeeAccountIds.FirstOrDefault(x => x.Id == salary.EmployeeId)?.PayableAccountId,
                                    Debit = 0,
                                    Credit = Math.Abs(Math.Round(salary.NetSalary + salary.LoanAmount + salary.IncomeTaxAmount, 2)),
                                    Description = TransactionType.Payroll.ToString(),
                                    DocumentId = runPayrollDetail.Id,
                                    TransactionType = TransactionType.Payroll,
                                    Year = DateTime.UtcNow.Year.ToString(),
                                    DocumentNumber = runPayrollDetail.RunPayroll.Date.ToString("d"),
                                    PeriodId = period.Id
                                },
                                
                                //Expense Transaction
                                new Transaction
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.RunPayroll.Date,
                                    ApprovalDate = DateTime.UtcNow,
                                    AccountId = accounts.FirstOrDefault(x => x.Code == "60300101")?.Id,
                                    Debit = Math.Abs(Math.Round(salary.NetSalary + salary.LoanAmount + salary.IncomeTaxAmount, 2)),
                                    Credit = 0,
                                    Description = TransactionType.Payroll.ToString(),
                                    DocumentId = runPayrollDetail.Id,
                                    TransactionType = TransactionType.Payroll,
                                    Year = DateTime.UtcNow.Year.ToString(),
                                    DocumentNumber = runPayrollDetail.RunPayroll.Date.ToString("d"),
                                    PeriodId = period.Id
                                },

                                //Employee Transaction Dr
                                new Transaction
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.RunPayroll.Date,
                                    ApprovalDate = DateTime.UtcNow,
                                    AccountId = employeeAccountIds.FirstOrDefault(x => x.Id == salary.EmployeeId)?.PayableAccountId,
                                    Debit = Math.Abs(Math.Round(salary.NetSalary + salary.LoanAmount + salary.IncomeTaxAmount, 2)),
                                    Credit = 0,
                                    Description = TransactionType.Payroll.ToString(),
                                    DocumentId = runPayrollDetail.Id,
                                    TransactionType = TransactionType.Payroll,
                                    Year = DateTime.UtcNow.Year.ToString(),
                                    DocumentNumber = runPayrollDetail.RunPayroll.Date.ToString("d"),
                                    PeriodId = period.Id
                                }
                            };

                                Guid? bankCashAccountId;
                                var emp = employeeAccountIds.FirstOrDefault(x => x.Id == salary.EmployeeId);
                                if (emp.SalaryType == "Bank" || emp.SalaryType == "بنكي")
                                {
                                    bankCashAccountId = accounts.FirstOrDefault(x => x.Code == "10500001")?.Id;
                                }
                                else
                                {
                                    bankCashAccountId = accounts.FirstOrDefault(x => x.Code == "10100001")?.Id;
                                }

                                //Banks/Cash Transaction
                                transactions.Add(new Transaction()
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.RunPayroll.Date,
                                    ApprovalDate = DateTime.UtcNow,
                                    AccountId = bankCashAccountId,
                                    Debit = 0,
                                    Credit = Math.Abs(Math.Round(salary.NetSalary + salary.LoanAmount + salary.IncomeTaxAmount, 2)),
                                    Description = TransactionType.Payroll.ToString(),
                                    DocumentId = runPayrollDetail.Id,
                                    TransactionType = TransactionType.Payroll,
                                    Year = DateTime.UtcNow.Year.ToString(),
                                    DocumentNumber = runPayrollDetail.RunPayroll.Date.ToString("d"),
                                    PeriodId = period.Id
                                });


                                if (salary.LoanAmount > 0)
                                {
                                    //Employee Loan Transaction
                                    transactions.Add(new Transaction()
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.RunPayroll.Date,
                                        ApprovalDate = DateTime.UtcNow,
                                        AccountId = employeeAccountIds.FirstOrDefault(x => x.Id == salary.EmployeeId)?.PayableAccountId,
                                        Debit = 0,
                                        Credit = Math.Abs(Math.Round(salary.LoanAmount, 2)),
                                        Description = TransactionType.Payroll.ToString(),
                                        DocumentId = runPayrollDetail.Id,
                                        TransactionType = TransactionType.Payroll,
                                        Year = DateTime.UtcNow.Year.ToString(),
                                        DocumentNumber = runPayrollDetail.RunPayroll.Date.ToString("d"),
                                        PeriodId = period.Id
                                    });

                                    //Bank/Cash Loan Transaction
                                    transactions.Add(new Transaction()
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.RunPayroll.Date,
                                        ApprovalDate = DateTime.UtcNow,
                                        AccountId = bankCashAccountId,
                                        Debit = Math.Abs(Math.Round(salary.LoanAmount, 2)),
                                        Credit = 0,
                                        Description = TransactionType.Payroll.ToString(),
                                        DocumentId = runPayrollDetail.Id,
                                        TransactionType = TransactionType.Payroll,
                                        Year = DateTime.UtcNow.Year.ToString(),
                                        DocumentNumber = runPayrollDetail.RunPayroll.Date.ToString("d"),
                                        PeriodId = period.Id
                                    });
                                }

                                if (salary.IncomeTaxAmount > 0)
                                {
                                    //Employee IncomeTax Transaction
                                    transactions.Add(new Transaction()
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.RunPayroll.Date,
                                        ApprovalDate = DateTime.UtcNow,
                                        AccountId = employeeAccountIds.FirstOrDefault(x => x.Id == salary.EmployeeId)?.PayableAccountId,
                                        Debit = 0,
                                        Credit = Math.Abs(Math.Round(salary.IncomeTaxAmount, 2)),
                                        Description = TransactionType.Payroll.ToString(),
                                        DocumentId = runPayrollDetail.Id,
                                        TransactionType = TransactionType.Payroll,
                                        Year = DateTime.UtcNow.Year.ToString(),
                                        DocumentNumber = runPayrollDetail.RunPayroll.Date.ToString("d"),
                                        PeriodId = period.Id
                                    });

                                    //VAT Transaction
                                    transactions.Add(new Transaction()
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.RunPayroll.Date,
                                        ApprovalDate = DateTime.UtcNow,
                                        AccountId = vatAccountId,
                                        Debit = Math.Abs(Math.Round(salary.IncomeTaxAmount, 2)),
                                        Credit = 0,
                                        Description = TransactionType.Payroll.ToString(),
                                        DocumentId = runPayrollDetail.Id,
                                        TransactionType = TransactionType.Payroll,
                                        Year = DateTime.UtcNow.Year.ToString(),
                                        DocumentNumber = runPayrollDetail.RunPayroll.Date.ToString("d"),
                                        PeriodId = period.Id
                                    });
                                }


                                //Employee Account
                                await Context.Transactions.AddRangeAsync(transactions, cancellationToken);
                            }
                        }


                        if (request.RunPayroll.Status)
                        {
                            var payrollId = Context.PaySchedules.AsNoTracking().FirstOrDefault(x => x.Id == request.RunPayroll.PayrollScheduleId);

                            if (payrollId != null)
                            {
                                payrollId.Proceed = true;
                                Context.PaySchedules.Update(payrollId);

                                if (payrollId.PayPeriod == "Fortnightly")
                                {
                                    var paySchedule = new PaySchedule()
                                    {
                                        PayPeriod = payrollId.PayPeriod,
                                        PayPeriodStartDate = payrollId.PayPeriodStartDate.AddDays(14),
                                        Name = payrollId.Name,
                                        PayPeriodEndDate = payrollId.PayPeriodEndDate.AddDays(14),
                                        IfPayDayFallOnHoliday = payrollId.IfPayDayFallOnHoliday,
                                        PayDate = payrollId.PayDate.AddDays(14),
                                        Default = payrollId.Default,
                                        IsHourlyPay = payrollId.IsHourlyPay,
                                    };
                                    await Context.PaySchedules.AddAsync(paySchedule, cancellationToken);
                                }
                                else
                                {
                                    var paySchedule = new PaySchedule()
                                    {
                                        PayPeriod = payrollId.PayPeriod,
                                        PayPeriodStartDate = payrollId.PayPeriodStartDate.AddMonths(1),
                                        Name = payrollId.Name,
                                        PayPeriodEndDate = payrollId.PayPeriodEndDate.AddMonths(1),
                                        IfPayDayFallOnHoliday = payrollId.IfPayDayFallOnHoliday,
                                        PayDate = payrollId.PayDate.AddMonths(1),
                                        Default = payrollId.Default,
                                        IsHourlyPay = payrollId.IsHourlyPay,
                                    };
                                    await Context.PaySchedules.AddAsync(paySchedule, cancellationToken);
                                }
                            }
                        }
                        else
                        {
                            var payrollId = Context.PaySchedules.AsNoTracking()
                                .FirstOrDefault(x => x.Id == request.RunPayroll.PayrollScheduleId);

                            if (payrollId != null)
                            {
                                payrollId.Proceed = true;
                                Context.PaySchedules.Update(payrollId);
                            }
                        }

                        ////Save changes to database
                        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);

                        scope.Complete();

                        return runPayroll.Id;

                    }
                    else
                    {

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        // create schedule
                        if (request.RunPayroll.Status)
                        {
                            var payrollId = Context.PaySchedules.AsNoTracking()
                                .FirstOrDefault(x => x.Id == request.RunPayroll.PayrollScheduleId);

                            if (payrollId != null)
                            {
                                payrollId.Proceed = true;
                                Context.PaySchedules.Update(payrollId);

                                if (payrollId.PayPeriod == "Fortnightly")
                                {
                                    var paySchedule = new PaySchedule()
                                    {
                                        PayPeriod = payrollId.PayPeriod,
                                        PayPeriodStartDate = payrollId.PayPeriodStartDate.AddDays(14),
                                        Name = payrollId.Name,
                                        PayPeriodEndDate = payrollId.PayPeriodEndDate.AddDays(14),
                                        IfPayDayFallOnHoliday = payrollId.IfPayDayFallOnHoliday,
                                        PayDate = payrollId.PayDate.AddDays(14),
                                        Default = payrollId.Default,
                                        IsHourlyPay = payrollId.IsHourlyPay,
                                    };
                                    await Context.PaySchedules.AddAsync(paySchedule, cancellationToken);

                                    var employee = Context.EmployeeSalaries
                                        .AsNoTracking()
                                        .Where(x => x.PayPeriodId == request.RunPayroll.PayrollScheduleId)
                                        .AsQueryable();
                                    foreach (var emp in employee)
                                    {
                                        emp.PayPeriodId = paySchedule.Id;
                                        Context.EmployeeSalaries.Update(emp);
                                    }
                                }
                                else
                                {
                                    var paySchedule = new PaySchedule()
                                    {
                                        PayPeriod = payrollId.PayPeriod,
                                        PayPeriodStartDate = payrollId.PayPeriodStartDate.AddMonths(1),
                                        Name = payrollId.Name,
                                        PayPeriodEndDate = payrollId.PayPeriodEndDate.AddMonths(1),
                                        IfPayDayFallOnHoliday = payrollId.IfPayDayFallOnHoliday,
                                        PayDate = payrollId.PayDate.AddMonths(1),
                                        Default = payrollId.Default,
                                        IsHourlyPay = payrollId.IsHourlyPay,
                                    };
                                    await Context.PaySchedules.AddAsync(paySchedule, cancellationToken);

                                    var employee = Context.EmployeeSalaries
                                        .AsNoTracking()
                                        .Where(x => x.PayPeriodId == request.RunPayroll.PayrollScheduleId)
                                        .AsQueryable();
                                    foreach (var emp in employee)
                                    {
                                        emp.PayPeriodId = paySchedule.Id;
                                        Context.EmployeeSalaries.Update(emp);
                                    }
                                }
                            }
                        }
                        else
                        {
                            var payrollId = Context.PaySchedules.AsNoTracking()
                                .FirstOrDefault(x => x.Id == request.RunPayroll.PayrollScheduleId);

                            if (payrollId != null)
                            {
                                payrollId.Proceed = true;
                                Context.PaySchedules.Update(payrollId);
                            }
                        }



                        var runPayroll = new RunPayroll()
                        {
                            Date = DateTime.UtcNow,
                            PayrollScheduleId = request.RunPayroll.PayrollScheduleId,
                            Status = request.RunPayroll.Status,
                            Hourly = request.RunPayroll.Hourly
                        };
                        await Context.RunPayrolls.AddAsync(runPayroll, cancellationToken);

                        CompanySubmissionPeriod period = null;
                        List<Account> accounts = null;
                        IQueryable<EmployeeRegistration> employeeAccountIds = null;
                        Guid vatAccountId = default;

                        if (request.RunPayroll.Status)
                        {
                            //Transaction
                            period = await Context.CompanySubmissionPeriods
                                .FirstOrDefaultAsync(x => x.PeriodStart.Date <= DateTime.UtcNow.Date
                                                          && x.PeriodEnd.Date >= DateTime.UtcNow.Date, cancellationToken: cancellationToken);

                            accounts = await Context.Accounts.AsNoTracking().Where(x =>
                                    x.Code == "10100001" ||             //Cash in Hand
                                    x.Code == "10500001" ||             //Banks
                                    x.Code == "60300101" ||             //Payroll Expense
                                    x.Name == "VAT Payable on Payroll"  //Payroll Vat
                            ).ToListAsync(cancellationToken: cancellationToken);

                            employeeAccountIds = Context.EmployeeRegistrations.AsNoTracking().AsQueryable();

                            var vatAccount = accounts.FirstOrDefault(x => x.Name == "VAT Payable on Payroll");
                            if (vatAccount == null)
                            {
                                var costCenter = await Context.CostCenters
                                    .AsNoTracking()
                                    .Include(x => x.Accounts)
                                    .FirstOrDefaultAsync(x => x.Code == "250000", cancellationToken: cancellationToken);

                                var code = costCenter.Accounts.OrderBy(x => x.Code).LastOrDefault()?.Code;
                                var account = new Account()
                                {
                                    Code = (Convert.ToInt64(code) + 1).ToString(),
                                    Name = "VAT Payable on Payroll",
                                    NameArabic = "ضريبة القيمة المضافة المستحقة على كشوف المرتبات",
                                    Description = "VAT Payable on Payroll",
                                    IsActive = true,
                                    CostCenterId = costCenter.Id
                                };
                                await Context.Accounts.AddAsync(account, cancellationToken);
                                vatAccountId = account.Id;
                            }
                            else
                            {
                                vatAccountId = vatAccount.Id;
                            }
                        }

                        foreach (var salary in request.RunPayroll.SalaryTemplateList)
                        {
                            var runPayrollDetail = new RunPayrollDetail()
                            {
                                RunPayrollId = runPayroll.Id,
                                EmployeeId = salary.EmployeeId,
                                BaseSalary = salary.BaseSalary,
                                SalaryType = salary.SalaryType,
                                Hour = salary.Hour,
                                HourAmount = salary.HourAmount,
                                WeekdayHourlySalary = salary.WeekdayHourlySalary,
                                AllowanceAmount = salary.AllowanceAmount,
                                GrossSalary = salary.GrossSalary,
                                DeductionAmount = salary.DeductionAmount,
                                ContributionAmount = salary.ContributionAmount,
                                TaxAmount = salary.IncomeTaxAmount,
                                LoanAmount = salary.LoanAmount,
                                NetSalary = salary.NetSalary,
                                ZeroSalary = salary.ZeroSalary,
                                Reason = salary.Reason,
                                IncomeTax = salary.IncomeTax,
                                AutoIncomeTax = salary.AutoIncomeTax,
                            };
                            await Context.RunPayrollDetails.AddAsync(runPayrollDetail, cancellationToken);

                            if (!salary.ZeroSalary)
                            {
                                foreach (var item in salary.SalaryDetailList)
                                {
                                    var runPayrollSalaryDetail = new RunPayrollSalaryDetail()
                                    {
                                        RunPayrollDetailId = runPayrollDetail.Id,
                                        ItemId = item.ItemId,
                                        Type = item.Type,
                                        AmountType = item.AmountType,
                                        TaxPlan = item.TaxPlan,
                                        Percent = item.Percent,
                                        Amount = item.Amount,
                                    };
                                    await Context.RunPayrollSalaryDetails.AddAsync(runPayrollSalaryDetail, cancellationToken);
                                }
                            }

                            if (salary.LoanAmount > 0)
                            {
                                var empLoan = await Context.LoanPayments
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.EmployeeRegistrationId == salary.EmployeeId && x.RecoveryMethod == RecoveryMethod.Salary && !x.IsActive, cancellationToken: cancellationToken);

                                if (empLoan != null)
                                {
                                    var loanPay = new LoanPay()
                                    {
                                        PaymentDate = DateTime.UtcNow,
                                        Amount = salary.LoanAmount,
                                        RemainingLoan = empLoan.RemainingLoan - salary.LoanAmount,
                                        Comments = "Pay by salary",
                                        LoanPaymentId = empLoan.Id,
                                    };
                                    await Context.LoanPays.AddAsync(loanPay, cancellationToken);

                                    empLoan.RemainingLoan = empLoan.RemainingLoan - salary.LoanAmount;
                                    if (empLoan.RemainingLoan <= 0)
                                    {
                                        empLoan.IsActive = true;
                                    }
                                    Context.LoanPayments.Update(empLoan);
                                }

                            }

                            if (request.RunPayroll.Status)
                            {
                                var transactions = new List<Transaction>
                            {
                                //Employee Transaction Cr
                                new Transaction
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.RunPayroll.Date,
                                    ApprovalDate = DateTime.UtcNow,
                                    AccountId = employeeAccountIds.FirstOrDefault(x => x.Id == salary.EmployeeId)?.PayableAccountId,
                                    Debit = 0,
                                    Credit = Math.Abs(Math.Round(salary.NetSalary + salary.LoanAmount + salary.IncomeTaxAmount, 2)),
                                    Description = TransactionType.Payroll.ToString(),
                                    DocumentId = runPayrollDetail.Id,
                                    TransactionType = TransactionType.Payroll,
                                    Year = DateTime.UtcNow.Year.ToString(),
                                    DocumentNumber = runPayrollDetail.RunPayroll.Date.ToString("d"),
                                    PeriodId = period.Id
                                },
                                
                                //Expense Transaction
                                new Transaction
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.RunPayroll.Date,
                                    ApprovalDate = DateTime.UtcNow,
                                    AccountId = accounts.FirstOrDefault(x => x.Code == "60300101")?.Id,
                                    Debit = Math.Abs(Math.Round(salary.NetSalary + salary.LoanAmount + salary.IncomeTaxAmount, 2)),
                                    Credit = 0,
                                    Description = TransactionType.Payroll.ToString(),
                                    DocumentId = runPayrollDetail.Id,
                                    TransactionType = TransactionType.Payroll,
                                    Year = DateTime.UtcNow.Year.ToString(),
                                    DocumentNumber =runPayrollDetail.RunPayroll.Date.ToString("d"),
                                    PeriodId = period.Id
                                },

                                //Employee Transaction Dr
                                new Transaction
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.RunPayroll.Date,
                                    ApprovalDate = DateTime.UtcNow,
                                    AccountId = employeeAccountIds.FirstOrDefault(x => x.Id == salary.EmployeeId)?.PayableAccountId,
                                    Debit = Math.Abs(Math.Round(salary.NetSalary + salary.LoanAmount + salary.IncomeTaxAmount, 2)),
                                    Credit = 0,
                                    Description = TransactionType.Payroll.ToString(),
                                    DocumentId = runPayrollDetail.Id,
                                    TransactionType = TransactionType.Payroll,
                                    Year = DateTime.UtcNow.Year.ToString(),
                                    DocumentNumber = runPayrollDetail.RunPayroll.Date.ToString("d"),
                                    PeriodId = period.Id
                                }
                            };

                                Guid? bankCashAccountId;
                                var emp = employeeAccountIds.FirstOrDefault(x => x.Id == salary.EmployeeId);
                                if (emp.SalaryType == "Bank" || emp.SalaryType == "بنكي")
                                {
                                    bankCashAccountId = accounts.FirstOrDefault(x => x.Code == "10500001")?.Id;
                                }
                                else
                                {
                                    bankCashAccountId = accounts.FirstOrDefault(x => x.Code == "10100001")?.Id;
                                }

                                //Banks/Cash Transaction
                                transactions.Add(new Transaction()
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.RunPayroll.Date,
                                    ApprovalDate = DateTime.UtcNow,
                                    AccountId = bankCashAccountId,
                                    Debit = 0,
                                    Credit = Math.Abs(Math.Round(salary.NetSalary + salary.LoanAmount + salary.IncomeTaxAmount, 2)),
                                    Description = TransactionType.Payroll.ToString(),
                                    DocumentId = runPayrollDetail.Id,
                                    TransactionType = TransactionType.Payroll,
                                    Year = DateTime.UtcNow.Year.ToString(),
                                    DocumentNumber = runPayrollDetail.RunPayroll.Date.ToString("d"),
                                    PeriodId = period.Id
                                });

                                if (salary.LoanAmount > 0)
                                {
                                    //Employee Loan Transaction
                                    transactions.Add(new Transaction()
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.RunPayroll.Date,
                                        ApprovalDate = DateTime.UtcNow,
                                        AccountId = employeeAccountIds.FirstOrDefault(x => x.Id == salary.EmployeeId)?.PayableAccountId,
                                        Debit = 0,
                                        Credit = Math.Abs(Math.Round(salary.LoanAmount, 2)),
                                        Description = TransactionType.Payroll.ToString(),
                                        DocumentId = runPayrollDetail.Id,
                                        TransactionType = TransactionType.Payroll,
                                        Year = DateTime.UtcNow.Year.ToString(),
                                        DocumentNumber = runPayrollDetail.RunPayroll.Date.ToString("d"),
                                        PeriodId = period.Id
                                    });

                                    //Bank/Cash Loan Transaction
                                    transactions.Add(new Transaction()
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.RunPayroll.Date,
                                        ApprovalDate = DateTime.UtcNow,
                                        AccountId = bankCashAccountId,
                                        Debit = Math.Abs(Math.Round(salary.LoanAmount, 2)),
                                        Credit = 0,
                                        Description = TransactionType.Payroll.ToString(),
                                        DocumentId = runPayrollDetail.Id,
                                        TransactionType = TransactionType.Payroll,
                                        Year = DateTime.UtcNow.Year.ToString(),
                                        DocumentNumber = runPayrollDetail.RunPayroll.Date.ToString("d"),
                                        PeriodId = period.Id
                                    });
                                }
                                if (salary.IncomeTaxAmount > 0)
                                {
                                    //Employee IncomeTax Transaction
                                    transactions.Add(new Transaction()
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.RunPayroll.Date,
                                        ApprovalDate = DateTime.UtcNow,
                                        AccountId = employeeAccountIds.FirstOrDefault(x => x.Id == salary.EmployeeId)?.PayableAccountId,
                                        Debit = 0,
                                        Credit = Math.Abs(Math.Round(salary.IncomeTaxAmount, 2)),
                                        Description = TransactionType.Payroll.ToString(),
                                        DocumentId = runPayrollDetail.Id,
                                        TransactionType = TransactionType.Payroll,
                                        Year = DateTime.UtcNow.Year.ToString(),
                                        DocumentNumber = runPayrollDetail.RunPayroll.Date.ToString("d"),
                                        PeriodId = period.Id
                                    });


                                    //VAT Transaction
                                    transactions.Add(new Transaction()
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.RunPayroll.Date,
                                        ApprovalDate = DateTime.UtcNow,
                                        AccountId = vatAccountId,
                                        Debit = Math.Abs(Math.Round(salary.IncomeTaxAmount, 2)),
                                        Credit = 0,
                                        Description = TransactionType.Payroll.ToString(),
                                        DocumentId = runPayrollDetail.Id,
                                        TransactionType = TransactionType.Payroll,
                                        Year = DateTime.UtcNow.Year.ToString(),
                                        DocumentNumber = runPayrollDetail.RunPayroll.Date.ToString("d"),
                                        PeriodId = period.Id
                                    });
                                }


                                //Employee Account
                                await Context.Transactions.AddRangeAsync(transactions, cancellationToken);
                            }

                        }
                        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);

                        scope.Complete();

                        return runPayroll.Id;
                    }
                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
