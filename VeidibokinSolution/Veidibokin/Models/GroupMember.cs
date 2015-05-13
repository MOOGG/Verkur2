using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veidibokin.Models
{
    public class GroupMember
    {
        [Key]
        [ForeignKey("Group")]
        [Column(Order = 1)]
        public int groupID { get; set; }
        [Key]
        [ForeignKey("User")]
        [Column(Order = 2)]
        public string userID { get; set; }
        public bool memberStatus { get; set; }
        public bool isAdmin { get; set; }

        public virtual Group Group { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}