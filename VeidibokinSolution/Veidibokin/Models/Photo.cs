using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Veidibokin.Models
{
    public class Photo
    {
        public int ID { get; set; }
        [StringLength(255)]
        [Required]
        public string photo { get; set; }
    }
}