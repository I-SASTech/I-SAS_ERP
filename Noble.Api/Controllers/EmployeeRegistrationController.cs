
using Focus.Business.EmployeeRegistrations.Commands.AddUpdateEmployeeRegistration;
using Focus.Business.EmployeeRegistrations.Queries;
using Focus.Business.EmployeeRegistrations.Queries.GetEmployeeAttachments;
using Focus.Business.EmployeeRegistrations.Queries.GetEmployeeRegistrationDetails;
using Focus.Business.EmployeeRegistrations.Queries.GetEmployeeRegistrationList;
using Microsoft.AspNetCore.Mvc;
using Noble.Api.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Focus.Business.Departments;
using Focus.Business.Departments.Commands.AddUpdateDepartment;
using Focus.Business.Departments.Queries.GetDepartmentDetails;
using Focus.Business.Departments.Queries.GetDepartmentList;
using Focus.Business.Designations;
using Focus.Business.Designations.Commands.AddUpdateDesignation;
using Focus.Business.Designations.Queries.GetDesignationDetails;
using Focus.Business.Designations.Queries.GetDesignationList;
using Focus.Business.Interface;
using Focus.Business.JournalVouchers.Commands.AddUpdateJournalVoucher;
using Focus.Business.JournalVouchers.Queries.GetJournalVoucherNumber;
using Focus.Business.TemporaryCashIssues.Commands;
using Focus.Business.TemporaryCashIssues.Queries;
using Focus.Business.TemporaryCashRequests.Commands;
using Focus.Business.TemporaryCashRequests.Queries;
using Focus.Business.TemporaryCashReturns.Commands;
using Focus.Domain.Enum;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Focus.Domain.Entities;

namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeRegistrationController : BaseController
    {

        #region Employee Registration
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<EmployeeRegistrationController> _logger;
        private readonly IApplicationDbContext _context;
        public EmployeeRegistrationController(IApplicationDbContext context, IHostingEnvironment hostingEnvironment, ILogger<EmployeeRegistrationController> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
            _context = context;
        }


        [DisplayName("Employee Code")]
        [Route("api/EmployeeRegistration/EmployeeCode")]
        [HttpGet("EmployeeCode")]
        //[Roles("CanAddEmployeeReg", "CanAddSignUpUser")]
        //[Authorize(Roles = "Can Save Employee")]

        public async Task<IActionResult> EmployeeCode()
        {
            var autoNo = await Mediator.Send(new GetEmployeeRegistrationCodeQuery());
            return Ok(autoNo);
        }

        [Route("api/EmployeeRegistration/SaveEmployee")]
        [HttpPost("SaveEmployee")]
        [Roles("CanAddEmployeeReg", "CanEditEmployeeReg", "CanAddInquiry", "CanEditInquiry", "CanViewInquiry", "CanAddTCAllocation", "CanDraftTCAllocation", "CanDraftTCIssue", "CanAddTCIssue", "CanDraftTCRequest", "CanAddTCRequest", "CanAddSignUpUser", "CanEditSignUpUser", "CanViewPurchaseDraft")]
        public async Task<IActionResult> SaveEmployee([FromBody] EmployeeRegistrationVm employeeVm)
        {
            Guid id;
            if (employeeVm.Id == Guid.Empty)
            {

                id = await Mediator.Send(new AddUpdateEmployeeRegistrationCommand
                {
                    Id = new Guid(),
                    Code = employeeVm.Code,
                    EnglishName = employeeVm.EnglishName,
                    FatherName = employeeVm.FatherName,
                    EmployeeType = employeeVm.EmployeeType,
                    ArabicName = employeeVm.ArabicName,
                    RegistrationDate = employeeVm.RegistrationDate,
                    Gender = employeeVm.Gender,
                    SalaryType = employeeVm.SalaryType,
                    MartialStatus = employeeVm.MartialStatus,
                    Nationality = employeeVm.Nationality,
                    DateOfBirth = employeeVm.DateOfBirth,
                    MobileNo = employeeVm.MobileNo,
                    OtherContact = employeeVm.OtherContact,
                    Email = employeeVm.Email,
                    BloodGroup = employeeVm.BloodGroup,
                    Address = employeeVm.Address,
                    City = employeeVm.City,
                    Country = employeeVm.Country,
                    PrimaryNameOfPerson = employeeVm.PrimaryNameOfPerson,
                    PrimaryRelation = employeeVm.PrimaryRelation,
                    PrimaryContactNumber = employeeVm.PrimaryContactNumber,
                    PrimaryReferenceEmail = employeeVm.PrimaryReferenceEmail,
                    SecondaryNameOfPerson = employeeVm.SecondaryNameOfPerson,
                    SecondaryRelation = employeeVm.SecondaryRelation,
                    SecondaryContactNumber = employeeVm.SecondaryContactNumber,
                    SecondaryReferenceEmail = employeeVm.SecondaryReferenceEmail,
                    HomePersonName = employeeVm.HomePersonName,
                    HomeRelation = employeeVm.HomeRelation,
                    HomeContactNumber = employeeVm.HomeContactNumber,
                    HomeReferenceEmail = employeeVm.HomeReferenceEmail,
                    HomeCity = employeeVm.HomeCity,
                    HomeCountry = employeeVm.HomeCountry,
                    IDNumber = employeeVm.IDNumber,
                    IDType = employeeVm.IDType,
                    Title = employeeVm.Title,
                    ExpiryDate = employeeVm.ExpiryDate,
                    PassportNumber = employeeVm.PassportNumber,
                    PassportIssueDate = employeeVm.PassportIssueDate,
                    PassportExpiryDate = employeeVm.PassportExpiryDate,
                    PassportIssuingAuthority = employeeVm.PassportIssuingAuthority,
                    DrivingLicenseNumber = employeeVm.DrivingLicenseNumber,
                    DrivingLicenseType = employeeVm.DrivingLicenseType,
                    DrivingExpiryDate = employeeVm.DrivingExpiryDate,
                    DrivingIssuingAuthority = employeeVm.DrivingIssuingAuthority,
                    MedicalPolicNumber = employeeVm.MedicalPolicNumber,
                    MedicalPolicType = employeeVm.MedicalPolicType,
                    MedicalPolicProvider = employeeVm.MedicalPolicProvider,
                    MedicalPolicExpiryDate = employeeVm.MedicalPolicExpiryDate,
                    DrivingLicense = employeeVm.DrivingLicense,
                    Photo = employeeVm.Photo,
                    CNIC = employeeVm.CNIC,
                    Passport = employeeVm.Passport,
                    DepartmentId = employeeVm.DepartmentId,
                    DesignationId = employeeVm.DesignationId,
                    NationalOrForeign = employeeVm.NationalOrForeign,
                    SpouseName1 = employeeVm.SpouseName1,
                    SpouseName2 = employeeVm.SpouseName2,
                    SpouseName3 = employeeVm.SpouseName3,
                    SpouseName4 = employeeVm.SpouseName4,

                    TemporaryCashRequester = employeeVm.TemporaryCashRequester,
                    TemporaryCashIssuer = employeeVm.TemporaryCashIssuer,
                    TemporaryCashReceiver = employeeVm.TemporaryCashReceiver,
                    Days = employeeVm.Days,
                    Limit = employeeVm.Limit,
                    PerDayWorkHour = employeeVm.PerDayWorkHour,
                    AttachmentList = employeeVm.AttachmentList,
                    IsSignup = employeeVm.IsSignup,
                    Reason = employeeVm.Reason,
                    BankName = employeeVm.BankName,
                    AccountName = employeeVm.AccountName,
                    IbanNumber = employeeVm.IbanNumber,
                    AccountNumber = employeeVm.AccountNumber,
                    BranchName = employeeVm.BranchName,
                    BankAddress = employeeVm.BankAddress,
                    AccountType = employeeVm.AccountType,
                    EmployeeAccess = employeeVm.EmployeeAccess,
                    IsActive = !employeeVm.IsActive,
                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateEmployeeRegistrationCommand
                {
                    Id = employeeVm.Id,
                    Code = employeeVm.Code,
                    EnglishName = employeeVm.EnglishName,
                    FatherName = employeeVm.FatherName,
                    ArabicName = employeeVm.ArabicName,
                    EmployeeType = employeeVm.EmployeeType,
                    RegistrationDate = employeeVm.RegistrationDate,
                    Gender = employeeVm.Gender,
                    SalaryType = employeeVm.SalaryType,
                    MartialStatus = employeeVm.MartialStatus,
                    Nationality = employeeVm.Nationality,
                    DateOfBirth = employeeVm.DateOfBirth,
                    MobileNo = employeeVm.MobileNo,
                    OtherContact = employeeVm.OtherContact,
                    Email = employeeVm.Email,
                    BloodGroup = employeeVm.BloodGroup,
                    Address = employeeVm.Address,
                    City = employeeVm.City,
                    Country = employeeVm.Country,
                    PrimaryNameOfPerson = employeeVm.PrimaryNameOfPerson,
                    PrimaryRelation = employeeVm.PrimaryRelation,
                    PrimaryContactNumber = employeeVm.PrimaryContactNumber,
                    PrimaryReferenceEmail = employeeVm.PrimaryReferenceEmail,
                    SecondaryNameOfPerson = employeeVm.SecondaryNameOfPerson,
                    SecondaryRelation = employeeVm.SecondaryRelation,
                    SecondaryContactNumber = employeeVm.SecondaryContactNumber,
                    SecondaryReferenceEmail = employeeVm.SecondaryReferenceEmail,
                    HomePersonName = employeeVm.HomePersonName,
                    HomeRelation = employeeVm.HomeRelation,
                    HomeContactNumber = employeeVm.HomeContactNumber,
                    HomeReferenceEmail = employeeVm.HomeReferenceEmail,
                    HomeCity = employeeVm.HomeCity,
                    HomeCountry = employeeVm.HomeCountry,
                    IDNumber = employeeVm.IDNumber,
                    IDType = employeeVm.IDType,
                    Title = employeeVm.Title,
                    ExpiryDate = employeeVm.ExpiryDate,
                    PassportNumber = employeeVm.PassportNumber,
                    PassportIssueDate = employeeVm.PassportIssueDate,
                    PassportExpiryDate = employeeVm.PassportExpiryDate,
                    PassportIssuingAuthority = employeeVm.PassportIssuingAuthority,
                    DrivingLicenseNumber = employeeVm.DrivingLicenseNumber,
                    DrivingLicenseType = employeeVm.DrivingLicenseType,
                    DrivingExpiryDate = employeeVm.DrivingExpiryDate,
                    DrivingIssuingAuthority = employeeVm.DrivingIssuingAuthority,
                    MedicalPolicNumber = employeeVm.MedicalPolicNumber,
                    MedicalPolicType = employeeVm.MedicalPolicType,
                    MedicalPolicProvider = employeeVm.MedicalPolicProvider,
                    MedicalPolicExpiryDate = employeeVm.MedicalPolicExpiryDate,
                    DrivingLicense = employeeVm.DrivingLicense,
                    Photo = employeeVm.Photo,
                    CNIC = employeeVm.CNIC,
                    Passport = employeeVm.Passport,
                    DepartmentId = employeeVm.DepartmentId,
                    DesignationId = employeeVm.DesignationId,
                    NationalOrForeign = employeeVm.NationalOrForeign,
                    SpouseName1 = employeeVm.SpouseName1,
                    SpouseName2 = employeeVm.SpouseName2,
                    SpouseName3 = employeeVm.SpouseName3,
                    SpouseName4 = employeeVm.SpouseName4,
                    TemporaryCashRequester = employeeVm.TemporaryCashRequester,
                    TemporaryCashIssuer = employeeVm.TemporaryCashIssuer,
                    TemporaryCashReceiver = employeeVm.TemporaryCashReceiver,
                    Days = employeeVm.Days,
                    Limit = employeeVm.Limit,
                    PerDayWorkHour = employeeVm.PerDayWorkHour,
                    AttachmentList = employeeVm.AttachmentList,
                    Reason = employeeVm.Reason,
                    BankName = employeeVm.BankName,
                    AccountName = employeeVm.AccountName,
                    IbanNumber = employeeVm.IbanNumber,
                    AccountNumber = employeeVm.AccountNumber,
                    BranchName = employeeVm.BranchName,
                    BankAddress = employeeVm.BankAddress,
                    AccountType = employeeVm.AccountType,
                    EmployeeAccess = employeeVm.EmployeeAccess,
                    IsActive = !employeeVm.IsActive,
                });
            }
            if (id != Guid.Empty)
            {
                var employee = await Mediator.Send(new GetEmployeeRegistrationDetailQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, employee = employee });
            }
            return Ok(new { IsSuccess = false });
        }

        [Route("api/EmployeeRegistration/SaveEmployeeStatus")]
        [HttpPost("SaveEmployeeStatus")]
        [Roles("CanViewSignUpUser", "CanViewEmployeeReg")]
        public async Task<IActionResult> SaveEmployeeStatus([FromBody] EmployeeRegistrationVm employeeVm)
        {
            if (employeeVm.Id != Guid.Empty)
            {
                var id = await Mediator.Send(new ChangeEmployeeStatus
                {
                    Id = employeeVm.Id,
                    IsActive = employeeVm.IsActive,
                    IsUser = employeeVm.IsUser,
                });
                return Ok(id);
            }
            return Ok(Guid.Empty);
        }

        [Route("api/EmployeeRegistration/EmployeeList")]
        [HttpGet("EmployeeList")]
        [Roles("CanViewEmployeeReg", "CanViewInquiry", "CanAddSignUpUser", "CanEditSignUpUser", "CreditServiceInvoices", "CashServiceInvoices", "CanHoldServiceInvoices", "CanEditHoldServiceInvoices", "CreditInvoices", "CashInvoices", "CanHoldInvoices", "CanEditHoldInvoices", "CanAddLoanPayment", "CanEditLoanPayment", "CanAddEmployeeSalary", "CanEditEmployeeSalary", "User")]
        public async Task<IActionResult> EmployeeList(string searchTerm, int? pageNumber, bool IsDropDown, bool isSignup, string employeeType,Guid? departmentId, Guid? designationId)
        {
            var employee = await Mediator.Send(new GetEmployeeRegistrationListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = IsDropDown,
                isSignup = isSignup,
                employeeType = employeeType,
                DepartmentId = departmentId,
                DesignationId = designationId,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(employee);
        }
        //[Route("api/EmployeeRegistration/DeleteEmployee")]
        //[HttpGet("DeleteEmployee")]
        //[Roles("Can Delete Employee", "Noble Admin")]
        //public async Task<IActionResult> DeleteEmployee(Guid id)
        //{
        //    var employeeId = await Mediator.Send(new DeleteEmployeeRegistrationCommand
        //    {
        //        Id = id
        //    });
        //    return Ok(employeeId);

        //}
        [Route("api/EmployeeRegistration/EmployeeDetail")]
        [HttpGet("EmployeeDetail")]
        [Roles("CanAddSignUpUser", "CanEditSignUpUser", "CanAddEmployeeReg", "CanEditEmployeeReg", "CanDraftTCIssue", "CanEditTCIssue", "CanAddTCIssue")]
        public async Task<IActionResult> EmployeeDetail(Guid id)
        {
            var employee = await Mediator.Send(new GetEmployeeRegistrationDetailQuery()
            {
                Id = id
            });
            return Ok(employee);

        }

        public class pos
        {
            public string html { get; set; }
        }




        [Route("api/Company/GetEmployeeAttachments")]
        [HttpGet("GetEmployeeAttachments")]
        [Roles("User", "Noble Admin", "CanAddEmployeeReg", "CanEditEmployeeReg")]
        public async Task<IActionResult> GetEmployeeAttachments(string searchTerm, int? pageNumber, Guid id)
        {
            var companyListModel = await Mediator.Send(
                new GetEmployeeAttachmentsQuery
                {
                    Id = id,
                    SearchTerm = searchTerm,
                    PageNumber = pageNumber ?? 1
                });
            return Ok(companyListModel);
        }
        #endregion
        #region Department

        [DisplayName("Department Code")]
        [Route("api/EmployeeRegistration/DepartmentCode")]
        [HttpGet("DepartmentCode")]
        [Roles("CanAddEmployeeReg", "CanEditEmployeeReg")]
        public async Task<IActionResult> DepartmentCode()
        {
            var autoNo = await Mediator.Send(new GetDepartmentCodeQuery());

            return Ok(autoNo);
        }

        [Route("api/EmployeeRegistration/SaveDepartment")]
        [HttpPost("SaveDepartment")]
        [Roles("CanAddEmployeeReg", "CanEditEmployeeReg")]
        public async Task<IActionResult> SaveDepartment([FromBody] DepartmentVm departmentVm)
        {
            Guid id;
            if (departmentVm.Id == Guid.Empty)
            {

                id = await Mediator.Send(new AddUpdateDepartmentCommand
                {
                    Id = new Guid(),
                    Code = departmentVm.Code,
                    BankId = departmentVm.BankId,
                    Name = departmentVm.Name,
                    NameArabic = departmentVm.NameArabic,
                    Description = departmentVm.Description,
                    IsActive = departmentVm.IsActive
                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateDepartmentCommand
                {
                    Id = departmentVm.Id,
                    Code = departmentVm.Code,
                    BankId = departmentVm.BankId,
                    Name = departmentVm.Name,
                    NameArabic = departmentVm.NameArabic,
                    Description = departmentVm.Description,
                    IsActive = departmentVm.IsActive
                });
            }

            return Ok(new { IsSuccess = true });
        }
        [Route("api/EmployeeRegistration/DepartmentList")]
        [HttpGet("DepartmentList")]
        [Roles("CanAddEmployeeReg", "CanEditEmployeeReg")]
        public async Task<IActionResult> DepartmentList(string searchTerm, int? pageNumber, bool isActive)
        {
            var department = await Mediator.Send(new GetDepartmentListQuery
            {
                isActive = isActive,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(department);
        }

        [Route("api/EmployeeRegistration/DepartmentDetail")]
        [HttpGet("DepartmentDetail")]
        [Roles("CanAddEmployeeReg", "CanEditEmployeeReg")]
        public async Task<IActionResult> DepartmentDetail(Guid Id)
        {
            var department = await Mediator.Send(new GetDepartmentDetailQuery()
            {
                Id = Id
            });
            return Ok(department);

        }
        #endregion


        #region Designation

        [DisplayName("Designation Code")]
        [Route("api/EmployeeRegistration/DesignationCode")]
        [HttpGet("DesignationCode")]
        [Roles("CanAddEmployeeReg", "CanEditEmployeeReg")]
        public async Task<IActionResult> DesignationCode()
        {
            var autoNo = await Mediator.Send(new GetDesignationCodeQuery());

            return Ok(autoNo);
        }

        [Route("api/EmployeeRegistration/SaveDesignation")]
        [HttpPost("SaveDesignation")]
        [Roles("CanAddEmployeeReg", "CanEditEmployeeReg")]
        public async Task<IActionResult> SaveDesignation([FromBody] DesignationVm designationVm)
        {
            Guid id;
            if (designationVm.Id == Guid.Empty)
            {

                id = await Mediator.Send(new AddUpdateDesignationCommand
                {
                    Id = new Guid(),
                    Code = designationVm.Code,
                    Name = designationVm.Name,
                    NameArabic = designationVm.NameArabic,
                    Description = designationVm.Description,
                    IsActive = designationVm.IsActive
                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateDesignationCommand
                {
                    Id = designationVm.Id,
                    Code = designationVm.Code,
                    Name = designationVm.Name,
                    NameArabic = designationVm.NameArabic,
                    Description = designationVm.Description,
                    IsActive = designationVm.IsActive
                });
            }

            return Ok(new { IsSuccess = true });
        }
        [Route("api/EmployeeRegistration/DesignationList")]
        [HttpGet("DesignationList")]
        [Roles("CanAddEmployeeReg", "CanEditEmployeeReg")]
        public async Task<IActionResult> DesignationList(string searchTerm, int? pageNumber, bool isActive)
        {
            var designation = await Mediator.Send(new GetDesignationListQuery
            {
                isActive = isActive,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(designation);
        }

        [Route("api/EmployeeRegistration/DesignationDetail")]
        [HttpGet("DesignationDetail")]
        [Roles("CanAddEmployeeReg", "CanEditEmployeeReg")]
        public async Task<IActionResult> DesignationDetail(Guid Id)
        {
            var designation = await Mediator.Send(new GetDesignationDetailQuery()
            {
                Id = Id
            });
            return Ok(designation);

        }
        #endregion

        #region Temporary Cash
        [Route("api/EmployeeRegistration/AutoTemporaryCashRequestCode")]
        [HttpGet("AutoTemporaryCashRequestCode")]
        [Roles("CanAddTCRequest", "CanDraftTCRequest")]
        public async Task<IActionResult> AutoTemporaryCashRequestCode()
        {

            var jvNumber = await Mediator.Send(new AutoTemporaryCashRequestCode { });

            return Ok(jvNumber);
        }

        [Route("api/EmployeeRegistration/AddTemporaryCashRequest")]
        [HttpPost("AddTemporaryCashRequest")]
        [Roles("CanAddTCRequest", "CanEditTCRequest", "CanDraftTCRequest")]
        public async Task<IActionResult> AddTemporaryCashRequest([FromBody] TemporaryCashRequestModel temporaryCash)
        {

            var message = await Mediator.Send(new AddUpdateTemporaryCashRequest
            {
                TemporaryCash = temporaryCash
            });

            if (temporaryCash.Id == Guid.Empty)
            {
                return Ok(new { Message = message, type = "Add" });
            }
            else
            {
                return Ok(new { Message = message, type = "Edit" });
            }
        }

        [Route("api/EmployeeRegistration/TemporaryCashRequestList")]
        [HttpGet("TemporaryCashRequestList")]
        [Roles("CanViewTCRequest", "CanDraftTCRequest", "CanAddTCIssue", "CanEditTCIssue", "CanDraftTCIssue")]

        public async Task<IActionResult> TemporaryCashRequestList(string searchTerm, int? pageNumber, ApprovalStatus status, bool isDropdown, DateTime? fromDate, DateTime? toDate)
        {
            var purchaseOrder = await Mediator.Send(new TemporaryCashRequestListQuery
            {
                SearchTerm = searchTerm,
                Status = status,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
                FromDate = fromDate,
                ToDate = toDate
            });

            return Ok(purchaseOrder);
        }

        [Route("api/EmployeeRegistration/TemporaryCashRequestDetail")]
        [HttpGet("TemporaryCashRequestDetail")]
        [Roles("CanEditTCRequest", "CanPrintTCRequest", "CanViewDetailTCRequest","CanAddTCIssue", "CanEditTCIssue", "CanDraftTCIssue")]
        public async Task<IActionResult> TemporaryCashRequestDetail(Guid id)
        {
            var temporaryCash = await Mediator.Send(new GetTemporaryCashRequestDetail()
            {
                Id = id
            });
            return Ok(temporaryCash);
        }

        [Route("api/EmployeeRegistration/TemporaryCashUserList")]
        [HttpGet("TemporaryCashUserList")]
        public async Task<IActionResult> TemporaryCashUserList()
        {
            var cashRequestUsers =await _context.CashRequestUsers.AsNoTracking().Where(x => x.IsActive).ToListAsync();
            return Ok(cashRequestUsers);
        }
        #endregion


        #region Temporary Cash Issue
        [Route("api/EmployeeRegistration/AutoTemporaryCashIssueCode")]
        [HttpGet("AutoTemporaryCashIssueCode")]
        [Roles("CanAddTCIssue", "CanDraftTCIssue")]
        public async Task<IActionResult> AutoTemporaryCashIssueCode()
        {

            var jvNumber = await Mediator.Send(new AutoTemporaryCashIssueCode { });

            return Ok(jvNumber);
        }

        [Route("api/EmployeeRegistration/AddTemporaryCashIssue")]
        [HttpPost("AddTemporaryCashIssue")]
        [Roles("CanAddTCIssue", "CanEditTCIssue", "CanDraftTCIssue")]
        public async Task<IActionResult> AddTemporaryCashIssue([FromBody] TemporaryCashIssueLookUp temporaryCash)
        {

            var message = await Mediator.Send(new AddUpdateTemporaryCashIssue
            {
                TemporaryCash = temporaryCash
            });

            if (temporaryCash.Id == Guid.Empty)
            {
                return Ok(new { Message = message, type = "Add" });
            }
            else
            {
                return Ok(new { Message = message, type = "Edit" });
            }
        }

        [Route("api/EmployeeRegistration/TemporaryCashIssueList")]
        [HttpGet("TemporaryCashIssueList")]
        [Roles("CanViewTCIssue", "CanDraftTCIssue")]
        public async Task<IActionResult> TemporaryCashIssueList(string searchTerm, int? pageNumber, ApprovalStatus status, bool isDropdown, DateTime? fromDate, DateTime? toDate)
        {
            var purchaseOrder = await Mediator.Send(new TemporaryCashIssueListQuery
            {
                SearchTerm = searchTerm,
                Status = status,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
                FromDate = fromDate,
                ToDate = toDate
            });

            return Ok(purchaseOrder);
        }

        [Route("api/EmployeeRegistration/TemporaryCashIssueDetail")]
        [HttpGet("TemporaryCashIssueDetail")]
        [Roles("CanEditTCIssue", "CanPrintTCIssue")]
        public async Task<IActionResult> TemporaryCashIssueDetail(Guid id)
        {
            var temporaryCash = await Mediator.Send(new GetTemporaryCashIssueDetail()
            {
                Id = id
            });
            return Ok(temporaryCash);
        }

        [Route("api/EmployeeRegistration/TemporaryCashBalance")]
        [HttpGet("TemporaryCashBalance")]
        [Roles("CanAddTCIssue", "CanEditTCIssue", "CanDraftTCIssue")]
        public async Task<IActionResult> TemporaryCashBalance()
        {
            var account = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Name == "Temporary Cash");
            if (account!=null)
            {
                var opening= _context.Transactions.Where(x => x.AccountId == account.Id).Sum(x => x.Debit - x.Credit);
                return Ok(opening);

            }
            return Ok(0);
        }
        #endregion

        #region Temporary Cash Return
        [Route("api/EmployeeRegistration/AddTemporaryCashReturn")]
        [HttpPost("AddTemporaryCashReturn")]
        public async Task<IActionResult> AddTemporaryCashReturn([FromBody] TemporaryCashReturnLookUp temporaryCashReturn)
        {

            var message = await Mediator.Send(new AddTemporaryCashReturnCommand
            {
                TemporaryCashReturn = temporaryCashReturn
            });

            if (temporaryCashReturn.Id == Guid.Empty)
            {
                return Ok(new { Message = message, type = "Add" });
            }
            else
            {
                return Ok(new { Message = message, type = "Edit" });
            }
        }
        #endregion

    }


}
