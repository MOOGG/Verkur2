using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Veidibokin.Repositories;

namespace Veidibokin.Models
{
    public class ProfileViewModel
    {
        public List<Feed> myFeedList { get; set; }
        public HttpPostedFileBase statusPicture { get; set; }
        public List<FollowList> myFullNameList { get; set; }
        public string userNameId { get; set; }
        public string OpenID { set; get; }
        public List<string> fullName { get; set; }
        public bool isFollowing { get; set; }
        public List<CatchFeed> myCatchFeedList { get; set; }
    }

    // viewmodel klasi
    public class FollowList
    {
        public string fullName { get; set; }
        public string userId { get; set; }
    }
}