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
    public class UserFollower
    {
        [Key]
        [Column(Order = 1)]
        public int userID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int followerID { get; set; }
    }
}