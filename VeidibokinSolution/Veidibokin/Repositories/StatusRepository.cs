using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Veidibokin.Models;


namespace Veidibokin.Repositories
{
    public class StatusRepository
    {
        // hér þarf að bæta við byte[] picture sem argument
        public int StatusToDB(string status, string thisuserid, string statusPicture, bool isPublic)
        {
            using (var dataContext = new ApplicationDbContext())
            {
                var myRepo = new UserRepository<UserStatus>(dataContext);

                // held að það þurfi ekkert að tjekka hvort picture sé null því ef null þá verður statusPicture = null
                var newStatus = new UserStatus()
                {
                    statusText = status,
                    //isPublic = true,
                    dateInserted = DateTime.Now,
                    userId = thisuserid,
                    photo = statusPicture
                };

                myRepo.Insert(newStatus);
                
                //Debug.WriteLine(userStatusRepository.GetAll());
                dataContext.SaveChanges();

				return newStatus.ID;
            }
        }

        public Catch CatchToDB(int zone, int fishType, int baitType, double? length, double? weight )
        {
            using (var dataContext = new ApplicationDbContext())
            {
                var myRepo = new UserRepository<Catch>(dataContext);

                var newCatch = new Catch()
                {
                    zoneID = zone,
                    fishTypeId = fishType,
                    baitTypeID = baitType,
                    length= length,
                    weight = weight
                };

                myRepo.Insert(newCatch);

                dataContext.SaveChanges();

                return newCatch;
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

        public List<Feed> ReturnProfileStatuses(string userId)
        {
            var returnList = new List<Feed>();

            using (var dataContext = new ApplicationDbContext())
            {
                var statuses = (from status in dataContext.UserStatuses
                                where (status.isPublic == true && status.userId == userId)
                                select new { status = status.statusText, date = status.dateInserted, userId = status.userId });

                List<Feed> fishfeed = (from users in dataContext.Users
                                join status in statuses on users.Id equals status.userId
                                orderby status.date descending
                                select new Feed { fullName = users.fullName, statusText = status.status, dateInserted = status.date, statusUserId = status.userId }).ToList();
                       
                return fishfeed;
            }
        }
        
        public List<Feed> ReturnOwnStatuses(string userId)
        {
            var returnList = new List<Feed>();

            using (var dataContext = new ApplicationDbContext())
            {
                var statuses = (from status in dataContext.UserStatuses
                                where (status.userId == userId)
                                select new { status = status.statusText, date = status.dateInserted, userId = status.userId });

                List<Feed> fishfeed = (from users in dataContext.Users
                                       join status in statuses on users.Id equals status.userId
                                       orderby status.date descending
                                       select new Feed { fullName = users.fullName, statusText = status.status, dateInserted = status.date, statusUserId = status.userId }).ToList();

                return fishfeed;
            }
        }
        
        public List<Feed> ReturnFeedStatuses(string userId)
        {

            using (var dataContext = new ApplicationDbContext())
            {
                var following = (from f in dataContext.UserFollowers
                                 where f.followerID == userId
                                 select f.userID);

                var statuses = (from status in dataContext.UserStatuses
                                where ((following.Contains(status.userId) && status.isPublic == true) || status.userId == userId)
                                select new { status = status.statusText, date = status.dateInserted, userId = status.userId, photo = status.photo });

                List<Feed> fishfeed = (from users in dataContext.Users
                                       join status in statuses on users.Id equals status.userId
                                       orderby status.date descending
                                       select new Feed { fullName = users.fullName, statusText = status.status, dateInserted = status.date, statusUserId = status.userId, statusPhoto = status.photo }).ToList();
                
                return fishfeed;
            }
        }

        public List<FollowList> ReturnFollowersList(string userId)
        {
            using (var dataContext = new ApplicationDbContext())
            {
                
               /* var followers = (from f in dataContext.UserFollowers
                                 where f.followerID == userId
                                 select f.userID);*/
                var followersNames = (from user in dataContext.Users
                                      join followid in dataContext.UserFollowers on user.Id equals followid.userID
                                     where followid.followerID == userId
                                     select new FollowList {fullName = user.fullName, userId = user.Id}).ToList();
               
                return followersNames;
            }
        }

        public List<FollowList> ReturnFollowingList(string userId)
        {
            using (var dataContext = new ApplicationDbContext())
            {
                var followingNames = (from user in dataContext.Users
                                     join followid in dataContext.UserFollowers on user.Id equals followid.followerID
                                     where followid.userID == userId
                                     select new FollowList { fullName = user.fullName, userId = user.Id }).ToList();

                return followingNames;
            }
        }

        public void MakeFollowers(string myId, string otherId)
        {
            using (var dataContext = new ApplicationDbContext())
            {
                var myRepo = new UserRepository<UserFollower>(dataContext);

                UserFollower followRelation = new UserFollower()
                {
                    userID = myId,
                    followerID = otherId
                };

                myRepo.Insert(followRelation);

                dataContext.SaveChanges();
            }
        }

        public bool AreFollowers(string id, string otherId)
        {
            using (var dataContext = new ApplicationDbContext())
            {
                var myRepo = new UserRepository<UserFollower>(dataContext);

                List<UserFollower> myList = myRepo.GetAll().ToList();

                for (int i = 0; i < myList.Count; i++)
                {
                    if (myList[i].userID == id && myList[i].followerID == otherId)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}