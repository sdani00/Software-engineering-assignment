using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}
