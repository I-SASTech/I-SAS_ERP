using Focus.Business.PurchaseOrders.Queries.GetPurchaseOrderDetails;
using System.Collections.Generic;

namespace Focus.Business.PurchaseOrders.Commands.AddPurchaseOrder.DocumentWiseList
{
    public class DocumentWiseLookUpModel
    {
        public string DocumentName { get; set; }
        public List<PurchaseOrderDetailLookUpModel> Lookups { get; set; }
    }
}
