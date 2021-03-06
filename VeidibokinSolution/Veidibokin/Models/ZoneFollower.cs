﻿using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Veidibokin.Models
{
    public class ZoneFollower
    {
        [Key]
        [ForeignKey("Zone")]
        [Column(Order = 1)]
        public int zoneID { get; set; }
        [Key]
        [ForeignKey("User")]
        [Column(Order = 2)]
        public string userID { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Zone Zone { get; set; }
    }
}