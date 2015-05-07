using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Veidibokin.Models;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;

namespace Veidibokin.Repositories
{
    public class UserRepositoryTest
    {

        public void QueryUserName()
        {
            using (var dataContext = new ApplicationDbContext())
            {
                var userStatusRepository = new UserRepository<UserStatus>(dataContext);

                var newStatus = new UserStatus()
                {
                    statusText = "BLABLABLABLA",
                    isPublic = true,
                    dateInserted = DateTime.Now,
                    userId = "83fcec4c-7340-4f13-8464-09237ffe5aa2"
                };

                userStatusRepository.Insert(newStatus);

                //Debug.WriteLine(userStatusRepository.GetAll());
                dataContext.SaveChanges();
            }
        }
    }
}