using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class LoginHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime LoginDateTime { get; set; }
        public DateTime LogoutDateTime { get; set; }
        public string IpAddress { get; set; }
        public string OperatingSystem { get; set; }
    }
}
