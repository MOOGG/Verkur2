using System;
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

        public List<UserStatus> ReturnUserStatuses(string userId)
        {
            var returnList = new List<UserStatus>();

            using (var dataContext = new ApplicationDbContext())
            {
                var statusRepo = new UserRepository<UserStatus>(dataContext);

                // búa til list breytu
                // þarf að query-a úr statusRepo.GetAll() alla þá sem statusa þar sem id er == userId

                returnList = statusRepo.GetAll().ToList();
                // muna að gera .ToList() í lokinn
            }
            return returnList;
        }
    }
}