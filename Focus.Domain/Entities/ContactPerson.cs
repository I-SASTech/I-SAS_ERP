using System;

namespace Focus.Domain.Entities
{
    public class ContactPerson : BaseEntity
    {
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public Guid ContactId { get; set; }
        public virtual Contact Contact { get; set; }

    }
}
