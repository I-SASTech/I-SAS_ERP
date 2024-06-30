using Focus.Business.Extensions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Noble.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Focus.Business.AdditionalCompany.Commands.AddUpdateAdditionalCompany;
using Focus.Business.AdditionalCompany.Queries.GetAdditionalCompanyList;
using Focus.Business.Warehouses.Queries.GetWarehouseLists;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Focus.Business.Warehouses.Commands.AddUpdateWarehouse;
using Focus.Business.Warehouses.Commands.DeleteWarehouse;
using Focus.Business.Contacts.Queries;
using Focus.Business.Warehouses.Queries.GetWarehouseDetails;
using Focus.Business.Terminals.Queries.GetTerminalDetails;
using Focus.Business.Terminals.Commands.DeleteTerminal;
using Focus.Business.Terminals.Queries.GetTerminalList;
using Focus.Business.Terminals.Commands.AddUpdateTerminal;
using Focus.Business.Terminals.Queries;
using Focus.Business.CompanyAccountSetups.Command.AddUpdateCommand;
using Focus.Business.Brands.Queries.GetBrandDetails;
using Focus.Business.MonthlyCosts.Commands.AddUpdateMonthlyCost;
using Focus.Business.DailyExpenses.Commands.AddUpdateDailyExpense;
using Focus.Business.DailyExpenses.Queries.GetDailyExpense;
using Focus.Business.DailyExpenses;

using Focus.Business.DailyExpenses.Queries.GetDailyExpenseList;
using Focus.Business.DailyExpenses.Queries.GetDailyExpenseDetail;
using Focus.Business.CompanyLicences.Commands.AddUpdateCompanyLicence;
using Focus.Business.Dashboard.Queries.GetInventoryList;
using Focus.Business.Transactions.JVTransaction;
using Focus.Business.AdditionalCompany.Queries.GetCompanyAttachments;
using Focus.Business.AdditionalCompany.Commands.AddUpdateCompanyAttachment;
using Focus.Business.Attachments.Commands;
using Focus.Business.Attachments.Queries;
using Focus.Business.BankPosTerminals.Commands.AddUpdateBankPosTerminal;
using Focus.Business.BankPosTerminals.Queries.GetBankPosTerminalDetails;
using Focus.Business.BankPosTerminals.Queries.GetBankPosTerminalList;
using Focus.Business.Claims.Command.ModuleWiseClaim;
using Focus.Business.CompanyActionProcess.Commands;
using Focus.Business.CompanyActionProcess.Queries;
using Focus.Business.Dashboard.Queries.GetContactBalanceQuery;
using Focus.Business.Dashboard.Queries;
using Focus.Business.NobleRole.Commands.AddUpdateNobleRole;
using Focus.Business.NobleRole.Queries.GetNobleRoleList;
using Focus.Business.NobleRole.Queries.GetNobleRoleDetails;
using Focus.Business.RolesPermission.Queries.GetRolesPermissionsDetails;
using Focus.Business.NobleUserRole.Commands.AddUpdateNobleUserRole;
using Focus.Business.NobleUserRole.Queries.GetNobleUserRoleDetails;
using Focus.Business.ModulesInformation.ModuleNames.Queries.GetModuleNameList;
using Focus.Business.ModulesInformation.ModuleRights.Queries.GetModuleRightList;
using Focus.Business.RolePermission.Commands.AddUpdateRolesPermissions;
using Focus.Business.RolePermission.Queries.GetByCategoryPermission;
using Focus.Business.RolePermission.Queries.GetRolesPermissionsList;
using Focus.Business.RolePermission.Queries.GetCompanyPermission;
using Focus.Business.RolePermission.Queries.RemoveUserRole;
using Microsoft.EntityFrameworkCore;
using Focus.Business.Exceptions;
using Focus.Business.FinancialYears.Commands;
using Focus.Business.FinancialYears.Queries.GetCurrentYear;
using Focus.Business.NoblePermission.Commands.AddUpdateNoblePermission;
using Focus.Business.NobleUserRole.Commands.UpdateUserPermission;
using Focus.Business.NobleUserRole.Queries.GetNoblePermissionList;
using Focus.Business.Permission.Commands.AddUpdateNoblePermission;
using Focus.Business.Permission.Queries.GetCompanyInformation;
using Focus.Business.Sales.Queries.GetRetailReport;
using Focus.Domain.Enum;
using Focus.Domain.Interface;
using Microsoft.Extensions.Configuration;
using GetProcessListQuery = Focus.Business.CompanyActionProcess.Queries.GetProcessListQuery;
using Focus.Business.CompanyLicences.Queries.GetCompanyAboutUsDetails;
using Focus.Business.CompanyLicences.Queries.GetCompanyLicenseForClientManagement;
using Focus.Business.Permission.Commands.AddUpdateNoblePaymentLimit;
using Focus.Business.MonthlyCosts.Commands;
using Focus.Business.MonthlyCosts.Queries;
using Focus.Business.Dashboard.Queries.AccountDashboardQuery;
using Focus.Business.Dashboard.Queries.InventoryDashBoadReport;
using Focus.Business.Dashboard.Queries.CashAndBankDashboardQuery;
using Focus.Business.Reports.TrialBalanceReport;
using Focus.Business.Permission.Commands;
using Focus.Business.Dashboard.Queries.EmployeeDashboardQuery;
using System.Net.NetworkInformation;
using System.Net;
using Focus.Business.Prefix.Model;
using Focus.Business.Prefix.Quries;
using Focus.Business.Prefix.Commands;
using Focus.Business;
using Newtonsoft.Json;
using System.Text;
using Focus.Business.InvoiceDefaults;
using Focus.Business.InvoiceDefaults.Commands;
using Focus.Business.InvoiceDefaults.Queries;
using Focus.Business.Reports.AdvanceAccountLedger.Queries;
using Focus.Business.Reports.AdvanceAccountLedgerDetailsReport.Queries;
using Focus.Business.Branches.Queries;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.Branches.BranchesPermission;
using Focus.Business.DailyExpenses.Commands.DeleteDailyExpense;
using Microsoft.Extensions.Hosting.Internal;

namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompanyComponent _companyComponent;
        private readonly IPrincipal _principal;
        private readonly ISendEmail _sendEmail;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserHttpContextProvider _contextProvider;
        private string _code;


        public readonly IConfiguration _configuration;

        public CompanyController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            ICompanyComponent companyComponent, IPrincipal principal, ISendEmail sendEmail, IHostingEnvironment hostingEnvironment,
            IUserHttpContextProvider contextProvider, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _companyComponent = companyComponent;
            _principal = principal;
            _sendEmail = sendEmail;
            _hostingEnvironment = hostingEnvironment;
            _contextProvider = contextProvider;
            _configuration = configuration;
        }

        #region For Company Accounts Setup
        [Route("api/Company/SaveCompanyAccountSetup")]
        [HttpPost("SaveCompanyAccountSetup")]
        [Roles("Can Save Company Setup", "Noble Admin")]
        public async Task<IActionResult> SaveCompanyAccountSetup([FromBody] CompanyAccountSetupVM companyVm)
        {
            var id = Guid.Empty;
            if (companyVm.Id == Guid.Empty)
            {
                id = await Mediator.Send(new AddUpdateCompanyAccountSetupCommand
                {
                    Id = new Guid(),
                    InventoryAccountId = companyVm.InventoryAccountId,
                    VatPayableAccountId = companyVm.VatPayableAccountId,
                    VatReceiableAccountId = companyVm.VatReceiableAccountId,
                    SaleAccountId = companyVm.SaleAccountId,
                    DiscountPayableAccountId = companyVm.DiscountPayableAccountId,
                    DiscountReceivableAccountId = companyVm.DiscountReceivableAccountId,
                    FreeofCostAccountId = companyVm.FreeofCostAccountId,
                    StockInAccountId = companyVm.StockInAccountId,
                    StockOutAccountId = companyVm.StockOutAccountId,
                    BundleAccountId = companyVm.BundleAccountId,
                    PromotionAccountId = companyVm.PromotionAccountId,
                    BankId = companyVm.BankId,
                    CashId = companyVm.CashId,
                    BarcodeType = companyVm.BarcodeType,
                    CurrencyId = companyVm.CurrencyId,
                    TaxMethod = companyVm.TaxMethod,
                    TaxRateId = companyVm.TaxRateId,
                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateCompanyAccountSetupCommand
                {
                    Id = companyVm.Id,
                    InventoryAccountId = companyVm.InventoryAccountId,
                    VatPayableAccountId = companyVm.VatPayableAccountId,
                    VatReceiableAccountId = companyVm.VatReceiableAccountId,
                    SaleAccountId = companyVm.SaleAccountId,
                    DiscountPayableAccountId = companyVm.DiscountPayableAccountId,
                    DiscountReceivableAccountId = companyVm.DiscountReceivableAccountId,
                    FreeofCostAccountId = companyVm.FreeofCostAccountId,
                    StockInAccountId = companyVm.StockInAccountId,
                    StockOutAccountId = companyVm.StockOutAccountId,
                    BundleAccountId = companyVm.BundleAccountId,
                    PromotionAccountId = companyVm.PromotionAccountId,
                    CashId = companyVm.CashId,
                    BankId = companyVm.BankId,
                    BarcodeType = companyVm.BarcodeType,
                    CurrencyId = companyVm.CurrencyId,
                    TaxMethod = companyVm.TaxMethod,
                    TaxRateId = companyVm.TaxRateId,

                });
            }

            return Ok(companyVm.Id);
        }
        [Route("api/Company/CompanyAccountSetupDetails")]
        [HttpGet("CompanyAccountSetupDetails")]
        [Roles("CanPrintItemBarcode", "CanViewDetailItem", "TouchInvoiceTemplate1", "TouchInvoiceTemplate2", "TouchInvoiceTemplate3", "CanPrintItemBarcode", "CanAddItem", "CanEditItem")]
        public async Task<IActionResult> CompanyAccountSetupDetails()
        {
            var companyAccountSetupDetails = await Mediator.Send(new GetCompanyAccountSetupDetailQuery());
            return Ok(companyAccountSetupDetails);
        }
        #endregion
        #region For Company

        [Route("api/Company/AddLicence")]
        [HttpPost("AddLicence")]
        [Roles("Noble Admin", "Super Admin", "Admin")]
        public async Task<IActionResult> AddLicence([FromBody] CompanyLicenceVm companyLicenceVm)
        {
            var result = await Mediator.Send(new AddUpdateCompanyLicence()
            {
                Id = companyLicenceVm.Id,
                CompanyId = companyLicenceVm.CompanyId,
                CompanyType = companyLicenceVm.CompanyType,
                FromDate = companyLicenceVm.FromDate,
                ToDate = companyLicenceVm.ToDate,
                NumberOfUsers = companyLicenceVm.NumberOfUsers,
                NumberOfTransactions = companyLicenceVm.NumberOfTransactions,
                IsActive = companyLicenceVm.IsActive,
                IsBlock = companyLicenceVm.IsBlock
            });

            return Ok(result);
        }
        [Route("api/Company/EditCompany")]
        [HttpPost("EditCompany")]
        [Authorize(Roles = "Noble Admin")]
        public IActionResult Edit(Guid Id)
        {
            var company = _companyComponent.GetCompanyById(Id);
            var logoBase64 = "";
            var imagePath = $"{_hostingEnvironment.WebRootPath}" + company.LogoPath;
            if (System.IO.File.Exists(imagePath))
            {
                var bytes = System.IO.File.ReadAllBytesAsync(imagePath);
                logoBase64 = Convert.ToBase64String(bytes.Result);
            }
            company.Base64Logo = logoBase64;
            return Ok(company);
        }

        [Route("api/Company/GetCompanyDetail")]
        [HttpGet("GetCompanyDetail")]
        public IActionResult GetCompanyDetail(Guid Id)
        {
            var company = _companyComponent.GetCompanyById(Id);
            var logoBase64 = "";
            var imagePath = $"{_hostingEnvironment.WebRootPath}" + company.LogoPath;
            if (System.IO.File.Exists(imagePath))
            {
                var bytes = System.IO.File.ReadAllBytesAsync(imagePath);
                logoBase64 = Convert.ToBase64String(bytes.Result);
            }
            company.Base64Logo = logoBase64;
            return Ok(company);
        }

        [Route("api/Company/PasswordVerify")]
        [HttpPost("PasswordVerify")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PasswordVerifyAsync([FromBody] PasswordVerifyVm passwordVerify)
        {
            var user = "";
            var loginUser = new ApplicationUser();
            if (passwordVerify.UserName.IndexOf('@') > -1)
            {
                //Validate email format
                string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                    @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                    @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                Regex re = new Regex(emailRegex);
                if (!re.IsMatch(passwordVerify.UserName))
                {
                    loginUser = await _userManager.FindByNameAsync(passwordVerify.UserName);
                    if (loginUser != null)
                    {
                        user = loginUser.UserName;
                        loginUser.TwoFactorEnabled = false;
                    }

                }
                else
                {
                    loginUser = await _userManager.FindByEmailAsync(passwordVerify.UserName);
                    if (loginUser != null)
                    {
                        user = loginUser.UserName;
                        loginUser.TwoFactorEnabled = false;
                    }
                }

            }
            else
            {
                loginUser = await _userManager.FindByNameAsync(passwordVerify.UserName);
                if (loginUser != null)
                {
                    user = loginUser.UserName;
                    loginUser.TwoFactorEnabled = false;
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, passwordVerify.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                loginUser.TwoFactorEnabled = true;
                return Ok(true);
            }
            return Ok(false);



        }
        [Route("api/Company/SaveCompany")]
        [HttpPost("SaveCompany")]
        public async Task<IActionResult> SaveCompany([FromBody] CompanyAndUserVm companyUser)
        {

            if (companyUser.Id == String.Empty)
            {
                var company = new Company
                {
                    NameArabic = companyUser.NameArabic,
                    NameEnglish = companyUser.NameEnglish,
                    AddressArabic = companyUser.AddressArabic,

                    AddressEnglish = companyUser.AddressEnglish,
                    VatRegistrationNo = companyUser.VatRegistrationNo,
                    CityArabic = companyUser.CityArabic,
                    CityEnglish = companyUser.CityEnglish,
                    CompanyRegNo = companyUser.ComercialRegNo,
                    CompanyEmail = companyUser.CompanyEmail,
                    CountryArabic = companyUser.CountryArabic,
                    Landline = companyUser.LandLine,
                    CountryEnglish = companyUser.CountryEnglish,
                    HouseNumber = companyUser.HouseNumber,
                    CreatedDate = DateTime.UtcNow,
                    Website = companyUser.Website,
                    PhoneNo = companyUser.PhoneNo,
                    ClientNo = companyUser.ClientNo,
                    Blocked = false,
                    ParentId = _principal.Identity.CompanyId(),
                    Postcode = companyUser.Postcode,
                    Town = companyUser.Town,

                };
                await _context.Companies.AddAsync(company);
                var register = new UserDetailDto()
                {
                    FirstName = companyUser.FirstName,
                    LastName = companyUser.LastName,
                    Password = companyUser.Password,
                    UserId = companyUser.FirstName,
                    Email = companyUser.Email,
                    PhoneNumber = companyUser.PhoneNumber,
                };
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = register.UserId,
                        Email = register.Email,
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        CompanyId = company.Id,

                    };

                    //GENERATE NEW USER PASSWORD

                    var result = await _userManager.CreateAsync(user, register.Password);
                    if (result.Succeeded)
                    {
                        var sendEmailDto = new SendEmailDto()
                        {
                            EmailTo = user.Email,
                            Subject = "Create Password"
                        };
                        //await _sendEmail.SendEmailAsync(sendEmailDto, user.UserName, user.Id);
                        await _userManager.AddToRoleAsync(user, register.RoleName = "Admin");
                        var companyNobleRole = new NobleRoles()
                        {
                            Name = "Admin",
                            NormalizedName = "ADMIN",
                            IsActive = true
                        };
                        await _context.AddAsync(companyNobleRole);
                        var claims = new List<Claim>
                        {
                            new Claim("Email",user.Email),
                            new Claim("FullName",$"{user.FirstName}{user.LastName}"),
                            new Claim("Organization",company.NameEnglish),
                            new Claim("CompanyId",company.Id.ToString()),
                            new Claim("NobleCompanyId",_principal.Identity.CompanyId().ToString())
                        };
                        await _userManager.AddClaimsAsync(user, claims);
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }
                        await Mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        });
                        await _context.SaveChangesAsync();
                        if (company.Id != Guid.Empty)
                        {
                            _context.SetModified(companyNobleRole, "CompanyId", company.Id);
                            _context.SaveChangesAfter();
                        }
                        return Ok(new { value = true, check = "Add" });
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                return null;
            }
            else
            {
                var company = new CompanyDto
                {
                    Id = Guid.Parse(companyUser.Id),
                    NameArabic = companyUser.NameArabic,
                    NameEnglish = companyUser.NameEnglish,
                    CompanyRegNo = companyUser.ComercialRegNo,
                    CityEnglish = companyUser.CityEnglish,
                    CityArabic = companyUser.CityArabic,
                    AddressArabic = companyUser.AddressArabic,
                    AddressEnglish = companyUser.AddressEnglish,
                    VatRegistrationNo = companyUser.VatRegistrationNo,
                    PhoneNo = companyUser.PhoneNo,
                    Website = companyUser.Website,
                    LandLine = companyUser.LandLine,
                    CountryEnglish = companyUser.CountryEnglish,
                    CountryArabic = companyUser.CountryArabic,
                    Postcode = companyUser.Postcode,
                    Town = companyUser.Town,
                    LogoPath = companyUser.LogoPath,
                    HouseNumber = companyUser.HouseNumber,
                    ClientNo = companyUser.ClientNo,
                };
                _companyComponent.UpdateCompany(company);
                return Ok(new { value = true, check = "Update" });
            }
        }

        [Route("api/Company/SaveMultiUnit")]
        [HttpPost("SaveMultiUnit")]
        //[Authorize(Roles = "Noble Admin")]
        public async Task<IActionResult> SaveMultiUnit([FromBody] CompanyDto company)
        {
            var response = _companyComponent.UpdateCompany(company);
            return Ok(new { value = true, check = "Update" });
        }
        [Route("api/Company/SaveDayStart")]
        [HttpPost("SaveDayStart")]
        //[Authorize(Roles = "Noble Admin")]
        public IActionResult SaveDayStart([FromBody] CompanyDto company)
        {
            var response = _companyComponent.UpdateCompany(company);
            return Ok(new { value = true, check = "Update" });
        }

        [Route("api/Company/List")]
        [HttpGet("List")]
        [Authorize(Roles = "Noble Admin")]
        public IActionResult CompanyList(Guid id)
        {
            return Ok(_companyComponent.GetCompaniesList(id));
        }


        [Route("api/Company/EditCompany")]
        [HttpGet("EditCompany")]
        [Roles("User", "Noble Admin")]
        public async Task<IActionResult> EditCompanyAsync(Guid Id)
        {
            var company = _companyComponent.GetCompanyById(Id);

            var attachments = _context.Attachments
                .AsNoTracking()
                .Where(x => x.DocumentId == company.Id)
                .AsQueryable();

            var attachmentList = await attachments.Select(x => new AttachmentLookUpModel
            {
                Id = x.Id,
                DocumentId = x.DocumentId,
                Date = x.Date,
                Description = x.Description,
                FileName = x.FileName,
                Path = x.Path
            }).ToListAsync();

            var companyDetails = new CompanyAndUserVm
            {
                NameEnglish = company.NameEnglish,
                NameArabic = company.NameArabic,
                AddressEnglish = company.AddressEnglish,
                AddressArabic = company.AddressArabic,
                ComercialRegNo = company.CompanyRegNo,
                VatRegistrationNo = company.VatRegistrationNo,
                CityEnglish = company.CityEnglish,
                CityArabic = company.CityArabic,
                ClientNo = company.ClientNo,
                CompanyEmail = company.CompanyEmail,
                CountryEnglish = company.CountryEnglish,
                CountryArabic = company.CountryArabic,
                LogoPath = company.LogoPath,
                Website = company.Website,
                LandLine = company.LandLine,
                PhoneNo = company.PhoneNo,
                CategoryEnglish = company.CategoryEnglish,
                CategoryArabic = company.CategoryArabic,
                CompanyNameEnglish = company.CompanyNameEnglish,
                CompanyNameArabic = company.CompanyNameArabic,
                AttachmentList = attachmentList,
                Id = company.Id.ToString(),
            };
            return Ok(companyDetails);
        }
        [Route("api/Company/UploadFilesAsync")]
        [HttpPost("UploadFilesAsync")]
        public IActionResult UploadFilesAsync(List<IFormFile> files, Guid? branchId)
        {
            var branch = _context.Branches.FirstOrDefault(x => x.Id == branchId);
            

            string dbFilePath;
            string filePath;
            var dbPath = string.Empty;
            if (files != null && files.Any())
            {
                //check if Attachment folder is not created
                var pathWithFolderName = Path.Combine(_hostingEnvironment.WebRootPath, "Attachment");
                if (!Directory.Exists(pathWithFolderName))
                {
                    var di = Directory.CreateDirectory(pathWithFolderName);
                }

                if(branch != null)
                {
                    string branchDetails = branch != null ? branch.Code + "-" + branch.BranchName : null;

                    string targetPath = _configuration.GetSection("Ftp:ServerFolder").Value + "/";
                    string host = _configuration.GetSection("Ftp:FtpHost").Value;

                    var pathWithBranchName = Path.Combine(_hostingEnvironment.WebRootPath, $"Attachment\\{branchDetails}");
                    if (!Directory.Exists(pathWithBranchName))
                    {
                        var di = Directory.CreateDirectory(pathWithBranchName);

                        string ftpFolderPath = $"{host}{targetPath}/{branchDetails}";

                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpFolderPath);
                        request.Method = WebRequestMethods.Ftp.MakeDirectory;
                        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    }

                    foreach (var file in files)
                    {
                        //save file in attachment folder
                        if (file.Length > 0)
                        {
                            var extenstion = Path.GetExtension(file.FileName);
                            var myUniqueFileName = Guid.NewGuid().ToString();
                            dbFilePath = myUniqueFileName + extenstion;
                            dbPath = $"/Attachment/{branchDetails}/" + dbFilePath;
                            filePath = Path.Combine(pathWithBranchName, dbFilePath);
                            var fileStream = new FileStream(filePath, FileMode.Create);
                            file.CopyTo(fileStream);
                            fileStream.Close();
                        }
                    }
                }
                else
                {
                    foreach (var file in files)
                    {
                        //save file in attachment folder
                        if (file.Length > 0)
                        {
                            var extenstion = Path.GetExtension(file.FileName);
                            var myUniqueFileName = Guid.NewGuid().ToString();
                            dbFilePath = myUniqueFileName + extenstion;
                            dbPath = "/Attachment/" + dbFilePath;
                            filePath = Path.Combine(pathWithFolderName, dbFilePath);
                            var fileStream = new FileStream(filePath, FileMode.Create);
                            file.CopyTo(fileStream);
                            fileStream.Close();
                        }
                    }
                }



            }
            return Ok(dbPath);
        }

        [Route("api/Company/BusinessList")]
        [HttpGet("BusinessList")]
        [Authorize(Roles = "Admin")]
        public IActionResult BusinessList()
        {
            return Ok(_companyComponent.GetCompaniesList(Guid.Empty));
        }
        [Route("api/Company/BusinessDetails")]
        [HttpGet("BusinessDetails")]
        [Authorize(Roles = "Admin")]
        public IActionResult BusinessDetails(Guid id)
        {
            return Ok(_companyComponent.GetCompanyById(id));
        }

        [Route("api/Company/SaveBusiness")]
        [HttpPost("SaveBusiness")]
        public async Task<IActionResult> SaveBusiness([FromBody] BusinessVm business)
        {
            if (business.Id == string.Empty)
            {
                var clientParentId = _principal.Identity.CompanyId();
                var ParentId = _principal.Identity.NobelCompanyId();
                if (business.ClientParentId != Guid.Empty)
                {
                    clientParentId = business.ClientParentId;
                    ParentId = _principal.Identity.CompanyId();

                }
                var company = new Company
                {
                    NameArabic = business.NameInArabic,
                    NameEnglish = business.NameInEnglish,
                    AddressArabic = business.AddressInArabic,
                    AddressEnglish = business.AddressInEnglish,

                    CityArabic = business.CityInArabic,
                    CityEnglish = business.CityInEnglish,

                    CategoryInArabic = business.CategoryInArabic,
                    CategoryInEnglish = business.CategoryInEnglish,

                    CountryArabic = business.CountryInArabic,
                    CountryEnglish = business.CountryInEnglish,

                    PhoneNo = business.PhoneNumber,//FOR ENGLISH
                    Landline = business.LandLine,//FOR ARABIC
                    CompanyEmail = business.Email,
                    CreatedDate = DateTime.UtcNow,
                    Blocked = false,
                    ParentId = ParentId,
                    ClientParentId = clientParentId,
                };
                await _context.Companies.AddAsync(company);
                var register = new UserDetailDto()
                {
                    FirstName = business.FirstName,
                    LastName = business.LastName,
                    Password = business.Password,
                    Email = business.Email,
                    UserId = business.UserId,
                    PhoneNumber = business.PhoneNumber,
                };
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = register.UserId,
                        Email = register.Email,
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        CompanyId = company.Id
                    };
                    var result = await _userManager.CreateAsync(user, register.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, register.RoleName = "Admin");
                        var companyNobleRole = new NobleRoles()
                        {
                            Name = "Admin",
                            NormalizedName = "ADMIN",
                            IsActive = true
                        };
                        await _context.AddAsync(companyNobleRole);

                        var claims = new List<Claim>
                        {
                            new Claim("Email",user.Email),
                            new Claim("FullName",$"{user.FirstName}{user.LastName}"),
                            new Claim("Organization",company.NameEnglish),
                            new Claim("CompanyId",company.Id.ToString()),
                            new Claim("NobleCompanyId",_principal.Identity.NobelCompanyId().ToString()),
                            new Claim("ClientParentId",company.Id.ToString()),

                        };
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }
                        await Mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        });
                        await _userManager.AddClaimsAsync(user, claims);
                        await _context.SaveChangesAsync();
                        if (company.Id != Guid.Empty)
                        {
                            _context.SetModified(companyNobleRole, "CompanyId", company.Id);
                            _context.SaveChangesAfter();
                        }
                        return Ok(new { value = true, check = "Add" });
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                return null;
            }
            else
            {
                var company = new CompanyDto
                {

                    NameArabic = business.NameInArabic,
                    NameEnglish = business.NameInEnglish,

                    CityEnglish = business.CityInEnglish,
                    CityArabic = business.CityInArabic,
                    AddressArabic = business.AddressInArabic,
                    AddressEnglish = business.AddressInEnglish,

                    CountryEnglish = business.CountryInEnglish,
                    LandLine = business.LandLine,
                    CountryArabic = business.CountryInArabic,

                };
                _companyComponent.UpdateCompany(company);
                return Ok(new { value = true, check = "Update" });
            }
        }

        [Route("api/Company/SaveLocation")]
        [HttpPost("SaveLocation")]
        //[Authorize(Roles = "Admin, Admin,User, Noble Admin")]
        public async Task<IActionResult> SaveLocation([FromBody] BusinessVm business)
        {
            if (business.Id == string.Empty)
            {
                var clientParentId = _principal.Identity.ClientParentId();
                var ParentId = _principal.Identity.NobelCompanyId();
                var busId = _principal.Identity.CompanyId();
                if (business.ClientParentId != Guid.Empty)
                {
                    clientParentId = business.ClientParentId;
                    ParentId = _principal.Identity.CompanyId();
                    busId = business.BusinessParentId;

                }
                var company = new Company
                {
                    NameArabic = business.NameInArabic,
                    NameEnglish = business.NameInEnglish,

                    AddressArabic = business.AddressInArabic,
                    AddressEnglish = business.AddressInEnglish,

                    CityArabic = business.CityInArabic,
                    CityEnglish = business.CityInEnglish,

                    CountryArabic = business.CountryInArabic,
                    CountryEnglish = business.CountryInEnglish,

                    Landline = business.LandLine,
                    PhoneNo = business.PhoneNumber,

                    CompanyEmail = business.Email,
                    Website = business.Website,

                    CompanyRegNo = business.CompanyRegNo,
                    VatRegistrationNo = business.VatRegistrationNo,

                    CreatedDate = DateTime.UtcNow,

                    Blocked = false,
                    ParentId = ParentId,
                    ClientParentId = clientParentId,
                    BusinessParentId = busId,

                };
                await _context.Companies.AddAsync(company);

                var result12 = await Mediator.Send(new GetBranchCodeQuery
                    {BusinessId = busId});


                var branch = new Branch
                {
                    Code = result12,
                    BranchName = company.NameEnglish,
                    ContactNo = company.PhoneNo,
                    Address = company.AddressEnglish,
                    City = company.CityEnglish,
                    PostalCode = company.Postcode,
                    Country = company.CountryEnglish,
                    LocationId = company.Id,
                    BusinessId = busId,
                    MainBranch = true,
                    Date = DateTime.Now,

                };

                 _context.Branches.Add(branch);

                var register = new UserDetailDto()
                {
                    FirstName = business.FirstName,
                    LastName = business.LastName,
                    Password = business.Password,
                    Email = business.UserEmail,
                    UserId = business.FirstName,
                    PhoneNumber = "2323",
                };
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = register.UserId,
                        Email = register.Email,
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        CompanyId = company.Id,
                        PhoneNumber = register.PhoneNumber
                    };
                    var result = await _userManager.CreateAsync(user, register.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, register.RoleName = "User");
                        var companyNobleRole = new NobleRoles()
                        {
                            Name = "Admin",
                            NormalizedName = "ADMIN",
                            IsActive = true
                        };
                        await _context.AddAsync(companyNobleRole);
                        var claims = new List<Claim>
                        {
                            new Claim("Email",user.Email),
                            new Claim("FullName",$"{user.FirstName}{user.LastName}"),
                            new Claim("Organization",company.NameEnglish),
                            new Claim("CompanyId",company.Id.ToString()),
                            new Claim("NobleCompanyId",_principal.Identity.NobelCompanyId().ToString()),
                            new Claim("ClientParentId",company.Id.ToString()),

                        };
                        await _userManager.AddClaimsAsync(user, claims);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }
                        await Mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        });

                        await _context.SaveChangesAsync();
                        if (company.Id != Guid.Empty)
                        {
                            _context.SetModified(companyNobleRole, "CompanyId", company.Id);
                            _context.SaveChangesAfter();
                        }



                        return Ok(new { value = true, check = "Add" });


                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                return null;
            }
            else
            {
                var company = new CompanyDto
                {
                    Id = Guid.Parse(business.Id),
                    NameArabic = business.NameInArabic,
                    NameEnglish = business.NameInEnglish,

                    CityEnglish = business.CityInEnglish,
                    CityArabic = business.CityInArabic,
                    AddressArabic = business.AddressInArabic,
                    AddressEnglish = business.AddressInEnglish,

                    CountryEnglish = business.CountryInEnglish,
                    CountryArabic = business.CountryInArabic,
                    LandLine = business.LandLine,

                    LogoPath = business.LogoPath,
                    CategoryEnglish = business.CategoryInEnglish,
                    CategoryArabic = business.CategoryInArabic,
                    CompanyNameEnglish = business.CompanyNameEnglish,
                    CompanyNameArabic = business.CompanyNameArabic,

                    CompanyRegNo = business.CompanyRegNo,
                    VatRegistrationNo = business.VatRegistrationNo,

                };
                _companyComponent.UpdateCompany(company);

                if (business.AttachmentList != null)
                {
                    var attachments = _context.Attachments
                        .AsNoTracking()
                        .Where(x => x.DocumentId == company.Id)
                        .AsQueryable();

                    if (attachments.Any())
                    {
                        _context.Attachments.RemoveRange(attachments);
                    }
                    foreach (var item in business.AttachmentList)
                    {
                        var attachment = new Focus.Domain.Entities.Attachment()
                        {
                            Date = item.Date,
                            DocumentId = company.Id,
                            Description = item.Description,
                            FileName = item.FileName,
                            Path = item.Path
                        };
                        //Add Attachments to database
                        await _context.Attachments.AddAsync(attachment);

                    }
                }
                Random rnd = new Random();
                for (int i = 0; i < 11; i++)
                {
                    _code += rnd.Next(0, 9).ToString();
                }
                await Mediator.Send(new AddUpdateSyncRecordCommand()
                {
                    SyncRecordModels = _context.SyncLog(),
                    Code = _code,
                });
                await _context.SaveChangesAsync();
                return Ok(new { value = true, check = "Update" });
            }
        }

        [Route("api/Company/UpdateUser")]
        [HttpPost("UpdateUser")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateUser([FromBody] RegisterVm model)
        {
            var applicationUser = await _userManager.FindByIdAsync(model.Id);
            applicationUser.ImagePath = model.ImagePath;
            applicationUser.FirstName = model.FirstName;
            applicationUser.LastName = model.LastName;

            await _userManager.UpdateAsync(applicationUser);


            return Ok(new { value = true });
        }

        [Route("api/Company/ImageSearch")]
        [HttpGet("ImageSearch")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ImageSearch(string id)
        {
            var applicationUser = await _userManager.FindByIdAsync(id);
            return Ok(applicationUser);
        }

        [Route("api/Company/LocationList")]
        [HttpGet("LocationList")]
        [Authorize(Roles = "Admin")]
        public IActionResult LocationList()
        {
            return Ok(_companyComponent.GetCompaniesList(Guid.Empty));
        }
        [Route("api/Company/LocationDetails")]
        [HttpGet("LocationDetails")]
        public IActionResult LocationDetails()
        {
            Guid companyId = _principal.Identity.CompanyId();
            return Ok(_companyComponent.GetCompanyById(companyId));
        }

        [Route("api/Company/SaveCompanyInformation")]
        [HttpPost("SaveCompanyInformation")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SaveCompanyInformation([FromBody] CompanyInformationVm companyInformationVm)
        {
            var id = Guid.Empty;
            if (companyInformationVm.Id == Guid.Empty)
            {
                id = await Mediator.Send(new AddUpdateAdditionalCompanyCommand
                {
                    Id = new Guid(),
                    NameArabic = companyInformationVm.NameArabic,
                    CommercialRegNo = companyInformationVm.CommercialRegNo,
                    VatRegistrationNo = companyInformationVm.VatRegistrationNo,
                    CityArabic = companyInformationVm.CityArabic,
                    CountryArabic = companyInformationVm.CountryArabic,
                    Mobile = companyInformationVm.Mobile,
                    PhoneNo = companyInformationVm.PhoneNo,
                    Email = companyInformationVm.Email,
                    Website = companyInformationVm.Website,
                    AddressArabic = companyInformationVm.AddressArabic
                });
            }
            //else
            //{
            //    id = await Mediator.Send(new AddUpdateAdditionalCompanyCommand
            //    {
            //        Id = companyInformationVm.Id,
            //        NameArabic = companyInformationVm.NameArabic,
            //        CommercialRegNo = companyInformationVm.CommercialRegNo,
            //        VatRegistrationNo = companyInformationVm.VatRegistrationNo,
            //        CityArabic = companyInformationVm.CityArabic,
            //        CountryArabic = companyInformationVm.CountryArabic,
            //        Mobile = companyInformationVm.Mobile,
            //        PhoneNo = companyInformationVm.PhoneNo,
            //        Email = companyInformationVm.Email,
            //        Website = companyInformationVm.Website,
            //        AddressArabic = companyInformationVm.AddressArabic,
            //        CreatedDate = companyInformationVm.CreatedDate,
            //    });
            //}
            //if (id != Guid.Empty)
            //{
            //    var department = await Mediator.Send(new GetAdditionalCompanyQuery
            //    {
            //        Id = id
            //    });
            //    return Json(new { IsSuccess = true, department = department });
            //}
            return Ok(new { IsSuccess = true, Id = id });
        }

        [Route("api/Company/SaveCompanyAttachment")]
        [HttpPost("SaveCompanyAttachment")]
        [Authorize(Roles = "User, Admin, Super Admin")]
        public async Task<IActionResult> SaveCompanyAttachment([FromBody] CompanyAttachmentLookupModel companyAttachment)
        {
            var id = Guid.Empty;
            if (companyAttachment.CompanyId != Guid.Empty)
            {
                id = await Mediator.Send(new AddUpdateCompanyAttachmentCommand
                {
                    Attachment = companyAttachment
                });
            }

            return Ok(new { IsSuccess = true, Id = id });
        }

        [Route("api/Company/GetCompanyInformation")]
        [HttpGet("GetCompanyInformation")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCompanyInformation()
        {
            Guid companyId = _principal.Identity.CompanyId();
            var companyListModel = await Mediator.Send(new GetCompanyInformationListQuery { CompanyId = companyId });
            return Ok(companyListModel);
        }

        [Route("api/Company/GetCompanyAttachments")]
        [HttpGet("GetCompanyAttachments")]
        [Authorize(Roles = "User, Admin, Super Admin")]
        public async Task<IActionResult> GetCompanyAttachments(string searchTerm, int? pageNumber)
        {
            var companyId = _principal.Identity.CompanyId();
            var companyListModel = await Mediator.Send(
                new GetCompanyAttachmentsQuery
                {
                    CompanyId = companyId,
                    SearchTerm = searchTerm,
                    PageNumber = pageNumber ?? 1
                });
            return Ok(companyListModel);
        }

        [Route("api/Company/CountWarehouseRows")]
        [HttpGet("CountWarehouseRows")]
        [Roles("CanViewWareHouse", "Noble Admin")]
        public async Task<IActionResult> CountWarehouseRows()
        {
            var warehouseListModel = await Mediator.Send(new GetWarehouseListQuery());
            return Ok(warehouseListModel);
        }
        [Route("api/Company/GetWarehouseInformation")]
        [HttpGet("GetWarehouseInformation")]
        [Roles("CreditPurchase", "CashPurchase", "CashInvoices", "CreditInvoices", "TouchInvoiceTemplate1", "TouchInvoiceTemplate2", "TouchInvoiceTemplate3", "CanPurchaseInvoiceCosting", "CanAddPurchaseReturn", "CanViewDetailPurchaseReturn", "CanViewProductInventoryReport", "CanPrintProductInventoryReport", "CanAddProductionBatch", "CanFilterItem", "CanViewWareHouse", "CanAddStockIn", "CanEditStockIn", "CanDraftStockIn", "CanImportStockIn", "CanAddStockOut", "CanEditStockOut", "CanDraftStockOut", "CanAddStockTransfer", "CanEditStockTransfer", "CanDraftStockTransfer", "Noble Admin", "CanHoldServiceInvoices", "CashServiceInvoices", "CreditServiceInvoices", "CanAddServiceSaleOrder", "CanDraftServiceSaleOrder", "CanEditServiceSaleOrder", "CanDuplicateServiceSaleOrder", "CanAddAutoTemplate", "CanEditAutoTemplate", "CanAddInventoryCount", "CanEditInventoryCount", "Simple")]
        public async Task<IActionResult> GetWarehouseInformation(bool isDropdown, string searchTerm, Guid companyId)
        {
            var warehouseListModel = await Mediator.Send(new GetWarehouseListQuery()
            {
                CompanyId = companyId,
                SearchTerm = searchTerm,
                IsDropdown = isDropdown
            });
            return Ok(warehouseListModel);
        }
        [Route("api/Company/SaveWarehouseInformation")]
        [HttpPost("SaveWarehouseInformation")]
        [Roles("CanAddWareHouse", "Noble Admin", "CanEditWareHouse")]
        public async Task<IActionResult> SaveWarehouseInformation([FromBody] WarehouseVM warehouseInformation, Guid companyId)
        {
            if (warehouseInformation.Id == Guid.Empty)
            {
                var message = await Mediator.Send(new AddUpdateWarehouseCommands
                {
                    Id = new Guid(),
                    StoreID = warehouseInformation.StoreID,
                    Name = warehouseInformation.Name,
                    NameArabic = warehouseInformation.NameArabic,
                    Address = warehouseInformation.Address,
                    City = warehouseInformation.City,
                    Country = warehouseInformation.Country,
                    ContactNo = warehouseInformation.ContactNo,
                    Email = warehouseInformation.Email,
                    LicenseNo = warehouseInformation.LicenseNo,
                    LicenseExpiry = warehouseInformation.LicenseExpiry,
                    CivilDefenceLicenseNo = warehouseInformation.CivilDefenceLicenseNo,
                    CivilDefenceLicenseExpiry = warehouseInformation.CivilDefenceLicenseExpiry,
                    CCTVLicenseNo = warehouseInformation.CCTVLicenseNo,
                    CCTVLicenseExpiry = warehouseInformation.CCTVLicenseExpiry,
                    isActive = warehouseInformation.IsActive,
                    CompanyId = companyId
                });
                if (message.Id != Guid.Empty)
                {
                    return Ok(new { message = message, Action = "Add" });

                }
                else
                {
                    return Ok(new { Message = message, Action = "Error" });
                }
            }
            else
            {
                var message = await Mediator.Send(new AddUpdateWarehouseCommands
                {
                    Id = warehouseInformation.Id,
                    StoreID = warehouseInformation.StoreID,
                    Name = warehouseInformation.Name,
                    NameArabic = warehouseInformation.NameArabic,
                    Address = warehouseInformation.Address,
                    City = warehouseInformation.City,
                    Country = warehouseInformation.Country,
                    ContactNo = warehouseInformation.ContactNo,
                    Email = warehouseInformation.Email,
                    LicenseNo = warehouseInformation.LicenseNo,
                    LicenseExpiry = warehouseInformation.LicenseExpiry,
                    CivilDefenceLicenseNo = warehouseInformation.CivilDefenceLicenseNo,
                    CivilDefenceLicenseExpiry = warehouseInformation.CivilDefenceLicenseExpiry,
                    CCTVLicenseNo = warehouseInformation.CCTVLicenseNo,
                    CCTVLicenseExpiry = warehouseInformation.CCTVLicenseExpiry,
                    isActive = warehouseInformation.IsActive,
                    CompanyId = companyId
                });
                if (message.Id != Guid.Empty)
                {
                    return Ok(new { message = message, Action = "Update" });

                }
                else
                {
                    return Ok(new { Message = message, Action = "Error" });
                }
            }

        }
        [Route("api/Company/DeleteWarehouse")]
        [HttpGet("DeleteWarehouse")]
        [Roles("CanDeleteWareHouse", "Noble Admin")]
        public async Task<IActionResult> DeleteWarehouse(Guid Id)
        {
            var warehouseId = await Mediator.Send(new DeleteWarehouseCommand
            {
                Id = Id
            });
            return Ok(warehouseId);

        }





        [Route("api/Company/WarehouseAutoGenerateCode")]
        [HttpGet("WarehouseAutoGenerateCode")]
        [Roles("CanAddWareHouse", "Noble Admin", "CanEditWareHouse")]
        public async Task<IActionResult> WarehouseAutoGenerateCode()
        {
            var autoNo = await Mediator.Send(new GetWarehouseCodeQuery());
            return Ok(autoNo);
        }
        [Route("api/Company/WarehouseDetailsViaId")]
        [HttpGet("WarehouseDetailsViaId")]
        [Roles("CanEditWareHouse", "Noble Admin")]
        public async Task<IActionResult> WarehouseDetailsViaId(Guid Id, Guid companyId)
        {
            var contact = await Mediator.Send(new GetWarehousesQuery()
            {
                CompanyId = companyId,
                Id = Id
            });
            return Ok(contact);

        }


        [DisplayName("Terminal Code")]
        [Route("api/Company/TerminalCode")]
        [HttpGet("TerminalCode")]
        [Roles("CanAddTerminal", "Noble Admin")]
        public async Task<IActionResult> TerminalCode(TerminalType terminalType)
        {
            var autoNo = await Mediator.Send(new GetTerminalCodeQuery()
            {
                TerminalType = terminalType
            });

            return Ok(autoNo);
        }

        [Route("api/Company/SaveTerminal")]
        [HttpPost("SaveTerminal")]
        [Roles("CanAddTerminal", "CanEditTerminal", "Noble Admin")]
        public async Task<IActionResult> SaveTerminal([FromBody] TerminalVm terminal)
        {
            var id = Guid.Empty;
            if (terminal.Id == Guid.Empty)
            {
                id = await Mediator.Send(new AddUpdateTerminalCommand
                {
                    Id = new Guid(),


                    Code = terminal.Code,
                    MACAddress = terminal.MACAddress,
                    IPAddress = terminal.IPAddress,
                    isActive = terminal.isActive,
                    OverWrite = terminal.OverWrite,
                    AccountId = terminal.AccountId,
                    CashAccountId = terminal.CashAccountId,
                    PosTerminalId = terminal.PosTerminalId,
                    PrinterName = terminal.PrinterName,
                    TerminalType = terminal.TerminalType,
                    OnPageLoadItem = terminal.OnPageLoadItem,
                    CategoryId = terminal.CategoryId,
                    CompanyId = terminal.CompanyId,
                    CompanyNameEnglish = terminal.CompanyNameEnglish,
                    BusinessNameEnglish = terminal.BusinessNameEnglish,
                    BusinessTypeEnglish = terminal.BusinessTypeEnglish,
                    CompanyNameArabic = terminal.CompanyNameArabic,
                    BusinessNameArabic = terminal.BusinessNameArabic,
                    BusinessTypeArabic = terminal.BusinessTypeArabic,
                    BusinessLogo = terminal.BusinessLogo,
                    TerminalUserType = (TerminalUserType)Enum.Parse(typeof(TerminalUserType), terminal.TerminalUserType)

                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateTerminalCommand
                {
                    Id = terminal.Id,
                    Code = terminal.Code,
                    OverWrite = terminal.OverWrite,
                    MACAddress = terminal.MACAddress,
                    IPAddress = terminal.IPAddress,
                    isActive = terminal.isActive,
                    AccountId = terminal.AccountId,
                    CashAccountId = terminal.CashAccountId,
                    PosTerminalId = terminal.PosTerminalId,
                    PrinterName = terminal.PrinterName,
                    OnPageLoadItem = terminal.OnPageLoadItem,
                    CategoryId = terminal.CategoryId,
                    TerminalType = terminal.TerminalType,
                    CompanyId = terminal.CompanyId,
                    CompanyNameEnglish = terminal.CompanyNameEnglish,
                    BusinessNameEnglish = terminal.BusinessNameEnglish,
                    BusinessTypeEnglish = terminal.BusinessTypeEnglish,
                    CompanyNameArabic = terminal.CompanyNameArabic,
                    BusinessNameArabic = terminal.BusinessNameArabic,
                    BusinessTypeArabic = terminal.BusinessTypeArabic,
                    BusinessLogo = terminal.BusinessLogo,
                    TerminalUserType = (TerminalUserType)Enum.Parse(typeof(TerminalUserType), terminal.TerminalUserType)
                });
            }
            if (id != Guid.Empty)
            {
                var terminals = await Mediator.Send(new GetTerminalDetailQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, terminals = terminals });
            }

            return Ok(new { IsSuccess = false });



        }
        [Route("api/Company/TerminalList")]
        [HttpGet("TerminalList")]
        //[Roles("CanViewTerminal", "CanEditSignUpUser", "CanAddSignUpUser")]
        public async Task<IActionResult> TerminalList(bool isActive, bool isSelected, bool editSelected, TerminalType terminalType, bool allShown, Guid? companyId)
        {
            var terminal = await Mediator.Send(new GetTerminalListQuery()
            {
                IsActive = isActive,
                IsSelected = isSelected,
                AllShown = allShown,
                EditSelected = editSelected,
                TerminalType = terminalType,
                CompanyId = companyId
            });
            return Ok(terminal);
        }
        [Route("api/Company/TerminalDelete")]
        [HttpGet("TerminalDelete")]
        [Roles("Can Delete Terminals", "Noble Admin")]
        public async Task<IActionResult> TerminalDelete(Guid id)
        {
            var terminalId = await Mediator.Send(new DeleteTerminalCommand
            {
                Id = id
            });
            return Ok(terminalId);

        }
        [Route("api/Company/TerminalDetail")]
        [HttpGet("TerminalDetail")]
       // [Roles("CanEditTerminal", "Noble Admin")]
        public async Task<IActionResult> TerminalDetail(Guid Id, Guid? companyId)
        {
            var terminal = await Mediator.Send(new GetTerminalDetailQuery()
            {
                Id = Id,
                CompanyId = companyId
            });
            return Ok(terminal);

        }
        [Route("api/Company/EditDailyExpenseDetail")]
        [HttpGet("EditDailyExpenseDetail")]
        [Roles("CanAddExpense", "CanEditExpense", "CanDraftExpense")]
        public async Task<IActionResult> DailyExpenseDetail(Guid Id)
        {
            var terminal = await Mediator.Send(new GetDailyExpenseDetailQuery()
            {
                Id = Id
            });
            return Ok(terminal);
        }
        [DisplayName("Auto Code Generate")]
        [Route("api/Company/AutoGenerateCode")]
        [HttpGet("AutoGenerateCode")]
        [Roles("CanAddExpense", "CanEditExpense", "CanDraftExpense", "CanViewDetailExpense", "CanPrintExpense", "CanPayExpenseBill")]
        public async Task<IActionResult> AutoGenerateCode(Guid? branchId)
        {
            var jvNumber = await Mediator.Send(new GetDailyExpenseNumberQuery()
            {
                BranchId= branchId,
                ApprovalStatus = ApprovalStatus.Approved
            });

            return Ok(jvNumber);
        }
        [Route("api/Company/SaveDailyExpense")]
        [HttpPost("SaveDailyExpense")]
        [Roles("CanAddExpense", "CanEditExpense", "CanDraftExpense", "CanPayExpenseBill")]
        public async Task<IActionResult> SaveDailyExpense([FromBody] DailyExpenseVm dailyExpense)
        {
            //var user = await _userManager.FindByIdAsync(_contextProvider.GetUserId().ToString());
            //var company = _context.Companies.FirstOrDefault(x => x.Id == user.CompanyId);

            //if (company != null && company.DayStart)
            //{
            //    if (!DayStart())
            //        throw new ApplicationException("Please start day first.");
            //}

            await Mediator.Send(new AddUpdateDailyExpenseCommand
            {
                ExpenseVm = dailyExpense
            });
            return Ok(new { IsSuccess = false });
        }

        private bool DayStart()
        {
            var dayId = User.Claims.FirstOrDefault(x => x.Type == "DayId");

            if (dayId.Value == "00000000-0000-0000-0000-000000000000")
            {
                return false;
            }

            return true;
        }

        [Route("api/Company/DailyExpenseList")]
        [HttpGet("DailyExpenseList")]
        [Roles("CanViewExpense", "CanDraftExpense")]
        public async Task<IActionResult> DailyExpenseList(ApprovalStatus status, string searchTerm, int? pageNumber, DateTime? fromDate, DateTime? toDate, string paymentMode, string referenceNo, decimal? totalAmount, Guid? branchId)
        {
            //var users = await _userManager.FindByIdAsync(_contextProvider.GetUserId().ToString());
            //var company = _Context.Companies.FirstOrDefault(x => x.Id == users.CompanyId);
            //if (company != null && company.DayStart)
            //{
            //    var user = await _Context.LoginPermissions.FirstOrDefaultAsync(x => x.UserId == Guid.Parse(users.Id));
            //    if (!user.AllowAll)
            //    {
            //        if (!DayStart())
            //            throw new ApplicationException("Please start day first.");
            //    }
            //}
            var dailyExpense = await Mediator.Send(new GetDailyExpenseListQuery
            {
                SearchTerm = searchTerm,
                Status = status,
                PageNumber = pageNumber ?? 1,
                FromDate = fromDate,
                ToDate = toDate,
                PaymentMode = paymentMode,
                ReferenceNo = referenceNo,
                TotalAmount = totalAmount,
                BranchId = branchId,
            });
            return Ok(dailyExpense);

        }
        [Route("api/Company/DailyExpenseDelete")]
        [HttpGet("DailyExpenseDelete")]
        [Roles("CanDeleteExpense")]
        public async Task<IActionResult> DailyExpenseDelete(Guid id)
        {
            if (!DayStart())
                throw new ApplicationException("Please start day first.");

            var dailyExpenseId = await Mediator.Send(new DeleteDailyExpensesCommand
            {
                Id = id
            });
            return Ok(dailyExpenseId);

        }
        [Route("api/Company/UpdateApprovalStatus")]
        [HttpPost("UpdateApprovalStatus")]
        [Roles("Noble Admin", "Can Edit Expense as Draft", "Can Save Expense as Draft", "Can Save Expense as Post", "Can Edit Expense as Post", "Can Save Expense as Reject", "Can Edit Expense as Reject")]
        public async Task<IActionResult> UpdateApprovalStatus([FromBody] UpdateApprovalStatusVm approvalStatusVm)
        {
            var message = await Mediator.Send(new UpdateBulkApproval()
            {
                SelectedId = approvalStatusVm.SelectedId,
                Action = approvalStatusVm.Action,
                AccountId = approvalStatusVm.AccountId,
                Reason = approvalStatusVm.Reason,
                PaymentType = approvalStatusVm.PaymentType,
                //Total =approvalStatusVm.Total
            });
            return Ok(message);
        }
        [Route("api/Company/SaveMonthlyCost")]
        [HttpPost("SaveMonthlyCost")]
        [Roles("CanAddMonthlyCost")]
        public async Task<IActionResult> SaveMonthlyCost([FromBody] MonthlyCostLookUpModel monthlyCost)
        {
            var id = Guid.Empty;
            if (monthlyCost.Id == Guid.Empty)
            {
                var message = await Mediator.Send(new AddUpdateMonthlyCostCommand()
                {
                    monthlyCost = monthlyCost,

                });

                return Ok(new { IsSuccess = true });
            }

            else
            {
                var message = await Mediator.Send(new AddUpdateMonthlyCostCommand()
                {
                    monthlyCost = monthlyCost,

                });
                return Ok(new { IsSuccess = true });
            }
        }

        [Route("api/Company/MonthlyCostDetail")]
        [HttpGet("MonthlyCostDetail")]
        [Roles("CanAddMonthlyCost")]
        public async Task<IActionResult> MonthlyCostDetail()
        {
            var purchase = await Mediator.Send(new MonthlyCostDetailQuery());

            return Ok(purchase);
        }
        [Route("api/Company/GetMacAddress")]
        [HttpGet("GetMacAddress")]
        [Roles("CanAddTerminal", "CanEditTerminal", "Noble Admin")]
        public async Task<IActionResult> GetMacAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            String sIpAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card
                {
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                    break;
                }
            }
            string strHostName = Dns.GetHostName(); ;
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            Console.WriteLine(addr[addr.Length - 1].ToString());
            if (addr[0].AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            {
                sIpAddress = addr[0].ToString();

            }

            return Ok(new { macAddress = sMacAddress, ipAddress = sIpAddress });
        }

        #endregion
        #region Dashboard

        [Route("api/Company/GetTransaction")]
        [HttpGet("GetTransaction")]
        public async Task<IActionResult> GetTransaction(DateTime? currentDate, DateTime? selectedDate, string overViewFilter)
        {
            var Cash = await Mediator.Send(new GetCashTransactionQuery
            {
                OverViewFilter = overViewFilter,
                CurrentDate = currentDate,
                SelectedDate = selectedDate
            });
            return Ok(Cash);
        }

        [Route("api/Company/GetTransaction")]
        [HttpGet("GetAccountTransaction")]
        public async Task<IActionResult> GetAccountTransaction(string overViewFilter)
        {
            var Cash = await Mediator.Send(new AccountDashboardQuery
            {
                OverViewFilter = overViewFilter,
            });
            return Ok(Cash);
        }

        [Route("api/Company/GetEmployeeRecord")]
        [HttpGet("GetEmployeeRecord")]
        public async Task<IActionResult> GetEmployeeRecord(string overViewFilter)
        {
            var Cash = await Mediator.Send(new EmployeeDashboardQuery
            {
                OverViewFilter = overViewFilter,
            });
            return Ok(Cash);
        }


        [Route("api/Company/InventoryDashboardQuery")]
        [HttpGet("InventoryDashboardQuery")]
        public async Task<IActionResult> InventoryDashboardQuery(string overViewFilter)
        {
            var Cash = await Mediator.Send(new InventoryDashboardQuery
            {
                OverViewFilter = overViewFilter,
            });
            return Ok(Cash);
        }

        [Route("api/Company/CashAndBankDashboardQuery")]
        [HttpGet("CashAndBankDashboardQuery")]
        public async Task<IActionResult> CashAndBankDashboardQuery(string overViewFilter)
        {
            var Cash = await Mediator.Send(new CashAndBankQuery
            {
                OverViewFilter = overViewFilter,
            });
            return Ok(Cash);
        }






        [Route("api/Company/GetInventory")]
        [HttpGet("GetInventory")]
        [Roles("CanAddProductionRecipe")]
        public async Task<IActionResult> GetInventory(DateTime searchTerm, int? pageNumber, Guid productId)
        {
            var inventory = await Mediator.Send(new GetInventoryListQuery()
            {
                SearchTerm = searchTerm,
                isDashboard = true,
                productId = productId,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(inventory);
        }

        //[Route("api/Company/SaleList")]
        //[HttpGet("SaleList")]
        //[Authorize(Roles = "User")]
        //public async Task<IActionResult> SaleList(InvoiceType status, DateTime search, int? pageNumber)
        //{
        //    var purchaseOrder = await Mediator.Send(new GetSaleListQuery
        //    {
        //        SearchDate = search,
        //        PageNumber = pageNumber ?? 1,
        //        Satus = status,
        //        isSaleFilter = true
        //    }); ;

        //    return Ok(purchaseOrder);
        //}

        [Route("api/Company/GetStockList")]
        [HttpGet("GetStockList")]

        public async Task<IActionResult> GetStockList(DateTime fromDate, DateTime toDate, Guid warehouseId, string search)
        {
            var inventory = await Mediator.Send(new GetStockListQuery()
            {
                FromDate = fromDate,
                ToDate = toDate,
                WarehouseId = warehouseId,
                Search = search,
            });
            return Ok(inventory);
        }

        [Route("api/Company/GetInventoryList")]
        [HttpGet("GetInventoryList")]
        [Roles("CanViewInventoryReport", "CanPrintInventoryReport", "CanViewStockValueReport", "CanPrintStockValueReport", "CanViewStockAverageValue", "CanPrintStockAverageValue")]

        public async Task<IActionResult> GetInventoryList(DateTime fromDate, DateTime toDate, int? pageNumber, Guid productId, int transactionType, bool isInventory, bool isProductFilter, bool isPSValue, bool isPASV, bool isTTSV, bool isCWProducts, bool isCustomersWP, Guid warehouseId, Guid customerId, bool isSWProducts, bool isSuppliersWP, bool isSuppliersDiscount, bool isCustomersDiscount, bool isDiscount, bool isProductCustomersDiscount, bool isProductSuppliersDiscount, bool focPurchase, bool focSale, Guid supplierId, bool isStock, string search)
        {
            var inventory = await Mediator.Send(new GetInventoryListQuery()
            {
                FromDate = fromDate,
                ToDate = toDate,
                IsStock = isStock,
                isInventory = isInventory,
                isProductFilter = isProductFilter,
                isProductStockValue = isPSValue,
                isProductAverageStockValue = isPASV,
                isTransactionTypeStockValue = isTTSV,
                isCustomerWiseProducts = isCWProducts,
                isCustomersWiseProduct = isCustomersWP,
                isSupplierWiseProducts = isSWProducts,
                isSuppliersWiseProduct = isSuppliersWP,
                isCustomersDiscount = isCustomersDiscount,
                isSuppliersDiscount = isSuppliersDiscount,
                isProductCustomersDiscount = isProductCustomersDiscount,
                isProductSuppliersDiscount = isProductSuppliersDiscount,
                isfocPurchase = focPurchase,
                isfocSale = focSale,
                isDiscount = isDiscount,
                productId = productId,
                WarehouseId = warehouseId,
                CustomerId = customerId,
                SupplierId = supplierId,
                TransactionType = transactionType,
                PageNumber = pageNumber ?? 1,
                Search = search,

            });
            return Ok(inventory);
        }

        [Route("api/Company/GetRetailList")]
        [HttpGet("GetRetailList")]
        public async Task<IActionResult> GetRetailList(DateTime fromDate, DateTime toDate)
        {
            var retail = await Mediator.Send(new GetRetailReportQuery()
            {
                FromDate = fromDate,
                ToDate = toDate,
            });
            return Ok(retail);
        }
        [Route("api/Company/GetLedgerList")]
        [HttpGet("GetLedgerList")]
        [Roles("Noble Admin", "CanViewAccountLedger", "CanPrintAccountLedger")]
        public async Task<IActionResult> GetLedgerList(DateTime fromDate, DateTime toDate, int? pageNumber, bool isLedger, Guid accountId, string dateType)
        {
            var ledger = await Mediator.Send(new GetTransactionQueryForLedger()
            {
                FromDate = fromDate,
                DateType = dateType,
                ToDate = toDate,
                isLedger = isLedger,
                AccountID = accountId,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(ledger);
        } 
        [Route("api/Company/GetAdvanceAccountLedger")]
        [HttpGet("GetAdvanceAccountLedger")]
        public async Task<IActionResult> GetAdvanceAccountLedger(DateTime fromDate, DateTime toDate, int? pageNumber, bool isLedger, Guid accountId, string dateType, string compareWith, string numberOfPeriods,string branchId)
        {
            var ledger = await Mediator.Send(new GetAdvanceAccountLedgerQuery()
            {
                FromDate = fromDate,
                DateType = dateType,
                ToDate = toDate,
                isLedger = isLedger,
                AccountID = accountId,
                CompareWith = compareWith,
                NumberOfPeriods = numberOfPeriods,
                BranchId = branchId,

            });
            return Ok(ledger);
        }
        [Route("api/Company/GetCostCenterWiseLedgerLedgerList")]
        [HttpPost("GetCostCenterWiseLedgerLedgerList")]
        [Roles("Noble Admin", "CanViewAccountLedger", "CanPrintAccountLedger")]
        public async Task<IActionResult> GetCostCenterWiseLedgerLedgerList([FromBody] List<TrialBalanceModel> accounts, DateTime fromDate, DateTime toDate, int? pageNumber, bool isLedger, string dateType, string branchId)
        {
            var ledger = await Mediator.Send(new TransactionQueryofLedgerAccountWise()
            {
                FromDate = fromDate,
                DateType = dateType,
                ToDate = toDate,
                AccountsId = accounts.Select(x => x.Id).ToList(),
                isLedger = isLedger,
                PageNumber = pageNumber ?? 1,
                BranchId = branchId,
            });
            return Ok(ledger);
        }
        [Route("api/Company/GetAdvanceAccountLedgerDetails")]
        [HttpPost("GetAdvanceAccountLedgerDetails")]
        [Roles("Noble Admin", "CanViewAccountLedger", "CanPrintAccountLedger")]
        public async Task<IActionResult> GetAdvanceAccountLedgerDetails([FromBody] List<TrialBalanceModel> accounts, DateTime fromDate, DateTime toDate, int? pageNumber, bool isLedger, string dateType, string compareWith, string numberOfPeriods)
        {
            var ledger = await Mediator.Send(new GetAdvanceAccountLedgerDetailsQuery()
            {
                FromDate = fromDate,
                DateType = dateType,
                ToDate = toDate,
                AccountsId = accounts.Select(x => x.Id).ToList(),
                isLedger = isLedger,
                CompareWith = compareWith,
                NumberOfPeriods = numberOfPeriods
            });
            return Ok(ledger);
        }
        #endregion
        #region For Noble Roles
        [Route("api/Company/SaveNobleRoles")]
        [HttpPost("SaveNobleRoles")]
        [Roles("User", "Super Admin", "Admin", "Supervisor", "Noble Admin")]
        public async Task<IActionResult> SaveNobleRoles([FromBody] NobleRoleVm nobleRoleVm)
        {
            Guid id;
            if (nobleRoleVm.Id == Guid.Empty)
            {
                id = await Mediator.Send(new AddUpdateNobleRoleCommand
                {
                    Id = new Guid(),
                    Name = nobleRoleVm.Name,
                    NameArabic = nobleRoleVm.NameArabic,
                    NormalizedName = nobleRoleVm.NormalizedName.ToUpper(),
                    isActive = nobleRoleVm.isActive
                });

            }
            else
            {
                id = await Mediator.Send(new AddUpdateNobleRoleCommand
                {
                    Id = nobleRoleVm.Id,
                    Name = nobleRoleVm.Name,
                    NameArabic = nobleRoleVm.NameArabic,
                    NormalizedName = nobleRoleVm.NormalizedName.ToUpper(),
                    isActive = nobleRoleVm.isActive
                });
            }
            if (id != Guid.Empty)
            {
                var nobleRole = await Mediator.Send(new GetNobleRoleDetailQuery()
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, Roles = nobleRole });
            }
            return Ok(new { IsSuccess = false });
        }

        [Route("api/Company/NobleRolesList")]
        [HttpGet("NobleRolesList")]
        [Roles("User", "Super Admin", "Admin", "Supervisor", "Noble Admin")]
        public async Task<IActionResult> NobleRolesList(bool isActive)
        {
            var nobleRole = await Mediator.Send(new GetNobleRoleListQuery { isActive = isActive });
            return Ok(nobleRole);
        }

        [Route("api/Company/NobleRolesDetail")]
        [HttpGet("NobleRolesDetail")]
        [Roles("User", "Super Admin", "Admin", "Supervisor", "Noble Admin")]
        public async Task<IActionResult> NobleRolesDetail(Guid Id, Guid companyId)
        {
            var nobleRole = await Mediator.Send(new GetNobleRoleDetailQuery()
            {
                Id = Id,
                CompanyId = companyId
            });
            return Ok(nobleRole);
        }

        [Route("api/Company/SaveRolePermissions")]
        [HttpPost("SaveRolePermissions")]
        [Roles("User", "Super Admin", "Admin", "Supervisor", "Noble Admin")]
        public async Task<IActionResult> SaveRolePermissions([FromBody] List<RolesPermissionsLookUpModel> rolePermissionsVm)
        {
            string allowAll = "";
            Guid roleId = Guid.Empty;
            var nobleRolePermission = new List<NobleRolePermission>();
            foreach (var item in rolePermissionsVm)
            {
                var pbItem = new NobleRolePermission()
                {
                    Id = item.PermissionId,
                    IsActive = item.IsActive,
                    RoleId = item.RoleId
                };
                roleId = item.RoleId;
                allowAll = item.AllowAll;
                nobleRolePermission.Add(pbItem);
            }
            var message = await Mediator.Send(new AddUpdateRolesPermissionsCommand
            {
                RoleId = roleId,
                AllowAll = allowAll,
                RolePermissions = nobleRolePermission
            });
            return Ok(new { Message = message, type = "Update" });
        }



        [Route("api/Company/DefaultRolesOnLocation")]
        [HttpGet("DefaultRolesOnLocation")]
        //[Roles("User", "Super Admin", "Admin", "Supervisor", "Noble Admin")]
        public async Task<IActionResult> DefaultRolesOnLocation(Guid companyId)
        {

            var nobleRole = await Mediator.Send(new CreateDefaultRolesOnLocation()
            {
                CompanyId = companyId
            });
            var currentUserId = User.Identity.UserId();
            var user = await _userManager.FindByIdAsync(currentUserId);

            //var autoNo = await Mediator.Send(new GetEmployeeRegistrationCodeQuery());
            //var id = await Mediator.Send(new AddUpdateEmployeeRegistrationCommand
            //{
            //    Id = new Guid(),
            //    Code = autoNo,
            //    Email = user.Email,
            //    EnglishName = user.FirstName,
            //    ArabicName = user.FirstName,
            //    RegistrationDate = DateTime.UtcNow,
            //    Gender = "Male",
            //    IDNumber = ""
            //});

            var terminal = _context.Terminals.AsNoTracking().FirstOrDefaultAsync();
            if (terminal == null)
            {
                throw new NotFoundException("Terminal not Found", null);
            }
            //user.EmployeeId = id;
            user.TerminalId = terminal.Result.Id;
            await _userManager.UpdateAsync(user);


            await Mediator.Send(new DefaultProcessList());

            var loginPermission = new LoginPermissions()
            {

                UserId = Guid.Parse(user.Id),
                ChangePriceDuringSale = true,
                GiveDicountDuringSale = true,
                ViewCounterDetails = true,
                TransferCounter = true,
                CloseCounter = true,
                HoldCounter = true,
                CloseDay = true,
                StartDay = true,
                IsSupervisor = true,
                ProcessSaleReturn = true,
                DailyExpenseList = true,
                IsTouchInvoice = true,
                AllowViewAllData = true,
            };
            await _context.AddAsync(loginPermission);
            //Save changes to database

            Random rnd = new Random();
            for (int i = 0; i < 11; i++)
            {
                _code += rnd.Next(0, 9).ToString();
            }
            await Mediator.Send(new AddUpdateSyncRecordCommand()
            {
                SyncRecordModels = _context.SyncLog(),
                Code = _code,
            });
            await _context.SaveChangesAsync();
            return Ok(new { /*EmployeeId = id,*/ nobleRole = nobleRole });
        }




        [Route("api/Company/SaveNobleRolePermissions")]
        [HttpPost("SaveNobleRolePermissions")]
        [Roles("User", "Super Admin", "Admin", "Supervisor", "Noble Admin")]
        public async Task<IActionResult> SaveNobleRolePermissions([FromBody] List<NobleRolesPermissionsLookUpModel> rolePermissionsVm)
        {
            string allowAll = "";
            bool isNoble = false;
            Guid roleId = Guid.Empty;
            Guid companyId = Guid.Empty;
            var nobleRolePermission = new List<NobleRolePermission>();
            var companyPermissions = new List<CompanyPermission>();

            foreach (var item in rolePermissionsVm)
            {
                {

                    if (item.Category != null)
                    {
                        var cmpItems = new CompanyPermission()
                        {
                            PermissionName = item.Description,
                            Value = item.Category,
                            NobleModuleId = item.NobleModuleId
                        };
                        companyPermissions.Add(cmpItems);

                    }

                    var pbItem = new NobleRolePermission()
                    {

                        IsActive = item.IsActive,
                        RoleId = item.RoleId
                    };
                    roleId = item.RoleId;
                    companyId = item.CompanyId;
                    allowAll = item.AllowAll;
                    isNoble = item.isNobel;
                    nobleRolePermission.Add(pbItem);
                }

            }

            var userId = await _userManager.Users.Where(x => x.CompanyId == companyId).FirstOrDefaultAsync();

            var message = await Mediator.Send(new AddUpdateRolesPermissionsCommand
            {
                RoleId = roleId,
                AllowAll = allowAll,
                CompanyId = companyId,
                IsNoble = isNoble,
                RolePermissions = nobleRolePermission,
                CompanyPermissions = companyPermissions,
                UserId = userId == null ? "" : userId.Id
            });
            return Ok(new { Message = message, type = "Update" });
        }


        [Route("api/Company/UpdateNobleRolePermissions")]
        [HttpPost("UpdateNobleRolePermissions")]
        [Roles("User", "Super Admin", "Admin", "Supervisor", "Noble Admin")]
        public async Task<IActionResult> UpdateNobleRolePermissions([FromBody] List<NobleRolesPermissionsLookUpModel> rolePermissionsVm)
        {


            var message = await Mediator.Send(new UpdatePermissionCommand
            {
                NobleRolesPermissionsLookUpModel = rolePermissionsVm
            });
            return Ok(new { Message = message, type = "Update" });
        }


        [Route("api/Company/RolePermissionsList")]
        [HttpGet("RolePermissionsList")]
        [Roles("User", "Super Admin", "Admin", "Supervisor", "Noble Admin")]
        public async Task<IActionResult> RolePermissionsList(Guid id)
        {
            var userPermission = await Mediator.Send(new GetRolesPermissionsListQuery { RoleId = id });
            return Ok(userPermission);
        }

        [Route("api/Company/RolePermissionsDetail")]
        [HttpGet("RolePermissionsDetail")]
        [Roles("User", "Super Admin", "Admin", "Supervisor", "Noble Admin")]
        public async Task<IActionResult> RolePermissionsDetail(Guid Id)
        {
            var userPermission = await Mediator.Send(new GetRolesPermissionsDetailQuery()
            {
                Id = Id
            });
            return Ok(userPermission);
        }

        [Route("api/Company/GetNobleUserRoleList")]
        [HttpGet("GetNobleUserRoleList")]
        [Roles("User", "Super Admin", "Admin", "Supervisor", "Noble Admin")]
        public async Task<IActionResult> GetNobleUserRoleList(Guid id)
        {
            var userPermission = await Mediator.Send(new GetNobleUserRoleDetailQuery { Id = id });
            return Ok(userPermission);
        }
        [Route("api/Company/SaveNobleUserRole")]
        [HttpPost("SaveNobleUserRole")]
        [Roles("User", "Super Admin", "Admin", "Supervisor", "Noble Admin")]
        public async Task<IActionResult> SaveNobleUserRole([FromBody] NobleUserRoleVm nobleUserRoleVm)
        {
            var id = Guid.Empty;
            if (nobleUserRoleVm.Id == Guid.Empty)
            {
                id = await Mediator.Send(new AddUpdateNobleUserRoleCommand
                {
                    Id = new Guid(),
                    UserId = nobleUserRoleVm.UserId,
                    RoleId = nobleUserRoleVm.RoleId,
                    IsActive = true
                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateNobleUserRoleCommand
                {
                    Id = nobleUserRoleVm.Id,
                    UserId = nobleUserRoleVm.UserId,
                    RoleId = nobleUserRoleVm.RoleId,
                    IsActive = true
                });
            }
            if (id != Guid.Empty)
            {
                var userPermission = await Mediator.Send(new GetNobleUserRoleDetailQuery()
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, Permission = userPermission });
            }
            return Ok(new { IsSuccess = false });
        }


        [Route("api/Company/GetByCategoryPermissionQuery")]
        [HttpGet("GetByCategoryPermissionQuery")]
        [Roles("User", "Super Admin", "Admin", "Supervisor", "Noble Admin")]
        public async Task<IActionResult> GetByCategoryPermissionQuery(string categoryName, Guid companyId, Guid roleId)
        {
            var category = await Mediator.Send(new GetByCategoryPermissionQuery
            {
                CompanyId = companyId,
                CategoryName = categoryName,
                RoleId = roleId
            });
            return Ok(category);
        }

        [Route("api/Company/GetCategoryName")]
        [HttpGet("GetCategoryName")]
        [Roles("User", "Super Admin", "Admin", "CanViewUserPermission", "Noble Admin")]
        public async Task<IActionResult> GetCategoryName(Guid companyId, Guid roleId)
        {
            var category = await Mediator.Send(new GetCategoryNameQuery
            {
                CompanyId = companyId,
                RoleId = roleId

            });
            return Ok(category);
        }
        [Route("api/Company/GetModuleNames")]
        [HttpGet("GetModuleNames")]
        [Roles("User", "Super Admin", "Admin", "Supervisor", "Noble Admin")]
        public async Task<IActionResult> GetModuleNames(bool isActive)
        {
            var moduleNames = await Mediator.Send(new GetModuleNameListQuery { IsActive = isActive });
            return Ok(moduleNames);
        }
        [Route("api/Company/GetRightsByModuleName")]
        [HttpGet("GetRightsByModuleName")]
        [Roles("User", "Super Admin", "Admin", "Supervisor", "Noble Admin")]
        public async Task<IActionResult> GetRightsByModuleName(Guid roleId, bool isActive, string moduleName, bool isNobel, bool isRights, Guid moduleId, Guid companyId)
        {
            var moduleRights = await Mediator.Send(new GetModuleRightListQuery { ModuleName = moduleName, IsActive = isActive, RoleId = roleId, isNobel = isNobel, isRights = isRights, ModuleId = moduleId, CompanyId = companyId });
            return Ok(moduleRights);
        }
        [Route("api/Company/GetListOfPermission")]
        [HttpGet("GetListOfPermission")]
        [Roles("User", "CanViewUserPermission", "Admin", "Supervisor", "Noble Admin")]
        public async Task<IActionResult> GetListOfPermission(Guid roleId)
        {
            var permission = await Mediator.Send(new GetNoblePermissionListQuery()
            {
                RoleId = roleId
            });
            return Ok(permission);
        }
        [Route("api/Company/GetListOfPermissionUserRole")]
        [HttpGet("GetListOfPermissionUserRole")]
        [Roles("User", "Admin", "Noble Admin")]
        public async Task<IActionResult> GetListOfPermissionUserRole()
        {
            var nobleRole = _context.NobleRoles.AsNoTracking().FirstOrDefault(x => x.Name == "Admin");
            if (nobleRole != null)
            {
                var roleId = nobleRole.Id;
                var permission = await Mediator.Send(new GetNoblePermissionListQuery()
                {
                    RoleId = roleId
                });
                return Ok(permission);
            }
            else
            {
                return Ok(null);
            }
           
        }
        #endregion
        //[Route("api/Company/GetDefaultWareHouse")]
        //[HttpGet("GetDefaultWareHouse")]
        ////[Roles("Can View Ware House", "Noble Admin", "Can Edit Promotion", "Can Add Stock In as Draft", "Can Edit Stock In as Draft", "Can Add Stock In as Post", "Can Edit Stock In as Post", "Can Add Stock Out as Draft", "Can Edit Stock Out as Draft", "Can Add Stock Out as Post", "Can Edit Stock Out as Post", "Can Save  Purchase Invoice as Draft", "Can Edit Purchase Invoice as Draft", "Can Save Sale Invoice as Hold", "Can Save Sale Invoice as Paid", "Can View  Sale Return", "Can Save Stock Transfer", "Can Edit Stock Transfer", "Can Save Stock Transfer", "Can View Report")]
        //public IActionResult GetDefaultWareHouse()
        //{
        //    var wareHouse = _Context.Warehouses.AsNoTracking().FirstOrDefault(x => x.StoreID == "S001");
        //    if (wareHouse == null)
        //    {
        //        return Ok(null);
        //    }
        //    return Ok(wareHouse.Id);
        //}

        [Route("api/Company/TermsAndConditionAgreed")]
        [HttpGet("TermsAndConditionAgreed")]
        public async Task<IActionResult> TermsAndConditionAgreed(Guid companyId, bool termsCondition)
        {
            var company = _context.Companies.AsNoTracking().FirstOrDefault(x => x.Id == companyId);
            if (company == null)
            {
                return Ok(false);

            }
            company.TermsCondition = termsCondition;
            _context.Update(company);
            Random rnd = new Random();
            for (int i = 0; i < 11; i++)
            {
                _code += rnd.Next(0, 9).ToString();
            }
            await Mediator.Send(new AddUpdateSyncRecordCommand()
            {
                SyncRecordModels = _context.SyncLog(),
                Code = _code,
            });
            await _context.SaveChangesAsync();

            return Ok(true);
        }

        #region Permission


        //[Route("api/Company/GetUserAccessPermission")]
        //[HttpGet("GetUserAccessPermission")]
        //public async Task<IActionResult> GetUserAccessPermission(string userId)
        //{

        //    var autoNo = await Mediator.Send(new UpdateClaimsCommand
        //    {
        //        User = userId
        //    });

        //    return Ok(autoNo);
        //}



        [Route("api/Company/GetCompanyPermission")]
        [HttpGet("GetCompanyPermission")]
        public async Task<IActionResult> GetCompanyPermission(Guid? branchId)
        {
            var permissionList = await Mediator.Send(new GetCompanyPermission
            {
                UserId = User.Identity.UserId(),
                BranchId = branchId

            });

            return Ok(permissionList);
        }

        [Route("api/Company/GetModuleWiseClaims")]
        [HttpGet("GetModuleWiseClaims")]
        [Roles("GetModuleWiseClaims")]
        public async Task<IActionResult> GetModuleWiseClaims(Guid? branchId)
        {
           
            try
            {
                var permissionList = await Mediator.Send(new GetModuleWiseClaims
                {
                    User = User.Identity.UserId(),
                    BranchId = branchId
                });
                var current = HttpContext;
                var companyId = User.Identity.CompanyId();
                permissionList.Add(new ModuleWiseClaimsLookupModel
                {
                    Token = "ServerAddress",
                    TokenName = current.Request.Scheme + "://" + current.Request.Host + "/api",

                });
                foreach (var item in permissionList)
                {
                    item.CompanyId = companyId;
                }
                var reportPath = _configuration.GetSection("ReportServer:Path").Value;

                using var ping = new Ping();
                try
                {
                    using var httpClient = new HttpClient();
                    StringContent content = new StringContent(JsonConvert.SerializeObject(permissionList), Encoding.UTF8, "application/json");

                    using var response = await httpClient.PostAsync(reportPath, content);
                    if (!response.IsSuccessStatusCode)
                    {
                        // Handle the error response
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        return Ok(permissionList);
                    }

                    string apiResponse = await response.Content.ReadAsStringAsync();
                    // Do something with the response
                    return Ok(permissionList);
                }
                catch (Exception ex)
                {
                    return Ok(permissionList);
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle the exception here.
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Route("api/Company/RemoveUserRole")]
        [HttpGet("RemoveUserRole")]
        [Roles("CanViewUserPermission")]
        public async Task<IActionResult> RemoveUserRole(Guid id)
        {
            var userRole = await Mediator.Send(new RemoveUserRoleQuery
            {
                Id = id

            });

            return Ok(userRole);
        }
        #endregion

        #region Bank Pos Terminal



        [Route("api/Company/SaveBankPosTerminal")]
        [HttpPost("SaveBankPosTerminal")]
        [Roles("CanAddPosTerminal", "CanEditPosTerminal")]
        public async Task<IActionResult> SaveBankPosTerminal([FromBody] BankPosTerminalVm regionVm)
        {
            var id = Guid.Empty;
            if (regionVm.Id == Guid.Empty)
            {

                id = await Mediator.Send(new AddUpdateBankPosTerminalCommand
                {
                    Id = new Guid(),
                    TerminalId = regionVm.TerminalId,
                    BankId = regionVm.BankId,
                    isActive = regionVm.isActive,



                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateBankPosTerminalCommand
                {
                    Id = regionVm.Id,
                    TerminalId = regionVm.TerminalId,
                    BankId = regionVm.BankId,
                    isActive = regionVm.isActive,

                });
            }
            if (id != Guid.Empty)
            {
                var region = await Mediator.Send(new GetBankPosTerminalDetailQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, region = region });
            }

            return Ok(new { IsSuccess = false });



        }
        [Route("api/Company/BankPosTerminalList")]
        [HttpGet("BankPosTerminalList")]
        [Roles("CanViewPosTerminal", "CanAddTerminal", "CanEditTerminal")]
        public async Task<IActionResult> BankPosTerminalList(bool isActive, Guid? bankId)
        {
            var region = await Mediator.Send(new GetBankPosTerminalListQuery
            {
                isActive = isActive,
                BankId = bankId,
            });
            return Ok(region);
        }


        [Route("api/Company/BankPosTerminalDetail")]
        [HttpGet("BankPosTerminalDetail")]
        [Roles("CanEditPosTerminal")]
        public async Task<IActionResult> BankPosTerminalDetail(Guid Id)
        {
            var region = await Mediator.Send(new GetBankPosTerminalDetailQuery()
            {
                Id = Id
            });
            return Ok(region);

        }
        #endregion

        #region ContactOpeningBalance

        [Route("api/Company/GetContactOpeningBalanceList")]
        [HttpGet("GetContactOpeningBalanceList")]
        [Roles("CanViewCustomerBalance", "CanViewSupplierBalance")]

        public async Task<IActionResult> GetContactOpeningBalanceList(Guid accountId, DateTime fromDate, DateTime toDate, int? pageNumber, bool isReport, string branchId)
        {
            var region = await Mediator.Send(new ContactBalanceQuery
            {
                BranchId = branchId,
                AccountId = accountId,
                FromDate = fromDate,
                ToDate = toDate,
                IsReport = isReport,
                PageNumber = pageNumber ?? 1,

            });
            return Ok(region);
        }

        #endregion

        #region Company Process
        [Route("api/Company/ProcessTypeList")]
        [HttpGet("ProcessTypeList")]
        public IActionResult ProcessTypeList()
        {
            var actionProcessType = Enum.GetNames(typeof(ActionProcessType)).ToList();
            return Ok(actionProcessType);
        }

        [Route("api/Company/SaveProcess")]
        [HttpPost("SaveProcess")]
        public async Task<IActionResult> SaveProcess([FromBody] ProcessLookUpModel process)
        {
            Guid id;
            if (process.Id == Guid.Empty)
            {

                id = await Mediator.Send(new AddUpdateCommand
                {
                    Process = process
                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateCommand
                {
                    Process = process
                });
            }
            if (id != Guid.Empty)
            {
                var processDetail = await Mediator.Send(new GetProcessDetailQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, process = processDetail });
            }

            return Ok(new { IsSuccess = false });



        }
        [Route("api/Company/ProcessList")]
        [HttpGet("ProcessList")]
        public async Task<IActionResult> ProcessList(string searchTerm, int? pageNumber, bool isActive, bool isDropdown, string document)
        {
            var process = await Mediator.Send(new GetProcessListQuery
            {
                Document = document,
                IsDropdown = isDropdown,
                isActive = isActive,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(process);
        }

        //[Route("api/Product/ColorDelete")]
        //[HttpGet("ColorDelete")]
        //[Roles("Can Delete Color", "Noble Admin")]
        //public async Task<IActionResult> ColorDelete(Guid id)
        //{
        //    var colorId = await Mediator.Send(new DeleteColorCommand
        //    {
        //        Id = id
        //    });
        //    return Ok(colorId);

        //}
        [Route("api/Company/ProcessDetail")]
        [HttpGet("ProcessDetail")]
        public async Task<IActionResult> ProcessDetail(Guid Id)
        {
            var process = await Mediator.Send(new GetProcessDetailQuery()
            {
                Id = Id
            });
            return Ok(process);

        }

        #endregion


        #region ApplicationUpdate

        [Route("api/Company/CheckApplicationUpdate")]
        [HttpGet("CheckApplicationUpdate")]
        public IActionResult CheckApplicationUpdate()
        {
            try
            {
                var version = GetType().Assembly.GetName().Version.ToString();
                using var conn = new SqlConnection(_configuration.GetConnectionString("ApplicationUpdateDb"));
                var query = "select Id, Status, Version, Detail, Date from ApplicationUpdates;";
                var result = conn.Query<ApplicationUpdateVm>(query).ToList();
                if (result.Count > 0)
                {
                    if (version == result[result.Count - 1].Version)
                        return Ok(new { IsSuccess = true, LatestVersion = result[result.Count - 1].Version, ApplicationVersion = version });
                    return Ok(new { IsSuccess = false, LatestVersion = result[result.Count - 1].Version, ApplicationVersion = version });
                }
                return Ok(new { IsSuccess = false, LatestVersion = version, ApplicationVersion = version });
            }
            catch (Exception e)
            {

                throw new ApplicationException(e.Message);
            }
        }

        [Route("api/Company/ApplicationUpdate")]
        [HttpGet("ApplicationUpdate")]
        public IActionResult ApplicationUpdate()
        {
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:7015/print/");
                //HTTP GET
                var responseTask = client.GetAsync("StopIisManager");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<string>();
                    readTask.Wait();
                    return Ok(new { IsSuccess = true });

                }
                return Ok(new { IsSuccess = false, Message = "Something went wrong. Please contact administrator" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        [Route("api/Company/LocalDatabaseUpdate")]
        [HttpGet("LocalDatabaseUpdate")]
        public async Task<IActionResult> LocalDatabaseUpdateAsync()
        {
            try
            {
                var counterId = _contextProvider.GetCounterId();
                var version = GetType().Assembly.GetName().Version.ToString();
                using var conn = new SqlConnection(_configuration.GetConnectionString("ApplicationUpdateDb"));
                var query = "select Id, Status, Version, Detail, Date from ApplicationUpdates;";
                var result = conn.Query<ApplicationUpdateVm>(query).ToList();
                if (result.Count > 0 && version == result[result.Count - 1].Version && counterId != null)
                {
                    var data = _context.ApplicationUpdates.OrderBy(x => x.CounterId == counterId).LastOrDefault();
                    if (data != null && data.Version == version)
                    {
                        return Ok(new { IsSuccess = true, Message = "Data Already Updated" });

                    }
                    var applicationUpdate = new ApplicationUpdate()
                    {
                        Status = result[result.Count - 1].Status,
                        Version = result[result.Count - 1].Version,
                        Detail = result[result.Count - 1].Detail,
                        DateTime = result[result.Count - 1].DateTime,
                        CounterId = counterId
                    };
                    await _context.ApplicationUpdates.AddAsync(applicationUpdate);

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    await Mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = _context.SyncLog(),
                        Code = _code,
                    });
                    await _context.SaveChangesAsync();
                }

                return Ok(new { IsSuccess = false });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion


        #region FinancialYear
        [Route("api/Company/GetCurrentYear")]
        [HttpGet("GetCurrentYear")]
        //[Roles("User", "Super Admin", "Admin", "Supervisor", "Noble Admin")]
        public async Task<IActionResult> GetCurrentYear(string month)
        {
            var currentYear = await Mediator.Send(new GetCurrentYearQuery()
            {
                Month = month
            });
            return Ok(currentYear);
        }

        [Route("api/Company/AddFinancialYear")]
        [HttpGet("AddFinancialYear")]
        //[Roles("User", "Super Admin", "Admin", "Supervisor", "Noble Admin")]
        public async Task<IActionResult> AddFinancialYear(string year, int month,string monthType)
        {
            var currentYear = await Mediator.Send(new AddUpdateFinancialYear()
            {
                Year = year,
                Month = month,
                MonthType = monthType
            });
            var periods = await Mediator.Send(new GetCurrentYearQuery());
            return Ok(periods);
        }
        [Route("api/Company/GetYear")]
        [HttpGet("GetYear")]
        [Roles("User", "Super Admin", "Admin", "Supervisor", "Noble Admin")]
        public async Task<IActionResult> GetYear()
        {
            var latestYear =await _context.YearlyPeriods.AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();
            return Ok(latestYear);
        }

        [Route("api/Company/AddUpdatePreviousFinancialYear")]
        [HttpGet("AddUpdatePreviousFinancialYear")]
        [Roles("User", "Super Admin", "Admin", "Supervisor", "Noble Admin")]
        public async Task<IActionResult> AddUpdatePreviousFinancialYear(string year, int month)
        {
            await Mediator.Send(new AddUpdatePreviousFinancialYear());
            var periods = await Mediator.Send(new GetCurrentYearQuery());
            return Ok(periods);
        }
        
        [Route("api/Company/CloseMonthPeriod")]
        [HttpGet("CloseMonthPeriod")]
        public async Task<IActionResult> CloseMonthPeriod(Guid id, bool status)
        {
            var result=await Mediator.Send(new CloseMonthPeriodCommand()
            {
                Id = id,
                Status = status
            });
            return Ok(result);
        }


        #endregion

        //#region Company Option
        //[Route("api/Company/SaveCompanyOption")]
        //[HttpPost("SaveCompanyOption")]
        //public async Task<IActionResult> SaveCompanyOption([FromBody] CompanyOptionLookUp companyOption)
        //{
        //    var result = await Mediator.Send(new AddUpdateOptionCommand
        //    {
        //        CompanyOption = companyOption
        //    });
        //    return Ok(new { IsSuccess = true });
        //}

        //[Route("api/Company/CompanyOptionList")]
        //[HttpGet("CompanyOptionList")]
        //public async Task<IActionResult> CompanyOptionList(string searchTerm)
        //{
        //    var color = await Mediator.Send(new GetCompanyOptionListQuery
        //    {
        //        SearchTerm = searchTerm,
        //    });
        //    return Ok(color);
        //}
        //[Route("api/Company/CompanyOptionDelete")]
        //[HttpGet("CompanyOptionDelete")]
        //public async Task<IActionResult> CompanyOptionDelete(Guid id)
        //{
        //    var companyOption = await Mediator.Send(new DeleteOptionCommand
        //    {
        //        Id = id
        //    });
        //    return Ok(companyOption);

        //}
        //[Route("api/Company/CompanyOptionDetail")]
        //[HttpGet("CompanyOptionDetail")]
        //public async Task<IActionResult> CompanyOptionDetail(Guid Id)
        //{
        //    var companyOption = await Mediator.Send(new GetCompanyOptionDetailQuery()
        //    {
        //        Id = Id
        //    });
        //    return Ok(companyOption);

        //}
        //#endregion

        #region NoblePermission

        [Route("api/Company/ApplyWhiteLabelling")]
        [HttpPost("ApplyWhiteLabelling")]
        public async Task<IActionResult> ApplyWhiteLabelling([FromBody] WhiteLabelLookUp whiteLabelLookUp)
        {
            var message = await Mediator.Send(new ApplyWhiteLabelling()
            {
                WhiteLabelLookUp = whiteLabelLookUp
            });

            return Ok(new { IsSuccess = true });
        }
        [Route("api/Company/SaveNoblePermission")]
        [HttpPost("SaveNoblePermission")]
        public async Task<IActionResult> SaveNoblePermission([FromBody] GetAllPermissionModuleAndGroup getAllPermission)
        {
            var message = await Mediator.Send(new AddUpdateNoblePermissionCommand()
            {
                PermissionModuleAndGroup = getAllPermission,
                UserId = User.Identity.UserId()
            });

            return Ok(new { IsSuccess = true });
        }
        [Route("api/Company/UpdateUserPermission")]
        [HttpPost("UpdateUserPermission")]
        public async Task<IActionResult> UpdateUserPermission([FromBody] List<PermissionsLookUp> permission, string roleName)
        {
            var message = await Mediator.Send(new UpdateUserPermissionCommand()
            {
                Permissions = permission,
                RoleName = roleName
            });

            return Ok(new { IsSuccess = true });
        }
        [Route("api/Company/UpdateUserPermissionBranches")]
        [HttpPost("UpdateUserPermissionBranches")]
        public async Task<IActionResult> UpdateUserPermissionBranches([FromBody] BranchPermissionModel branchPermissionModel)
        {
            var message = await Mediator.Send(new BranchPermissionQuery()
            {
                Permissions = branchPermissionModel.Permissions,
                BranchId = branchPermissionModel.BranchId,
                IsBranch = branchPermissionModel.IsBranch,
                RoleName = branchPermissionModel.RoleName
            });

            return Ok(new { IsSuccess = true });
        }
        [Route("api/Company/GetBranchWisePermission")]
        [HttpGet("GetBranchWisePermission")]
        public async Task<IActionResult> GetBranchWisePermission(Guid branchId)
        {
            var message = await Mediator.Send(new BranchPermissionDetailQuery
            {
                BranchId = branchId
            });

            return Ok(new { IsSuccess = true, Message = message });
        }

        [Route("api/Company/GetCompanyInformationForPermission")]
        [HttpGet("GetCompanyInformationForPermission")]
        public async Task<IActionResult> GetCompanyInformationForPermission()
        {
            var message = await Mediator.Send(new GetCompanyInformationQuery());

            return Ok(new { IsSuccess = true, Message = message });
        }
        [Route("api/Company/AboutUs")]
        [HttpGet("AboutUs")]
        public async Task<IActionResult> AboutUs(bool isAboutUs)
        {
            var message = await Mediator.Send(new GetCompanyAboutUsDetail()
            {
                IsAboutUs = isAboutUs
            });

            return Ok(new { IsSuccess = true, Message = message });
        }
        [Route("api/Company/GetCompanyList")]
        [HttpGet("GetCompanyList")]
        public async Task<IActionResult> GetCompanyList()
        {
            var message = await Mediator.Send(new GetCompanyLicenseList());

            return Ok(new { IsSuccess = true, Message = message });
        }
        [Route("api/Company/AddUpdateNoblePaymentLimit")]
        [HttpGet("AddUpdateNoblePaymentLimit")]
        public async Task<IActionResult> AddUpdateNoblePaymentLimit()
        {
            var message = await Mediator.Send(new AddUpdateNoblePaymentLimitCommand());

            return Ok(new { IsSuccess = true, Message = message });
        }


        #endregion

        [Route("api/Company/SaveAttachment")]
        [HttpPost("SaveAttachment")]
        public async Task<IActionResult> SaveAttachment([FromBody] List<AttachmentLookUpModel> attachment)
        {
            var message = await Mediator.Send(new AddAttachmentCommand()
            {
                Attachment = attachment
            });

            return Ok(new { IsSuccess = true, Message = message });
        }
        [Route("api/Company/AttachmentList")]
        [HttpGet("AttachmentList")]
        public async Task<IActionResult> AttachmentList(Guid id)
        {
            var result = await Mediator.Send(new AttachmentListQuery()
            {
                Id = id
            });

            return Ok(result);
        }


        #region SendEmailOfAllAttachment
        //public class Pos
        //{
        //    public string HtmlString { get; set; }

        //}

        //[Route("api/Company/SentTemplateAsAttachment")]
        //[HttpPost("SentTemplateAsAttachment")]

        //public IActionResult SentTemplateAsAttachment([FromForm] Pos html)
        //{
        //    try
        //    {

        //        var builder = new StringBuilder();
        //        builder.AppendLine("<!DOCTYPE html>");
        //        builder.AppendLine("<head>");
        //        builder.AppendLine(" <meta http-equiv='content-type' content='text/html; charset=utf-8'>");
        //        builder.AppendLine("</head>");
        //        builder.AppendLine("<body style='color: black;font-family: sans-serif; margin:0px;'>");
        //        builder.AppendLine(html.HtmlString);
        //        builder.AppendLine("</body>");
        //        builder.AppendLine("</html>");


        //        PdfConversionSettings config;
        //        config = new PdfConversionSettings
        //        {
        //            PdfToolPath = _hostingEnvironment.WebRootPath + "\\wkhtmltopdf.exe",
        //            Content = builder.ToString(),
        //            Size = PdfPageSize.A4,
        //            Margins = new PdfPageMargins() { Bottom = 0, Left = 0, Right = 0, Top = 0 },
        //            Orientation = PdfPageOrientation.Portrait,
        //            LowQuality = true,
        //            Grayscale = true,
        //            Zoom = 0.0f
        //        };


        //        if (!Directory.Exists(_hostingEnvironment.WebRootPath + "\\Rotativa\\"))
        //        {
        //            Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\Rotativa\\");
        //        }

        //        var path = _hostingEnvironment.WebRootPath + "\\Rotativa\\testing1.pdf";

        //        if (System.IO.File.Exists(_hostingEnvironment.WebRootPath + "\\Rotativa\\testing1.pdf"))
        //        {
        //            System.IO.File.Delete(_hostingEnvironment.WebRootPath + "\\Rotativa\\testing1.pdf");
        //        }


        //        using (var fileStream = new FileStream(path, FileMode.Create))
        //        {
        //            PdfConvert.Convert(config, fileStream);
        //            fileStream.Close();
        //        }


        //        return Ok("success");
        //    }
        //    catch (Exception ex)
        //    {


        //        Console.WriteLine(ex.Message);
        //        return Ok(ex.Message);
        //    }
        //    //return Ok("");
        //}

        #endregion


        [Route("api/Company/UserDefineFlowSave")]
        [HttpPost("UserDefineFlowSave")]
        public async Task<IActionResult> UserDefineFlowSave([FromBody] UserDefineFlow userDefineFlow)
        {
            try
            {
                var flow = await _context.UserDefineFlows.AsNoTracking().FirstOrDefaultAsync(x => x.CompanyId == userDefineFlow.CompanyId);
                if (flow == null)
                {
                    var userFlow = new UserDefineFlow
                    {
                        CompanyId = userDefineFlow.CompanyId,
                        QuotationToSaleOrder = userDefineFlow.QuotationToSaleOrder,
                        QuotationToSaleInvoice = userDefineFlow.QuotationToSaleInvoice,
                        PartiallyQuotation = userDefineFlow.PartiallyQuotation,
                        PartiallySaleOrder = userDefineFlow.PartiallySaleOrder,
                    };
                    await _context.UserDefineFlows.AddAsync(userFlow);
                }
                else
                {
                    flow.QuotationToSaleOrder = userDefineFlow.QuotationToSaleOrder;
                    flow.QuotationToSaleInvoice = userDefineFlow.QuotationToSaleInvoice;
                    flow.PartiallyQuotation = userDefineFlow.PartiallyQuotation;
                    flow.PartiallySaleOrder = userDefineFlow.PartiallySaleOrder;
                    _context.UserDefineFlows.Update(flow);
                }

                Random rnd = new Random();
                for (int i = 0; i < 11; i++)
                {
                    _code += rnd.Next(0, 9).ToString();
                }
                await Mediator.Send(new AddUpdateSyncRecordCommand()
                {
                    SyncRecordModels = _context.SyncLog(),
                    Code = _code,
                });

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Route("api/Company/UserDefineFlowEdit")]
        [HttpGet("UserDefineFlowEdit")]
        public async Task<IActionResult> UserDefineFlowEdit(Guid companyId)
        {
            try
            {
                var flow = await _context.UserDefineFlows.AsNoTracking().FirstOrDefaultAsync(x => x.CompanyId == companyId);
                if (flow == null)
                {
                    var userFlow = new UserDefineFlow();
                    return Ok(userFlow);
                }
                else
                {
                    var userFlow = new UserDefineFlow
                    {
                        CompanyId = flow.CompanyId,
                        QuotationToSaleOrder = flow.QuotationToSaleOrder,
                        QuotationToSaleInvoice = flow.QuotationToSaleInvoice,
                        PartiallyQuotation = flow.PartiallyQuotation,
                        PartiallySaleOrder = flow.PartiallySaleOrder
                    };
                    return Ok(userFlow);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #region Prefixies

        [Route("api/Company/SavePrefixies")]
        [HttpPost("SavePrefixies")]
        public async Task<IActionResult> SavePrefixies([FromBody] PrefixiesLookupModel prefix)
        {
            var message = await Mediator.Send(new AddUpdatePrefixiesCommand()
            {
                Pre = prefix
            });

            return Ok(new { IsSuccess = true, Message = message });
        }

        [Route("api/Company/PrefixDetails")]
        [HttpGet("PrefixDetails")]
        public async Task<IActionResult> PrefixDetails()
        {
            var result = await Mediator.Send(new PrefixiesDetails());
            return Ok(result);
        }

        #endregion

        #region InvoiceDefault

        [Route("api/Company/SaveInvoiceDefault")]
        [HttpPost("SaveInvoiceDefault")]
        public async Task<IActionResult> InvoiceDefault([FromBody] InvoiceDefaultLookUpModel invoiceDefault)
        {
            var message = await Mediator.Send(new AddUpdateInvoiceDefaultCommand()
            {
                Id = invoiceDefault.Id,
                IsSalePrice=invoiceDefault.IsSalePrice,
                IsCustomerPrice = invoiceDefault.IsCustomerPrice,
                IsSalePriceLabel = invoiceDefault.IsSalePriceLabel,
                IsCustomerPriceLabel = invoiceDefault.IsCustomerPriceLabel
            });

            return Ok(new { IsSuccess = true, Message = message });
        }

        [Route("api/Company/InvoiceDefaultDetails")]
        [HttpGet("InvoiceDefaultDetails")]
        public async Task<IActionResult> InvoiceDefaultDetails()
        {
            var result = await Mediator.Send(new InvoiceDefaultDetailQuery());
            return Ok(result);
        }

        #endregion



    }
}