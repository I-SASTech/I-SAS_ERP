using System;


namespace Focus.Business.Sales.Commands.UpdateSale
{
    public class UpdateSaleLookUpModel
    {
        public Guid Id { get; set; }
        public string Reason { get; set; }
        public bool IsClose { get; set; }
        public bool IsDeliveryChallan { get; set; }
        public bool IsPurchaseOrder { get; set; }
    }
}
