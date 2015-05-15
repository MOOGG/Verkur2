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
    /// <summary>
    /// GroupRepository sér um að sækja gögn úr grunni og skrá niður í sambandi
    /// við allt sem viðkemur hópum innan síðunnar
    /// </summary>
	public class GroupRepository
    {
        // GroupStatusToDB skrifar hópastatus niður í grunn í GroupStatus töfluna
		public void GroupStatusToDB(string status, string thisuserid, string statusPicture, int groupId)
		{
		    int statusID = 0;

		    using (var dataContext = new ApplicationDbContext())
		    {
		        var myUserRepo = new UserRepository<UserStatus>(dataContext);

		        var newStatus = new UserStatus()
		        {
		            statusText = status,
		            isPublic = false,
		            dateInserted = DateTime.Now,
		            userId = thisuserid,
		            photo = statusPicture,
		        };

		        myUserRepo.Insert(newStatus);
		        dataContext.SaveChanges();
                statusID = newStatus.ID;
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

        // ReturnGroupStatuses skilar lista af statusum úr viðkomandi hóp
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

        // ReturnMembersList skilar lista af hópmeðlimum viðkomandi hóps
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

        // ReturnGroupRequestList skilar lista af meðlimabeiðnum viðkomandi hóps
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

        // AddGroupToDb býr til nýjan hóp í grunni í Groups töfluna
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

        // AddGroupMemberToDb bætir nýjum notenda í viðkomandi hóp
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

        // IsMember athugar hvort að notandi sé nú þegar meðlimur af hóp.
        // Ef svo er skilar fallið true
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

        // MakeMember skráir að notandi sé orðinn meðlimur af viðkomandi hóp
        // með því að breyta mamberStatus breytunni í true
	    public void MakeMember(int groupId, string userId)
	    {
	        using (var dataContext = new ApplicationDbContext())
	        {

	           GroupMember makeMember = (from f in dataContext.GroupMembers
	                where (f.groupID == groupId && f.userID == userId)
	                select f).First();

	            makeMember.memberStatus = true;

                dataContext.SaveChanges();

	        }
	    }

        // DenyMemberReq sér um að henda út notenda úr GroupMembers töflunni sem
        // hefur verið neitað um inngöngu í hóp. Virkni ekki kláruð
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

	            dataContext.SaveChanges();
	        }
	    }

        // ReturnGroupName skilar nafni viðkomandi hóps
        public List<string> ReturnGroupName(int id)
        {
            using (var dataContext = new ApplicationDbContext())
            {
                List<string> groupName = (from groups in dataContext.Groups
                                         where (groups.ID == id)
                                         select groups.groupName).ToList();
                return groupName;
            }
        }

        // ReturnGroupDescription skilar lýsingu viðkomandi hóps
        public List<string> ReturnGroupDescription(int id)
        {
            using (var dataContext = new ApplicationDbContext())
            {
                List<string> groupDescription = (from groups in dataContext.Groups
                                          where (groups.ID == id)
                                          select groups.description).ToList();
                return groupDescription;
            }
        }
	}
}