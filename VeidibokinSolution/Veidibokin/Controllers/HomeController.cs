﻿using System;
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
            //var testRepo = new UserRepositoryTest();
            //testRepo.QueryUserName();

			return View();
		}

        // er ég kannski ekki að senda rétt á milli frá formi í Index til controllers ?
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddStatus(UserStatus Status)
        {   
            var userId = User.Identity.GetUserId();
            var dataContext = new ApplicationDbContext();
            var userRepo = new UserRepository<UserStatus>(dataContext);
            userRepo.AddStatus(Status, userId, dataContext);

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
	}
}