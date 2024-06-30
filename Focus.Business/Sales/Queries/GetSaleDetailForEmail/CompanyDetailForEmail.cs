using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.TaxRates.Queries.GetTaxRateList;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Sales.Queries.GetSaleDetailForEmail
{
    public class CompanyDetailForEmail : IRequest<CompanyDto>
    {
        public Guid Id;

        public class Handler : IRequestHandler<CompanyDetailForEmail, CompanyDto>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context,
                ILogger<CompanyDetailForEmail> logger,
                IMapper mapper, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _mediator = mediator;
            }
            public async Task<CompanyDto> Handle(CompanyDetailForEmail request, CancellationToken cancellationToken)
            {

                try
                {
                    _context.DisableTenantFilter = true;
                    if (request.Id == Guid.Empty)
                    {
                        throw new ArgumentNullException(nameof(request.Id));
                    }
                    var dbCompany = _context.Companies.FirstOrDefault(x => x.Id == request.Id);
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
                            Step1 = dbCompany.Step1,
                            Step2 = dbCompany.Step2,
                            Step3 = dbCompany.Step3,
                            Step4 = dbCompany.Step4,
                            Step5 = dbCompany.Step5,

                        };
                    }
                    return new CompanyDto();
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
                finally
                {
                    _context.DisableTenantFilter = false;
                }
            }
        }
    }
}
