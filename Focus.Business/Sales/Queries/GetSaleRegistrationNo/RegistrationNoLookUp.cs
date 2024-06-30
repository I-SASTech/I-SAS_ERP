using Focus.Business.Prefix.Model;

namespace Focus.Business.Sales.Queries.GetSaleRegistrationNo
{
    public class RegistrationNoLookUp
    {
        public string Hold { get; set; }
        public string Paid { get; set; }
        public string SaleReturn { get; set; }
        public string ProformaInvoice { get; set; }
        public string GlobalInvoice { get; set; }
        public string ReceiptInvoice { get; set; }
        public string PurchaseOrder { get; set; }

        public string DocumentNo { get; set; }
        public string Draft { get; set; }
        public string Post { get; set; }
        public string PurchaseReturn { get; set; }
        public PrefixiesLookupModel prefixiesLookupModel { get; set; }
    }
}
