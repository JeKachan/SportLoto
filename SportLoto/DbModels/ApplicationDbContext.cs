//using Microsoft.AspNet.Identity.EntityFramework;
//using SportLoto.DbModels;
//using System.Data.Entity;

//namespace SportLoto.DbModels
//{
//    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
//    {
//        public ApplicationDbContext()
//            : base("DBConnection", throwIfV1Schema: false)
//        {
//        }

//        public static ApplicationDbContext Create()
//        {
//            return new ApplicationDbContext();
//        }

//        public DbSet<Ticket> Tickets { get; set; }
//        public DbSet<Drawing> Drawings { get; set; }
//        public DbSet<Transaction> Transactions { get; set; }

//    }
//}