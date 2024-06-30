using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
   public class PaymentVoucherAttachment : BaseEntity, ITenant
   {
       public string Name { get; set; }
       public string NameArabic { get; set; }
       public string Path { get; set; }
       public string Description { get; set; }
       public Guid? PaymentVoucherId { get; set; }
       public virtual PaymentVoucher PaymentVoucher { get; set; }
       public Guid? PurchaseOrderExpenseId { get; set; }
       public virtual PurchaseOrderExpense PurchaseOrderExpense { get; set; }

    }
}
