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
        public void StatusToDB(string status, string thisuserid, string statusPicture)
        {
            using (var dataContext = new ApplicationDbContext())
            {
                var myRepo = new UserRepository<UserStatus>(dataContext);

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
                
                //Debug.WriteLine(userStatusRepository.GetAll());
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

        public List<Feed> ReturnProfileStatuses(string userId)
        {
            var returnList = new List<Feed>();

            using (var dataContext = new ApplicationDbContext())
            {
                var Following = (from f in dataContext.UserFollowers
                                 where f.followerID == userId
                                 select f.userID);

                var statuses = (from status in dataContext.UserStatuses
                                where ((Following.Contains(status.userId) & status.isPublic == true) || status.userId == userId)
                                select new { status = status.statusText, date = status.dateInserted, userId = status.userId });

                var fishfeed = (from users in dataContext.Users
                                join status in statuses on users.Id equals status.userId
                                orderby status.date descending
                                select new { fullname = users.fullName, status = status.status, date = status.date });
                
                foreach (var item in fishfeed)
                {
                    returnList.Add(new Feed()
                    {
                        fullName = item.fullname,
                        statusText = item.status,
                        dateInserted = item.date,
                    });
                }
                return returnList;
            }
        }

        public List<Feed> ReturnFeedStatuses(string userId)
        {
            var returnList = new List<Feed>();

            using (var dataContext = new ApplicationDbContext())
            {
                var Following = (from f in dataContext.UserFollowers
                                 where f.followerID == userId
                                 select f.userID);

                var statuses = (from status in dataContext.UserStatuses
                                where ((Following.Contains(status.userId) & status.isPublic == true) || status.userId == userId)
                                select new { status = status.statusText, date = status.dateInserted, userId = status.userId, photo = status.photo });

                var fishfeed = (from users in dataContext.Users
                                join status in statuses on users.Id equals status.userId
                                orderby status.date descending
                                select new { fullname = users.fullName, status = status.status, date = status.date, photo = status.photo });

                foreach (var item in fishfeed)
                {
                    returnList.Add(new Feed()
                    {
                        fullName = item.fullname,
                        statusText = item.status,
                        dateInserted = item.date,
                        statusPicture = item.photo
                    });
                }
            }
            return returnList;
        }

        public List<string> ReturnFollowList(string userId)
        {
            List<string> returnList = null;

            using (var dataContext = new ApplicationDbContext())
            {
                var followers = (from f in dataContext.UserFollowers
                                 where f.userID == userId
                                 select f.userID);

                // eftir að tengja töflur sama til að fá nöfn...
                if (followers != null)
                {
                    foreach (var data in followers)
                    {
                        returnList.Add(data);
                    }
                }
            }
            return returnList;
        } 
    }
}