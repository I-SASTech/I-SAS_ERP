using Focus.Business.PurchasePosts.Queries.GetPurchasePostList;
using Focus.Domain.Enum;
using System;
using System.Linq;

namespace Focus.Business.QuotationTemplates.Queries
{
    public class QuotationTemplateListLookUpModel
    
       {
        public Guid Id { get; set; }
        public string RegistrationNumber { get; set; }
      
        public string TemplateName { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public bool IsService { get; set; }


        public static QuotationTemplateListLookUpModel Create(Focus.Domain.Entities.QuotationTemplate purchaseOrder)
        {
            var netAmount = new TotalAmount();
            var lookUpModel = new QuotationTemplateListLookUpModel
            {
                Id = purchaseOrder.Id,
                Description = purchaseOrder.Description,
                RegistrationNumber = purchaseOrder.RegistrationNo,
                TemplateName = purchaseOrder.TemplateName,
                Date = purchaseOrder.Date.ToString("MM/dd/yyyy hh:mm tt"),

            };
            return lookUpModel;
        }
    }
}
