using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
    public class Bussiness : BaseEntity
    {
        public string NameArabic { get; set; }
        public string NameEnglish { get; set; }
        public string BusinessCategory { get; set; }
        public string AddressArabic { get; set; }
        public string AddressEnglish { get; set; }
        public string CityEnglish { get; set; }
        public string CityArabic { get; set; }
        public string CountryEnglish { get; set; }
        public string CountryArabic { get; set; }
        public string PhoneNo { get; set; }


    }
}
