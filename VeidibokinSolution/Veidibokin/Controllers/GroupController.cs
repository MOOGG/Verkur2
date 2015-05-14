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
    public class GroupController : Controller
    {
		[Authorize]
		public ActionResult GroupPostStatus(GroupViewModel collection)
		{
			string status = collection.myFeed.statusText.ToString();
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

			var myGroupRepo = new GroupRepository();
			int currentGroupId = 1;

			myGroupRepo.GroupStatusToDB(status, userId, fileName, currentGroupId);

			return RedirectToAction("GroupPage", new
			{
				id = currentGroupId
			});
		}

		[Authorize]
		public ActionResult GroupPage(int id)
		{
			var myGroupRepo = new GroupRepository();

			var groupStatusList = new List<GroupFeed>();
			var groupMembers = new List<GroupMembersList>();

			groupStatusList = myGroupRepo.ReturnGroupStatuses(id);
			groupMembers = myGroupRepo.ReturnMembersList(id);

			GroupViewModel displayGroup = new GroupViewModel();

			displayGroup.myFeedList = groupStatusList;
			displayGroup.myFullNameList = groupMembers;

			return View(displayGroup);
		}

		public ActionResult CreateGroup()
		{



			return View();
		}

    }
}