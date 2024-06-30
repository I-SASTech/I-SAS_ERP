using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Origins.Queries.GetOriginDetails;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Permission.Queries.GetCompanyInformation
{
    public class GetCompanyInformationQuery : IRequest<CompanyLookUp>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetCompanyInformationQuery, CompanyLookUp>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetOriginDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<CompanyLookUp> Handle(GetCompanyInformationQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var companiesList = await _context.Companies.AsNoTracking().Include(x => x.CompanyLicences).ToListAsync(cancellationToken: cancellationToken);
                    var companyInfo = new CompanyLookUp
                    {
                        CompanyLicenseLookUps = new List<CompanyLicenseLookUp>(),
                        CompanyInfoLookUps = new List<CompanyInfoLookUp>(),
                    };
                    foreach (var company in companiesList)
                    {
                        companyInfo.CompanyInfoLookUps.Add(new CompanyInfoLookUp()
                        {
                            // Company Lookup
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
                            BusinessParentId = company.BusinessParentId,
                            ClientParentId = company.ClientParentId,
                            CompanyNameEnglish = company.CompanyNameEnglish,
                            CompanyNameArabic = company.CompanyNameArabic,
                        });
                        var comLicense = companiesList.FirstOrDefault(x => x.Id == company.Id);
                        if (comLicense != null)
                        {
                            //Company License LookUp
                            foreach (var license in comLicense.CompanyLicences)
                            {
                                companyInfo.CompanyLicenseLookUps.Add(new CompanyLicenseLookUp()
                                {
                                    Id = license.Id,
                                    FromDate = license.FromDate,
                                    IsActive = license.IsActive,
                                    IsBlock = license.IsBlock,
                                    ToDate = license.ToDate,
                                    CompanyId = license.CompanyId
                                });
                            }
                        }
                    }
                    

                    return companyInfo;
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
