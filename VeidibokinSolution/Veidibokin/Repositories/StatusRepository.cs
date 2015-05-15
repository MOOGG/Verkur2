using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Veidibokin.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;


namespace Veidibokin.Repositories
{
    /// <summary>
    /// Hér að neðan er StatusRepository-ið okkar sem sér um að sækja gögn úr grunni
    /// og skila þeim frá sér á viðeigandi formi (list, int, etc). Einnig sér það um að
    /// taka við gögnum frá Controller og búa til færslur í viðeigandi töflum
    /// </summary>
    public class StatusRepository
    {
        // StatusToDb býr til nýjan status og skráir hann niður í grunn.
        // Aðgerðin keyrir inn status án catchId ef catchId = null
        public int StatusToDB(string status, string thisuserid, string statusPicture, bool isPublic, int? catchId)
        {
            using (var dataContext = new ApplicationDbContext())
            {
                var myRepo = new UserRepository<UserStatus>(dataContext);

                var newStatus = new UserStatus()
                {
                    statusText = status,
                    isPublic = isPublic,
                    dateInserted = DateTime.Now,
                    userId = thisuserid,
                    photo = statusPicture,
                    catchID = catchId
                };

                myRepo.Insert(newStatus);
                
                dataContext.SaveChanges();

				return newStatus.ID;
            }
        }

        // CatchToDB sér um að búa til nýjan fisk og skrá hann í viðeigandi töflu. Færibreyturnar
        // length og weight meiga vera null
        public Catch CatchToDB(int zone, int fishType, int baitType, double? length, double? weight)
        {
            using (var dataContext = new ApplicationDbContext())
            {
                var myRepo = new UserRepository<Catch>(dataContext);

                var newCatch = new Catch()
                {
                    zoneID = zone,
                    fishTypeId = fishType,
                    baitTypeID = baitType,
                    length = length,
                    weight = weight
                };

                myRepo.Insert(newCatch);
                
                dataContext.SaveChanges();
              
                return newCatch;
            }
        }

        // ReturnProfileStatuses sækir statusa ákveðins notenda og setur þá í lista
        public List<Feed> ReturnProfileStatuses(string userId)
        {
            var returnList = new List<Feed>();

            using (var dataContext = new ApplicationDbContext())
            {
                var statuses = (from status in dataContext.UserStatuses
                                where (status.isPublic == true && status.userId == userId)
                                select new { status = status.statusText, date = status.dateInserted, userId = status.userId, photo = status.photo, catchId = status.catchID });

                List<Feed> fishfeed = (from users in dataContext.Users
                                       join status in statuses on users.Id equals status.userId
                                       orderby status.date descending
                                       select new Feed { fullName = users.fullName, statusText = status.status, dateInserted = status.date, statusUserId = status.userId, statusPhoto = status.photo, catchId = status.catchId }).ToList();
                       
                return fishfeed;
            }
        }

        // ReturnUserName skilar nafni viðkomandi notanda
        public List<string> ReturnUserName(string userId)
        {
            using(var dataContext = new ApplicationDbContext())
            {
                List<string> fullName = (from users in dataContext.Users
                                         where (users.Id == userId)
                                         select users.fullName).ToList();
                return fullName;
            }
        }

        // ReturnOwnStatuses skilar þínum eigin statusum og skilar þeim í lista
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

        // ReturnFeedStatuses skilar öllum þeim statusum frá þeim notendum sem þú ert að fylgja
        // og skilar þeim í lista. Þeir eru svo birtir á heimasíðu þinni (feed page)
        public List<Feed> ReturnFeedStatuses(string userId)
        {
            using (var dataContext = new ApplicationDbContext())
            {
                var following = (from f in dataContext.UserFollowers
                                 where f.followerID == userId
                                 select f.userID);

                var statuses = (from status in dataContext.UserStatuses
                                where ((following.Contains(status.userId) && status.isPublic == true) || status.userId == userId)
                                select new { status = status.statusText, date = status.dateInserted, userId = status.userId, photo = status.photo, catchId = status.catchID });

                List<Feed> fishfeed = (from users in dataContext.Users
                                       join status in statuses on users.Id equals status.userId
                                       orderby status.date descending
                                       select new Feed { fullName = users.fullName, statusText = status.status, dateInserted = status.date, statusUserId = status.userId, statusPhoto = status.photo, catchId = status.catchId }).ToList();
                
                return fishfeed;
            }
        }

        // ReturnFollowersList skilar lista yfir það fólk sem fylgir þér
        public List<FollowList> ReturnFollowersList(string userId)
        {
            using (var dataContext = new ApplicationDbContext())
            {
                var followersNames = (from user in dataContext.Users
                                      join followid in dataContext.UserFollowers on user.Id equals followid.userID
                                      where followid.followerID == userId
                                      select new FollowList {fullName = user.fullName, userId = user.Id}).ToList();
               
                return followersNames;
            }
        }

        // ReturnFollowingList skilar lista sem sýnir hverjum þú sem notandi ert að fylgja
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

        // MakeFollowers skráir niður í UserFollowers, venslin þegar þú fylgir einnhverjum
        public void MakeFollowers(string myId, string otherId)
        {
            using (var dataContext = new ApplicationDbContext())
            {
                var myRepo = new UserRepository<UserFollower>(dataContext);

                UserFollower followRelation = new UserFollower()
                {
                    userID = otherId,
                    followerID = myId
                };

                myRepo.Insert(followRelation);

                dataContext.SaveChanges();
            }
        }

        // ReturnCatch skilar lista af fiskum sem einnhver notandi á
        public List<CatchFeed> ReturnCatch(string userId)
        {
            using (var dataContext = new ApplicationDbContext())
            {
                List<CatchFeed> myCatch = (from c in dataContext.Catches
                                           join statuses in dataContext.UserStatuses on c.ID equals statuses.catchID
                                           join zoneID in dataContext.Zones on c.zoneID equals zoneID.ID
                                           join fishID in dataContext.FishTypes on c.fishTypeId equals fishID.ID
                                           join baitID in dataContext.BaitTypes on c.baitTypeID equals baitID.ID
                                           where statuses.userId == userId
                                           select new CatchFeed { catchID = c.ID, zone = zoneID.zoneName, bait = baitID.name, fish = fishID.name, length = c.length, weight = c.weight }).ToList();

                return myCatch;
            }
        }

        // AreFollowers athugar í UserFollowers töfluna hvort þú ert að fylgja einnhverjum nú þegar
        // og skilar þá gildinu true.
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