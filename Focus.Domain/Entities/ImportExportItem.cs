using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
   public class ImportExportItem : BaseEntity, ITenant
    {
        public Guid? ServiceId { get; set; }
        public virtual ImportExportType Service { get; set; }
        public Guid? StuffingLocationId { get; set; }
        public virtual ImportExportType StuffingLocation { get; set; }
        public Guid? PortOfLoadingId { get; set; }
        public virtual ImportExportType PortOfLoading { get; set; }
        public Guid? PortOfDestinationId { get; set; }
        public virtual ImportExportType PortOfDestination { get; set; }
        public Guid? CarrierId { get; set; }
        public virtual ImportExportType Carrier { get; set; }
        public string Description { get; set; }
        public string Ft { get; set; }
        public string Hc { get; set; }
        public string Tt { get; set; }
        public string Etd { get; set; }
        public Guid? SaleOrderId { get; set; }
        public virtual SaleOrder SaleOrder { get; set; }
    }
}
