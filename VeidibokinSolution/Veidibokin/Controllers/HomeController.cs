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
            myStatusRepo.StatusToDB(status, userId, fileName);

            // hvaða view-i á ég að skila hér ???
            return RedirectToAction("Index", "Home");
            //return View("Index");
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
            //var userId = User.Identity.GetUserId();

            statusList = myProfileRepo.ReturnProfileStatuses(id);
            followList = myProfileRepo.ReturnFollowersList(id);

            ProfileViewModel displayProfile = new ProfileViewModel();

            displayProfile.myFeedList = statusList;
            displayProfile.myFullNameList = followList;
            //displayProfile.followRelations.myId = id;
            //displayProfile.followRelations.otherId = displayProfile.myFeedList[0].statusUserId;
            
            string myId = User.Identity.GetUserId();
            string otherId = id;//displayProfile.myFeedList[0].statusUserId;

            //myProfileRepo.MakeFollowers(myId, otherId);
            //ViewData["StatusList"] = statusList;

            //ViewBag.UserStatuses = statusList;

            // finna út hvaða view á að vera hér !
            return View(displayProfile);
        }

        public ActionResult Follow(string id)
        {
            // COPY ÚR FALLI FYRIR OFAN... ÞARF ÉG AÐ GERA ÞETTA TIL AÐ BIRTA displayProfile VIEW HÉR AÐ OFAN ???
            var myProfileRepo = new StatusRepository();

            var statusList = new List<Feed>();
            var followList = new List<FollowList>();
            //var userId = User.Identity.GetUserId();

            statusList = myProfileRepo.ReturnProfileStatuses(id);
            followList = myProfileRepo.ReturnFollowersList(id);

            ProfileViewModel displayProfile = new ProfileViewModel();

            displayProfile.myFeedList = statusList;
            displayProfile.myFullNameList = followList;
            // harðkóðun prófuð til þess að athuga virkni með að skrá niður í grunn. Notum samt ekki þennan
            // controller í view-i
            string yourId = id;
            string otherId = User.Identity.GetUserId();

            //ProfileViewModel displayProfile = new ProfileViewModel();

            // mögulega þarf ég ekkert að senda þetta inní viewmodel en ég þarf bool breytu til að vita
            // hvort ég eigi að birta "fylgja" hnappinn... hvernig er best að gera þetta ??
            //displayProfile.followRelations.myId = yourId;
            //displayProfile.followRelations.otherId = "06d222ac-be42-4c73-aae6-8a550a155d4e";
            //displayProfile.followRelations.isFollowing = true;

            StatusRepository myRepo = new StatusRepository();
            myRepo.MakeFollowers(yourId, otherId);

            return RedirectToAction("Index", "Home");
            //return View(displayProfile);
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