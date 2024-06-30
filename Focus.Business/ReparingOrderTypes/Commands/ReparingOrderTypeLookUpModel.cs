using Focus.Domain.Enum;
using System;

namespace Focus.Business.ReparingOrderTypes.Commands
{
    public class ReparingOrderTypeLookUpModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public ReparingOrderTypeEnum ReparingOrderTypes{ get; set; }
        public bool isActive { get; set; }
        public Guid? BranchId { get; set; }
    }
}
