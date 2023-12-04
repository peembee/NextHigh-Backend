using GoApptechBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GoApptechBackend.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<EmployeePoints> EmployeePoints { get; set; }
        public DbSet<EmployeeRank> EmployeeRanks { get; set; }
        public DbSet<PingPongPoints> PingPongPoints { get; set; }
        public DbSet<PingPongRank> PingPongRanks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
