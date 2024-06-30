using System;

namespace Focus.Business.AutoPurchasing
{
    public class AutoPurchaseSettingLookUp
    {
        public Guid Id { get; set; }
        public int Day { get; set; }
        public string TaxMethod { get; set; }
        public Guid? TaxRateId { get; set; }
        public Guid? WareHouseId { get; set; }
    }
}
