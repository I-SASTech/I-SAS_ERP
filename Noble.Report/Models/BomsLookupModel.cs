using System;
using System.Collections.Generic;

namespace Noble.Report.Models
{
    public class BomsLookupModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public Guid? SaleOrderId { get; set; }
        public string SaleOrder { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? WareHouseId { get; set; }
        public string ApprovalStatus { get; set; }
        public string Status { get; set; }
        public List<SaleOrderItemLookupModel> BomSaleOrderItem { get; set; }
        public string WareHouseName { get; set; }
    }
}
