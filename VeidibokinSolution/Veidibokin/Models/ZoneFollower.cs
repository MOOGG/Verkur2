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
        [Column(Order = 1)]
        public int zoneID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int userID { get; set; }
    }
}