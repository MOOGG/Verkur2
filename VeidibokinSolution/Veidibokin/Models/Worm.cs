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
    public class Worm
    {
        [Key]
        [ForeignKey("User")]
        [Column(Order = 1)]
        public string userID { get; set; }
        [Key]
        [ForeignKey("UserStatus")]
        [Column(Order = 2)]
        public int statusID { get; set; }
        public int wormRating { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual UserStatus UserStatus { get; set; }
    }
}