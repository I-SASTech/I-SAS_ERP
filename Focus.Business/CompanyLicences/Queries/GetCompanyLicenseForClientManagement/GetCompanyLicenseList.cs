using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.CompanyLicences.Queries.GetCompanyLicenceList;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.CompanyLicences.Queries.GetCompanyLicenseForClientManagement
{
    public class GetCompanyLicenseList : IRequest<GetCompanyDataLookUp>
    {

        public class Handler : IRequestHandler<GetCompanyLicenseList, GetCompanyDataLookUp>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetCompanyLicenceListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<GetCompanyDataLookUp> Handle(GetCompanyLicenseList request,
                CancellationToken cancellationToken)
            {
                try
                {
                    var companyData = _context.Companies.AsNoTracking().Include(x => x.CompanyLicences).ToList();


                    var companyInfoLookUpList = new List<CompanyInfoLookUp>();
                    foreach (var company in companyData)
                    {
                        var lookUp = new CompanyInfoLookUp()
                        {
                            Id = company.Id,
                            CreatedDate = company.CreatedDate,
                            Blocked = company.Blocked,
                            LogoPath = company.LogoPath,
                            HouseNumber = company.HouseNumber,
                            CompanyRegNo = company.CompanyRegNo,
                            NameEnglish = company.NameEnglish,
                            NameArabic = company.NameArabic,
                            VatRegistrationNo = company.VatRegistrationNo,
                            CompanyEmail = company.CompanyEmail,
                            CityEnglish = company.CityEnglish,
                            CityArabic = company.CityArabic,
                            CountryEnglish = company.CountryEnglish,
                            CountryArabic = company.CountryArabic,
                            CategoryInEnglish = company.CategoryInEnglish,
                            CategoryInArabic = company.CategoryInArabic,
                            AddressEnglish = company.AddressEnglish,
                            AddressArabic = company.AddressArabic,
                            PhoneNo = company.PhoneNo,
                            Landline = company.Landline,
                            Website = company.Website,
                            Postcode = company.Postcode,
                            Town = company.Town,
                            ClientNo = company.ClientNo,
                            ParentId = company.ParentId,
                            ClientParentId = company.ClientParentId,
                            BusinessParentId = company.BusinessParentId,
                            CompanyNameEnglish = company.CompanyNameEnglish,
                            CompanyNameArabic = company.CompanyNameArabic,
                            //NobleGroupId = company.Nob,
                            LicenseType = company.CompanyLicences.Count > 0
                                ? company.CompanyLicences.LastOrDefault()?.LicenseType
                                : "",
                            //GroupName = company.NobleGroup == null ? "" : company.NobleGroup.GroupName + '-' + Enum.GetName(typeof(GroupType), company.NobleGroup.GroupType),
                            EndDate = company.CompanyLicences.Count > 0
                                ? company.CompanyLicences.LastOrDefault()?.ToDate.ToString("d")
                                : "",
                            TechnicalSupportPeriod = company.CompanyLicences.Count > 0
                                ? company.CompanyLicences.LastOrDefault()?.TechnicalSupportPeriod
                                : "",
                            IsTechnicalSupport = company.CompanyLicences.Count > 0 && (company.CompanyLicences.LastOrDefault().IsTechnicalSupport || company.CompanyLicences.LastOrDefault().IsUpdateTechnicalSupport),
                            CompanyLicenseLookUps = company.CompanyLicences.Select(x =>
                                new CompanyLicenseLookUp()
                                {
                                    FromDate = x.FromDate,
                                    ToDate = x.ToDate,
                                    IsTechnicalSupport = x.IsTechnicalSupport,
                                    IsUpdateTechnicalSupport = x.IsUpdateTechnicalSupport,
                                    LicenseType = x.LicenseType,
                                    PaymentFrequency = x.PaymentFrequency,
                                    IsActive = x.IsActive,
                                    IsBlock = x.IsBlock

                                }).ToList()
                        };
                        companyInfoLookUpList.Add(lookUp);
                    }
                    return new GetCompanyDataLookUp()
                    {
                        Companies = companyInfoLookUpList.Where(x =>
                            x.ParentId != Guid.Empty && x.ClientParentId == null && x.BusinessParentId == null).ToList(),
                        Businesses = companyInfoLookUpList.Where(x => x.ParentId != Guid.Empty && x.ClientParentId != null && x.BusinessParentId == null).ToList(),
                        Locations = companyInfoLookUpList.Where(x => x.ParentId != Guid.Empty && x.ClientParentId != null && x.BusinessParentId != null).ToList(),
                    };
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex.Message);
                    throw new ApplicationException(ex.Message);
                }
            }
        }
    }
}
