using Focus.Domain.Enum;
using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
  public  class ImportExportType : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public ImportExportTypes ImportExportTypes { get; set; }
        public bool isActive { get; set; }
        public virtual ICollection<SaleOrder> OrderTypes { get; set; }
        public virtual ICollection<SaleOrder> Incoterms { get; set; }
        public virtual ICollection<SaleOrder> Commodities { get; set; }
        public virtual ICollection<ImportExportItem> Services { get; set; }
        public virtual ICollection<ImportExportItem> StuffingLocations { get; set; }
        public virtual ICollection<ImportExportItem> PortOfLoadings { get; set; }
        public virtual ICollection<ImportExportItem> PortOfDestinations { get; set; }
        public virtual ICollection<ImportExportItem> Carriers { get; set; }

    }
}
