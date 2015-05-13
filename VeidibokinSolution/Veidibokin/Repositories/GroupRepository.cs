using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Veidibokin.Models;

namespace Veidibokin.Repositories
{
	public class GroupRepository : StatusRepository
	{
		// hér þarf að bæta við byte[] picture sem argument
		public void GroupStatusToDB(string status, string thisuserid, string statusPicture, int groupId)
		{
			using (var dataContext = new ApplicationDbContext())
			{
				var isPublic = false;
				var myRepo = new UserRepository<UserStatus>(dataContext);
				StatusToDB(status, thisuserid, statusPicture, isPublic);

				// held að það þurfi ekkert að tjekka hvort picture sé null því ef null þá verður statusPicture = null
				var newStatus = new UserStatus()
				{
					statusText = status,
					isPublic = true,
					dateInserted = DateTime.Now,
					userId = thisuserid,
					photo = statusPicture
				};

				myRepo.Insert(newStatus);

				dataContext.SaveChanges();
			}
		}

		/*public byte[] returnImageFromDb(string userId)
		{
			byte[] returnPic = null;
			using (var dataContext = new ApplicationDbContext())
			{
				var image = dataContext.UserStatuses.FirstOrDefault(i => i.userId == userId);
				returnPic = image.photo;

			}
			return returnPic;
		}*/
		public List<GroupFeed> ReturnGroupStatuses(string userId)
		{
			var returnList = new List<GroupFeed>();

			using (var dataContext = new ApplicationDbContext())
			{
				var statuses = (from status in dataContext.UserStatuses
								where (status.isPublic == true && status.userId == userId)
								select new { status = status.statusText, date = status.dateInserted, userId = status.userId });

				List<GroupFeed> groupfeed = (from users in dataContext.Users
									   join status in statuses on users.Id equals status.userId
									   orderby status.date descending
											select new GroupFeed { fullName = users.fullName, statusText = status.status, dateInserted = status.date, statusUserId = status.userId }).ToList();

				return groupfeed;
			}
		}

		public List<GroupFeed> ReturnFeedStatuses(int groupId)
		{

			using (var dataContext = new ApplicationDbContext())
			{
				var memberStatuses = (from m in dataContext.GroupStatuses
								 where m.groupID == groupId
								 select m.statusID);

				var statuses = (from status in dataContext.UserStatuses
								where memberStatuses.Contains(status.ID)
								select new { status = status.statusText, date = status.dateInserted, userId = status.userId, photo = status.photo });

				List<GroupFeed> groupfeed = (from users in dataContext.Users
									   join status in statuses on users.Id equals status.userId
									   orderby status.date descending
									   select new GroupFeed { fullName = users.fullName, statusText = status.status, dateInserted = status.date, statusUserId = status.userId, statusPhoto = status.photo }).ToList();

				return groupfeed;
			}
		}

		 /*List<GroupMembersList> ReturnMembersList(string userId)
		{
			using (var dataContext = new ApplicationDbContext())
			{
				var membersNames = (from user in dataContext.GroupMembers
									join followid in dataContext.UserFollowers on user.groupID equals followid.userID
									where (memberStatus == true && ))
									select new GroupMembersList { fullName = user.fullName, userId = user.Id }).ToList();

				return membersNames;
			}
		}*/
	}
}