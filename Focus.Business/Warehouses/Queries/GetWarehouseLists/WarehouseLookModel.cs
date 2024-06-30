﻿using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Warehouses.Queries.GetWarehouseLists
{
    public class WarehouseLookModel : IMapFrom<Warehouse>
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
        public DateTime LicenseExpiry { get; set; }
        public string CivilDefenceLicenseNo { get; set; }
        public DateTime CivilDefenceLicenseExpiry { get; set; }
        public string CCTVLicenseNo { get; set; }
        public DateTime CCTVLicenseExpiry { get; set; }
        public bool IsActive { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Warehouse, WarehouseLookModel>();
        }
    }
}
