﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Veidibokin.Models;


namespace Veidibokin.Repositories
{
    public class StatusRepository
    {
        public void StatusToDB(string status, string thisuserid)
        {
            using (var dataContext = new ApplicationDbContext())
            {
                var myRepo = new UserRepository<UserStatus>(dataContext);

                var newStatus = new UserStatus()
                {
                    statusText = status,
                    isPublic = true,
                    dateInserted = DateTime.Now,
                    userId = thisuserid
                };

                myRepo.Insert(newStatus);

                //Debug.WriteLine(userStatusRepository.GetAll());
                dataContext.SaveChanges();
            }
        }

        public List<Feed> ReturnUserStatuses(string userId)
        {
            var returnList = new List<Feed>();

            using (var dataContext = new ApplicationDbContext())
            {
                //var statusRepo = new UserRepository<Feed>(dataContext);

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
                        dateInserted = item.date
                    });
                } 

            }
            return returnList;
        }

        //public List<UserStatus> returnList { get; set; }
    }
}