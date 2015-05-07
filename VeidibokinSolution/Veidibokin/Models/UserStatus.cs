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
    public class UserStatus : Element
    {
        public int ID { get; set; }
        public System.DateTime dateInserted { get; set; }
        public string statusText { get; set; }
        [ForeignKey("User")]
        public string userId { get; set; }
        [ForeignKey("Photo")]
        public int photoId { get; set; }
        public bool isPublic { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Photo Photo { get; set; }
    }
}