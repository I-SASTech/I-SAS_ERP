using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Focus.Business.Sales.Queries.GetSaleDetail;
using System;

namespace Focus.Business.StockRequests.Models
{
    public class StockRequestItemsLookupModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public DateTime Date { get; set; }
        public Guid? BranchId { get; set; }
        public Guid StockRequestId { get; set; }
        public decimal Quantity  { get; set; }
        public decimal Amount  { get; set; }
        public decimal RemainingQuantity { get; set; }
        public decimal TransferAmount { get; set; }
        public decimal TransferQuantity { get; set; }
        public decimal LineTotal { get; set; }
        public decimal TotalPiece { get; set; }
        public decimal highQty { get; set; }
        public ProductDropdownLookUpModel Product { get; set; }
    }
}
