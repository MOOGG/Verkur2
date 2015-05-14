using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veidibokin.Models
{
    public class UserStatusViewModel
    {
        public List<Feed> myFeedList { get; set; }
        public List<Catch> myCatchList { get; set; }
        public Feed myFeed { get; set; }
        public Catch myCatch { get; set; }
        public HttpPostedFileBase myPic { get; set; }
        public List<FollowList> myFollowList { get; set; }         
    }
}