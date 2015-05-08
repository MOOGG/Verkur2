using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Veidibokin.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Windows.Markup;
namespace Veidibokin.Repositories
{
    public class UserRepository<Tag> : ParentRepository<Tag> where Tag : class 
    {
        protected DbSet<Tag> DbSet;

        public UserRepository(DbContext dataContext)
        {
            DbSet = dataContext.Set<Tag>();
        }

        public IQueryable<Tag> GetAll()
        {
            return DbSet;
        }

        public Tag GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual void Insert(Tag entity)
        {
            DbSet.Add(entity);           
        }

        public void Delete(Tag entity)
        {
            DbSet.Remove(entity);
        }

        /*public void StatusToDB(string status, string thisuserid)
        {
            //using (var dataContext = new ApplicationDbContext())
            //{
                //var userStatusRepository = new UserRepository<UserStatus>(dataContext);

                var newStatus = new UserStatus()
                {
                    statusText = status,
                    isPublic = true,
                    dateInserted = DateTime.Now,
                    userId = thisuserid
                };

                userStatusRepository.Insert(newStatus);

                //Debug.WriteLine(userStatusRepository.GetAll());
                dataContext.SaveChanges();
            //}
        }*/
    }
}