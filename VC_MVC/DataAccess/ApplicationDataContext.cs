using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VC_MVC.Models;

namespace VC_MVC.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Quote> Quotes { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarShowroom> CarShowrooms { get; set; }

        public DbSet<CarToCarShowroom> CarToCarShowrooms { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarToCarShowroom>().HasKey(k => new { k.CarId, k.CarShowroomId });

            modelBuilder.Entity<CarToCarShowroom>()
                .HasOne(x => x.Car)
                .WithMany(x => x.CarToCarShowrooms)
                .HasForeignKey(x => x.CarId);

            modelBuilder.Entity<CarToCarShowroom>()
               .HasOne(x => x.CarShowroom)
               .WithMany(x => x.CarToCarShowrooms)
               .HasForeignKey(x => x.CarShowroomId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
