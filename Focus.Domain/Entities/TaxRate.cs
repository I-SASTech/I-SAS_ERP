using Focus.Domain.Interface;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class TaxRate : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Name { get; set; }
        public string NameArabic { get; set; }

        public decimal Rate { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }
        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public virtual ICollection<PurchasePostItem> PurchasePostItems { get; set; }
        public virtual ICollection<SaleItem> SaleItems { get; set; }
        public virtual ICollection<SaleOrderItem> SaleOrderItems { get; set; }
        public virtual ICollection<StockAdjustment> StockAdjustments { get; set; }
        public virtual ICollection<PaymentVoucher> PaymentVouchers { get; set; }
        public virtual ICollection<PurchaseOrderExpense> PurchaseOrderExpenses { get; set; }
        public virtual ICollection<PurchasePostExpense> PurchasePostExpenses { get; set; }
        public virtual ICollection<DailyExpenseDetail> DailyExpenseDetails { get; set; }
        public virtual ICollection<AutoPurchaseTemplateItem> AutoPurchaseTemplateItems { get; set; }
        public virtual ICollection<GoodReceiveNoteItem> GoodReceiveNoteItems { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Sale> SaleUnRegisteredTaxes { get; set; }
        public virtual ICollection<SaleOrder> SaleOrders { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<CreditNoteItem> CreditNoteItems { get; set; }
        public virtual ICollection<PaymentRefund> PaymentRefunds { get; set; }
        public virtual ICollection<BranchVoucher> BranchVouchers { get; set; }

    }
}
