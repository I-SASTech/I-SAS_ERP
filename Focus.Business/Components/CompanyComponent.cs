using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Extensions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.Components
{
    public class CompanyComponent : ICompanyComponent
    {
        private readonly IApplicationDbContext _context;
        private readonly IPrincipal _principal;
        private readonly UserManager<ApplicationUser> _userManager;

        private string _code;
        private readonly IMediator _mediator;

        public CompanyComponent(IApplicationDbContext context,
            IPrincipal principal,
            UserManager<ApplicationUser> userManager, IMediator mediator)
        {
            _context = context;
            _principal = principal;
            _userManager = userManager;
        }


        //Get company Data

        public bool VatRegistrationNumberExists(string vrn)
        {
            var data = _context.Companies.FirstOrDefault(o => o.VatRegistrationNo == vrn);
            return data == null;
        }
        public List<CompanyDto> GetCompaniesList(Guid id)
        {
            var companies = new List<Company>();

            var companyId = id == Guid.Empty ? _principal.Identity.CompanyId() : id;
            var companyInfo = _context.Companies.Find(companyId);
            if (companyInfo.NameEnglish == "noble@gmail.com")

            {
                companies = _context.Companies.Where(x => x.ParentId == companyInfo.Id && x.ClientParentId == null)
                    .Include(x => x.CompanyLicences).ToList();
            }
            else if ((companyInfo?.ParentId != Guid.Empty || companyInfo?.ParentId != null)
                    && companyInfo.ClientParentId == null && companyInfo.BusinessParentId == null)
            {
                companies = _context.Companies.Where(x => x.ParentId == companyInfo.ParentId && x.ClientParentId == companyInfo.Id).ToList();
            }
            else if ((companyInfo?.ParentId != Guid.Empty || companyInfo?.ParentId != null)
                     && companyInfo.ClientParentId != null && companyInfo.BusinessParentId == null)
            {
                companies = _context.Companies.Where(x => x.ClientParentId == companyInfo.ClientParentId && x.BusinessParentId == companyInfo.Id).ToList();
            }
            var companyList = new List<CompanyDto>();
            _context.DisableTenantFilter = true;
            foreach (var company in companies)
            {
                var companyLincence = company.CompanyLicences.LastOrDefault();
                
                var companyPermissionType = _context.NobleGroups.OrderBy(x => EF.Property<Guid>(x, "CompanyId") == company.Id).LastOrDefault();
                
                var companyDto = new CompanyDto
                {
                    Id = company.Id,
                    NameEnglish = company.NameEnglish,
                    NameArabic = company.NameArabic,
                    VatRegistrationNo = company.VatRegistrationNo,
                    CompanyEmail = company.CompanyEmail,
                    CityEnglish = company.CityEnglish,
                    CityArabic = company.CityArabic,
                    CategoryEnglish = company.CategoryInEnglish,
                    CategoryArabic = company.CategoryInArabic,
                    CountryEnglish = company.CountryEnglish,
                    CountryArabic = company.CountryArabic,
                    CompanyRegNo = company.CompanyRegNo,
                    AddressEnglish = company.AddressEnglish,
                    AddressArabic = company.AddressArabic,
                    PhoneNo = company.PhoneNo,
                    Website = company.Website,
                    Postcode = company.Postcode,
                    LandLine = company.Landline,
                    ClientNo = company.ClientNo,
                    ParentId = company?.ParentId,
                    BusinessId = company?.BusinessParentId,
                    ClientParentId = company?.ClientParentId,
                    CompanyType = companyLincence?.CompanyType.ToString(),
                    FromDate = companyLincence?.FromDate,
                    ToDate = companyLincence?.ToDate,
                    IsMultiUnit = company.IsMultiUnit,
                    IsProduction = company.IsProduction,
                    InvoiceWoInventory = company.InvoiceWoInventory,
                    IsArea = company.IsArea,
                    English = company.English,
                    Arabic = company.Arabic,
                    Terminal = company.Terminal,
                    DayStart = company.DayStart,
                    CashVoucher = company.CashVoucher,
                    IsOpenDay = company.IsOpenDay,
                    DailyExpense = company.ExpenseAccount,
                    IsTransferAllow = company.IsTransferAllow,
                    MasterProduct = company.MasterProduct,
                    SaleOrder = company.SaleOrder,
                    SimpleInvoice = company.SimpleInvoice,
                    SoInventoryReserve = company.SoInventoryReserve,
                    PurchaseOrder = company.PurchaseOrder,
                    InternationalPurchase = company.InternationalPurchase,
                    PartiallyPurchase = company.PartiallyPurchase,
                    VersionAllow = company.VersionAllow,
                    ExpenseToGst = company.ExpenseToGst,
                    AutoPurchaseVoucher = company.AutoPurchaseVoucher,
                    IsForMedical = company.IsForMedical,
                    SuperAdminDayStart = company.SuperAdminDayStart,
                    BankDetail = company.BankDetail,
                    TaxInvoiceLabel = string.IsNullOrEmpty(company.TaxInvoiceLabel) ? "Tax Invoice": company.TaxInvoiceLabel,
                    TaxInvoiceLabelAr = string.IsNullOrEmpty(company.TaxInvoiceLabelAr) ? "فاتورة ضريبية" : company.TaxInvoiceLabelAr,
                    SimplifyTaxInvoiceLabel = string.IsNullOrEmpty(company.SimplifyTaxInvoiceLabel) ? "Simplified Tax Invoice" : company.SimplifyTaxInvoiceLabel,
                    SimplifyTaxInvoiceLabelAr = string.IsNullOrEmpty(company.SimplifyTaxInvoiceLabel) ? "فاتورة ضريبية مبسطة" : company.SimplifyTaxInvoiceLabelAr,
                    //IsActive = companyLincence?.IsActive,
                    //IsBlock = companyLincence?.IsBlock,
                    CompanyLicenceId = companyLincence?.Id ?? Guid.Empty,
                    CompanyPermissionType = companyPermissionType != null? companyPermissionType.GroupName + "-" + Enum.GetName(typeof(GroupType), companyPermissionType.GroupType):"",
                    CompanyLicences = company.CompanyLicences.Select( x => new CompanyLicenceDto()
                    {
                        Id = x.Id,
                        CompanyId = x.CompanyId,
                        CompanyType = x.CompanyType,
                        FromDate = x.FromDate,
                        ToDate = x.ToDate,
                        NumberOfUsers = x.NumberOfUsers,
                        NumberOfTransactions = (int)x.NoOfTransactionsAllow,
                        IsActive = x.IsActive,
                        IsBlock = x.IsBlock
                    }).ToList()

                };
               
                companyList.Add(companyDto);
            }
            return companyList;
        }
        public CompanyDto GetCompanyByName(string companyName)
        {
            if (companyName == null) throw new ArgumentNullException(nameof(companyName));
            var company = _context.Companies.FirstOrDefault(x =>
                string.Equals(x.NameEnglish.ToLower(), companyName.ToLower(), StringComparison.OrdinalIgnoreCase));
            if (company == null) throw new NotFoundException(companyName, "Company");
            return new CompanyDto
            {
                Id = company.Id,
                NameEnglish = company.NameEnglish,
                CreatedDate = company.CreatedDate.ToString("dd/MMM/yyyy"),
                VatRegistrationNo = company.VatRegistrationNo,
                LogoPath = company.LogoPath
            };
        }
        public CompanyDto GetCompanyById(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            var dbCompany = _context.Companies.AsNoTracking().FirstOrDefault(x => x.Id == companyId);
            if (dbCompany != null)
            {
                return new CompanyDto
                {
                    Id = dbCompany.Id,
                    NameEnglish = dbCompany.NameEnglish,
                    NameArabic = dbCompany.NameArabic,
                    CreatedDate = dbCompany.CreatedDate.ToString("dd/MMM/yyyy"),
                    VatRegistrationNo = dbCompany.VatRegistrationNo,
                    CompanyRegNo = dbCompany.CompanyRegNo,
                    LogoPath = dbCompany.LogoPath,
                    AddressEnglish = dbCompany.AddressEnglish,
                    AddressArabic = dbCompany.AddressArabic,
                    CategoryEnglish = dbCompany.CategoryInEnglish,
                    CategoryArabic = dbCompany.CategoryInArabic,
                    Postcode = dbCompany.Postcode,
                    CountryArabic = dbCompany.CountryArabic,
                    Town = dbCompany.Town,
                    HouseNumber = dbCompany.HouseNumber,
                    CityArabic = dbCompany.CityArabic,
                    CityEnglish = dbCompany.CityEnglish,
                    ParentId = dbCompany.ParentId,
                    ClientParentId = dbCompany.ClientParentId,
                    Website = dbCompany.Website,
                    LandLine = dbCompany.Landline,
                    PhoneNo = dbCompany.PhoneNo,
                    ClientNo = dbCompany.ClientNo,
                    CompanyEmail = dbCompany.CompanyEmail,
                    CountryEnglish = dbCompany.CountryEnglish,
                    BusinessId = dbCompany.BusinessParentId,
                    IsMultiUnit = dbCompany.IsMultiUnit,
                    IsProduction = dbCompany.IsProduction,
                    CompanyNameEnglish = dbCompany.CompanyNameEnglish,
                    CompanyNameArabic = dbCompany.CompanyNameArabic,
                    IsProceed = dbCompany.IsProceed,
                    Step1=dbCompany.Step1,
                    Step2=dbCompany.Step2,
                    Step3=dbCompany.Step3,
                    Step4=dbCompany.Step4,
                    Step5=dbCompany.Step5,
                    
                };
            }
            return new CompanyDto();
        }
        public CompanyDto UpdateCompany(CompanyDto company)
        {
            var dbCompany = _context.Companies.FirstOrDefault(x => x.Id == company.Id);
            if (dbCompany == null) return new CompanyDto();
            //Check if user is changing VAT Registration number
            if (company.VatRegistrationNo != dbCompany.VatRegistrationNo)
            {
                //TODO: Check that we are not updating VAT Number same as any other company's'
            }

            if (company.IsInternationalPurchase)
            {
                Random rnd = new Random();
                for (int i = 0; i < 11; i++)
                {
                    _code += rnd.Next(0, 9).ToString();
                }

                if (!company.PurchaseOrder)
                {
                    dbCompany.PurchaseOrder = company.PurchaseOrder;
                    dbCompany.InternationalPurchase = false;
                    dbCompany.PartiallyPurchase = false;
                    dbCompany.VersionAllow = false;
                    dbCompany.ExpenseToGst = false;
                    dbCompany.AutoPurchaseVoucher = false;
                }
                else if(company.PurchaseOrder && !company.InternationalPurchase)
                {
                    dbCompany.PurchaseOrder = company.PurchaseOrder;
                    dbCompany.InternationalPurchase = false;
                    dbCompany.PartiallyPurchase = company.PartiallyPurchase;
                    dbCompany.VersionAllow = company.VersionAllow;
                    dbCompany.ExpenseToGst = false;
                    dbCompany.AutoPurchaseVoucher = false;
                }
                else if (company.PurchaseOrder && company.InternationalPurchase)
                {
                    dbCompany.PurchaseOrder = company.PurchaseOrder;
                    dbCompany.InternationalPurchase = company.InternationalPurchase;
                    dbCompany.PartiallyPurchase = company.PartiallyPurchase;
                    dbCompany.VersionAllow = company.VersionAllow;
                    dbCompany.ExpenseToGst = company.ExpenseToGst;
                    dbCompany.AutoPurchaseVoucher = company.AutoPurchaseVoucher;
                }

                _context.Companies.Update(dbCompany);

                _mediator.Send(new AddUpdateSyncRecordCommand()
                {
                    SyncRecordModels = _context.SyncLog(),
                    Code = _code,
                });
                _context.SaveChanges();
                return company;
            }

            if (company.IsMulti)
            {
                Random rnd = new Random();
                for (int i = 0; i < 11; i++)
                {
                    _code += rnd.Next(0, 9).ToString();
                }

                dbCompany.IsMultiUnit = company.IsMultiUnit;
                dbCompany.IsProduction = company.IsProduction;
                dbCompany.InvoiceWoInventory = company.InvoiceWoInventory;
                dbCompany.IsArea = company.IsArea;
                dbCompany.English = company.English;
                dbCompany.Arabic = company.Arabic;

                _context.Companies.Update(dbCompany);
                _mediator.Send(new AddUpdateSyncRecordCommand()
                {
                    SyncRecordModels = _context.SyncLog(),
                    Code = _code,
                });
                _context.SaveChanges();
                return company;
            }

            if (company.IsDayStart)
            {
                Random rnd = new Random();
                for (int i = 0; i < 11; i++)
                {
                    _code += rnd.Next(0, 9).ToString();
                }

                dbCompany.DayStart = company.DayStart;
                dbCompany.Terminal = company.Terminal;
                if (dbCompany.DayStart == false)
                {
                    dbCompany.IsOpenDay = false;
                    dbCompany.IsTransferAllow = false;
                }
                _context.Companies.Update(dbCompany);
                _mediator.Send(new AddUpdateSyncRecordCommand()
                {
                    SyncRecordModels = _context.SyncLog(),
                    Code = _code,
                });
                _context.SaveChanges();
                return company;
            }

            if (company.IsCashVoucher)
            {
                Random rnd = new Random();
                for (int i = 0; i < 11; i++)
                {
                    _code += rnd.Next(0, 9).ToString();
                }

                dbCompany.CashVoucher = company.CashVoucher;

                _context.Companies.Update(dbCompany);
                _mediator.Send(new AddUpdateSyncRecordCommand()
                {
                    SyncRecordModels = _context.SyncLog(),
                    Code = _code,
                });
                _context.SaveChanges();
                return company;
            }
            if (company.IsMasterProduct)
            {
                Random rnd = new Random();
                for (int i = 0; i < 11; i++)
                {
                    _code += rnd.Next(0, 9).ToString();
                }

                dbCompany.MasterProduct = company.MasterProduct;
                dbCompany.SuperAdminDayStart = company.SuperAdminDayStart;
                dbCompany.BankDetail = company.BankDetail;

                _context.Companies.Update(dbCompany);
                _mediator.Send(new AddUpdateSyncRecordCommand()
                {
                    SyncRecordModels = _context.SyncLog(),
                    Code = _code,
                });
                _context.SaveChanges();
                return company;
            } 

            if (company.OpenDay)
            {
                Random rnd = new Random();
                for (int i = 0; i < 11; i++)
                {
                    _code += rnd.Next(0, 9).ToString();
                }

                dbCompany.IsOpenDay = company.IsOpenDay;

                _context.Companies.Update(dbCompany);
                _mediator.Send(new AddUpdateSyncRecordCommand()
                {
                    SyncRecordModels = _context.SyncLog(),
                    Code = _code,
                });
                _context.SaveChanges();
                return company;
            } 
            if (company.DailyExpense)
            {
                Random rnd = new Random();
                for (int i = 0; i < 11; i++)
                {
                    _code += rnd.Next(0, 9).ToString();
                }

                dbCompany.ExpenseAccount = company.IsDailyExpense;

                _context.Companies.Update(dbCompany);
                _mediator.Send(new AddUpdateSyncRecordCommand()
                {
                    SyncRecordModels = _context.SyncLog(),
                    Code = _code,
                });
                _context.SaveChanges();
                return company;
            }
            if (company.IsForMedical)
            {
                Random rnd = new Random();
                for (int i = 0; i < 11; i++)
                {
                    _code += rnd.Next(0, 9).ToString();
                }

                dbCompany.IsForMedical = company.IsForMedical;

                _context.Companies.Update(dbCompany);
                _mediator.Send(new AddUpdateSyncRecordCommand()
                {
                    SyncRecordModels = _context.SyncLog(),
                    Code = _code,
                });
                _context.SaveChanges();
                return company;
            }
            if (company.IsTaxLabel)
            {
                Random rnd = new Random();
                for (int i = 0; i < 11; i++)
                {
                    _code += rnd.Next(0, 9).ToString();
                }

                dbCompany.TaxInvoiceLabel = company.TaxInvoiceLabel;

                _context.Companies.Update(dbCompany);
                _mediator.Send(new AddUpdateSyncRecordCommand()
                {
                    SyncRecordModels = _context.SyncLog(),
                    Code = _code,
                });
                _context.SaveChanges();
                return company;
            }
            if (company.IsTaxInvoiceLabelAr)
            {
                Random rnd = new Random();
                for (int i = 0; i < 11; i++)
                {
                    _code += rnd.Next(0, 9).ToString();
                }

                dbCompany.TaxInvoiceLabelAr = company.TaxInvoiceLabelAr;

                _context.Companies.Update(dbCompany);
                _mediator.Send(new AddUpdateSyncRecordCommand()
                {
                    SyncRecordModels = _context.SyncLog(),
                    Code = _code,
                });
                _context.SaveChanges();
                return company;
            }
            if (company.IsSimplifyTaxInvoiceLabel)
            {
                Random rnd = new Random();
                for (int i = 0; i < 11; i++)
                {
                    _code += rnd.Next(0, 9).ToString();
                }

                dbCompany.SimplifyTaxInvoiceLabel = company.SimplifyTaxInvoiceLabel;

                _context.Companies.Update(dbCompany);
                _mediator.Send(new AddUpdateSyncRecordCommand()
                {
                    SyncRecordModels = _context.SyncLog(),
                    Code = _code,
                });
                _context.SaveChanges();
                return company;
            }
            if (company.IsSimplifyTaxInvoiceLabelAr)
            {
                Random rnd = new Random();
                for (int i = 0; i < 11; i++)
                {
                    _code += rnd.Next(0, 9).ToString();
                }

                dbCompany.SimplifyTaxInvoiceLabelAr = company.SimplifyTaxInvoiceLabelAr;

                _context.Companies.Update(dbCompany);
                _mediator.Send(new AddUpdateSyncRecordCommand()
                {
                    SyncRecordModels = _context.SyncLog(),
                    Code = _code,
                });
                _context.SaveChanges();
                return company;
            }
      
       
            if (company.TransferAllow)
            {
                Random rnd = new Random();
                for (int i = 0; i < 11; i++)
                {
                    _code += rnd.Next(0, 9).ToString();
                }

                dbCompany.IsTransferAllow = company.IsTransferAllow;

                _context.Companies.Update(dbCompany);
                _mediator.Send(new AddUpdateSyncRecordCommand()
                {
                    SyncRecordModels = _context.SyncLog(),
                    Code = _code,
                });
                _context.SaveChanges();
                return company;
            }

            if (company.IsSaleOrder)
            {
                Random rnd = new Random();
                for (int i = 0; i < 11; i++)
                {
                    _code += rnd.Next(0, 9).ToString();
                }

                dbCompany.SaleOrder = company.SaleOrder;
                dbCompany.SimpleInvoice = company.SimpleInvoice;
                dbCompany.SoInventoryReserve = company.SoInventoryReserve;

                _context.Companies.Update(dbCompany);
                _mediator.Send(new AddUpdateSyncRecordCommand()
                {
                    SyncRecordModels = _context.SyncLog(),
                    Code = _code,
                });
                _context.SaveChanges();
                return company;
            }

            dbCompany.VatRegistrationNo = company.VatRegistrationNo;
            dbCompany.NameEnglish = company.NameEnglish;
            dbCompany.NameArabic = company.NameArabic;
            dbCompany.CompanyRegNo = company.CompanyRegNo;
            dbCompany.AddressEnglish = company.AddressEnglish;
            dbCompany.AddressArabic = company.AddressArabic;
            dbCompany.CategoryInEnglish = company.CategoryEnglish;
            dbCompany.CategoryInArabic = company.CategoryArabic;
            dbCompany.CountryEnglish = company.CountryEnglish;
            dbCompany.CountryArabic = company.CountryArabic;
            dbCompany.LogoPath = company.LogoPath;
            dbCompany.CityArabic = company.CityArabic;
            dbCompany.CityEnglish = company.CityEnglish;
            dbCompany.Landline = company.LandLine;
            dbCompany.CompanyNameEnglish = company.CompanyNameEnglish;
            dbCompany.CompanyNameArabic = company.CompanyNameArabic;

            _context.Companies.Update(dbCompany);
            //_mediator.Send(new AddUpdateSyncRecordCommand()
            //{
            //    SyncRecordModels = _context.SyncLog(),
            //    Code = _code,
            //});
            _context.SaveChanges();
            return company;


        }
        public string GetCompanyLogo(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            var company = GetCompanyById(companyId);
            if (company != null)
            {
                if (!string.IsNullOrEmpty(company.LogoPath))
                {
                    return company.LogoPath;
                }
                else
                {
                    return "/ProfileImages/user-no-image.png";
                }
            }
            else
            {
                return "/ProfileImages/user-no-image.png";
            }
        }
        public void UpdateCompanyLogo(CompanyDto company)
        {
            Random rnd = new Random();
            for (int i = 0; i < 11; i++)
            {
                _code += rnd.Next(0, 9).ToString();
            }
            if (company.Id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(company.Id));
            }
            var companyDb = _context.Companies.FirstOrDefault(x => x.Id == company.Id);
            if (companyDb != null)
            {
                companyDb.LogoPath = company.LogoPath;
                _context.Companies.Update(companyDb);
                _mediator.Send(new AddUpdateSyncRecordCommand()
                {
                    SyncRecordModels = _context.SyncLog(),
                    Code = _code,
                });
                _context.SaveChanges();
            }
        }


        private static string GetStatus(bool value)
        {
            return value
                ?
                "<span class='badge badge-pill badge-danger'> Blocked </span>"
                : "<span class='badge badge-pill badge-success'> Active </span>";
        }


        public async Task<bool> DeleteCompany(Guid companyId)
        {
            var company = _context.Companies.FirstOrDefault(x => x.Id == companyId);
            var userList = _userManager.Users.Where(x => x.CompanyId == company.Id).ToList();
            if (company != null)
            {
                foreach (var user in userList)
                {
                    //Remove Claim
                    var claims = await _userManager.GetClaimsAsync(user);
                    var c = await _userManager.RemoveClaimsAsync(user, claims);

                    // Remover Role
                    var role = await _userManager.GetRolesAsync(user);
                    var r1 = await _userManager.RemoveFromRoleAsync(user, role[0]);

                    //Delete User
                    var u = await _userManager.DeleteAsync(user);
                }
                var cmp = _context.Companies.Remove(company);
                if (cmp != null)
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    await _context.SaveChangesAsync();
                    return true;
                }

            }

            return false;

        }

        public void GetRoleByUser(UserDetailDto userDetailDto)
        {
            var user = new ApplicationUser
            {
                Id = userDetailDto.Id
            };
        }

        //public CompanyDto GetCompanySetupAccount()
        //{
        //    try
        //    {



        //        var companyDb = _context.CompanyAccountSetups.FirstOrDefault();

        //        if (companyDb != null)
        //        {
        //            return new CompanyDto
        //            {
        //                Currency = companyDb.CurrencyId
        //            };
        //        }
        //        else
        //        {
        //            return new CompanyDto();
        //        }


        //    }
        //    catch (Exception e)
        //    {
        //        throw new ApplicationException("Account ");
        //        _ = e.Message;
        //    }

        //}
    }
}