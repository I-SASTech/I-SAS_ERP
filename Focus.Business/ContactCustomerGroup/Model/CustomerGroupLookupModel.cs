using Focus.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Focus.Business.ContactCustomerGroup.Model
{
    public class CustomerGroupLookupModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public List<ContactListLookupModel> ContactsList { get; set; }
    }
}
