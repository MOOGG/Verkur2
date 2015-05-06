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
    public class GroupCatch
    {
        [Key]
        [ForeignKey("Group")]
        [Column(Order = 1)]
        public int groupID { get; set; }
        [Key]
        [ForeignKey("Catch")]
        [Column(Order = 2)]
        public int catchID { get; set; }
    
        public virtual Group Group { get; set; }
        public virtual Catch Catch { get; set; }
    }
}