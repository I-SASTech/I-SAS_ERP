using System;
using System.Collections.Generic;

namespace Focus.Business.PurchaseTemplate.Command
{
    public class PurchaseTemplateLookUpModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public int Day { get; set; }
        public Guid SupplierId { get; set; }
        public Guid TaxRateId { get; set; }
        public Guid WareHouseId { get; set; }
        public string TaxMethod { get; set; }
        public string Note { get; set; }
        public bool IsRaw { get; set; }
        public ICollection<PurchaseTemplateItemLookupModel> PurchaseOrderItems { get; set; }
        public bool IsMultiUnit { get; set; }
        public bool IsActive { get; set; }
        public Guid? BranchId { get; set; }
    }
}
