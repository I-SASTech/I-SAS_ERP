﻿using System;

namespace Focus.Business.PurchaseTemplate.Command
{
    public class CompanyLookUp
    {
        public Guid Id { get; set; }
        public Guid? InventoryAccountId { get; set; }
        public Guid? VatPayableAccountId { get; set; }
        public Guid? VatReceiableAccountId { get; set; }
        public Guid? SaleAccountId { get; set; }
        public Guid? DiscountPayableAccountId { get; set; }
        public Guid? DiscountReceivableAccountId { get; set; }
        public Guid? FreeofCostAccountId { get; set; }
        public Guid? StockInAccountId { get; set; }
        public Guid? StockOutAccountId { get; set; }
        public Guid? BundleAccountId { get; set; }
        public Guid? PromotionAccountId { get; set; }
        public Guid? BankId { get; set; }
        public Guid? CashId { get; set; }
        public Guid? TaxRateId { get; set; }
        public string CurrencyId { get; set; }
        public string BarcodeType { get; set; }
        public string TaxMethod { get; set; }
        public string BackupPath { get; set; }
        public bool AutoSync { get; set; }
        public int AutoSyncInterval { get; set; }
        public Guid CompanyId { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
