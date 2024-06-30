using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class EmailConfiguration : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Email { get; set; }   
        public string Password { get; set; }   
        public string SmtpServer { get; set; }   
        public int Port { get; set; }   
        public string Server { get; set; }   
        public bool IsActive { get; set; }   
        public bool IsDefault { get; set; }   
    }
}
