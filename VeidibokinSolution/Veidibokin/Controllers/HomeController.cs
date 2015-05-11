using System;
using System.Collections.Generic;
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

            //ViewData["StatusList"] = statusList;

            //ViewBag.UserStatuses = statusList;

            // finna út hvaða view á að vera hér !
            return View(temp);
		}

        // er ég kannski ekki að senda rétt á milli frá formi í Index til controllers ?
        public ActionResult PostStatus(FormCollection collection)
        {
            // væri ekki best að koma picture hér inn... en getur input þá verið FormCollection ?
            string status = collection.Get("myFeedList");

            if (String.IsNullOrEmpty(status))
            {
                return View("Error");
            }

            var userId = User.Identity.GetUserId();

            var myStatusRepo = new StatusRepository();
            myStatusRepo.StatusToDB(status, userId);

            // hvaða view-i á ég að skila hér ???
            return RedirectToAction("Index", "Home");
            //return View("Index");
        }

        // gæti líka gert nýtt fall sem sér sérstaklega um myndina.... eins og þetta hér f neðan
	    [HttpPost]
	    [Authorize]
	    [ValidateAntiForgeryToken]
	    public async Task<ActionResult> PostPicture(UserStatusViewModel model)
	    {

	        if (model.statusPicture != null)
	        {
	            
	        }
	        return null;
	    }

		public ActionResult SearchResult(string searchString)
		{
			ApplicationDbContext db = new ApplicationDbContext();

			var name = from n in db.Zones
							select n;

			if (!String.IsNullOrEmpty(searchString))
			{
				name = name.Where(s => s.zoneName.Contains(searchString));
			}

			return View(name);
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