using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veidibokin.Models;

namespace Veidibokin.Repositories
{
	public class GroupRepository
    {
        // !!!!!!!KEMUR VILLA HÉR, þegar reynt er að pósta group status.... skoða þetta !
		public void GroupStatusToDB(string status, string thisuserid, string statusPicture, int groupId)
		{
		    int statusID = 0;

		    using (var dataContext = new ApplicationDbContext())
		    {
		        var myUserRepo = new UserRepository<UserStatus>(dataContext);
		        //var myGroupRepo = new UserRepository<GroupStatus>(dataContext);

		        // held að það þurfi ekkert að tjekka hvort picture sé null því ef null þá verður statusPicture = null
		        var newStatus = new UserStatus()
		        {
		            statusText = status,
		            isPublic = false,
		            dateInserted = DateTime.Now,
		            userId = thisuserid,
		            photo = statusPicture,
		        };

                statusID = newStatus.ID;

		        myUserRepo.Insert(newStatus);
		        // ÞARF AÐ GERA SAVECHANGES TVISVAR ??? kemur einnhver villa þegar maður postar groupstatus
		        dataContext.SaveChanges();
		    }

		    using (var dataContext1 = new ApplicationDbContext())
		    {
		        var myGroupRepo = new UserRepository<GroupStatus>(dataContext1);

				var newGroupStatus = new GroupStatus()
				{
					groupID = groupId,
					statusID = statusID
				};

				myGroupRepo.Insert(newGroupStatus);
                dataContext1.SaveChanges();
		    }	
		
		}

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

	    public int AddGroupToDb(string groupName, string groupDescription, string photoPath, string userId)
	    {
	        using (var dataContext = new ApplicationDbContext())
	        {
	            var myGroupRepo = new UserRepository<Group>(dataContext);

	            var newGroup = new Group()
	            {
                    groupName = groupName,
                    description = groupDescription,
                    photo = photoPath
	            };

                myGroupRepo.Insert(newGroup);

	            var adminUser = new GroupMember()
	            {
	                groupID = newGroup.ID,
	                userID = userId,
	                memberStatus = true,
	                isAdmin = true
	            };

                var myGroupMemberRepo = new UserRepository<GroupMember>(dataContext);
	            myGroupMemberRepo.Insert(adminUser);

                dataContext.SaveChanges();

	            return newGroup.ID;
	        }
	    }

	    public void AddGroupMemberToDb(int groupId, string userId)
	    {
	        using (var dataContext = new ApplicationDbContext())
	        {
	            var myRepo = new UserRepository<GroupMember>(dataContext);

                var thisUser = new GroupMember()
                {
                    groupID = groupId,
                    userID = userId,
                    memberStatus = false,
                    isAdmin = false
                };

                myRepo.Insert(thisUser);

	            dataContext.SaveChanges();

	            return;
	        }
	    }

        public bool IsMember(int groupId, string userId)
        {
            using (var dataContext = new ApplicationDbContext())
            {
                var myRepo = new UserRepository<GroupMember>(dataContext);

                List<GroupMember> myList = myRepo.GetAll().ToList();

                for (int i = 0; i < myList.Count; i++)
                {
                    if (myList[i].groupID == groupId && myList[i].userID == userId)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

	    public void MakeMember(int groupId, string userId)
	    {
	        using (var dataContext = new ApplicationDbContext())
	        {
	            var myRepo = new UserRepository<GroupMember>(dataContext);

	            List<GroupMember> makeMember = new List<GroupMember>();

	            makeMember = (from f in dataContext.GroupMembers
                              where f.groupID == groupId && f.userID == userId
	                          select f).ToList();

	            foreach (var data in makeMember)
	            {
	                data.memberStatus = true;
	            }

	            return;
	        }
	    }

	    public void DenyMemberReq(int groupId, string userId)
	    {
	        using (var dataContext = new ApplicationDbContext())
	        {
                var myRepo = new UserRepository<GroupMember>(dataContext);

                List<GroupMember> denyMember = new List<GroupMember>();

                denyMember = (from f in dataContext.GroupMembers
                            where f.groupID == groupId && f.userID == userId
                            select f).ToList();

                myRepo.Delete(denyMember[0]);
	        }
	    }
	}
}