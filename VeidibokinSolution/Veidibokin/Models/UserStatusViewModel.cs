using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veidibokin.Models
{
    public class UserStatusViewModel
    {
        public List<Feed> myFeedList { get; set; }
        public HttpPostedFileBase picture { get; set; }
    }
}