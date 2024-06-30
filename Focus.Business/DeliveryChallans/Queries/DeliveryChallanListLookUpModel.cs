using Focus.Business.PurchasePosts.Queries.GetPurchasePostList;
using Focus.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Focus.Business.DeliveryChallans.Queries
{
    public class DeliveryChallanListLookUpModel
    
       {
        public Guid Id { get; set; }
        public Guid? EditDeliveryChallanId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNameAr { get; set; }
        public string RegistrationNumber { get; set; }
        public string DocumentNumberForOrder { get; set; }
        public string DocumentNumberForSale { get; set; }
      
        public string TemplateName { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Reason { get; set; }
        public bool IsProcessed { get; set; }
        public bool IsService { get; set; }
        public bool IsClose { get; set; }
        public Guid? IsReserved { get; set; }

        public ICollection<DeliveryChallanListLookUpModel> deliveryChallanListLookUpModels { get; set; }
        public string BranchCode { get; set; }
        public int RowCount { get;  set; }
        public int PageSize { get;  set; }
        public int CurrentPage { get;  set; }
        public int PageCount { get;  set; }
        public bool IsDefault { get; set; }
        public string CustomerEmail { get; set; }
    }
}
