using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SchoolPlanner.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<SchoolActivity> SchoolActivity { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
    }
}
