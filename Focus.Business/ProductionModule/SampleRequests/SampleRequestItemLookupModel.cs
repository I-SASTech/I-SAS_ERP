using System;
using System.Collections.Generic;
using System.Text;
using Focus.Business.Sales.Queries.GetSaleDetail;

namespace Focus.Business.ProductionModule.SampleRequests
{
    public class SampleRequestItemLookupModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string Description { get; set; }
        public int HighQuantity { get; set; }
        public int UnitPerPack { get; set; }

        public int Quantity { get; set; }
        public Guid SampleRequestId { get; set; }
        public ProductDropdownLookUpModel Product { get; set; }

    }
}
