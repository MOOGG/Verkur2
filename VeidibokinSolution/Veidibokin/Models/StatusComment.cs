using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veidibokin.Models
{
    public class StatusComment
    {
        public int ID { get; set; }
        public int userID { get; set; }
        public int statusID { get; set; }
        public string comment { get; set; }
        public int type { get; set; }
    }
}