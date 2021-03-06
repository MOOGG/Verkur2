﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veidibokin.Models
{
    public class Feed
    {
        public string statusUserId { get; set; }
        public string fullName { get; set; }
        public string statusText { get; set; }
        public System.DateTime dateInserted { get; set; }
        public string statusPhoto { get; set; }
        public int? catchId { get; set; }
    }
}