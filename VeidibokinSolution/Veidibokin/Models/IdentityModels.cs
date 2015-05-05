using System.Data;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Veidibokin.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string fullName { get; set; }
        public int postalCode { get; set; }
        public string town { get; set; }
        public int photoID { get; set; }
        public char gender { get; set; }
        //public System.DateTime birthday { get; set; } ákveðið að sleppa þessum upplýsingum að sinni
        public string info { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserStatus> UserStatuses { get; set; }
        
        // hér búum við til tilvik af entity klösunum, búm þá til inní Models möppuna (UserStatus, UserComment,
        // UserInfo etc., f.ex. public DbSet<UserStatus> UserStatuses { get; set; }
              
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}