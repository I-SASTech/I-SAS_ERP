using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Focus.Business.Models
{
   public class ZoneDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
    }
}
