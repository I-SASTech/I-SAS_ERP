using System;
using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;

namespace Focus.Business.Products.Queries.GetProductList
{
    public class SyncRecordLookUpModel : IMapFrom<SyncRecord>
    {
        public int Id { get; set; }
        public Guid CompanyId { get; set; }
        public string Table { get; set; }
        public string ColumnId { get; set; }
        public string ColumnType { get; set; }
        public bool Push { get; set; } = false;
        public bool Pull { get; set; } = false;
        public string Action { get; set; }
        public string ChangeDate { get; set; }
        public string PushDate { get; set; }
        public string PullDate { get; set; }
        public string ColumnName { get; set; }
        public bool IsGeneral { get; set; } = false;
        public int Pending { get; set; }
        public int Synced { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SyncRecord, SyncRecordLookUpModel>();
                     //.ForMember(x => x.Inventory, prod => prod.MapFrom(z => z.Inventories.LastOrDefault()))
                     //.ForMember(x => x.PurchasePrice, prod => prod.MapFrom(z => z.Inventories.LastOrDefault(x => x.TransactionType == TransactionType.PurchasePost || x.TransactionType == TransactionType.StockIn || x.TransactionType == TransactionType.StockProduction)))
                     //.ForMember(x => x.SizeName, prod => prod.MapFrom(z => z.Size.Name))
                     //.ForMember(x => x.SizeNameArabic, prod => prod.MapFrom(z => z.Size.NameArabic))
                     //.ForMember(x => x.ColorName, prod => prod.MapFrom(z => z.Color.Name))
                     //.ForMember(x => x.ColorNameArabic, prod => prod.MapFrom(z => z.Color.NameArabic))
                     //.ForMember(x => x.TaxRate, prod => prod.MapFrom(z => z.TaxRate.Rate));

        }
    }
}
