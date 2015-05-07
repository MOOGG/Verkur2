using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veidibokin.Models;

namespace Veidibokin.Controllers
{
	public class ContentController : Controller
	{
		public ActionResult NewUserStatus(int userid, FormCollection collection)
		{
			string statusID = collection["userStatusID"];
			string status = collection["StatusText"];

			if (String.IsNullOrEmpty(statusID))
			{
				return View("Error");
			}
			if (String.IsNullOrEmpty(status))
			{
				return RedirectToAction("Index", "Content", new { id = statusID });
			}

			string userName = User.Identity.Name;
			int id = Int32.Parse(statusID);
			UserStatus user = UserStatus.Instance.GetMovieById(userName, id);//TODO fall í repo sem sækir statusinn 
			if (user != null)
			{
				UserStatus userStatus = new UserStatus { statusText = status, userId = userName, ID = id };
				MovieAppRepository.Instance.AddReview(userStatus);
				return RedirectToAction("Index", "Content", new { id = statusID });
			}
			return View("Error");
		}
	}
}