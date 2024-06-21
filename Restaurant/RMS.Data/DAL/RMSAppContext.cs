using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RMS.Core.Entities;
using RMS.Core.Entities.Employee;
using System.Reflection;

namespace RMS.Data.DAL
{
    public class RMSAppContext : IdentityDbContext<AppUser>
    {
        public RMSAppContext(DbContextOptions<RMSAppContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().HasDiscriminator<string>("UserRole").
                HasValue<AppUser>("Member")
                .HasValue<Admin>("Admin")
                .HasValue<Customer>("Customer")
                .HasValue<Waiter>("Waiter");

        }
        public DbSet<Category> Categories { get; set;}
        public DbSet<Table> Tables { get; set;}
        public DbSet<Slide> Slides { get; set;}
        
    }
}
