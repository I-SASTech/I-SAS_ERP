using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Focus.Business.Extensions;
using Focus.Business.HR.Payroll.Allowances;
using Focus.Business.HR.Payroll.Allowances.Commands.AddAllowance;
using Focus.Business.HR.Payroll.Allowances.Queries;
using Focus.Business.HR.Payroll.AllowanceTypes.Commands.AddUpdateAllowance;
using Focus.Business.HR.Payroll.AllowanceTypes.Queries.GetAllowanceTypeDetails;
using Focus.Business.HR.Payroll.AllowanceTypes.Queries.GetAllowanceTypeList;
using Focus.Business.HR.Payroll.Contributions;
using Focus.Business.HR.Payroll.Contributions.Commands;
using Focus.Business.HR.Payroll.Contributions.Queries;
using Focus.Business.HR.Payroll.Deductions;
using Focus.Business.HR.Payroll.Deductions.Commands;
using Focus.Business.HR.Payroll.Deductions.Queries;
using Focus.Business.HR.Payroll.LoanPayments.Commands;
using Focus.Business.HR.Payroll.LoanPayments.Queries;
using Focus.Business.HR.Payroll.EmployeeSalaries.Commands;
using Focus.Business.HR.Payroll.EmployeeSalaries.Queries;
using Focus.Business.HR.Payroll.LoanPays.Commands;
using Focus.Business.HR.Payroll.PayrollSchedule.Commands;
using Focus.Business.HR.Payroll.PayrollSchedule.Queries;
using Focus.Business.HR.Payroll.RunPayrolls.Commands;
using Focus.Business.HR.Payroll.RunPayrolls.Queries;
using Focus.Business.HR.Payroll.RunPayrolls.Queries.GetPaySlip;
using Focus.Business.HR.Payroll.RunPayrolls.Queries.RunPayrollPrintQuery;
using Focus.Business.HR.Payroll.SalaryTemplates;
using Focus.Business.HR.Payroll.SalaryTemplates.Commands.AddSalaryTemplate;
using Focus.Business.HR.Payroll.SalaryTemplates.Queries;
using Focus.Business.HR.Payroll.SaveSalaryTaxSlab.Commands;
using Focus.Business.HR.Payroll.SaveSalaryTaxSlab.Queries;
using Focus.Business.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noble.Api.Models;
using Focus.Business.ChequeBooks.Queries;
using Focus.Business.ChequeBooks.Commands;
using Focus.Business.ChequeBooks.ChequeAndGurantee;
using Focus.Business.Holidays.Commands.AddHoliday;
using Focus.Business.Holidays;
using Focus.Business.Holidays.Queries.GetHolidayDetails;
using Focus.Business.Holidays.Queries.GetHolidaysList;
using Focus.Business.ManualAttendance;
using Focus.Business.ManualAttendance.Command;
using Focus.Business.ManualAttendance.Queries;
using Focus.Business.ManualAttendance.Queries.EmployeeTodayAttendence;
using Focus.Business.ManualAttendance.Queries.EmployeeAttendenceDateWiseGroupby;

namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : BaseController
    {
        public readonly IApplicationDbContext Context;
        private readonly IHostingEnvironment _Environment;
        public PayrollController(IApplicationDbContext context, IHostingEnvironment environment)
        {

            Context = context;
            _Environment = environment;

        }

        #region For Allowance
        [Route("api/Payroll/AllowanceAutoGenerateNo")]
        [HttpGet("AllowanceAutoGenerateNo")]
        [Roles("CanAddAllowance")]
        public async Task<IActionResult> AllowanceAutoGenerateNo()
        {
            var autoNo = await Mediator.Send(new GetAllowanceCodeQuery());
            return Ok(autoNo);
        }

        [Route("api/Payroll/SaveAllowanceInformation")]
        [HttpPost("SaveAllowanceInformation")]
        [Roles("CanAddAllowance", "CanEditAllowance")]
        public async Task<IActionResult> SaveAllowanceInformation([FromBody] AllowanceLookUp allowance)
        {
            if (allowance.Id == Guid.Empty)
            {
                var id = await Mediator.Send(new AddUpdateAllowanceCommand()
                {
                    Allowance = allowance
                });

            }
            else
            {
                var id = await Mediator.Send(new AddUpdateAllowanceCommand()
                {
                    Allowance = allowance
                });
            }


            return Ok(new { IsSuccess = true });
        }

        [Route("api/Payroll/AllowanceList")]
        [HttpGet("AllowanceList")]
        [Roles("CanViewAllowance", "CanAddSaleryTemplate", "CanEditSaleryTemplate", "CanAddEmployeeSalary", "CanEditEmployeeSalary", "CanEditSaleryTemplate")]
        public async Task<IActionResult> AllowanceList(string searchTerm, int? pageNumber, bool isDropdown)
        {
            var allowance = await Mediator.Send(new GetAllowanceListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(allowance);
        }

        [Route("api/Payroll/AllowanceDetail")]
        [HttpGet("AllowanceDetail")]
        [Roles("CanEditAllowance")]
        public async Task<IActionResult> AllowanceDetail(Guid id)
        {
            var allowance = await Mediator.Send(new GetAllowanceDetailQuery()
            {
                Id = id
            });
            return Ok(allowance);
        }

        #endregion
        #region AllowanceType



        [Route("api/Payroll/SaveAllowanceType")]
        [HttpPost("SaveAllowanceType")]
        [Roles("CanAddAllowanceType", "CanEditAllowanceType")]
        public async Task<IActionResult> SaveAllowanceType([FromBody] AllowanceTypeVm allowanceTypeVm)
        {
            var id = Guid.Empty;
            if (allowanceTypeVm.Id == Guid.Empty)
            {

                id = await Mediator.Send(new AddUpdateAllowanceTypeCommand
                {
                    Id = new Guid(),
                    Name = allowanceTypeVm.Name,
                    NameArabic = allowanceTypeVm.NameArabic,
                    Description = allowanceTypeVm.Description,
                    IsActive = allowanceTypeVm.IsActive,



                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateAllowanceTypeCommand
                {
                    Id = allowanceTypeVm.Id,
                    Name = allowanceTypeVm.Name,
                    NameArabic = allowanceTypeVm.NameArabic,
                    Description = allowanceTypeVm.Description,
                    IsActive = allowanceTypeVm.IsActive,

                });
            }
            if (id != Guid.Empty)
            {
                var allowanceType = await Mediator.Send(new GetAllowanceTypeDetailQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, allowanceType = allowanceType });
            }

            return Ok(new { IsSuccess = false });



        }

        [Route("api/Payroll/AllowanceTypeList")]
        [HttpGet("AllowanceTypeList")]
        [Roles("CanAddAllowance", "CanViewAllowanceType")]

        public async Task<IActionResult> AllowanceTypeList(bool isDropdown)
        {
            var allowanceType = await Mediator.Send(new GetAllowanceTypeListQuery { IsDropdown = isDropdown });
            return Ok(allowanceType);
        }


        [Route("api/Payroll/AllowanceTypeDetail")]
        [HttpGet("AllowanceTypeDetail")]
        [Roles("CanEditAllowanceType")]
        public async Task<IActionResult> AllowanceTypeDetail(Guid Id)
        {
            var allowanceType = await Mediator.Send(new GetAllowanceTypeDetailQuery()
            {
                Id = Id
            });
            return Ok(allowanceType);

        }
        #endregion



        #region For Deduction

        [Route("api/Payroll/DeductionAutoGenerateNo")]
        [HttpGet("DeductionAutoGenerateNo")]
        [Roles("CanAddDeduction")]
        public async Task<IActionResult> DeductionAutoGenerateNo()
        {
            var autoNo = await Mediator.Send(new GetDeductionCodeQuery());
            return Ok(autoNo);
        }

        [Route("api/Payroll/SaveDeductionInformation")]
        [HttpPost("SaveDeductionInformation")]
        [Roles("CanAddDeduction", "CanEditDeduction")]
        public async Task<IActionResult> SaveDeductionInformation([FromBody] DeductionLookUpModel deduction)
        {
            if (deduction.Id == Guid.Empty)
            {
                var id = await Mediator.Send(new AddUpdateDeduction()
                {
                    Deduction = deduction
                });

            }
            else
            {
                var id = await Mediator.Send(new AddUpdateDeduction()
                {
                    Deduction = deduction
                });
            }


            return Ok(new { IsSuccess = true });
        }

        [Route("api/Payroll/DeductionList")]
        [HttpGet("DeductionList")]
        [Roles("CanViewDeduction", "CanAddSaleryTemplate", "CanEditSaleryTemplate", "CanAddEmployeeSalary", "CanEditEmployeeSalary")]
        public async Task<IActionResult> DeductionList(string searchTerm, int? pageNumber, bool isDropdown)
        {
            var result = await Mediator.Send(new DeductionListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(result);
        }

        [Route("api/Payroll/DeductionDetail")]
        [HttpGet("DeductionDetail")]
        [Roles("CanEditDeduction")]
        public async Task<IActionResult> DeductionDetail(Guid id)
        {
            var deduction = await Mediator.Send(new GetDeductionDetailQuery()
            {
                Id = id
            });
            return Ok(deduction);

        }

        #endregion
        #region For Contribution

        [Route("api/Payroll/ContributionAutoGenerateNo")]
        [HttpGet("ContributionAutoGenerateNo")]
        [Roles("CanAddContribution")]
        public async Task<IActionResult> ContributionAutoGenerateNo()
        {
            var autoNo = await Mediator.Send(new GetContributionCodeQuery());
            return Ok(autoNo);
        }

        [Route("api/Payroll/SaveContributionInformation")]
        [HttpPost("SaveContributionInformation")]
        [Roles("CanAddContribution", "CanEditContribution")]
        public async Task<IActionResult> SaveContributionInformation([FromBody] ContributionLookUpModel contribution)
        {
            if (contribution.Id == Guid.Empty)
            {
                var id = await Mediator.Send(new AddUpdateContribution()
                {
                    Contribution = contribution
                });

            }
            else
            {
                var id = await Mediator.Send(new AddUpdateContribution()
                {
                    Contribution = contribution
                });
            }


            return Ok(new { IsSuccess = true });
        }

        [Route("api/Payroll/ContributionList")]
        [HttpGet("ContributionList")]
        [Roles("CanViewContribution", "CanAddSaleryTemplate", "CanEditSaleryTemplate", "CanAddEmployeeSalary", "CanEditEmployeeSalary")]
        public async Task<IActionResult> ContributionList(string searchTerm, int? pageNumber, bool isDropdown)
        {
            var result = await Mediator.Send(new ContributionListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(result);
        }

        [Route("api/Payroll/ContributionDetail")]
        [HttpGet("ContributionDetail")]
        [Roles("CanEditContribution")]
        public async Task<IActionResult> ContributionDetail(Guid id)
        {
            var contribution = await Mediator.Send(new GetContributionDetailQuery()
            {
                Id = id
            });
            return Ok(contribution);

        }

        #endregion


        #region Salary Template
        [Route("api/Payroll/SalaryTemplateAutoGenerateNo")]
        [HttpGet("SalaryTemplateAutoGenerateNo")]
        [Roles("CanAddSaleryTemplate")]
        public async Task<IActionResult> SalaryTemplateAutoGenerateNo()
        {
            var autoNo = await Mediator.Send(new GetSalaryTemplateCode());
            return Ok(autoNo);
        }
        [Route("api/Payroll/SalaryTemplateList")]
        [HttpGet("SalaryTemplateList")]
        [Roles("CanViewSaleryTemplate", "CanAddEmployeeSalary", "CanEditEmployeeSalary")]
        public async Task<IActionResult> SalaryTemplateList(string searchTerm, int? pageNumber, bool isDropdown)
        {
            var result = await Mediator.Send(new GetSalaryTemplateListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(result);
        }
        [Route("api/Payroll/SalaryTemplateDetail")]
        [HttpGet("SalaryTemplateDetail")]
        [Roles("CanEditSaleryTemplate")]
        public async Task<IActionResult> SalaryTemplateDetail(Guid id)
        {
            var salaryTemplate = await Mediator.Send(new GetSalaryTemplateDetailQuery()
            {
                Id = id
            });
            return Ok(salaryTemplate);

        }


        [Route("api/Payroll/SaveSalaryTemplate")]
        [HttpPost("SaveSalaryTemplate")]
        [Roles("CanAddSaleryTemplate", "CanEditSaleryTemplate")]
        public async Task<IActionResult> SaveSalaryTemplate([FromBody] SalaryTemplateLookUp salaryTemplate)
        {
            if (salaryTemplate.Id == Guid.Empty)
            {
                var id = await Mediator.Send(new AddUpdateSalaryTemplateCommand()
                {
                    SalaryTemplate = salaryTemplate
                });

            }
            else
            {
                var id = await Mediator.Send(new AddUpdateSalaryTemplateCommand()
                {
                    SalaryTemplate = salaryTemplate
                });
            }


            return Ok(new { IsSuccess = true });
        }
        #endregion

        #region Payroll Schedule
        [Route("api/Payroll/SavePayrollSchedule")]
        [HttpPost("SavePayrollSchedule")]
        [Roles("CanAddPayRollSchedule", "CanEditPayRollSchedule")]
        public async Task<IActionResult> SavePayrollSchedule([FromBody] PayrollScheduleLookUpModel payrollSchedule)
        {
            if (payrollSchedule.Id == Guid.Empty)
            {
                var id = await Mediator.Send(new AddUpdatePayrollSchedule()
                {
                    PayrollSchedule = payrollSchedule
                });

            }
            else
            {
                var id = await Mediator.Send(new AddUpdatePayrollSchedule()
                {
                    PayrollSchedule = payrollSchedule
                });
            }


            return Ok(new { IsSuccess = true });
        }

        [Route("api/Payroll/PayrollScheduleList")]
        [HttpGet("PayrollScheduleList")]
        [Roles("CanViewPayRollSchedule", "CanAddRunPayroll", "CanDraftRunPayroll", "CanEditOpenRunPayroll", "CanAddEmployeeSalary", "CanEditEmployeeSalary")]
        public async Task<IActionResult> PayrollScheduleList(string searchTerm, int? pageNumber, bool isDropdown, string salaryType)
        {
            var result = await Mediator.Send(new PayrollScheduleListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
                SalaryType = salaryType,
            });

            return Ok(result);
        }

        [Route("api/Payroll/PayrollScheduleDetail")]
        [HttpGet("PayrollScheduleDetail")]
        [Roles("CanEditPayRollSchedule")]
        public async Task<IActionResult> PayrollScheduleDetail(Guid id)
        {
            var payrollSchedule = await Mediator.Send(new PayrollScheduleDetailQuery
            {
                Id = id
            });
            return Ok(payrollSchedule);
        }
        #endregion

        #region Payroll Schedule
        [Route("api/Payroll/SaveSalaryTaxSlab")]
        [HttpPost("SaveSalaryTaxSlab")]
        [Roles("CanAddSalaryTaxSlab", "CanEditSalaryTaxSlab")]
        public async Task<IActionResult> SaveSalaryTaxSlab([FromBody] SalaryTaxSlabLookUpModel salaryTaxSlab)
        {
            if (salaryTaxSlab.Id == Guid.Empty)
            {
                var id = await Mediator.Send(new AddUpdateSalaryTaxSlab()
                {
                    SalaryTaxSlab = salaryTaxSlab
                });

            }
            else
            {
                var id = await Mediator.Send(new AddUpdateSalaryTaxSlab()
                {
                    SalaryTaxSlab = salaryTaxSlab
                });
            }


            return Ok(new { IsSuccess = true });
        }

        [Route("api/Payroll/SalaryTaxSlabList")]
        [HttpGet("SalaryTaxSlabList")]
        [Roles("CanViewSalaryTaxSlab")]
        public async Task<IActionResult> SalaryTaxSlabList(string searchTerm, int? pageNumber, bool isDropdown)
        {
            var result = await Mediator.Send(new GetSalaryTaxSlabListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(result);
        }
        [Route("api/Payroll/SalaryTaxSlabDetail")]
        [HttpGet("SalaryTaxSlabDetail")]
        [Roles( "CanEditSalaryTaxSlab")]
        public async Task<IActionResult> SalaryTaxSlabDetail(Guid? id)
        {
            var payrollSchedule = await Mediator.Send(new GetSalaryTaxSlabDetailQuery
            {
                Id = id
            });
            return Ok(payrollSchedule);
        }

        #endregion

        #region Save Employee Salary
        [Route("api/Payroll/SaveEmployeeSalary")]
        [HttpPost("SaveEmployeeSalary")]
        [Roles("CanAddEmployeeSalary", "CanEditEmployeeSalary")]
        public async Task<IActionResult> SaveEmployeeSalary([FromBody] EmployeeSalaryLookUpModel salaryTemplate)
        {
            if (salaryTemplate.Id == Guid.Empty)
            {
                var id = await Mediator.Send(new AddUpdateEmployeeSalaryCommand()
                {
                    EmployeeSalary = salaryTemplate
                });

            }
            else
            {
                var id = await Mediator.Send(new AddUpdateEmployeeSalaryCommand()
                {
                    EmployeeSalary = salaryTemplate
                });
            }


            return Ok(new { IsSuccess = true });
        }

        [Route("api/Payroll/EmployeeSalaryList")]
        [HttpGet("EmployeeSalaryList")]
        [Roles("CanViewEmployeeSalary")]
        public async Task<IActionResult> EmployeeSalaryList(string searchTerm, int? pageNumber, bool isDropdown)
        {
            var result = await Mediator.Send(new GetEmployeeSalaryListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(result);
        }

        [Route("api/Payroll/EmployeeSalaryDetail")]
        [HttpGet("EmployeeSalaryDetail")]
        [Roles("CanEditEmployeeSalary")]
        public async Task<IActionResult> EmployeeSalaryDetail(Guid? id)
        {
            var employeeSalary = await Mediator.Send(new GetEmployeeSalaryDetailQuery
            {
                Id = id
            });
            return Ok(employeeSalary);
        }
        [Route("api/Payroll/EmployeeEditDetail")]
        [HttpGet("EmployeeEditDetail")]
        public async Task<IActionResult> EmployeeEditDetail(Guid? id)
        {
            var employee = await Context.EmployeeRegistrations.AsNoTracking().Include(x => x.EmployeeSalaries)
                .FirstOrDefaultAsync(x => x.Id == id);

            var employeeSalary = await Mediator.Send(new GetEmployeeSalaryDetailQuery
            {
                Id = employee.EmployeeSalaries.FirstOrDefault()?.Id
            });
            return Ok(employeeSalary);
        }
        #endregion

        #region For LoanPayment



        [Route("api/Payroll/SaveLoanPaymentInformation")]
        [HttpPost("SaveLoanPaymentInformation")]
        [Roles("CanAddLoanPayment", "CanEditLoanPayment")]
        public async Task<IActionResult> SaveLoanPaymentInformation([FromBody] LoanPaymentLookUpModel loanPayment)
        {
            if (loanPayment.Id == Guid.Empty)
            {
                var id = await Mediator.Send(new AddUpdateLoanPayment()
                {
                    LoanPayment = loanPayment
                });

            }
            else
            {
                var id = await Mediator.Send(new AddUpdateLoanPayment()
                {
                    LoanPayment = loanPayment
                });
            }


            return Ok(new { IsSuccess = true });
        }

        [Route("api/Payroll/LoanPaymentList")]
        [HttpGet("LoanPaymentList")]
        [Roles("CanViewLoanPayment")]
        public async Task<IActionResult> LoanPaymentList(string searchTerm, int? pageNumber, bool isDropdown)
        {
            var result = await Mediator.Send(new LoanPaymentListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(result);
        }

        [Route("api/Payroll/LoanPaymentDetail")]
        [HttpGet("LoanPaymentDetail")]
        [Roles("CanEditLoanPayment")]
        public async Task<IActionResult> LoanPaymentDetail(Guid id)
        {
            var loanPayment = await Mediator.Send(new GetLoanPaymentDetailQuery()
            {
                Id = id
            });
            return Ok(loanPayment);

        }

        #endregion

        #region For LoanRecovery


        [Route("api/Payroll/SaveLoanRecovery")]
        [HttpPost("SaveLoanRecovery")]
        [Roles("CanViewLoanPayment")]
        public async Task<IActionResult> SaveLoanRecovery([FromBody] LoanPayLookUpModel loanPay)
        {
            if (loanPay.Id == Guid.Empty)
            {
                var id = await Mediator.Send(new AddUpdateLoanPay()
                {
                    LoanPay = loanPay
                });

            }
            else
            {
                var id = await Mediator.Send(new AddUpdateLoanPay()
                {
                    LoanPay = loanPay
                });
            }


            return Ok(new { IsSuccess = true });
        }
        #endregion

        #region For Run Payroll
        [Route("api/Payroll/RunPayrollDetailList")]
        [HttpGet("RunPayrollDetailList")]
        [Roles("CanAddRunPayroll", "CanDraftRunPayroll", "CanEditOpenRunPayroll")]
        public async Task<IActionResult> RunPayrollDetailList(string searchTerm, int? pageNumber, bool isDropdown, Guid? designationId, Guid? departmentId, Guid? employeeId, Guid? payrollScheduleId)
        {
            var result = await Mediator.Send(new EmployeePayRunListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
                DesignationId = designationId,
                DepartmentId = departmentId,
                EmployeeId = employeeId,
                PayrollScheduleId = payrollScheduleId,
            });

            return Ok(result);
        }

        [Route("api/Payroll/DeleteLoanRecovery")]
        [HttpGet("DeleteLoanRecovery")]
        [Roles("CanViewLoanPayment")]
        public async Task<IActionResult> DeleteLoanRecovery(Guid id)
        {
            var loanPayment = await Mediator.Send(new DeleteLoanPayQuery()
            {
                Id = id
            });
            return Ok(loanPayment);

        }

        [Route("api/Payroll/SaveRunPayroll")]
        [HttpPost("SaveRunPayroll")]
        [Roles("CanAddRunPayroll", "CanDraftRunPayroll", "CanEditOpenRunPayroll")]
        public async Task<IActionResult> SaveRunPayroll([FromBody] RunPayrollLookUpModel runPayroll)
        {
            if (runPayroll.Id == Guid.Empty)
            {
                var id = await Mediator.Send(new AddUpdateRunPayrollCommand()
                {
                    RunPayroll = runPayroll
                });

            }
            else
            {
                var id = await Mediator.Send(new AddUpdateRunPayrollCommand()
                {
                    RunPayroll = runPayroll
                });
            }


            return Ok(new { IsSuccess = true });
        }

        [Route("api/Payroll/RunPayrollList")]
        [HttpGet("RunPayrollList")]
        [Roles("CanViewRunPayroll")]
        public async Task<IActionResult> RunPayrollList(string searchTerm, int? pageNumber, bool isDropdown)
        {
            var result = await Mediator.Send(new GetRunPayrollListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(result);
        }

        [Route("api/Payroll/RunPayrollDetail")]
        [HttpGet("RunPayrollDetail")]
        [Roles("CanEditOpenRunPayroll")]
        public async Task<IActionResult> RunPayrollDetail(Guid id)
        {
            var employeeSalary = await Mediator.Send(new GetEmployeePayRunDetailQuery()
            {
                Id = id
            });
            return Ok(employeeSalary);

        }

        [Route("api/Payroll/ChecKPaySchedule")]
        [HttpGet("ChecKPaySchedule")]
        [Roles("CanAddRunPayroll", "CanDraftRunPayroll")]
        public async Task<IActionResult> ChecKPaySchedule()
        {
            var count = await Context.PaySchedules.CountAsync(x => !x.Proceed);
            return Ok(count);

        }

        [Route("api/Payroll/RunPayrollDetailPrint")]
        [HttpGet("RunPayrollDetailPrint")]
        [Roles("CanViewBankRunPayroll", "CanViewCashRunPayroll")]
        public async Task<IActionResult> RunPayrollDetailPrint(Guid id, string prop)
        {
            var employeeSalary = await Mediator.Send(new GetRunPayrollPrintQuery()
            {
                Id = id,
                Prop = prop,
            });
            return Ok(employeeSalary);

        }

        [Route("api/Payroll/GetPaySlip")]
        [HttpGet("GetPaySlip")]
        [Roles("CanViewBankRunPayroll", "CanViewCashRunPayroll")]
        public async Task<IActionResult> GetPaySlip(Guid employeeId, Guid runPayrollId)
        {
            var employeeSalary = await Mediator.Send(new GetPaySlipQuery()
            {
                EmployeeId = employeeId,
                RunPayrollId = runPayrollId
            });
            return Ok(employeeSalary);

        }

        [Route("api/Payroll/RunPayrollDetailPrintCsv")]
        [HttpPost("RunPayrollDetailPrintCsv")]
        [Roles("CanDownloadCsvRunPayroll")]
        public async Task<IActionResult> RunPayrollDetailPrintCsv([FromBody] RunPayrollPrintLookUpModel runPayroll)
        {
            var companyName = User.Identity.Organization();

            var period = new[]
            {
                "Payroll: " ,
                runPayroll.PayrollSchedule
            };

            List<string> columnHeaders = new List<string>();
            columnHeaders.Add("#");
            columnHeaders.Add("Employee");
            columnHeaders.Add("Base Salary");

            foreach (var allowance in runPayroll.AllowanceHeader)
            {
                columnHeaders.Add(allowance.NameInPayslip);
            }
            foreach (var deduction in runPayroll.DeductionHeader)
            {
                columnHeaders.Add(deduction.NameInPayslip);
            }
            foreach (var contribution in runPayroll.ContributionHeader)
            {
                columnHeaders.Add(contribution.NameInPayslip);
            }

            columnHeaders.Add("Income Tax");
            columnHeaders.Add("Loan");
            columnHeaders.Add("Net Salary");

            List<string> columnFooter = new List<string>();
            columnFooter.Add("");
            columnFooter.Add("Total");
            columnFooter.Add(runPayroll.TotalBaseSalary.ToString("0.00"));

            foreach (var allowance in runPayroll.AllowanceFooter)
            {
                columnFooter.Add(allowance.Amount.ToString("0.00"));
            }
            foreach (var deduction in runPayroll.DeductionFooter)
            {
                columnFooter.Add(deduction.Amount.ToString("0.00"));
            }
            foreach (var contribution in runPayroll.ContributionFooter)
            {
                columnFooter.Add(contribution.Amount.ToString("0.00"));
            }

            columnFooter.Add(runPayroll.TotalIncomeTaxAmount.ToString("0.00"));
            columnFooter.Add(runPayroll.TotalLoanAmount.ToString("0.00"));
            columnFooter.Add(runPayroll.TotalNetSalary.ToString("0.00"));

            
            int i = 1;
            var sb = new StringBuilder();

            foreach (var payroll in runPayroll.SalaryTemplateList)
            {
                string value = i+++","+ payroll.EmployeeEnglishName.Replace(",", " ")+"," + payroll.BaseSalary;
                foreach (var allowance in payroll.AllowanceList)
                {
                    value += "," + allowance.Amount;
                }
                foreach (var deduction in payroll.DeductionList)
                {
                    value += "," + deduction.Amount;
                }
                foreach (var contribution in payroll.ContributionList)
                {
                    value += "," + contribution.Amount;
                }

                value += "," + payroll.IncomeTaxAmount + "," + payroll.LoanAmount + "," + payroll.NetSalary;
                sb.AppendLine(value);
            }

            var buffer = Encoding.UTF8.GetBytes(
                $"{string.Join(",", companyName)}\r\n" +
                $"{("Payroll Sheet ")}\r\n" +
                $"{string.Join(",", period)}\r\n" +
                $"{string.Join(",", columnHeaders)}\r\n " +
                $"{sb}\r\n " +
                $"{string.Join(",", columnFooter)}");

            var data = Encoding.UTF8.GetPreamble().Concat(buffer).ToArray();

            return File(data, "text/csv", $"PayrollSheet.csv");
        }
        #endregion

        #region For ChequeBook

      

        [Route("api/Payroll/SaveChequeBookInformation")]
        [HttpPost("SaveChequeBookInformation")]
        [Roles("CanAddCheque")]
        public async Task<IActionResult> SaveChequeBookInformation([FromBody] ChequeBookLookUpModel chequeBook)
        {
            if (chequeBook.Id == Guid.Empty)
            {
                var id = await Mediator.Send(new AddUpdateChequeBook()
                {
                    ChequeBook = chequeBook
                });

            }
            else
            {
                var id = await Mediator.Send(new AddUpdateChequeBook()
                {
                    ChequeBook = chequeBook
                });
            }


            return Ok(new { IsSuccess = true });
        }

        [Route("api/Payroll/ChequeBookList")]
        [HttpGet("ChequeBookList")]
        [Roles("CanAddCheque")]
        public async Task<IActionResult> ChequeBookList( bool isDropdown,Guid id)
        {
            var result = await Mediator.Send(new ChequeBookListQuery
            {
                IsDropDown = isDropdown,
                Id=id
            });

            return Ok(result);
        }
        [Route("api/Payroll/SaveChequeGuranteeInformation")]
        [HttpPost("SaveChequeGuranteeInformation")]
        [Roles("CanEditCheque")]
        public async Task<IActionResult> SaveChequeGuranteeInformation([FromBody] ChequeBookItemLookUpModel chequeBook)
        {
            if (chequeBook.Id == Guid.Empty)
            {
                var id = await Mediator.Send(new AddUpdateChequeGurantee()
                {
                    ChequeBook = chequeBook
                });

            }
            else
            {
                var id = await Mediator.Send(new AddUpdateChequeGurantee()
                {
                    ChequeBook = chequeBook
                });
            }


            return Ok(new { IsSuccess = true });
        }
        [Route("api/Payroll/GetChequeGuranteeDetailQuery")]
        [HttpGet("GetChequeGuranteeDetailQuery")]
        [Roles("CanEditCheque")]
        public async Task<IActionResult> GetChequeGuranteeDetailQuery(Guid id)
        {
            var result = await Mediator.Send(new GetChequeGuranteeDetailQuery
            {
                Id=id
            });

            return Ok(result);
        }

        [Route("api/Payroll/ChequeBookDetail")]
        [HttpGet("ChequeBookDetail")]
        [Roles("CanAddCheque")]
        public async Task<IActionResult> ChequeBookDetail(Guid id)
        {
            var chequeBook = await Mediator.Send(new GetChequeBookDetailQuery()
            {
                Id = id
            });
            return Ok(chequeBook);

        }
        [Route("api/Payroll/ChequeAndGuranteeDetail")]
        [HttpGet("ChequeAndGuranteeDetail")]
        [Roles("CanViewCheque")]
        public async Task<IActionResult> ChequeAndGuranteeDetail(Guid bankId)
        {
            var chequeBook = await Mediator.Send(new ChequeAndGuranteeDetailQuery()
            {
                BankId = bankId
            });
            return Ok(chequeBook);

        }

        [Route("api/Payroll/BlockChequeBook")]
        [HttpGet("BlockChequeBook")]
        [Roles("CanBlockCheque")]
        public async Task<IActionResult> BlockChequeBook(Guid id,string reason)
        {
            var loanPayment = await Mediator.Send(new BlockChequeBook()
            {
                Id = id,
                Reason = reason,
            });
            return Ok(loanPayment);

        }
        [Route("api/Payroll/ChequeBookDashboardList")]
        [HttpPost ("ChequeBookDashboardList")]
        [Roles("CanViewCheque")]
        public async Task<IActionResult> ChequeBookDashboardList([FromBody] List<ChequeLookUp> ids )
        {
            var list = await Mediator.Send(new ChequeBookDashboardList()
            {
                BankId = ids.Select(x=>x.Id).ToList(),
            });
            return Ok(list);
        }

        [Route("api/Payroll/IssuedToListQuery")]
        [HttpGet("IssuedToListQuery")]
        [Roles("CanEditCheque")]
        public async Task<IActionResult> IssuedToListQuery()
        {
            var loanPayment = await Mediator.Send(new IssuedToListQuery()
            {
              
            });
            return Ok(loanPayment);

        }
        #endregion
        #region For Holidays
        

        [Route("api/Payroll/SaveHolidays")]
        [HttpPost("SaveHolidays")]
        public async Task<IActionResult> SaveHolidays([FromBody] HolidayLookupModel holiday)
        {
            if (holiday.Id == Guid.Empty)
            {
                var id = await Mediator.Send(new AddHolidayCommand()
                {
                    Holiday = holiday
                });

            }
            else
            {
                var id = await Mediator.Send(new AddHolidayCommand()
                {
                    Holiday = holiday
                });
            }


            return Ok(new { IsSuccess = true });
        }

      

        [Route("api/Payroll/HolidaysDetail")]
        [HttpGet("HolidaysDetail")]
        public async Task<IActionResult> HolidaysDetail(Guid? id)
        {
            var allowance = await Mediator.Send(new GetHolidayDetailQuery()
            {
                Id = id
            });
            return Ok(allowance);
        }

        [Route("api/Payroll/WeekDaysList")]
        [HttpGet("WeekDaysList")]
        public async Task<IActionResult> WeekDaysList( )
        {
            var result = await Mediator.Send(new GetWeekDaysListQuery
            {
                IsDropDown = true,
                PageNumber =  1,
            });

            return Ok(result);
        }


        [Route("api/Payroll/ManualList")]
        [HttpGet("ManualList")]
        public async Task<IActionResult> ManualList()
        {
            var result = await Mediator.Send(new ManualAttendanceListQuery
            {
            });

            return Ok(result);
        }


        #endregion

        #region ManualAttendence
        [Route("api/Payroll/SaveManualAttendence")]
        [HttpPost("SaveManualAttendence")]
        public async Task<IActionResult> SaveManualAttendence([FromBody] ManualAttendenceLookUpModel manualAttendence)
        {
            if (manualAttendence.Id == Guid.Empty)
            {
                var id = await Mediator.Send(new AddManualAttendence()
                {
                    ManualAttendence = manualAttendence
                });

            }
            else
            {
                var id = await Mediator.Send(new AddManualAttendence()
                {
                    ManualAttendence = manualAttendence
                });
            }


            return Ok(new { IsSuccess = true });
        }



        [Route("api/Payroll/EmployeeOverTimeQuery")]
        [HttpGet("EmployeeOverTimeQuery")]
        public async Task<IActionResult> EmployeeOverTimeQuery(Guid? id)
        {
            var overTime = await Mediator.Send(new EmployeeAttendenceQuery()
            {
                EmployeeId = id,
               
            });
            return Ok(overTime);
        }
        [Route("api/Payroll/EmployeeTodayAttendence")]
        [HttpGet("EmployeeTodayAttendence")]
        public async Task<IActionResult> EmployeeTodayAttendence()
        {
            var overTime = await Mediator.Send(new EmployeeTodayAttendence()
            {

            });
            return Ok(overTime);
        }
        [Route("api/Payroll/EmployeeAttendenceFilterReport")]
        [HttpGet("EmployeeAttendenceFilterReport")]
        public async Task<IActionResult> EmployeeAttendenceFilterReport( DateTime fromDate, DateTime toDate)
        {
            var overTime = await Mediator.Send(new EmployeeAttendenceFilterReport()
            {
                FromDate = fromDate,
                ToDate = toDate,
            });
            return Ok(overTime);
        }

        [Route("api/Payroll/EmployeeAttendenceDateWiseQuery")]
        [HttpPost("EmployeeAttendenceDateWiseQuery")]
        public async Task<IActionResult> EmployeeAttendenceDateWiseQuery(DateTime fromDate, DateTime toDate,[FromBody] List<Guid?> employee)
        {
            var overTime = await Mediator.Send(new EmployeeAttendenceDateWiseQuery()
            {
                FromDate = fromDate,
                ToDate = toDate,
                Employee = employee,
            });
            return Ok(overTime);
        }

        #endregion

    }
}
