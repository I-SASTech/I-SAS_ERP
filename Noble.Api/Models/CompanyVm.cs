using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noble.Api.Models
{
    public class CompanyVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string VatRegistrationNo { get; set; }
        public bool Blocked { get; set; }
        public string LogoPath { get; set; }
        public string CompanyRegNo { get; set; }
        public string HouseNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Postcode { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public Guid ParentId { get; set; }
    }
}
