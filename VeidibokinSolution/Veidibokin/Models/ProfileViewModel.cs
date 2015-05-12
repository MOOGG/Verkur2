using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veidibokin.Models
{
    public class ProfileViewModel
    {
        public List<Feed> myFeedList { get; set; }
        public HttpPostedFileBase statusPicture { get; set; }
        public List<string> myFollowersList { get; set; } 
    }
}