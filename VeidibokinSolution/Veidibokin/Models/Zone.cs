﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veidibokin.Models
{
    public class Zone
    {
        public int ID { get; set; }
        public int photoID { get; set; }
        [StringLength(30)]
        public string name { get; set; }
        public string description { get; set; }
        public string location { get; set; }
    }
}