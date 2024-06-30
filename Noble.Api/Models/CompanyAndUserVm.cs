using Focus.Business.Attachments.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Noble.Api.Models
{
    public class CompanyAndUserVm
    {
        //Company Data
    
        public string NameEnglish { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Id { get; set; }

     
        public string VatRegistrationNo { get; set; }
        public string CompanyRegNo { get; set; }
        public string LogoPath { get; set; }
        public string HouseNumber { get; set; }

  
        public string AddressEnglish { get; set; }
        public string AddressArabic { get; set; }
        public string CategoryEnglish { get; set; }
        public string CategoryArabic { get; set; }
        public string Postcode { get; set; }
        public string Town { get; set; }
        public string CityEnglish { get; set; }
        public string CityArabic { get; set; }
        public string CountryEnglish { get; set; }
        public string CountryArabic { get; set; }
        public string CompanyNameEnglish { get; set; }
        public string CompanyNameArabic { get; set; }
   
        public string PhoneNo { get; set; }
        public string Website { get; set; }
    
        public string CompanyEmail { get; set; }
        public string LandLine { get; set; }
        public string ComercialRegNo { get; set; }

        //User
        public string FirstName { get; set; }
        public string UserId { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
  
        public string ClientNo { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
    }
}
