using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Permission.Queries.GetCompanyInformation
{
    public class CompanyInfoLookUp
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Blocked { get; set; }
        public string LogoPath { get; set; }
        public string HouseNumber { get; set; }
        public string CompanyRegNo { get; set; }
        public string NameEnglish { get; set; }
        public string NameArabic { get; set; }
        public string VatRegistrationNo { get; set; }
        public string CompanyEmail { get; set; }
        public string CityEnglish { get; set; }
        public string CityArabic { get; set; }
        public string CountryEnglish { get; set; }
        public string CountryArabic { get; set; }
        public string CategoryInEnglish { get; set; }
        public string CategoryInArabic { get; set; }
        public string AddressEnglish { get; set; }
        public string AddressArabic { get; set; }
        public string PhoneNo { get; set; }
        public string Landline { get; set; }
        public string Website { get; set; }
        public string Postcode { get; set; }
        public string Town { get; set; }
        //Auto Generate ClintNo 
        public string ClientNo { get; set; }
        public Guid ParentId { get; set; }
        public Guid? ClientParentId { get; set; }
        public Guid? BusinessParentId { get; set; }
        public string CompanyNameEnglish { get; set; }
        public string CompanyNameArabic { get; set; }
    }
}
