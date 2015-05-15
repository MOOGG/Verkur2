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
        // GRÆJA lausn fyrir harðkóðun í þessum controller
		[Authorize]
        [HttpPost]
        public ActionResult GroupPostStatus(GroupViewModel collection)
		{
			string status = collection.myFeed.statusText.ToString();
			HttpPostedFileBase file = collection.statusPicture;
			string directory = @"~/Content/Images/";
			string path = null;
			string fileName = null;
		    int groupId = collection.groupId;
            //bla = @Model.groupId

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

            // MUNA AÐ GRÆJA ÞETTA SVO ÞETTA SÉ EKKI HARÐKÓÐAÐ !!!!!!!
			int currentGroupId = 1;

			myGroupRepo.GroupStatusToDB(status, userId, fileName, groupId);

			return RedirectToAction("GroupPage", new
			{
				id = groupId
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
		    displayGroup.groupId = id;

			return View(displayGroup);
		}

        [HttpGet]
        public ActionResult CreateGroup()
        {
            var emptyGroupModel = new GroupViewModel();
            return View(emptyGroupModel);
        }

        [HttpPost]
        public ActionResult CreateGroup(GroupViewModel collection)
        {
            string creatorId = User.Identity.GetUserId();
            HttpPostedFileBase file = collection.myGroup.groupPic;
            string directory = @"~/Content/Images/";
            string path = null;
            string fileName = null;

            if (file != null && file.ContentLength > 0)
            {
                WebImage img = new WebImage(file.InputStream);
                if (img.Width > 300)
                    img.Resize(300, 300);
                fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                path = Path.Combine(Server.MapPath(directory), fileName);
                img.Save(path);
            }

            var groupName = collection.myGroup.groupName;
            var groupDescription = collection.myGroup.groupDescription;

            var myGroupRepo = new GroupRepository();

            int groupId = myGroupRepo.AddGroupToDb(groupName, groupDescription, path, creatorId);

            var groupView = new GroupViewModel();
            groupView.groupId = groupId;

            return RedirectToAction("GroupPage", new
            {
                id = groupId
            });
        }

        //public ActionResult ShowMemberRequests(int groupId)//, string userId)
        public ActionResult ShowMemberRequests(int id)
        {
            var myRepo = new GroupRepository();

            var listOfRequest = myRepo.ReturnGroupRequestList(id);

            var returnView = new GroupViewModel();

            returnView.myFullNameList = listOfRequest;

            return View(returnView);
        }

        public ActionResult RequestGroupAccess(int id)
        {
            var myRepo = new GroupRepository();

            string userId = User.Identity.GetUserId();

            myRepo.AddGroupMemberToDb(id, userId);

            return RedirectToAction("GroupPage", new
            {
                id = id
            });
        }
    }
}