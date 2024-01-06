using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScheduleManager.DataAccess.Models;

namespace ScheduleManager.DataAccess.Context
{
    public class ScheduleManagerContext : IdentityDbContext<User, Role, Guid>
    {
        public ScheduleManagerContext(DbContextOptions<ScheduleManagerContext> options) : base(options)
        {
        }

        public DbSet<TripRequest> TripRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Role>().HasData(
                new Role() { Id = new Guid("e41581ea-fc9b-4317-8945-199c92473e7a"), Name = "SMA", ConcurrencyStamp = "1", NormalizedName = "Schedule Manager Admin" },
                new Role() { Id = new Guid("e41581ea-fc9b-4317-8945-199c92473e7b"), Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" });
        }
    }
}
