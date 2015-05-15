using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veidibokin.Models
{
    public class CatchFeed
    {
        public int catchID { get; set; }
        public string zone { get; set; }
        public string fish { get; set; }
        public string bait { get; set; }
        public double? length { get; set; }
        public double? weight { get; set; }
    }
}