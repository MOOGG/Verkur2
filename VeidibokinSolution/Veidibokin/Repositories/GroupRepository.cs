using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Veidibokin.Models;

namespace Veidibokin.Repositories
{
	public class GroupRepository
	{
		// hér þarf að bæta við byte[] picture sem argument
		public void GroupStatusToDB(string status, string thisuserid, string statusPicture, int groupId)
		{
			using (var dataContext = new ApplicationDbContext())
			{
				var myUserRepo = new UserRepository<UserStatus>(dataContext);
				var myGroupRepo = new UserRepository<GroupStatus>(dataContext);
				
				// held að það þurfi ekkert að tjekka hvort picture sé null því ef null þá verður statusPicture = null
				var newStatus = new UserStatus()
				{
					statusText = status,
					isPublic = false,
					dateInserted = DateTime.Now,
					userId = thisuserid,
					photo = statusPicture,
				};

				myUserRepo.Insert(newStatus);
				
				int statusID = newStatus.ID;

				var newGroupStatus = new GroupStatus()
				{
					groupID = groupId,
					statusID = statusID
				};

				myGroupRepo.Insert(newGroupStatus);

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
		public List<GroupFeed> ReturnGroupStatuses(int GroupID)
		{
            using (var dataContext = new ApplicationDbContext())
            {
                var groupStatuses = (from g in dataContext.GroupStatuses
                                 where g.groupID == GroupID
                                 select g.statusID);

                var statuses = (from status in dataContext.UserStatuses
                                where groupStatuses.Contains(status.ID)
                                select new { status = status.statusText, date = status.dateInserted, userId = status.userId, photo = status.photo });

                List<GroupFeed> groupfeed = (from users in dataContext.Users
                                       join status in statuses on users.Id equals status.userId
                                       orderby status.date descending
                                       select new GroupFeed { fullName = users.fullName, statusText = status.status, dateInserted = status.date, statusUserId = status.userId, statusPhoto = status.photo }).ToList();
                
                return groupfeed;
            }
        }

		 public List<GroupMembersList> ReturnMembersList(int groupId)
		{
			using (var dataContext = new ApplicationDbContext())
			{
				var membersNames = (from user in dataContext.Users
									join groupmemberid in dataContext.GroupMembers on user.Id equals groupmemberid.userID
									where (groupmemberid.memberStatus == true && groupmemberid.groupID == groupId )
									select new GroupMembersList { fullName = user.fullName, userId = user.Id }).ToList();

				return membersNames;
			}
		}

		 public List<GroupMembersList> ReturnGroupRequestList(int groupId)
		 {
			 using (var dataContext = new ApplicationDbContext())
			 {
				 var membersNames = (from user in dataContext.Users
									 join groupmemberid in dataContext.GroupMembers on user.Id equals groupmemberid.userID
									 where (groupmemberid.memberStatus == false && groupmemberid.groupID == groupId)
									 select new GroupMembersList { fullName = user.fullName, userId = user.Id }).ToList();

				 return membersNames;
			 }
		 }
			
	}
}