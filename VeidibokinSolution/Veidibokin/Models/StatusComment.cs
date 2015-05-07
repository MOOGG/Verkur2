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
    public class StatusComment
    {
        public int ID { get; set; }
        [ForeignKey("User")]
        public string userID { get; set; }
        [ForeignKey("Status")]
        public int statusID { get; set; }
        public string comment { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual UserStatus Status { get; set; }
    }
}