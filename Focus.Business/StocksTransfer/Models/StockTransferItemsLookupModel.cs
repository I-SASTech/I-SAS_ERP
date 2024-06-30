using Focus.Business.Sales.Queries.GetSaleDetail;
using System;

namespace Focus.Business.StocksTransfer.Models
{
    public class StockTransferItemsLookupModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal TransferQuantity { get; set; }
        public decimal Amount { get; set; }
        public decimal TransferAmount { get; set; }
        public Guid? BranchId { get; set; }
        public Guid StockTransferId { get; set; }
        public decimal RemainingQuantity { get; set; }
        public decimal ReceivedQuantity { get; set; }
        public decimal LineTotal { get; set; }
        public ProductDropdownLookUpModel Product { get; set; }
    }
}
