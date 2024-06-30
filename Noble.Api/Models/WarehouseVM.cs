using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noble.Api.Models
{
    public class WarehouseVM
    {
        public Guid Id { get; set; }
        public string StoreID { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string LicenseNo { get; set; }
        public DateTime? LicenseExpiry { get; set; }
        public string CivilDefenceLicenseNo { get; set; }
        public DateTime? CivilDefenceLicenseExpiry { get; set; }
        public string CCTVLicenseNo { get; set; }
        public DateTime? CCTVLicenseExpiry { get; set; }
        public bool IsActive { get; set; }
    }
}
