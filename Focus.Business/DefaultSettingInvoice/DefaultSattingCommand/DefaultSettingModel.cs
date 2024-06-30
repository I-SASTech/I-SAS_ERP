using System;

namespace Focus.Business.DefaultSettingInvoice.DefaultSattingCommand
{
    public  class DefaultSettingModel
    {
        public Guid Id { get; set; }
        public bool IsSaleCredit { get; set; }
        public bool IsPurchaseCredit { get; set; }
        public bool IsCustomeCredit { get; set; }
        public bool IsCustomerPayCredit { get; set; }
        public bool IsSupplierPayCredit { get; set; }
        public bool IsSupplierCredit { get; set; }
        public bool IsCashCustomer { get; set; }
        public string UserId { get; set; }
    }
}
