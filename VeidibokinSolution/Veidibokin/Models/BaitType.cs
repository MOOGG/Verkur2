using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veidibokin.Models
{
    public class BaitType
    {
        public int ID { get; set; }
        [StringLength(30)]
        [Required]
        public string name { get; set; }
    }
}