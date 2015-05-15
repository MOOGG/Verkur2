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
        /// <summary>
        /// Hér er status sem gerður er inní hópum skrifaður niður í
        /// grunn í viðeigandi töflu.
        /// </summary>
		[Authorize]
        [HttpPost]
        public ActionResult GroupPostStatus(GroupViewModel collection, int groupId)
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

			myGroupRepo.GroupStatusToDB(status, userId, fileName, groupId);

			return RedirectToAction("GroupPage", new
			{
				id = groupId
			});
		}

        /// <summary>
        /// Hér er hópasíða fyllt af viðeigandi efni (statusar, fylgjendur o.s.frv.),
        /// n.tt. viewModel fyllt og skilað upp til View
        /// </summary>
		[Authorize]
		public ActionResult GroupPage(int id)
		{
			var myGroupRepo = new GroupRepository();

			var groupStatusList = new List<GroupFeed>();
			var groupMembers = new List<GroupMembersList>();

            var requestMembers = new List<GroupMembersList>();

			groupStatusList = myGroupRepo.ReturnGroupStatuses(id);
			groupMembers = myGroupRepo.ReturnMembersList(id);
		    requestMembers = myGroupRepo.ReturnGroupRequestList(id);

            var groupName = new List<string>();
            var description = new List<string>();

            groupName = myGroupRepo.ReturnGroupName(id);
            description = myGroupRepo.ReturnGroupDescription(id);

			GroupViewModel displayGroup = new GroupViewModel();

			displayGroup.myFeedList = groupStatusList;
			displayGroup.myFullNameList = groupMembers;
		    displayGroup.groupId = id;

            displayGroup.groupName = groupName;
            displayGroup.description = description;

			return View(displayGroup);
		}

        /// <summary>
        /// Skilum upp tómu view til þess að koma í veg fyrir null villu
        /// </summary>
        [HttpGet]
        public ActionResult CreateGroup()
        {
            var emptyGroupModel = new GroupViewModel();
            return View(emptyGroupModel);
        }

        /// <summary>
        /// Hér er hópur búinn til og skráður niður í grunn
        /// </summary>
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

        /// <summary>
        /// Fyllum viewmodel af þeim sem hafa óskað um inngöngu í hóp
        /// og skilum því upp til view
        /// </summary>
        public ActionResult ShowMemberRequests(int groupId)
        {
            var myRepo = new GroupRepository();

            var listOfRequest = myRepo.ReturnGroupRequestList(groupId);

            var returnView = new GroupViewModel();

            returnView.myFullNameList = listOfRequest;
            returnView.groupId = groupId;

            return View(returnView);
        }

        /// <summary>
        /// Virknin bakvið að óska um inngöngu í hóp. Notandi sem óskar um inngöngu er
        /// skráður niður í GroupMembers töflu en þar er bool breyta sem ákvarðar
        /// hvort notandi er meðlimur er ekki. Í upphafi er hún stillt sem false
        /// </summary>
        public ActionResult RequestGroupAccess(int groupId)
        {
            var myRepo = new GroupRepository();

            string userId = User.Identity.GetUserId();

            // IsMember athugar hvort notandi sé partur af hóp nú þegar
            if (!myRepo.IsMember(groupId, userId))
            {
                myRepo.AddGroupMemberToDb(groupId, userId);
            }

            return RedirectToAction("GroupPage", new
            {
                id = groupId
            });
        }

        /// <summary>
        /// Hér er breytunni isMember breytt í true í GroupMember töflunni
        /// </summary>
        public ActionResult AcceptRequest(int groupId, string userId)
        {
            var myRepo = new GroupRepository();

            myRepo.MakeMember(groupId, userId);

            return RedirectToAction("GroupPage", new
            {
                id = groupId
            });
        }

        /// <summary>
        /// Hér er notanda hafnað um inngöngu og hent útúr GroupMembers töflunni.
        /// Ekki er búið að útfæra þessa virni að fullu, og virkar hún því ekki
        /// </summary>
        public ActionResult DenyRequest(int groupId, string userId)
        {
            var myRepo = new GroupRepository();
            myRepo.DenyMemberReq(groupId, userId);

            return RedirectToAction("GroupPage", new
            {
                id = groupId
            });
        }


    }
}