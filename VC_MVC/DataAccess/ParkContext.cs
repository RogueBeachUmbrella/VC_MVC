using VC_MVC.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace VC_MVC.DataAccess
{
    public class ParkContext : DbContext
    {
        public ParkContext(DbContextOptions<ParkContext> options) : base(options) { }
        public DbSet<Park> Parks { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Entrancefee> EntranceFees { get; set; }
        public DbSet<Entrancepass> Entrancepasses { get; set; }
        public DbSet<Operatinghour> OperatingHours { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
