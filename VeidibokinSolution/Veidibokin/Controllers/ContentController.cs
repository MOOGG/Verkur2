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
			string status = collection["userStatusID"];
			string statusText = collection["StatusText"];

			if (String.IsNullOrEmpty(status))
			{
				return View("Error");
			}
			if (String.IsNullOrEmpty(statusText))
			{
				return RedirectToAction("Index", "Content", new { id = status });
			}

			string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
			int id = Int32.Parse(status);
			UserStatus user = UserStatus.Instance.GetMovieById(username, id);
			if (user != null)
			{
				UserStatus userStatus = new NewUserStatus { Text = statusText, Username = username, MovieId = id };
				MovieAppRepository.Instance.AddReview(userStatus);
				return RedirectToAction("Detail", "MovieApp", new { id = status });
			}
			return View("Error");
		}
	}
}