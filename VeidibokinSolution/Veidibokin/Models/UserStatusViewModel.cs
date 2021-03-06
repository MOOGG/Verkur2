﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veidibokin.Controllers;

namespace Veidibokin.Models
{
    /// <summary>
    /// Þetta ViewModel var notað við birtingar userstatusa
    /// </summary>
    public class UserStatusViewModel
    {
        public List<Feed> myFeedList { get; set; }
        public Feed myFeed { get; set; }
        public Catch myCatch { get; set; }
        public HttpPostedFileBase myPic { get; set; }
        public List<FollowList> myFollowList { get; set; }
        public List<CatchFeed> myCatchFeedList { get; set; }
    }
}