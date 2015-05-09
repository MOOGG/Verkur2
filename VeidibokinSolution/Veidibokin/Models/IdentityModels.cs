using System.Data;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veidibokin.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string fullName { get; set; }
        public int postalCode { get; set; }
        public string town { get; set; }
        public byte[] photo { get; set; }
        public string gender { get; set; }
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
        public DbSet<Catch> Catches { get; set; }
        public DbSet<FishType> FishTypes { get; set; }
        public DbSet<BaitType> BaitTypes { get; set; }
        public DbSet<Zone> Zones { get; set; } 
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<StatusComment> StatusComments { get; set; }
        public DbSet<ZoneFollower> ZoneFollowers { get; set; }
        public DbSet<UserFollower> UserFollowers { get; set; }
        public DbSet<GroupStatus> GroupStatuses { get; set; }
        public DbSet<Worm> Worms { get; set; }
        
        // hér búum við til tilvik af entity klösunum, búm þá til inní Models möppuna (UserStatus, UserComment,
        // UserInfo etc., f.ex. "public DbSet<UserStatus> UserStatuses { get; set; }"
        // muna að tilgreina lykla í entity klösunum, "public int ID ..." automatic key
        // til að hlutur mappist ofan í töflur þá þarf tagið að vera DbSet !
              
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