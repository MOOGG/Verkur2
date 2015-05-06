using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veidibokin.Models
{
    public class Group
    {
        public int ID { get; set; }
        public int photoID { get; set; }
        [StringLength(30)]
        public string groupName { get; set; }
        public string description { get; set; }
    }
}