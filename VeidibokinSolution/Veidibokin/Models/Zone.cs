using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veidibokin.Models
{
    public class Zone
    {
        public int ID { get; set; }
        public byte[] photo { get; set; }
        [StringLength(50)]
        [Required]
        public string zoneName { get; set; }
        public string description { get; set; }
        public string location { get; set; }
    }
}