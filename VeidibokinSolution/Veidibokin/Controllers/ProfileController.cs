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
        /// <summary>
        /// ProfilePage birtir prófíl síðu notenda sem þú vilt skoða
        /// </summary>
		[Authorize]
		public ActionResult ProfilePage(string id)
		{
			var myProfileRepo = new StatusRepository();

			var statusList = new List<Feed>();
			var followList = new List<FollowList>();
            var catchList = new List<CatchFeed>();
            
			statusList = myProfileRepo.ReturnProfileStatuses(id);
			followList = myProfileRepo.ReturnFollowingList(id);
            catchList = myProfileRepo.ReturnCatch(id);
            
            var userList = new List<string>();

			statusList = myProfileRepo.ReturnProfileStatuses(id);
			followList = myProfileRepo.ReturnFollowingList(id);
            userList = myProfileRepo.ReturnUserName(id);

			ProfileViewModel displayProfile = new ProfileViewModel();

			displayProfile.myFeedList = statusList;
			displayProfile.myFullNameList = followList;
			displayProfile.userNameId = id;
            displayProfile.fullName = userList;
            displayProfile.myCatchFeedList = catchList;

			return View(displayProfile);
		}

        /// <summary>
        /// Follow útfærir virknina þegar þú sem notandi vilt "fylgja" öðrum notenda
        /// </summary>
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
            displayProfile.isFollowing = true;

			string yourId = id;
			string otherId = User.Identity.GetUserId();

            // AreFollowers fallið athugar niður í grunn hvort notendur
            // eru nú þegar að fylgja hvorum öðrum. Ef ekki þá skrifum við vensl í töflu
            if (!myProfileRepo.AreFollowers(id,otherId))
		    {
                var myRepo = new StatusRepository();
                myRepo.MakeFollowers(otherId, id);
		    }
            
			return RedirectToAction("ProfilePage", new
			{
				id = id
			});
		}

        /// <summary>
        /// Skilar tómu view-i þegar index síðunni þinni er hlaðið inn til
        /// þess að fá ekki null villu
        /// </summary>
        [HttpGet]
        public ActionResult PostStatus()
        {
            var viewModel = new UserStatusViewModel();
            return View(viewModel);
        }

        /// <summary>
        /// Í PostStatus er status settur niður í grunn í viðeigandi töflu
        /// </summary>
        [HttpPost]
        public ActionResult PostStatus(UserStatusViewModel collection, int? catchId)
		{
            string status = collection.myFeed.statusText.ToString();
            HttpPostedFileBase file = collection.myPic;
            string directory = @"~/Content/Images/";
            string path = null;
            string fileName = null;

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

            myStatusRepo.StatusToDB(status, userId, fileName, isPublic, catchId);
            
            return RedirectToAction("Index", "Home");
		}
    }
}