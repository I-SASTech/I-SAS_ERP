using Focus.Business.Interface;
using Focus.Business.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Noble.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using InputModel = Noble.Api.Models.InputModel;
using Focus.Business.Models;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using Focus.Business.Claims.Command.UpdateClaims;
using Focus.Business.Extensions;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Focus.Business;
using Microsoft.AspNetCore.Authentication;
using Focus.Business.DayStarts.Queries.IsDayStart;
using Focus.Business.PrintSettings.Queries.GetPrintSettingsDetails;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using Focus.Business.NobleUserRole.Commands.AddUpdateNobleUserRole;
using Focus.Business.RolePermission.Queries.RemoveUserRole;
using Focus.Business.LoginHistories.Commands.AddUpdateLoginHistory;
using Focus.Business.LoginHistories.Queries.GetLoginHistoryList;
using Focus.Business.DayStarts.WholeSaleQueries;
using Focus.Business.Permission.Commands.AddUpdateNoblePermission;
using Focus.Business.DefaultSettingInvoice.DefaultSattingCommand;
using Focus.Domain.Enum;
using System.Net.NetworkInformation;
using Dapper;
using Focus.Business.SyncRecords.Commands;
using DocumentFormat.OpenXml.InkML;

namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserComponent _userComponent;
        private readonly ICompanyComponent _companyComponent;
        private readonly ISendEmail _sendEmail;
        private readonly UrlEncoder _utEncoder;
        private readonly IConfiguration _configuration;
        private readonly IPrincipal _principal;
        private readonly IApplicationDbContext _context;
        private string _code;


        public string SharedKey { get; private set; }
        public string[] RecoveryCodes { get; private set; }

        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
            IApplicationDbContext context, UrlEncoder utEncoder, IUserComponent userComponent, ISendEmail sendEmail, IConfiguration configuration,
            IPrincipal principal, ICompanyComponent companyComponent)
        {
            _signInManager = signInManager;
            _utEncoder = utEncoder;
            _context = context;
            _userManager = userManager;
            _sendEmail = sendEmail;
            _userComponent = userComponent;
            _configuration = configuration;
            _principal = principal;
            _companyComponent = companyComponent;
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> LogInAsync([FromBody] InputModel input)
        {
            SignInResult result = new SignInResult();
            ApplicationUser user;
            if (ModelState.IsValid)
            {
                try
                {
                    if (input.Email.IndexOf('@') > -1)
                    {
                        //Validate email format
                        string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                        Regex re = new Regex(emailRegex);
                        if (!re.IsMatch(input.Email))
                        {
                            result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, input.RememberMe, lockoutOnFailure: false);
                            user = await _userManager.Users.Select(x => new ApplicationUser
                            {
                                Id = x.Id,
                                UserName = x.UserName,
                                CompanyId = x.CompanyId,
                                Email = x.Email,
                                TerminalId = x.TerminalId,
                                EmployeeId = x.EmployeeId,
                                OnlineTerminalId = x.OnlineTerminalId,
                                FirstName = x.FirstName,
                                ImagePath = x.ImagePath,
                                EmailConfirmed = x.EmailConfirmed,
                                PhoneNumber = x.PhoneNumber,
                            }).FirstOrDefaultAsync(x => x.UserName == input.Email);

                            if (user != null)
                                input.Email = user.Email;

                            if (user != null && user.IsActive)
                            {
                                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                                return Ok(new LoginModel());
                            }
                        }
                        else
                        {
                            user = await _userManager.Users.Select(x => new ApplicationUser
                            {
                                Id = x.Id,
                                UserName = x.UserName,
                                CompanyId = x.CompanyId,
                                Email = x.Email,
                                TerminalId = x.TerminalId,
                                EmployeeId = x.EmployeeId,
                                OnlineTerminalId = x.OnlineTerminalId,
                                FirstName = x.FirstName,
                                ImagePath = x.ImagePath,
                                EmailConfirmed = x.EmailConfirmed,
                                PhoneNumber = x.PhoneNumber,
                            }).FirstOrDefaultAsync(x => x.Email == input.Email);

                            if (user != null)
                                result = await _signInManager.PasswordSignInAsync(user.UserName, input.Password, input.RememberMe, lockoutOnFailure: false);

                            if (user != null && user.IsActive)
                            {
                                ModelState.AddModelError(string.Empty, "User is Not Active.Please Activate User");
                                return Ok(new LoginModel());
                            }
                        }

                    }
                    else
                    {
                        result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, input.RememberMe, lockoutOnFailure: false);
                        user = await _userManager.Users.Select(x => new ApplicationUser
                        {
                            Id = x.Id,
                            UserName = x.UserName,
                            CompanyId = x.CompanyId,
                            Email = x.Email,
                            TerminalId = x.TerminalId,
                            EmployeeId = x.EmployeeId,
                            OnlineTerminalId = x.OnlineTerminalId,
                            FirstName = x.FirstName,
                            ImagePath = x.ImagePath,
                            EmailConfirmed = x.EmailConfirmed,
                            PhoneNumber = x.PhoneNumber,
                        }).FirstOrDefaultAsync(x => x.UserName == input.Email);

                        if (user != null)
                            input.Email = user.Email;
                        if (user is { IsActive: true })
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                            return Ok(new LoginModel());
                        }
                    }
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: false
                    if (result.RequiresTwoFactor)
                    {
                        return Ok(true);
                    }

                    if (result.Succeeded)
                    {
                        var token = await GenerateJwtToken(user);
                        var expiration = "";
                        var isPayment = false;
                        var isNotPayment = false;

                        //var paymentLimits = await _context.PaymentLimits.Select(x=>new
                        //{
                        //    x.IsActive,
                        //    x.ToDate,
                        //}).LastOrDefaultAsync();

                        //if (user.Email.ToLower() != "noble@gmail.com")
                        //{
                        //    if (paymentLimits is { IsActive: true })
                        //    {
                        //        if (paymentLimits.ToDate >= DateTime.UtcNow)
                        //        {
                        //            expiration = "Your License Expire on " + paymentLimits.ToDate.ToString("d");
                        //            isPayment = true;
                        //        }
                        //        else
                        //        {
                        //            return Ok(new LoginModel()
                        //            {
                        //                Token = token,
                        //                CompanyId = user.CompanyId,
                        //                Expiration = "You do not pay your payment",
                        //                IsNotPayment = true
                        //            });
                        //        }
                        //    }
                        //}

                        //var companyLicence = await _context.CompanyLicences
                        //    .Select(x => new
                        //    {
                        //        x.GracePeriod,
                        //        x.IsActive,
                        //        x.IsBlock,
                        //        x.LicenseType,
                        //        x.ToDate,
                        //        x.FromDate,
                        //    }).OrderBy(x => x.ToDate).LastOrDefaultAsync();

                        //if (user.Email.ToLower() != "noble@gmail.com")
                        //{
                        //    if (companyLicence == null)
                        //    {
                        //        return Ok(new LoginModel()
                        //        {
                        //            Token = token,
                        //            CompanyId = user.CompanyId,
                        //            Expiration = "You do not have purchase any License. Please contact to support to purchase and activate your license"
                        //        });
                        //    }

                        //    if (!companyLicence.IsActive && !companyLicence.GracePeriod)
                        //        return Ok(new LoginModel()
                        //        {
                        //            Token = token,
                        //            CompanyId = user.CompanyId,
                        //            Expiration = "Your Company License has been expired. Please contact to support and activate your license"
                        //        });

                        //    if (companyLicence.IsBlock)
                        //        return Ok(new LoginModel()
                        //        {
                        //            Token = token,
                        //            CompanyId = user.CompanyId,
                        //            Expiration = "Your Company License has been blocked. Please contact to support and activate your license"
                        //        });

                        //    if (companyLicence.LicenseType != "Unlimited")
                        //    {
                        //        if (!(companyLicence.FromDate.Date <= DateTime.UtcNow.Date &&
                        //              companyLicence.ToDate.Date >= DateTime.UtcNow.Date))
                        //            return Ok(new LoginModel()
                        //            {
                        //                Token = token,
                        //                CompanyId = user.CompanyId,
                        //                Expiration = "Your Company License has been expired. Please contact to support and activate your license"
                        //            });
                        //    }
                        //    else if (companyLicence.GracePeriod)
                        //    {
                        //        if (!(companyLicence.FromDate.Date <= DateTime.UtcNow.Date &&
                        //              companyLicence.ToDate.Date >= DateTime.UtcNow.Date))
                        //            return Ok(new LoginModel()
                        //            {
                        //                Token = token,
                        //                CompanyId = user.CompanyId,
                        //                Expiration = "Your Company License has been expired. Please contact to support and activate your license"
                        //            });
                        //    }
                        //}

                        var company = await _context.Companies
                            .Select(x => new
                            {
                                x.Id,
                                x.IsProceed,
                                x.Step1,
                                x.Step2,
                                x.Step3,
                                x.Step4,
                                x.Step5,
                                x.TermsCondition,
                            }).FirstOrDefaultAsync(x => x.Id == user.CompanyId);

                        var companyPermission = await _context.CompanyPermissions.Select(x => new { x.Value }).ToListAsync();

                        var dayStart = false;
                        bool isExpenseDay = false;
                        string dayStartTime = "";

                        var loginPermissions = await _context.LoginPermissions.AsNoTracking()
                            .Select(x => new
                            {
                                x.UserId,
                                x.IsOverAllAccess,
                                x.TerminalUserType,
                                x.IsTouchInvoice,
                                x.StartDay,
                                x.TouchScreen,
                                x.IsExpenseAccount,
                                x.CloseDay,
                                x.IsSupervisor,
                                x.TransferCounter,
                                x.AllowAll,
                            }).FirstOrDefaultAsync(x => x.UserId == Guid.Parse(user.Id));


                        var nobleUserRole = await _context.NobleUserRoles.Select(x => new { x.UserId, x.RoleId }).FirstOrDefaultAsync(x => x.UserId == user.Id);
                        var nobleRole = new { Id = Guid.Empty, Name = "" };
                        if (nobleUserRole != null)
                        {
                            nobleRole = await _context.NobleRoles
                                .Select(x => new { x.Id, x.Name })
                                .FirstOrDefaultAsync(x => x.Id == nobleUserRole.RoleId);
                        }

                        var userBranch = new { UserId = "", BranchId = Guid.Empty, BranchName = "",MainBranch=false, Code = "" };
                        if (nobleUserRole != null)
                        {
                            userBranch = await _context.BranchUsers
                                .Select(x => new { x.UserId, x.BranchId, x.Branch.BranchName,x.Branch.MainBranch, x.Branch.Code })
                                .FirstOrDefaultAsync(x => x.UserId == user.Id);
                        }

                        //BankDetail
                        //CanStartDay
                        Guid? userCounterId = null;
                        bool canDayStart = companyPermission.Any(x => x.Value == "CanStartDay");
                        bool bankDetail = companyPermission.Any(x => x.Value == "BankDetail");

                        if (canDayStart && bankDetail)
                        {
                            var isDayStart = await Mediator.Send(new IsWholeSaleDayStart()
                            {
                                UserId = Guid.Parse(user.Id),
                                EmployeeId = user.EmployeeId,
                                IsLogin = true,
                                Email = user.Email,
                                CompanyId = user.CompanyId,
                                IsSupervisor = loginPermissions.IsSupervisor,
                            });

                            if (isDayStart.IsDayStart)
                            {
                                //token = isDayStart.Token;
                                dayStart = isDayStart.IsDayStart;
                                dayStartTime = isDayStart.FromTime;
                                userCounterId = isDayStart.CounterId;

                            }
                        }
                        else if (canDayStart)
                        {
                            var isDayStart = await Mediator.Send(new IsDayStartQuery
                            {
                                UserId = Guid.Parse(user.Id),
                                EmployeeId = user.EmployeeId,
                                IsLogin = true,
                                Email = user.Email,
                                CompanyId = user.CompanyId,
                                IsSupervisor = loginPermissions.IsSupervisor,

                            });
                            if (isDayStart.IsDayStart)
                            {
                                //token = isDayStart.Token;
                                dayStart = isDayStart.IsDayStart;
                                isExpenseDay = isDayStart.IsExpenseDay;
                                dayStartTime = isDayStart.FromTime?.ToString("hh:mm tt");
                            }
                            userCounterId = _context.DayStarts.FirstOrDefault(x => x.StartTerminalFor == user.UserName && x.IsActive && !x.IsDayStart)?.CounterId;

                        }

                        var role = await _userComponent.GetRoleByUser(user.Id);
                        var warehouseId = _context.Warehouses.Select(x => new { x.Id, x.StoreID }).FirstOrDefault(x => x.StoreID == "S001")?.Id;
                        var companyOptions = await _context.CompanyOptions.AsNoTracking().Where(x => x.LocationId == user.CompanyId).ToListAsync();


                        string businessLogo = null;
                        bool overWrite = false;
                        string businessNameArabic = null;
                        string businessNameEnglish = null;
                        string businessTypeArabic = null;
                        string businessTypeEnglish = null;
                        string companyNameArabic = null;
                        string companyNameEnglish = null;
                        Terminal terminal = null;


                        if (role == "Noble Admin" || role=="Admin")
                        {
                            return Ok(new LoginModel()
                            {
                                BusinessLogo = businessLogo,
                                OverWrite = overWrite,
                                BusinessNameArabic = businessNameArabic,
                                BusinessNameEnglish = businessNameEnglish,
                                BusinessTypeArabic = businessTypeArabic,
                                BusinessTypeEnglish = businessTypeEnglish,
                                CompanyNameArabic = companyNameArabic,
                                CompanyNameEnglish = companyNameEnglish,
                                CompanyOptions = companyOptions,
                                DayStartTime = dayStartTime,
                                Expiration = expiration,
                                IsPayment = isPayment,
                                IsNotPayment = isNotPayment,

                                CounterId = userCounterId ?? Guid.Empty,

                                IsProceed = company.IsProceed,
                                Step1 = company.Step1,
                                Step2 = company.Step2,
                                Step3 = company.Step3,
                                Step4 = company.Step4,
                                Step5 = company.Step5,
                                TermsCondition = company.TermsCondition,

                                CompanyId = user.CompanyId,
                                FullName = user.FirstName,
                                RoleName = role,
                                Token = token,
                                ImagePath = user.ImagePath,
                                UserId = user.Id,
                                UserName = user.Email,
                                LoginUserName = user.UserName,
                                EmployeeId = user.EmployeeId,
                                EmailConfirmed = user.EmailConfirmed,
                                PhoneNo = user.PhoneNumber,
                                UserRoleName = nobleRole.Name,
                                IsTouchInvoice = loginPermissions?.IsTouchInvoice,
                                TouchScreen = loginPermissions?.TouchScreen,
                                IsExpenseAccount = loginPermissions?.IsExpenseAccount ?? false,
                                AllowAll = loginPermissions?.AllowAll,
                                IsDayStart = dayStart,
                                IsExpenseDay = isExpenseDay,
                                WarehouseId = warehouseId,
                                IsSupervisor = loginPermissions?.IsSupervisor ?? false,
                                IsPermissionToStartDay = loginPermissions?.StartDay ?? false,
                                IsPermissionToCloseDay = loginPermissions?.CloseDay ?? false,
                                TransferCounter = loginPermissions?.TransferCounter ?? false,
                                NobleRole = String.IsNullOrEmpty(nobleRole.Name) ? "" : nobleRole.Name,
                                TerminalUserType = loginPermissions?.TerminalUserType.ToString(),
                                TerminalId = terminal?.Id,
                                BranchId = userBranch?.BranchId,
                                BranchName = userBranch?.Code + " " + userBranch?.BranchName,
                                MainBranch= userBranch?.MainBranch??false,
                            });

                        }

                        // Fetch System Mac Address
                        if (role != "Noble Admin")
                        {
                            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                            String sMacAddress = string.Empty;

                            foreach (NetworkInterface adapter in nics)
                            {
                                if (sMacAddress == String.Empty)
                                {
                                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                                    break;
                                }
                            }
                            var isNormalPrefix = await _context.CompanyPermissions.AnyAsync(x => x.Value == "NormalPrefix");

                            if (!isNormalPrefix)
                            {
                                if (loginPermissions != null)
                                {
                                    if (loginPermissions.TerminalUserType == TerminalUserType.Offline)
                                    {
                                        if (nobleRole.Name != "Admin")
                                        {
                                            terminal = await _context.Terminals.AsNoTracking().FirstOrDefaultAsync(x => x.MACAddress == sMacAddress && x.Id == user.TerminalId.Value);
                                            if (terminal != null)
                                            {
                                                user.TerminalId = terminal.Id;

                                            }
                                            if (terminal == null)
                                            {

                                                return Ok(new LoginModel()
                                                {
                                                    Token = token,
                                                    IsUseMachine = true,
                                                    CompanyId = user.CompanyId,
                                                    Expiration = "Please assign terminal to this system"
                                                });
                                            }
                                        }
                                        else
                                        {
                                            terminal = await _context.Terminals.AsNoTracking().FirstOrDefaultAsync(x => x.Id == user.TerminalId.Value);

                                        }
                                    }
                                    else if (loginPermissions.TerminalUserType == TerminalUserType.Online)
                                    {
                                        if (nobleRole.Name != "Admin")
                                        {
                                            terminal = await _context.Terminals.AsNoTracking().FirstOrDefaultAsync(x => x.MACAddress == sMacAddress && x.Id == user.OnlineTerminalId.Value);
                                            if (terminal != null)
                                            {
                                                user.TerminalId = terminal.Id;

                                            }
                                            if (terminal == null)
                                            {

                                                return Ok(new LoginModel()
                                                {
                                                    Token = token,
                                                    IsUseMachine = true,
                                                    CompanyId = user.CompanyId,
                                                    Expiration = "Please assign terminal to this system"
                                                });
                                            }
                                        }
                                        else
                                        {
                                            terminal = await _context.Terminals.AsNoTracking().FirstOrDefaultAsync(x => x.Id == user.OnlineTerminalId.Value);

                                        }
                                    }
                                    else
                                    {
                                        if (nobleRole.Name != "Admin")
                                        {
                                            if (user.TerminalId == null && user.OnlineTerminalId == null)
                                            {
                                                return Ok(new LoginModel()
                                                {
                                                    Token = token,
                                                    IsUseMachine = true,
                                                    CompanyId = user.CompanyId,
                                                    Expiration = "Please assign terminal to this system"
                                                });
                                            }
                                        }

                                        var localTerminal = await _context.Terminals.AsNoTracking().FirstOrDefaultAsync(x => x.MACAddress == sMacAddress && x.Id == user.TerminalId);
                                        if (localTerminal == null)
                                        {
                                            var onlineTerminal = await _context.Terminals.AsNoTracking().FirstOrDefaultAsync(x => x.MACAddress == sMacAddress && x.Id == user.OnlineTerminalId);
                                            if (onlineTerminal == null && nobleRole.Name != "Admin")
                                            {
                                                return Ok(new LoginModel()
                                                {
                                                    Token = token,
                                                    IsUseMachine = true,
                                                    CompanyId = user.CompanyId,
                                                    Expiration = "You do not have access to use this machine"
                                                });
                                            }
                                        }
                                        else
                                        {
                                            terminal = localTerminal;
                                        }
                                    }
                                }

                            }
                            //else if (user.TerminalId != null || user.OnlineTerminalId != null)
                            //{
                            //    var localTerminal = await _context.Terminals.AsNoTracking().FirstOrDefaultAsync(x => x.Id == user.TerminalId);
                            //    var onlineTerminal = await _context.Terminals.AsNoTracking().FirstOrDefaultAsync(x => x.Id == user.OnlineTerminalId);
                            //    if (loginPermissions != null)
                            //    {
                            //        if (localTerminal?.MACAddress == sMacAddress)
                            //        {
                            //            if (nobleRole.Name != "Admin")
                            //            {
                            //                return Ok(new LoginModel()
                            //                {
                            //                    Token = token,
                            //                    IsUseMachine = true,
                            //                    CompanyId = user.CompanyId,
                            //                    Expiration = "You do not have access to use this machine"
                            //                });
                            //            }
                            //        }

                            //        terminal = loginPermissions.TerminalUserType == TerminalUserType.Both ? (localTerminal?.MACAddress != sMacAddress ? onlineTerminal : localTerminal) : loginPermissions.TerminalUserType == TerminalUserType.Online ? onlineTerminal : localTerminal;
                            //    }
                            //}
                            else if (!isNormalPrefix && role != "Noble Admin" && nobleRole.Name != "Admin")
                            {
                                return Ok(new LoginModel()
                                {
                                    Token = token,
                                    IsUseMachine = true,
                                    CompanyId = user.CompanyId,
                                    Expiration = "You do not have access to use this machine"
                                });
                            }

                        }



                        businessNameArabic = terminal?.BusinessNameArabic;
                        businessNameEnglish = terminal?.BusinessNameEnglish;
                        businessTypeArabic = terminal?.BusinessTypeArabic;
                        businessTypeEnglish = terminal?.BusinessTypeEnglish;
                        companyNameArabic = terminal?.CompanyNameArabic;
                        companyNameEnglish = terminal?.CompanyNameEnglish;
                        businessLogo = terminal?.BusinessLogo;
                        overWrite = terminal?.OverWrite ?? false;
                        var onPageLoadItem = terminal?.OnPageLoadItem ?? false;




                        var printSetting = await Mediator.Send(new GetPrintSettingDetailQuery());
                        var defaultSetting = await _context.DefaultSettings.AsNoTracking().FirstOrDefaultAsync();
                        var defaultSettingModel = new DefaultSettingModel();
                        if (defaultSetting != null)
                        {
                            defaultSettingModel.IsCustomerPayCredit = defaultSetting.IsCustomerPayCredit;
                            defaultSettingModel.IsSupplierPayCredit = defaultSetting.IsSupplierPayCredit;
                            defaultSettingModel.IsCustomeCredit = defaultSetting.IsCustomeCredit;
                            defaultSettingModel.IsSupplierCredit = defaultSetting.IsSupplierCredit;
                            defaultSettingModel.IsSaleCredit = defaultSetting.IsSaleCredit;
                            defaultSettingModel.IsPurchaseCredit = defaultSetting.IsPurchaseCredit;
                            defaultSettingModel.IsCashCustomer = defaultSetting.IsCashCustomer;
                        }


                        return Ok(new LoginModel()
                        {
                            BusinessLogo = businessLogo,
                            OverWrite = overWrite,
                            BusinessNameArabic = businessNameArabic,
                            BusinessNameEnglish = businessNameEnglish,
                            BusinessTypeArabic = businessTypeArabic,
                            BusinessTypeEnglish = businessTypeEnglish,
                            CompanyNameArabic = companyNameArabic,
                            CompanyNameEnglish = companyNameEnglish,
                            CompanyOptions = companyOptions,
                            OnPageLoadItem = onPageLoadItem,
                            DayStartTime = dayStartTime,
                            Expiration = expiration,
                            IsPayment = isPayment,
                            IsNotPayment = isNotPayment,

                            CounterId = userCounterId ?? Guid.Empty,

                            IsProceed = company.IsProceed,
                            Step1 = company.Step1,
                            Step2 = company.Step2,
                            Step3 = company.Step3,
                            Step4 = company.Step4,
                            Step5 = company.Step5,
                            TermsCondition = company.TermsCondition,

                            CompanyId = user.CompanyId,
                            FullName = user.FirstName,
                            RoleName = role,
                            Token = token,
                            ImagePath = user.ImagePath,
                            UserId = user.Id,
                            UserName = user.Email,
                            LoginUserName = user.UserName,
                            EmployeeId = user.EmployeeId,
                            EmailConfirmed = user.EmailConfirmed,
                            PhoneNo = user.PhoneNumber,
                            UserRoleName = nobleRole.Name,
                            IsTouchInvoice = loginPermissions?.IsTouchInvoice,
                            TouchScreen = loginPermissions?.TouchScreen,
                            IsExpenseAccount = loginPermissions?.IsExpenseAccount ?? false,
                            AllowAll = loginPermissions?.AllowAll,
                            IsDayStart = dayStart,
                            IsExpenseDay = isExpenseDay,
                            InvoicePrint = printSetting?.InvoicePrint,
                            PrintSizeA4 = printSetting?.PrintSize,
                            TermsInAr = printSetting?.TermsInAr,
                            TermsInEng = printSetting?.TermsInEng,
                            ReturnDays = printSetting?.ReturnDays,
                            CashAccountId = printSetting?.CashAccountId,
                            BankAccountId = printSetting?.BankAccountId,
                            PrinterName = terminal == null ? (printSetting?.IsBlindPrint == true ? printSetting?.PrinterName : "") : terminal.PrinterName,
                            IsHeaderFooter = printSetting?.IsHeaderFooter ?? false,
                            IsBlindPrint = printSetting?.IsBlindPrint ?? false,
                            AutoPaymentVoucher = printSetting?.AutoPaymentVoucher ?? false,
                            IsDeliveryNote = printSetting?.IsDeliveryNote ?? false,
                            TermAndConditionLength = printSetting?.TermAndConditionLength ?? false,
                            PrintTemplate = printSetting?.PrintTemplate,
                            //IsForMedical = company.IsForMedical,
                            WarehouseId = warehouseId,
                            IsSupervisor = loginPermissions?.IsSupervisor ?? false,
                            IsPermissionToStartDay = loginPermissions?.StartDay ?? false,
                            IsPermissionToCloseDay = loginPermissions?.CloseDay ?? false,
                            TransferCounter = loginPermissions?.TransferCounter ?? false,
                            NobleRole = String.IsNullOrEmpty(nobleRole.Name) ? "" : nobleRole.Name,
                            DefaultSettingModel = defaultSettingModel,
                            TerminalUserType = loginPermissions?.TerminalUserType.ToString(),
                            TerminalId = terminal?.Id,
                            BranchId = userBranch?.BranchId,
                            BranchName = userBranch?.Code + " " + userBranch?.BranchName,
                            MainBranch = userBranch?.MainBranch ?? false,
                        });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return Ok(new LoginModel());
                    }
                }
                catch (Exception exception)
                {
                    return Ok(null);
                }

            }
            return Ok(null);
        }

        [Route("api/account/GetWhiteLabeling")]
        [HttpGet("GetWhiteLabeling")]
        [AllowAnonymous]
        public async Task<IActionResult> GetWhiteLabeling()
        {
            var labelData = await _context.WhiteLabelings.AsNoTracking().FirstOrDefaultAsync();
            var lang = _configuration.GetSection("Portuguese").Value == "True";
            if (labelData == null)
            {
                var label = new WhiteLabelLookUp()
                {
                    Heading = "ERP FOR SMALL & MEDIUM ENTERPRISE",
                    Description = "Handle All your Needs & Automate Business Process",
                    CompanyName = "TechoQode (Pvt) Ltd.",
                    ApplicationName = "NOBLEPOS",
                    Portuguese = lang
                };
                return Ok(label);
            }
            else
            {
                var lookUp = new WhiteLabelLookUp()
                {
                    Heading = labelData.Heading,
                    Description = labelData.Description,
                    CompanyName = labelData.CompanyName,
                    ApplicationName = labelData.ApplicationName,
                    AddressLine1 = labelData.AddressLine1,
                    AddressLine2 = labelData.AddressLine2,
                    AddressLine3 = labelData.AddressLine3,
                    Email = labelData.Email,
                    FavName = labelData.FavName,
                    Portuguese = lang
                };
                return Ok(lookUp);
            }
        }



        [Route("api/account/ImpersonateUser")]
        [HttpGet("ImpersonateUser")]
        [Authorize(Roles = "Noble Admin")]
        public async Task<IActionResult> ImpersonateUser(String userId)
        {
            var currentUserId = User.Identity.UserId();

            var impersonatedUser = await _userManager.FindByIdAsync(userId);

            var userPrincipal = await _signInManager.CreateUserPrincipalAsync(impersonatedUser);

            userPrincipal.Identities.First().AddClaim(new Claim("OriginalUserId", currentUserId));
            userPrincipal.Identities.First().AddClaim(new Claim("IsImpersonating", "true"));


            // sign out the current user
            await _signInManager.SignOutAsync();

            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, userPrincipal);

            return Ok(true);
        }
        [Route("api/account/ImpersonateUser")]
        [HttpGet("ImpersonateUser")]
        [Authorize(Roles = "Noble Admin")]
        public async Task<IActionResult> GetComapnyUser(String userId)
        {
            var currentUserId = User.Identity.UserId();

            var impersonatedUser = await _userManager.FindByIdAsync(userId);

            var userPrincipal = await _signInManager.CreateUserPrincipalAsync(impersonatedUser);

            userPrincipal.Identities.First().AddClaim(new Claim("OriginalUserId", currentUserId));
            userPrincipal.Identities.First().AddClaim(new Claim("IsImpersonating", "true"));


            // sign out the current user
            await _signInManager.SignOutAsync();

            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, userPrincipal);

            return Ok(true);
        }
        [Route("api/account/GetCompanySetupAccount")]
        [HttpGet("GetCompanySetupAccount")]
        public async Task<IActionResult> GetCompanySetupAccount()
        {
            Currency currencyId = null;
            var companyAccount = await _context.CompanyAccountSetups.AsNoTracking().FirstOrDefaultAsync();
            var printSetting = await _context.PrintSettings.AsNoTracking().FirstOrDefaultAsync();
            if (companyAccount != null) currencyId = await _context.Currencies.AsNoTracking().FirstOrDefaultAsync(x => x.Name == companyAccount.CurrencyId);
            if (currencyId == null)
            {
                return Ok(null);
            }
            return Ok(new { Currency = currencyId.Sign, CurrencyArabic=currencyId.ArabicSign, printSetting?.TaxMethod, printSetting?.TaxRateId, printSetting?.DiscountTypeOption });
        }
        [Route("api/account/DuplicateEmail")]
        [HttpGet("DuplicateEmail")]
        public async Task<IActionResult> DuplicateEmail(string email)
        {
            var duplicate = await _userManager.FindByEmailAsync(email);
            if (duplicate == null)
            {
                return Ok(false);
            }
            return Ok(true);
        }

        [Route("api/account/GetUserPermission")]
        [HttpGet("GetUserPermission")]
        public async Task<IActionResult> GetUserPermission(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || id == "null")
                {
                    return Ok(null);
                }

                var permission = await _context.LoginPermissions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.UserId == Guid.Parse(id));

                if (permission == null)
                {
                    return Ok(null);
                }

                return Ok(new
                {
                    permission.UserId,
                    permission.ChangePriceDuringSale,
                    permission.GiveDicountDuringSale,
                    permission.ViewCounterDetails,
                    permission.TransferCounter,
                    permission.CloseCounter,
                    permission.HoldCounter,
                    permission.CloseDay,
                    permission.StartDay,
                    permission.ProcessSaleReturn,
                    permission.DailyExpenseList,
                    permission.InvoiceWoInventory,
                    permission.IsTouchInvoice,
                    permission.AllowAll,
                    permission.PermissionToStartExpenseDay,
                    permission.IsSupervisor,
                    permission.IsInquiryHandler,
                    permission.TouchScreen,
                    permission.TemporaryCashReceiver,
                    permission.TemporaryCashIssuer,
                    permission.TemporaryCashRequester,
                    permission.IsExpenseAccount,
                    permission.AllowViewAllData,
                    permission.Days,
                    permission.Limit,
                    permission.TerminalUserType,
                    permission.IsOverAllAccess,
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        [Route("api/account/AuthenticatorCode")]
        [HttpGet("AuthenticatorCode")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticatorCode(string email, string password, string code, string recoveryCode)

        {


            var userWith2Fa = await _userManager.FindByEmailAsync(email);

            if (userWith2Fa == null)
            {
                return Ok(false);
            }
            if (!ModelState.IsValid)
            {
                return Ok(null);
            }
            // Strip spaces and hypens
            //if (recoveryCode != stri)
            //{
            //    var codeRecovery = recoveryCode.Replace(" ", string.Empty);
            //    var ok = await _signInManager.TwoFactorRecoveryCodeSignInAsync(codeRecovery);
            //    if (!ok.Succeeded)
            //        return Ok(false);
            //}
            {


                var verificationCode = code.Replace(" ", string.Empty).Replace("-", string.Empty);
                var is2faTokenValid = await _userManager.VerifyTwoFactorTokenAsync(
                    userWith2Fa, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);
                if (!is2faTokenValid)
                {

                    return Ok(false);
                }
            }

            userWith2Fa.TwoFactorEnabled = false;
            var result = await _signInManager.PasswordSignInAsync(email, password, true, lockoutOnFailure: false);
            if (result.RequiresTwoFactor)
            {

                return Ok(true);
            }
            userWith2Fa.TwoFactorEnabled = true;
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(email);
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == user.Email);
                var token = await GenerateJwtToken(appUser);
                var userCompany = _companyComponent.GetCompanyById(user.CompanyId);
                var role = await _userComponent.GetRoleByUser(user.Id);

                return Ok(new LoginModel()
                {
                    CompanyId = userCompany.Id,
                    FullName = user.FirstName,
                    RoleName = role,
                    Token = token,
                    ImagePath = user.ImagePath,
                    UserId = user.Id,
                    UserName = user.Email,
                    EmployeeId = user.EmployeeId,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    EmailConfirmed = user.EmailConfirmed,
                });
            }

            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Ok(new LoginModel());
            }

        }
        [Route("api/account/Allow2Factor")]
        [HttpGet("Allow2Factor")]
        [Roles("User", "Super Admin", "Admin", "Noble Admin", "Mobile Customer")]
        public async Task<IActionResult> Allow2Factor(string id)
        {
            var applicationUser = await _userManager.FindByIdAsync(id);
            if (applicationUser == null)
            {
                return Ok(false);
            }
            var disable2FaResult = await _userManager.SetTwoFactorEnabledAsync(applicationUser, false);
            if (disable2FaResult.Succeeded)
            {
                return Ok(true);
            }
            return Ok(false);

        }

        [Route("api/account/ResetAuthenticator")]
        [HttpGet("ResetAuthenticator")]
        [Roles("User", "Super Admin", "Admin", "Noble Admin", "Mobile Customer")]
        public async Task<IActionResult> ResetAuthenticator(string id)
        {
            var applicationUser = await _userManager.FindByIdAsync(id);
            if (applicationUser == null)
            {
                return Ok(false);
            }
            await _userManager.SetTwoFactorEnabledAsync(applicationUser, false);
            await _userManager.ResetAuthenticatorKeyAsync(applicationUser);
            await _signInManager.RefreshSignInAsync(applicationUser);
            return Ok(true);

        }

        [Route("api/account/Disable2Fa")]
        [HttpGet("Disable2Fa")]
        [Roles("User", "Super Admin", "Admin", "Noble Admin", "Mobile Customer")]

        public async Task<IActionResult> Disable2Fa(string id)
        {
            var applicationUser = await _userManager.FindByIdAsync(id);
            applicationUser.TwoFactorEnabled = false;

            IdentityResult result = await _userManager.UpdateAsync(applicationUser);
            if (result.Succeeded)
            { return Ok(true); }

            return Ok(false);

        }



        [Route("api/account/LoadSharedKeyAndQrCodeUriAsync")]
        [HttpGet("LoadSharedKeyAndQrCodeUriAsync")]
        [Roles("User", "Super Admin", "Admin", "Noble Admin", "Mobile Customer")]

        public async Task<IActionResult> LoadSharedKeyAndQrCodeUriAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            // Load the authenticator key & QR code URI to display on the form
            var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(unformattedKey))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }
            SharedKey = FormatKey(unformattedKey);
            var email = await _userManager.GetEmailAsync(user);
            var carAuthenticatorUri = GenerateQrCodeUri(email, unformattedKey);
            return Ok(carAuthenticatorUri);
        }
        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }
            return result.ToString().ToLowerInvariant();
        }
        private string GenerateQrCodeUri(string email, string unformattedKey)
        {
            return string.Format(
                AuthenticatorUriFormat,
                _utEncoder.Encode("Noble"),
                _utEncoder.Encode(email),
                unformattedKey);

        }


        [Route("api/account/OnPostAsync")]
        [HttpGet("OnPostAsync")]
        [Roles("User", "Super Admin", "Admin", "Noble Admin", "Mobile Customer")]

        public async Task<IActionResult> OnPostAsync(string id, string code)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (!ModelState.IsValid)
            {
                return Ok(null);
            }
            // Strip spaces and hypens
            var verificationCode = code.Replace(" ", string.Empty).Replace("-", string.Empty);
            var is2faTokenValid = await _userManager.VerifyTwoFactorTokenAsync(
                user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);
            if (!is2faTokenValid)
            {
                ModelState.AddModelError("Input.Code", "Verification code is invalid.");
                return Ok(false);
            }
            await _userManager.SetTwoFactorEnabledAsync(user, true);
            var userId = await _userManager.GetUserIdAsync(user);
            var a = _userManager.CountRecoveryCodesAsync(user);
            if (await _userManager.CountRecoveryCodesAsync(user) == 0)
            {
                var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
                RecoveryCodes = recoveryCodes.ToArray();
                return Ok(RecoveryCodes);
            }

            else
            {
                var userData = await _userManager.FindByIdAsync(id);
                if (userData == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(userData);
                var userIdSearch = await _userManager.GetUserIdAsync(userData);
                if (!isTwoFactorEnabled)
                {
                    throw new InvalidOperationException($"Cannot generate recovery codes for user with ID '{userIdSearch}' as they do not have 2FA enabled.");
                }

                var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
                RecoveryCodes = recoveryCodes.ToArray();

                return Ok(RecoveryCodes);

            }
        }

        private async Task<object> GenerateJwtToken(ApplicationUser user)
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                identity.AddClaim(new Claim("UserId", user.Id));
                identity.AddClaim(new Claim("CompanyId", user.CompanyId.ToString()));
            }

            var token = await Mediator.Send(new UpdateClaimsCommand
            {
                User = user.Id,
                ApplicationUser = user,

            });
            return token;
        }
        [Route("api/account/logout")]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            var result = "Success";
            return Ok(result);
        }

        [Route("api/account/UserList")]
        [HttpGet("UserList")]
        [Roles("User", "Noble Admin")]
        public IActionResult UserList()
        {
            try
            {
                var listUsers = _userComponent.ListingUsers();
                return Ok(listUsers);
            }
            catch (Exception e)
            {


                throw new ApplicationException("List Error" + e);
            }
        }
        [Route("api/account/ForRoleUsersList")]
        [HttpGet("ForRoleUsersList")]
        [Roles("User", "Noble Admin")]

        public IActionResult ForRoleUsersList(bool isHistory, bool istransferTerminal, bool isSupervisorTerminal, bool alluser)
        {
            try
            {
                if (alluser)
                {
                    var listUsers = _userComponent.ListingUsers();
                    return Ok(listUsers);
                }
                else
                {
                    var listUsers = _userComponent.ForRoleUsersList(isHistory, istransferTerminal, isSupervisorTerminal);
                    return Ok(listUsers);
                }

            }
            catch (Exception e)
            {


                throw new ApplicationException("List Error" + e);
            }
        }
        [Route("api/account/UserDetail")]
        [HttpGet("UserDetail")]
        [Roles("CanAddSignUpUser", "CanEditSignUpUser")]

        public async Task<IActionResult> UserDetail(Guid id)
        {
            try
            {
                var listUsers = _context.LoginPermissions.FirstOrDefault(x => x.UserId == id);
                var user = await _userManager.FindByIdAsync(id.ToString());
                var nobleUserRoles = _context.NobleUserRoles.AsNoTracking().FirstOrDefault(x => x.UserId == user.Id);
                if (listUsers != null)
                {


                    var loginUser = new LoginVm
                    {
                        Id = listUsers == null ? user.Id : listUsers.Id.ToString(),
                        UserId = Guid.Parse(user.Id),
                        BankId = user.BankId,
                        DocumentType = user.DocumentType,
                        FirstName = user.FirstName,
                        IsActive = user.IsActive,
                        IsSupervisor = listUsers.IsSupervisor,
                        LastName = user.LastName,
                        RoleId = nobleUserRoles?.RoleId,
                        Email = user.Email,
                        UserName = user.UserName,
                        ChangePriceDuringSale = listUsers.ChangePriceDuringSale,
                        IsExpenseAccount = listUsers.IsExpenseAccount,
                        CloseCounter = listUsers.CloseCounter,
                        CloseDay = listUsers.CloseDay,
                        StartDay = listUsers.StartDay,
                        GiveDicountDuringSale = listUsers.GiveDicountDuringSale,
                        TransferCounter = listUsers.TransferCounter,
                        HoldCounter = listUsers.HoldCounter,
                        DailyExpenseList = listUsers.DailyExpenseList,
                        ProcessSaleReturn = listUsers.ProcessSaleReturn,
                        ViewCounterDetails = listUsers.ViewCounterDetails,
                        InvoiceWoInventory = listUsers.InvoiceWoInventory,
                        IsTouchInvoice = listUsers.IsTouchInvoice,
                        TouchScreen = listUsers.TouchScreen,
                        AllowAll = listUsers.AllowAll,
                        PermissionToStartExpenseDay = listUsers.PermissionToStartExpenseDay,
                        TerminalId = user.TerminalId,
                        OnlineTerminalId = user.OnlineTerminalId,
                        TemporaryCashReceiver = listUsers.TemporaryCashReceiver,
                        TemporaryCashIssuer = listUsers.TemporaryCashIssuer,
                        TemporaryCashRequester = listUsers.TemporaryCashRequester,
                        Days = listUsers.Days,
                        Limit = listUsers.Limit,
                        AllowViewAllData = listUsers.AllowViewAllData,
                        TerminalUserType = listUsers.TerminalUserType.ToString(),
                        IsOverAllAccess = listUsers.IsOverAllAccess,
                    };
                    return Ok(loginUser);
                }
                var users = new LoginVm
                {
                    Id = listUsers == null ? user.Id : listUsers.Id.ToString(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                    OnlineTerminalId = user.OnlineTerminalId,
                    UserId = Guid.Parse(user.Id)
                };
                return Ok(users);

            }
            catch (Exception e)
            {
                throw new ApplicationException("List Error" + e);
            }
        }





        [Route("api/account/SaveMobileCustomer")]
        [HttpPost("SaveMobileCustomer")]
        [AllowAnonymous]
        public async Task<IActionResult> SaveMobileCustomer([FromBody] LoginVm loginVm)
        {
            if (loginVm.Id == "00000000-0000-0000-0000-000000000000" || loginVm.Id == null)
            {

                var isloginEmailExist = await _userManager.FindByEmailAsync(loginVm.Email);
                if (isloginEmailExist == null)
                {
                    var register = new UserDetailDto()
                    {
                        FirstName = loginVm.FirstName,
                        LastName = loginVm.LastName,
                        Password = loginVm.Password,
                        Email = loginVm.Email,
                        PhoneNumber = loginVm.PhoneNumber,
                        CompanyId = loginVm.CompanyId
                    };
                    var user = new ApplicationUser
                    {
                        UserName = register.Email,
                        Email = register.Email,
                        EmployeeId = loginVm.EmployeeId,
                        CompanyId = register.CompanyId.Value
                    };
                    var result = await _userManager.CreateAsync(user, register.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, register.RoleName = "Mobile Customer");
                        var claims = new List<Claim>
                        {   new Claim("Email",user.Email),
                            //new Claim("FullName",null),
                            //new Claim("Organization",null),
                            new Claim("CompanyId",loginVm.CompanyId.ToString()),
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
                        return Ok(new { value = true, check = "Add" });
                    }
                }
            }

            return Ok(new { value = false, check = "Already Exists" });
        }
        //[Route("api/account/UserDelete")]
        //[HttpGet("UserDelete")]
        //[Roles("Can Delete  Sign up User", "Noble Admin")]

        //public async Task<IActionResult> UserDelete(Guid id)
        //{
        //    try
        //    {
        //        var listUsers = _context.LoginPermissions.FirstOrDefault(x => x.UserId == id);
        //        if (listUsers != null)
        //        {
        //            _context.LoginPermissions.Remove(listUsers);
        //            await _context.SaveChangesAsync();

        //            return Ok(listUsers);
        //        }
        //        return Ok(listUsers);
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new ApplicationException("List Error");
        //    }
        //}


        [Route("api/account/SaveUser")]
        [HttpPost("SaveUser")]
        //[Roles("CanEditSignUpUser", "CanAddSignUpUser")]
        public async Task<IActionResult> SaveUser([FromBody] LoginVm loginVm)
        {
            try
            {
                if (loginVm.Id == "00000000-0000-0000-0000-000000000000")
                {
                    //var loginEmail = await _context.LoginPermissions.FindAsync(loginVm.Email);

                    var isloginEmailExist = await _userManager.FindByEmailAsync(loginVm.Email);
                    var isloginUserIdExist = await _userManager.FindByNameAsync(loginVm.UserName);
                    if (isloginEmailExist == null && isloginUserIdExist == null)
                    {
                        var userForCode = _userManager.Users.OrderBy(x => x.Code).LastOrDefault();
                        string code = null;
                        if (userForCode != null)
                        {
                            if (string.IsNullOrEmpty(userForCode.Code))
                            {
                                code = "U" + "-" + "00001";
                            }
                            else
                            {
                                string fetchNo = userForCode.Code.Substring(userForCode.Code.Length - 5);
                                //fetchNo =  fetchNo.Substring(fetchNo.Length-5);
                                Int32 autoNo = Convert.ToInt32((fetchNo));
                                var format = "00000";
                                autoNo++;
                                code = "U" + "-" + autoNo.ToString(format);
                            }
                        }

                        var register = new UserDetailDto()
                        {
                            FirstName = loginVm.FirstName,
                            LastName = loginVm.LastName,
                            Password = loginVm.Password,
                            Email = loginVm.Email,
                            UserId = loginVm.UserName,
                            PhoneNumber = loginVm.PhoneNumber,
                            IsActive = loginVm.IsActive,
                            TerminalId = loginVm.TerminalId,
                            OnlineTerminalId = loginVm.OnlineTerminalId,
                            BankId = loginVm.BankId,
                            DocumentType = loginVm.DocumentType,
                            Code = code,
                        };

                        var user = new ApplicationUser
                        {
                            UserName = register.UserId,
                            Email = register.Email,
                            TerminalId = register.TerminalId,
                            FirstName = register.FirstName,
                            LastName = register.LastName,
                            EmployeeId = loginVm.EmployeeId,
                            CompanyId = User.Identity.CompanyId(),
                            IsActive = register.IsActive,
                            OnlineTerminalId = register.OnlineTerminalId,
                            BankId = register.BankId,
                            DocumentType = register.DocumentType,
                            Code = register.Code
                        };



                        var permissions = new LoginPermissions
                        {
                            UserId = Guid.Parse(user.Id),
                            IsExpenseAccount = loginVm.IsExpenseAccount,
                            ChangePriceDuringSale = loginVm.ChangePriceDuringSale,
                            GiveDicountDuringSale = loginVm.GiveDicountDuringSale,
                            ViewCounterDetails = loginVm.ViewCounterDetails,
                            TransferCounter = loginVm.TransferCounter,
                            CloseCounter = loginVm.CloseCounter,
                            HoldCounter = loginVm.HoldCounter,
                            StartDay = loginVm.StartDay,
                            CloseDay = loginVm.CloseDay,
                            ProcessSaleReturn = loginVm.ProcessSaleReturn,
                            DailyExpenseList = loginVm.DailyExpenseList,
                            InvoiceWoInventory = loginVm.InvoiceWoInventory,
                            IsTouchInvoice = loginVm.IsTouchInvoice,
                            TouchScreen = loginVm.TouchScreen,
                            AllowAll = loginVm.AllowAll,
                            PermissionToStartExpenseDay = loginVm.PermissionToStartExpenseDay,
                            TemporaryCashReceiver = loginVm.TemporaryCashReceiver,
                            TemporaryCashIssuer = loginVm.TemporaryCashIssuer,
                            TemporaryCashRequester = loginVm.TemporaryCashRequester,
                            Days = loginVm.Days,
                            Limit = loginVm.Limit,
                            AllowViewAllData = loginVm.AllowViewAllData,
                            TerminalUserType = (TerminalUserType)Enum.Parse(typeof(TerminalUserType), loginVm.TerminalUserType),
                            IsOverAllAccess = loginVm.IsOverAllAccess
                        };

                        var result = await _userManager.CreateAsync(user, register.Password);
                        await _context.LoginPermissions.AddAsync(permissions);

                        // Sync Log
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }
                        var log = _context.SyncLog();
                        var branches = await _context.Branches.AsNoTracking().ToListAsync();
                        var list = new List<SyncRecordModel>();
                        foreach (var branch in branches)
                        {
                            var syncRecords = log.Select(x => new SyncRecordModel
                            {
                                Table = x.Table,
                                ColumnId = x.ColumnId,
                                CompanyId = x.CompanyId,
                                ColumnType = x.ColumnType,
                                Push = x.Push,
                                Pull = x.Pull,
                                Action = x.Action,
                                ChangeDate = DateTime.Now,
                                PullDate = x.PullDate,
                                PushDate = x.PushDate,
                                ColumnName = x.ColumnName,
                                BranchId = branch.Id,
                                Code = _code,
                            }).ToList();
                            list.AddRange(syncRecords);

                            list.Add(new SyncRecordModel()
                            {
                                Table = "AspNetUsers",
                                ColumnId = user.Id,
                                CompanyId = User.Identity.CompanyId(),
                                ColumnType = "uniqueidentifier",
                                Action = "Insert",
                                ChangeDate = DateTime.Now,
                                ColumnName = "Id",
                                BranchId = branch.Id,
                                Code = _code,
                            });
                        }
                        await _context.SaveChangesAsync();


                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, register.RoleName = "User");
                            var claims = new List<Claim>
                        {
                            new Claim("Email",user.Email),
                            new Claim("FullName",$"{user.FirstName}{user.LastName}"),
                            new Claim("Organization",_principal.Identity.Organization()),
                            new Claim("CompanyId",_principal.Identity.CompanyId().ToString()),
                            new Claim("NobleCompanyId",_principal.Identity.CompanyId().ToString()),
                            new Claim("ChangePriceDuringSale",loginVm.ChangePriceDuringSale.ToString(), ClaimValueTypes.Boolean),
                            new Claim("IsExpenseAccount",loginVm.IsExpenseAccount.ToString(), ClaimValueTypes.Boolean),
                            new Claim("GiveDicountDuringSale",loginVm.GiveDicountDuringSale.ToString(), ClaimValueTypes.Boolean),
                            new Claim("ViewCounterDetails",loginVm.ViewCounterDetails.ToString(), ClaimValueTypes.Boolean),
                            new Claim("TransferCounter",loginVm.TransferCounter.ToString(), ClaimValueTypes.Boolean),
                            new Claim("CloseCounter",loginVm.CloseCounter.ToString(), ClaimValueTypes.Boolean),
                            new Claim("HoldCounter",loginVm.HoldCounter.ToString(), ClaimValueTypes.Boolean),
                            new Claim("CloseDay",loginVm.CloseDay.ToString(), ClaimValueTypes.Boolean),
                            new Claim("ProcessSaleReturn",loginVm.ProcessSaleReturn.ToString(), ClaimValueTypes.Boolean),
                            new Claim("DailyExpenseList",loginVm.DailyExpenseList.ToString(), ClaimValueTypes.Boolean),
                            new Claim("InvoiceWoInventory",loginVm.InvoiceWoInventory.ToString(), ClaimValueTypes.Boolean),
                            new Claim("IsTouchInvoice",loginVm.IsTouchInvoice.ToString(), ClaimValueTypes.Boolean),
                            //new Claim("TouchScreen",loginVm.TouchScreen),
                            new Claim("AllowAll",loginVm.AllowAll.ToString(), ClaimValueTypes.Boolean),
                            new Claim("PermissionToStartExpenseDay",loginVm.PermissionToStartExpenseDay.ToString(), ClaimValueTypes.Boolean),
                        };

                            await _userManager.AddClaimsAsync(user, claims);
                            await using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                            var claimList = conn.Query<string>("select Id from AspNetUserClaims where UserId='"+user.Id+"';").ToList();
                            conn.Close();
                            foreach (var branch in branches)
                            {
                                list.AddRange(claimList.Select(x => new SyncRecordModel
                                {
                                    Table = "AspNetUserClaims",
                                    ColumnId = x,
                                    CompanyId = User.Identity.CompanyId(),
                                    ColumnType = "int",
                                    Action = "Insert",
                                    ChangeDate = DateTime.Now,
                                    ColumnName = "Id",
                                    BranchId = branch.Id,
                                    Code = _code,
                                }).ToList());


                                list.Add(new SyncRecordModel()
                                {
                                    Table = "AspNetUserRoles",
                                    ColumnId = user.Id,
                                    CompanyId = User.Identity.CompanyId(),
                                    ColumnType = "uniqueidentifier",
                                    Action = "Insert",
                                    ChangeDate = DateTime.Now,
                                    ColumnName = "UserId",
                                    BranchId = branch.Id,
                                    Code = _code,
                                });
                            }
                            await Mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = list,
                                IsServer=true,
                                
                            });


                            await Mediator.Send(new AddUpdateNobleUserRoleCommand
                            {
                                UserId = user.Id,
                                RoleId = loginVm.RoleId.Value,
                                IsActive = true
                            });

                            await _context.SaveChangesAsync();
                            return Ok(new { value = true, check = "Add" });
                        }
                    }
                }
                else
                {
                    var loginPermissions = _context.LoginPermissions.FirstOrDefault(x => x.Id == Guid.Parse(loginVm.Id));

                    var currentUser = await _userManager.FindByIdAsync(loginVm.UserId.ToString());

                    await Mediator.Send(new RemoveUserRoleQuery
                    {
                        UserId = currentUser.Id

                    });
                    await Mediator.Send(new AddUpdateNobleUserRoleCommand
                    {
                        UserId = currentUser.Id,
                        RoleId = loginVm.RoleId.Value,
                        IsActive = true
                    });
                    currentUser.UserName = loginVm.UserName;
                    currentUser.FirstName = loginVm.UserName;
                    currentUser.NormalizedUserName = loginVm.UserName.ToUpper();
                    currentUser.IsActive = loginVm.IsActive;
                    currentUser.TerminalId = loginVm.TerminalId;
                    currentUser.DocumentType = loginVm.DocumentType;
                    currentUser.BankId = loginVm.BankId;
                    currentUser.OnlineTerminalId = loginVm.OnlineTerminalId;
                    await _userManager.UpdateAsync(currentUser);

                    if ((currentUser.EmployeeId == Guid.Empty || currentUser.EmployeeId == null) && !_context.Accounts.Any(x => x.Name == loginVm.FirstName))
                    {
                        var accounts = await _context.Accounts
                              .AsNoTracking()
                              .Include(x => x.CostCenter)
                              .Where(x => x.CostCenter.Code == "126000" ||
                                          x.CostCenter.Code == "603001").ToListAsync();
                        var accEmp = accounts.MaxBy(x => x.CostCenter.Code == "126000");
                        var accPay = accounts.MaxBy(x => x.CostCenter.Code == "603001");

                        if (accEmp == null || accPay == null)
                            throw new ApplicationException("COA is not created yet.");

                        var accountList = new List<Account>()
                            {
                                new Account()
                                {
                                    IsActive = true,
                                    Name = loginVm.FirstName,
                                    Code = (Convert.ToInt64(accEmp.Code) + 1).ToString(),
                                    CostCenterId = accEmp.CostCenterId,
                                },

                                new Account()
                                {
                                    IsActive = true,
                                    Name = loginVm.FirstName,
                                    Code = (Convert.ToInt64(accPay.Code) + 1).ToString(),
                                    CostCenterId = accPay.CostCenterId,
                                }
                            };

                        await _context.Accounts.AddRangeAsync(accountList);
                    }


                    if (loginPermissions == null)
                    {
                        var permissions = new LoginPermissions
                        {
                            UserId = Guid.Parse(loginVm.Id),
                            ChangePriceDuringSale = loginVm.ChangePriceDuringSale,
                            IsExpenseAccount = loginVm.IsExpenseAccount,
                            GiveDicountDuringSale = loginVm.GiveDicountDuringSale,
                            ViewCounterDetails = loginVm.ViewCounterDetails,
                            TransferCounter = loginVm.TransferCounter,
                            CloseCounter = loginVm.CloseCounter,
                            HoldCounter = loginVm.HoldCounter,
                            StartDay = loginVm.StartDay,
                            CloseDay = loginVm.CloseDay,
                            ProcessSaleReturn = loginVm.ProcessSaleReturn,
                            DailyExpenseList = loginVm.DailyExpenseList,
                            InvoiceWoInventory = loginVm.InvoiceWoInventory,
                            IsTouchInvoice = loginVm.IsTouchInvoice,
                            TouchScreen = loginVm.TouchScreen,
                            AllowAll = loginVm.AllowAll,
                            IsSupervisor = loginVm.IsSupervisor,
                            PermissionToStartExpenseDay = loginVm.PermissionToStartExpenseDay,
                            TemporaryCashReceiver = loginVm.TemporaryCashReceiver,
                            TemporaryCashIssuer = loginVm.TemporaryCashIssuer,
                            TemporaryCashRequester = loginVm.TemporaryCashRequester,
                            Days = loginVm.Days,
                            Limit = loginVm.Limit,
                            AllowViewAllData = loginVm.AllowViewAllData,
                            TerminalUserType = (TerminalUserType)Enum.Parse(typeof(TerminalUserType), loginVm.TerminalUserType),
                            IsOverAllAccess = loginVm.IsOverAllAccess
                        };
                        await _context.LoginPermissions.AddAsync(permissions);
                        var user = await _userManager.FindByIdAsync(loginVm.UserId.ToString());
                        var claims = new List<Claim>
                        {
                            new Claim("ChangePriceDuringSale",loginVm.ChangePriceDuringSale.ToString(), ClaimValueTypes.Boolean),
                            new Claim("IsExpenseAccount",loginVm.IsExpenseAccount.ToString(), ClaimValueTypes.Boolean),
                            new Claim("GiveDicountDuringSale",loginVm.GiveDicountDuringSale.ToString(), ClaimValueTypes.Boolean),
                            new Claim("ViewCounterDetails",loginVm.ViewCounterDetails.ToString(), ClaimValueTypes.Boolean),
                            new Claim("TransferCounter",loginVm.TransferCounter.ToString(), ClaimValueTypes.Boolean),
                            new Claim("CloseCounter",loginVm.CloseCounter.ToString(), ClaimValueTypes.Boolean),
                            new Claim("IsSupervisor",loginVm.IsSupervisor.ToString(), ClaimValueTypes.Boolean),
                            new Claim("HoldCounter",loginVm.HoldCounter.ToString(), ClaimValueTypes.Boolean),
                                    new Claim("StartDay",loginVm.StartDay.ToString(), ClaimValueTypes.Boolean),
                            new Claim("CloseDay",loginVm.CloseDay.ToString(), ClaimValueTypes.Boolean),
                            new Claim("ProcessSaleReturn",loginVm.ProcessSaleReturn.ToString(), ClaimValueTypes.Boolean),
                            new Claim("DailyExpenseList",loginVm.DailyExpenseList.ToString(), ClaimValueTypes.Boolean),
                                    new Claim("InvoiceWoInventory",loginVm.InvoiceWoInventory.ToString(), ClaimValueTypes.Boolean),
                                    new Claim("IsTouchInvoice",loginVm.IsTouchInvoice.ToString(), ClaimValueTypes.Boolean),
                                    //new Claim("TouchScreen",loginVm.TouchScreen),
                                    new Claim("AllowAll",loginVm.AllowAll.ToString(), ClaimValueTypes.Boolean),
                                    new Claim("PermissionToStartExpenseDay",loginVm.PermissionToStartExpenseDay.ToString(), ClaimValueTypes.Boolean),

                        };
                        await _userManager.AddClaimsAsync(user, claims);

                    }
                    else
                    {

                        var user = await _userManager.FindByIdAsync(User.Identity.UserId());
                        loginPermissions.ChangePriceDuringSale = loginVm.ChangePriceDuringSale;
                        loginPermissions.IsExpenseAccount = loginVm.IsExpenseAccount;
                        loginPermissions.GiveDicountDuringSale = loginVm.GiveDicountDuringSale;
                        loginPermissions.ViewCounterDetails = loginVm.ViewCounterDetails;
                        loginPermissions.TransferCounter = loginVm.TransferCounter;
                        loginPermissions.CloseCounter = loginVm.CloseCounter;
                        loginPermissions.HoldCounter = loginVm.HoldCounter;
                        loginPermissions.CloseDay = loginVm.CloseDay;
                        loginPermissions.StartDay = loginVm.StartDay;
                        loginPermissions.ProcessSaleReturn = loginVm.ProcessSaleReturn;
                        loginPermissions.DailyExpenseList = loginVm.DailyExpenseList;
                        loginPermissions.InvoiceWoInventory = loginVm.InvoiceWoInventory;
                        loginPermissions.IsTouchInvoice = loginVm.IsTouchInvoice;
                        loginPermissions.TouchScreen = loginVm.TouchScreen;
                        loginPermissions.IsSupervisor = loginVm.IsSupervisor;
                        loginPermissions.AllowAll = loginVm.AllowAll;
                        loginPermissions.PermissionToStartExpenseDay = loginVm.PermissionToStartExpenseDay;
                        loginPermissions.TemporaryCashReceiver = loginVm.TemporaryCashReceiver;
                        loginPermissions.TemporaryCashIssuer = loginVm.TemporaryCashIssuer;
                        loginPermissions.TemporaryCashRequester = loginVm.TemporaryCashRequester;
                        loginPermissions.Days = loginVm.Days;
                        loginPermissions.Limit = loginVm.Limit;
                        loginPermissions.AllowViewAllData = loginVm.AllowViewAllData;
                        loginPermissions.TerminalUserType = loginVm.TerminalUserType == "" ? TerminalUserType.Both : (TerminalUserType)Enum.Parse(typeof(TerminalUserType), loginVm.TerminalUserType);
                        loginPermissions.IsOverAllAccess = loginVm.IsOverAllAccess;
                        await _userManager.RemoveClaimAsync(user, new Claim("ChangePriceDuringSale", loginVm.ChangePriceDuringSale.ToString(), ClaimValueTypes.Boolean));
                        await _userManager.RemoveClaimAsync(user, new Claim("IsExpenseAccount", loginVm.IsExpenseAccount.ToString(), ClaimValueTypes.Boolean));
                        await _userManager.RemoveClaimAsync(user, new Claim("GiveDicountDuringSale", loginVm.GiveDicountDuringSale.ToString(), ClaimValueTypes.Boolean));
                        await _userManager.RemoveClaimAsync(user, new Claim("ViewCounterDetails", loginVm.ViewCounterDetails.ToString(), ClaimValueTypes.Boolean));
                        await _userManager.RemoveClaimAsync(user, new Claim("TransferCounter", loginVm.TransferCounter.ToString(), ClaimValueTypes.Boolean));
                        await _userManager.RemoveClaimAsync(user, new Claim("CloseCounter", loginVm.CloseCounter.ToString(), ClaimValueTypes.Boolean));
                        await _userManager.RemoveClaimAsync(user, new Claim("HoldCounter", loginVm.HoldCounter.ToString(), ClaimValueTypes.Boolean));
                        await _userManager.RemoveClaimAsync(user, new Claim("CloseDay", loginVm.CloseDay.ToString(), ClaimValueTypes.Boolean));
                        await _userManager.RemoveClaimAsync(user, new Claim("StartDay", loginVm.StartDay.ToString(), ClaimValueTypes.Boolean));
                        await _userManager.RemoveClaimAsync(user, new Claim("ProcessSaleReturn", loginVm.ProcessSaleReturn.ToString(), ClaimValueTypes.Boolean));
                        await _userManager.RemoveClaimAsync(user, new Claim("DailyExpenseList", loginVm.DailyExpenseList.ToString(), ClaimValueTypes.Boolean));
                        await _userManager.RemoveClaimAsync(user, new Claim("InvoiceWoInventory", loginVm.InvoiceWoInventory.ToString(), ClaimValueTypes.Boolean));
                        await _userManager.RemoveClaimAsync(user, new Claim("IsTouchInvoice", loginVm.IsTouchInvoice.ToString(), ClaimValueTypes.Boolean));
                        //await _userManager.RemoveClaimAsync(user, new Claim("TouchScreen", loginVm.TouchScreen));
                        await _userManager.RemoveClaimAsync(user, new Claim("AllowAll", loginVm.AllowAll.ToString(), ClaimValueTypes.Boolean));
                        await _userManager.RemoveClaimAsync(user, new Claim("IsSupervisor", loginVm.IsSupervisor.ToString(), ClaimValueTypes.Boolean));
                        await _userManager.RemoveClaimAsync(user, new Claim("PermissionToStartExpenseDay", loginVm.PermissionToStartExpenseDay.ToString(), ClaimValueTypes.Boolean));
                        var claims = new List<Claim>
                        {
                            new Claim("ChangePriceDuringSale",loginVm.ChangePriceDuringSale.ToString(), ClaimValueTypes.Boolean),
                            new Claim("IsExpenseAccount",loginVm.IsExpenseAccount.ToString(), ClaimValueTypes.Boolean),
                            new Claim("GiveDicountDuringSale",loginVm.GiveDicountDuringSale.ToString(), ClaimValueTypes.Boolean),
                            new Claim("ViewCounterDetails",loginVm.ViewCounterDetails.ToString(), ClaimValueTypes.Boolean),
                            new Claim("TransferCounter",loginVm.TransferCounter.ToString(), ClaimValueTypes.Boolean),
                            new Claim("CloseCounter",loginVm.CloseCounter.ToString(), ClaimValueTypes.Boolean),
                            new Claim("HoldCounter",loginVm.HoldCounter.ToString(), ClaimValueTypes.Boolean),
                            new Claim("CloseDay",loginVm.CloseDay.ToString(), ClaimValueTypes.Boolean),
                            new Claim("StartDay",loginVm.StartDay.ToString(), ClaimValueTypes.Boolean),
                            new Claim("ProcessSaleReturn",loginVm.ProcessSaleReturn.ToString(), ClaimValueTypes.Boolean),
                            new Claim("DailyExpenseList",loginVm.DailyExpenseList.ToString(), ClaimValueTypes.Boolean),
                            new Claim("InvoiceWoInventory",loginVm.InvoiceWoInventory.ToString(), ClaimValueTypes.Boolean),
                            new Claim("IsTouchInvoice",loginVm.IsTouchInvoice.ToString(), ClaimValueTypes.Boolean),
                            //new Claim("TouchScreen",loginVm.TouchScreen),
                            new Claim("AllowAll",loginVm.AllowAll.ToString(), ClaimValueTypes.Boolean),
                            new Claim("IsSupervisor",loginVm.IsSupervisor.ToString(), ClaimValueTypes.Boolean),
                            new Claim("PermissionToStartExpenseDay",loginVm.PermissionToStartExpenseDay.ToString(), ClaimValueTypes.Boolean),
                        };
                        await _userManager.AddClaimsAsync(user, claims);
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
                    //Save changes to database
                    await _context.SaveChangesAsync();

                }
                return Ok("Already Exists");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        #region For ForgetPassword
        [Route("api/account/SendUserEmail")]
        [HttpGet("SendUserEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> SendUserEmail(string emailTo, string appLink)
        {
            var isloginEmailExist = await _userManager.FindByEmailAsync(emailTo);
            if (isloginEmailExist != null)
            {
                var sendEmailDto = new SendEmailDto()
                {
                    EmailTo = emailTo,
                    Subject = "Password Reset"
                };
                var result = _sendEmail.SendEmailAsync(sendEmailDto, isloginEmailExist.UserName, isloginEmailExist.Id, appLink);
                return Ok(result);
            }
            return Ok("Error");
        }
        [Route("api/account/UpdatePassword")]
        [HttpPost("UpdatePassword")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordVerifyVm passwordVerify)
        {
            bool result = await _userComponent.UpdatePassword(passwordVerify.UserId, passwordVerify.Password);
            if (result)
            {
                return Ok(result);
            }
            return Ok("Error");
        }
        #endregion


        #region For Setup Update
        [Route("api/account/SetupUpdateInCompany")]
        [HttpPost("SetupUpdateInCompany")]
        public IActionResult SetupUpdateInCompany([FromBody] StepsVm stepsVm)
        {
            var dbCompany = _context.Companies.FirstOrDefault(x => x.Id == stepsVm.CompanyId);
            if (stepsVm.Step1 != false)
            {
                dbCompany.Step1 = stepsVm.Step1;
            }
            if (stepsVm.Step2 != false)
            {
                dbCompany.Step2 = stepsVm.Step2;
            }
            if (stepsVm.Step3 != false)
            {
                dbCompany.Step3 = stepsVm.Step3;
            }
            if (stepsVm.Step3 != false)
            {
                dbCompany.Step4 = stepsVm.Step4;
            }
            if (stepsVm.Step5 != false)
            {
                dbCompany.Step5 = stepsVm.Step5;
            }
            _context.SaveChanges();
            return Ok(true);
        }
        #endregion

        #region For Login History

        [Route("api/account/LoginHistory")]
        [HttpPost("LoginHistory")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginHistory([FromBody] LoginHistoryVm loginHistoryVm)
        {
            var message = await Mediator.Send(new AddUpdateLoginHistoryCommand()
            {
                OperatingSystem = loginHistoryVm.OperatingSystem,
                IsLogin = loginHistoryVm.IsLogin,
                UserId = loginHistoryVm.UserId,
                CompanyId = loginHistoryVm.CompanyId
            });
            return Ok(message);
        }

        [Route("api/account/LoginHistoryList")]
        [HttpGet("LoginHistoryList")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginHistoryList(Guid userId, DateTime fromDate, DateTime toDate)
        {


            var message = await Mediator.Send(new GetLoginHistoryListQuery()
            {
                UserId = userId,
                FromDate = fromDate,
                ToDate = toDate
            });
            return Ok(message);
        }

        #endregion
    }
}

