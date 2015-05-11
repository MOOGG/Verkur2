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

            statusList = myStatusRepo.ReturnFeedStatuses(userId);

            UserStatusViewModel temp = new UserStatusViewModel();

            temp.myFeedList = statusList;

            return View(temp);
		}

        // er ég kannski ekki að senda rétt á milli frá formi í Index til controllers ?
        // ??????? Gæti ég ekki BARA sent módelið inn hér að neðan, Check it out !! ???????????
        [HttpPost]
        public ActionResult PostStatus(UserStatusViewModel collection)
        {
            string status = collection.myFeedList[0].statusText.ToString();
            HttpPostedFileBase file = collection.myPic;
            string directory = @"~/Content/Images/";
            string path = null;

            if (String.IsNullOrEmpty(status))
            {
                return View("Error");
            }

            if (file != null && file.ContentLength > 0)
            {
                var img = Path.GetFileName(file.FileName);
                path = Path.Combine(Server.MapPath(directory),
                           System.IO.Path.GetFileName(file.FileName));
                file.SaveAs(path);
            }

            var userId = User.Identity.GetUserId();

            var myStatusRepo = new StatusRepository();
            myStatusRepo.StatusToDB(status, userId, path);

            // hvaða view-i á ég að skila hér ???
            return RedirectToAction("Index", "Home");
            //return View("Index");
        }

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

        public ActionResult ProfilePage()
        {
            var myProfileRepo = new StatusRepository();

            var statusList = new List<Feed>();
            var userId = User.Identity.GetUserId();

            statusList = myProfileRepo.ReturnFeedStatuses(userId);

            //ViewData["StatusList"] = statusList;

            //ViewBag.UserStatuses = statusList;

            // finna út hvaða view á að vera hér !
            return View(statusList);
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