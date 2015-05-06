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
        [Column(Order = 1)]
        public int groupID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int userID { get; set; }
        public bool memberStatus { get; set; }
        public bool isAdmin { get; set; }
    }
}