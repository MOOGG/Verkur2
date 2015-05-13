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
        public List<FollowList> myFullNameList { get; set; }
        public FollowRelations followRelations { get; set; }
    }

    // viewmodel klasi
    public class FollowList
    {
        public string fullName { get; set; }
        public string userId { get; set; }
    }

    public class FollowRelations
    {
        public string myId { get; set; }
        public string otherId { get; set; }
    }
}