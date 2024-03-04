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
        public DbSet<EmployeeRank> EmployeeRanks { get; set; }
        public DbSet<PingPongRank> PingPongRanks { get; set; }

        public DbSet<Quiz> Quizzes { get; set; }

        public DbSet<EmployeeResult> EmployeeResults { get; set; }

        public DbSet<PingPongResults> PingPongResults { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
