using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Veidibokin.Models;
using Veidibokin.Repositories;

namespace Veidibokin.Controllers
{
	public class HomeController : Controller
	{
        [AllowAnonymous]
		public ActionResult Index()
		{
			return View();
		}

        // er ég kannski ekki að senda rétt á milli frá formi í Index til controllers ?
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddStatus(UserStatus status)
        {
            // tek inn statusText frá ViewModel-i

            // þarf ég að gera fall sem gerir allt það sem er hér að neðan eins og í UserRepoTest... ???

            // bý til statusinn sem ég ætla að adda
            var newStatus = new UserStatus()
            {
                statusText = status.statusText,
                isPublic = true,
                dateInserted = DateTime.Now,
                userId = User.Identity.GetUserId()
            };

            var dataContext = new ApplicationDbContext();    
            var userStatusRepo = new UserRepository<UserStatus>(dataContext);
            userStatusRepo.Insert(newStatus);
            dataContext.SaveChanges();

            //insertStatus(model);

            // hvaða view-i á ég að skila hér ???
            return RedirectToAction("Index", "Home");
            //return View("Index");
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

	    public void insertStatus(UserStatus status)
	    {
	        using (var dataContext = new ApplicationDbContext())
	        {
                var newStatus = new UserStatus()
                {
                    statusText = status.statusText,
                    isPublic = true,
                    dateInserted = DateTime.Now,
                    userId = User.Identity.GetUserId()
                };

	            dataContext.UserStatuses.Add(newStatus);
	            dataContext.SaveChanges();
	        }
	    }
	}
}