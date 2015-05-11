using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veidibokin.Models
{
    public class Feed
    {
        public string fullName { get; set; }
        public string statusText { get; set; }
        public System.DateTime dateInserted { get; set; }
        public string statusPicture { get; set; }
    }
}