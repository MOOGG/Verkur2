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
    public class ProfileController : Controller
    {
		[Authorize]
		public ActionResult ProfilePage(string id)
		{
			var myProfileRepo = new StatusRepository();

			var statusList = new List<Feed>();
			var followList = new List<FollowList>();

			statusList = myProfileRepo.ReturnProfileStatuses(id);
			followList = myProfileRepo.ReturnFollowingList(id);

			ProfileViewModel displayProfile = new ProfileViewModel();

			displayProfile.myFeedList = statusList;
			displayProfile.myFullNameList = followList;
			displayProfile.userNameId = id;

			return View(displayProfile);
		}

		public ActionResult Follow(string id)
		{
			var statusList = new List<Feed>();
			var followList = new List<FollowList>();

			var myProfileRepo = new StatusRepository();
			statusList = myProfileRepo.ReturnProfileStatuses(id);
			followList = myProfileRepo.ReturnFollowingList(id);

			var displayProfile = new ProfileViewModel();
			displayProfile.myFeedList = statusList;
			displayProfile.myFullNameList = followList;
			displayProfile.OpenID = id;

			string yourId = id;
			string otherId = User.Identity.GetUserId();

			var myRepo = new StatusRepository();
			myRepo.MakeFollowers(yourId, otherId);

			return RedirectToAction("ProfilePage", new
			{
				id = id
			});
			//return View(displayProfile.OpenID);
		}
		public ActionResult PostStatus(UserStatusViewModel collection, int? catchId)
		{
			string status = collection.myFeed.statusText.ToString();
			HttpPostedFileBase file = collection.myPic;
			string directory = @"~/Content/Images/";
			string path = null;
			string fileName = null;

			if (String.IsNullOrEmpty(status))
			{
				return View("Error");
			}

			if (file != null && file.ContentLength > 0)
			{
				WebImage img = new WebImage(file.InputStream);
				if (img.Width > 300)
					img.Resize(300, 300);
				fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
				path = Path.Combine(Server.MapPath(directory), fileName);
				img.Save(path);
			}

			var userId = User.Identity.GetUserId();

			var myStatusRepo = new StatusRepository();
			var isPublic = true;

			myStatusRepo.StatusToDB(status, userId, fileName, isPublic);

			// hvaða view-i á ég að skila hér ???
			return RedirectToAction("Index", "Home");
			//return View("Index");
		}
    }
}