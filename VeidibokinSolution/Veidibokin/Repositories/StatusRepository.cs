﻿using System;
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
                var Following = (from f in dataContext.UserFollowers
                                 where f.followerID == userId
                                 select f.userID);

                var statuses = (from status in dataContext.UserStatuses
                                where ((Following.Contains(status.userId) && status.isPublic == true) || status.userId == userId)
                                select new { status = status.statusText, date = status.dateInserted, userId = status.userId, photo = status.photo });

                List<Feed> fishfeed = (from users in dataContext.Users
                                       join status in statuses on users.Id equals status.userId
                                       orderby status.date descending
                                       select new Feed { fullName = users.fullName, statusText = status.status, dateInserted = status.date, statusUserId = status.userId, statusPhoto = status.photo }).ToList();
                
                return fishfeed;
            }
        }

        public List<FullNameForFeed> ReturnFollowList(string userId)
        {
            using (var dataContext = new ApplicationDbContext())
            {
                
               /* var followers = (from f in dataContext.UserFollowers
                                 where f.followerID == userId
                                 select f.userID);*/
                var followersName = (from user in dataContext.Users
                    join followid in dataContext.UserFollowers on user.Id equals followid.userID
                    where followid.followerID == userId
                    select new FullNameForFeed {fullName = user.fullName, userId = user.Id}).ToList();
               
                return followersName;
            }
        } 
    }
}