using Focus.Domain.Enum;
using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class DeliveryChallanReserved : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public DateTime Date { get; set; }
        public bool IsClose { get; set; }
        public bool IsService { get; set; }
        public bool BilingAddress { get; set; }
        public string CustomerAddress { get; set; }
        public string RegistrationNo { get; set; }
        public Guid? SaleOrderId { get; set; }
        public Guid? SaleInvoiceId { get; set; }
        public Guid? DeliveryChallanId { get; set; }
        public virtual Sale SaleInvoice { get; set; }
        public string TemplateName { get; set; }
        public  string Description { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public Guid? DeliveryId { get; set; }
        public Guid? ShippingId { get; set; }
        public Guid? BillingId { get; set; }
        public Guid? NationalId { get; set; }
        public virtual ICollection<DeliveryChallanReserverdItem> DeliveryChallanReserverdItems { get; set; }
        public Guid? BranchId { get; set; }
    }
}
