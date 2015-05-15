using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Veidibokin.Models;
using Veidibokin.Repositories;
using System.IO;
using System.Drawing;
using System.Net;
using System.Web.Helpers;

namespace Veidibokin.Controllers
{
	public class HomeController : Controller
	{
        [Authorize]
		public ActionResult Index()
        {
            var myStatusRepo = new StatusRepository();

            var statusList = new List<Feed>();
            var userId = User.Identity.GetUserId();
            var followList = new List<FollowList>();
            var catchList = new List<CatchFeed>();

            statusList = myStatusRepo.ReturnFeedStatuses(userId);
            followList = myStatusRepo.ReturnFollowersList(userId);
            catchList = myStatusRepo.ReturnCatch();

            UserStatusViewModel feedView = new UserStatusViewModel();
            
            feedView.myFeedList = statusList;
            feedView.myFollowList = followList;
            feedView.myCatchFeedList = catchList;

            return View(feedView);
		}

	    public ActionResult About()
		{
			//ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			//ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}