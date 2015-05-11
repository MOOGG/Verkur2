using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veidibokin.Models
{
    public class SingleFeed
    {
        public Feed feed { get; set; }
        public string userId { get; set; }
        public string fullName { get; set; }
        public string statusText { get; set; }
        public System.DateTime dateInserted { get; set; }
        public string statusPicture { get; set; }
    }
}