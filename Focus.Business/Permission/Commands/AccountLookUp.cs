using System;


namespace Focus.Business.Permission.Commands
{
   public class AccountLookUp
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Guid? CostCenterId { get; set; }
      
    }
}
