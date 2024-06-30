using Focus.Domain.Interface;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class Currency : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Sign { get; set; }
        public string ArabicSign { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<OtherCurrency> OtherCurrencies { get; set; }
        public virtual ICollection<Bank> Banks { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
