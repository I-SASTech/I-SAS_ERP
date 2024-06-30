using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.Warehouses.Queries.GetWarehouseDetails
{
    public class WarehouseLookUpModel
    {
        public Guid Id { get; set; }
        public string StoreID { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ContactNo { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string LicenseNo { get; set; }
        public DateTime? LicenseExpiry { get; set; }
        public string CivilDefenceLicenseNo { get; set; }
        public DateTime? CivilDefenceLicenseExpiry { get; set; }
        public string CCTVLicenseNo { get; set; }
        public DateTime? CCTVLicenseExpiry { get; set; }

        public static Expression<Func<Warehouse, WarehouseLookUpModel>> Projection
        {
            get
            {
                return warehouseLookUpModel => new WarehouseLookUpModel
                {
                    Id = warehouseLookUpModel.Id,
                    StoreID = warehouseLookUpModel.StoreID,
                    Name = warehouseLookUpModel.Name,
                    NameArabic = warehouseLookUpModel.NameArabic,
                    Address = warehouseLookUpModel.Address,
                    City = warehouseLookUpModel.City,
                    Country = warehouseLookUpModel.Country,
                    ContactNo = warehouseLookUpModel.ContactNo,
                    Email = warehouseLookUpModel.Email,
                    LicenseNo = warehouseLookUpModel.LicenseNo,
                    LicenseExpiry = warehouseLookUpModel.LicenseExpiry,
                    CivilDefenceLicenseNo = warehouseLookUpModel.CivilDefenceLicenseNo,
                    CivilDefenceLicenseExpiry = warehouseLookUpModel.CivilDefenceLicenseExpiry,
                    CCTVLicenseNo = warehouseLookUpModel.CCTVLicenseNo,
                    CCTVLicenseExpiry = warehouseLookUpModel.CCTVLicenseExpiry,
                    IsActive = warehouseLookUpModel.isActive,
                };
            }
        }

        public static WarehouseLookUpModel Create(Warehouse warehouses)
        {
            return Projection.Compile().Invoke(warehouses);
        }
    }
}
