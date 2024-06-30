using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.SaleOrders
{
   public class ImportExportItemLookUpModel
    {
        public Guid Id { get; set; }
        public Guid? ServiceId { get; set; }
        public Guid? StuffingLocationId { get; set; }
        public Guid? PortOfLoadingId { get; set; }
        public Guid? PortOfDestinationId { get; set; }
        public Guid? CarrierId { get; set; }
        public string Description { get; set; }
        public string Ft { get; set; }
        public string Hc { get; set; }
        public string Tt { get; set; }
        public string Etd { get; set; }
        public Guid? SaleOrderId { get; set; }
        public string ServiceName { get; internal set; }
        public string ServiceNameAr { get; internal set; }
        public string StuffingLocationName { get; internal set; }
        public string StuffingLocationNameAr { get; internal set; }
        public string PortOfLoadingName { get; internal set; }
        public string PortOfLoadingNameAr { get; internal set; }
        public string PortOfDestinationNameAr { get; internal set; }
        public string PortOfDestinationName { get; internal set; }
        public string CarrierName { get; internal set; }
        public string CarrierNameAr { get; internal set; }
    }
}
