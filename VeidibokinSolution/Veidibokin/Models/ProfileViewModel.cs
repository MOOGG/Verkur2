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
        //public List<string> myFollowersList { get; set; }
        public List<FullNameForFeed> myFullNameList { get; set; } 
    }

    // viewmodel klasi
    public class FullNameForFeed
    {
        public string fullName { get; set; }
        public string userId { get; set; }
    }
}