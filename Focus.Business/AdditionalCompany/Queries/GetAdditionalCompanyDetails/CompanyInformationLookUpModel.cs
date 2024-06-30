using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.AdditionalCompany.Queries.GetAdditionalCompanyDetails
{
    public class CompanyInformationLookUpModel
    {
        public Guid Id { get; set; }
        public string NameArabic { get; set; }
        public string CommercialRegNo { get; set; }
        public string VatRegistrationNo { get; set; }
        public string CityArabic { get; set; }
        public string CountryArabic { get; set; }
        public string Mobile { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string AddressArabic { get; set; }
        public DateTime CreatedDate { get; set; }

        public static Expression<Func<CompanyInformation, CompanyInformationLookUpModel>> Projection
        {
            get
            {
                return comoanyInformation => new CompanyInformationLookUpModel
                {
                    Id = comoanyInformation.Id,
                    NameArabic = comoanyInformation.NameArabic,
                    CommercialRegNo = comoanyInformation.CommercialRegNo,
                    VatRegistrationNo = comoanyInformation.VatRegistrationNo,
                    CityArabic = comoanyInformation.CityArabic,
                    CountryArabic = comoanyInformation.CountryArabic,
                    Mobile = comoanyInformation.Mobile,
                    PhoneNo = comoanyInformation.PhoneNo,
                    Email = comoanyInformation.Email,
                    Website = comoanyInformation.Website,
                    AddressArabic = comoanyInformation.AddressArabic,
                    CreatedDate = comoanyInformation.CreatedDate,
                };
            }
        }

        public static CompanyInformationLookUpModel Create(CompanyInformation company)
        {
            return Projection.Compile().Invoke(company);
        }
    }
}
