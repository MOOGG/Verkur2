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
using PagedList;
using PagedList.Mvc;

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
            followList = myStatusRepo.ReturnFollowingList(userId);

            UserStatusViewModel feedView = new UserStatusViewModel();
            
            feedView.myFeedList = statusList;
            feedView.myFollowList = followList;

            return View(feedView);
		}

        // er ég kannski ekki að senda rétt á milli frá formi í Index til controllers ?
        // ??????? Gæti ég ekki BARA sent módelið inn hér að neðan, Check it out !! ???????????
        public ActionResult PostStatus(UserStatusViewModel collection)
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
			var isPublic = true;

            myStatusRepo.StatusToDB(status, userId, fileName, isPublic);

            // hvaða view-i á ég að skila hér ???
            return RedirectToAction("Index", "Home");
            //return View("Index");
        }

        public ActionResult ProfilePage(string id)
        {
            var myProfileRepo = new StatusRepository();

            var statusList = new List<Feed>();
            var followList = new List<FollowList>();
            //var userId = User.Identity.GetUserId();

            statusList = myProfileRepo.ReturnProfileStatuses(id);
            followList = myProfileRepo.ReturnFollowersList(id);

            ProfileViewModel displayProfile = new ProfileViewModel();

            displayProfile.myFeedList = statusList;
            displayProfile.myFullNameList = followList;

            //ViewData["StatusList"] = statusList;

            //ViewBag.UserStatuses = statusList;

            // finna út hvaða view á að vera hér !
            return View(displayProfile);
        }

		public ActionResult GroupPage(string id)
		{
			var myGroupRepo = new GroupRepository();

			var groupStatusList = new List<GroupFeed>();
			var groupMembers = new List<GroupMembersList>();

			groupStatusList = myGroupRepo.ReturnGroupStatuses(id);
			//groupMembers = myGroupRepo.ReturnMembersList(id);

			GroupViewModel displayGroup = new GroupViewModel();

			displayGroup.myFeedList = groupStatusList;
			displayGroup.myFullNameList = groupMembers;

			return View(displayGroup);
		}

		public ActionResult GroupPostStatus(GroupViewModel collection)
		{
			string status = collection.myFeedList[0].statusText.ToString();
			HttpPostedFileBase file = collection.statusPicture;
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

			var myStatusRepo = new GroupRepository();
			var isPublic = false;

			myStatusRepo.StatusToDB(status, userId, fileName, isPublic);

			return RedirectToAction("GroupPage", "Home");
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