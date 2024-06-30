using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noble.Api.Models
{
    public class CompanyInformationVm
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
    }
}
