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

            statusList = myStatusRepo.ReturnFeedStatuses(userId);
            followList = myStatusRepo.ReturnFollowersList(userId);

            UserStatusViewModel feedView = new UserStatusViewModel();
            
            feedView.myFeedList = statusList;
            feedView.myFollowList = followList;

            return View(feedView);
		}

        // er ég kannski ekki að senda rétt á milli frá formi í Index til controllers ?
        // ??????? Gæti ég ekki BARA sent módelið inn hér að neðan, Check it out !! ???????????
        public ActionResult PostStatus(UserStatusViewModel collection, int? catchId)
        {
            string status = collection.myFeedList[0].statusText.ToString();
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
            myStatusRepo.StatusToDB(status, userId, fileName);

            // hvaða view-i á ég að skila hér ???
            return RedirectToAction("Index", "Home");
            //return View("Index");
        }

		[Authorize]
        public ActionResult PostCatch (UserStatusViewModel collection)
        {
            int zoneID = collection.myCatchList[0].zoneID;
            int baitID = collection.myCatchList[0].baitTypeID;
            int fishID = collection.myCatchList[0].fishTypeId;
            double? length = collection.myCatchList[0].length;
            double? weight = collection.myCatchList[0].weight;
                        
            var myCatchRepo = new StatusRepository();
            Catch newCatch = myCatchRepo.CatchToDB(zoneID, baitID, fishID, length, weight);

            var catchId = newCatch.ID;
            PostStatus(collection, catchId);
            
            return RedirectToAction("Index", "Home");
        }
        
        
        [Authorize]
		public ActionResult SearchResult(string searchString)
		{
            SearchResultViewModel empty = new SearchResultViewModel();
            empty.mySearchResultList = new List<SearchResult>();
            if (!String.IsNullOrEmpty(searchString))
            {
                var mySearchRepo = new SearchRepository();

                var searchResultList = new List<SearchResult>();
            
                searchResultList = mySearchRepo.ReturnSearchResult(searchString);

                SearchResultViewModel temp = new SearchResultViewModel();

                temp.mySearchResultList = searchResultList;
               
                return View(temp);
            }
            else
            {
                return View(empty);
            }
		}

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

            return RedirectToAction("ProfilePage", "Home");
            //return View(displayProfile.OpenID);
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