using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veidibokin.Models
{
    public class Group
    {
        public int ID { get; set; }
        public byte[] photo { get; set; }
        [StringLength(30)]
        [Required]
        public string groupName { get; set; }
        public string description { get; set; }

        //public virtual Photo Photo { get; set; }
    }
}