using Microsoft.EntityFrameworkCore;
using SebUzduotisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SebUzduotisApi.Context
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = string.Format("Data Source={0}\\Database.db", AppDomain.CurrentDomain.BaseDirectory);
            optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasIndex(b => b.PersonalId)
                .IsUnique();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<BaseRate> BaseRates { get; set; }
        public DbSet<Agreement> Agreements { get; set; }
    }
}
