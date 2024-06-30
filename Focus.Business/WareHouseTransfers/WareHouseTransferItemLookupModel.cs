using System;
using Focus.Business.Sales.Queries.GetSaleDetail;

namespace Focus.Business.WareHouseTransfers
{
    public class WareHouseTransferItemLookupModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public Guid WareHouseTransferId { get; set; }
        public ProductDropdownLookUpModel Product { get; set; }
    }
}
