using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Monager.Database.Entities;

namespace Monager.Database
{
    public class MonagerDbContext : DbContext
    {
        public MonagerDbContext()
        {
        }

        public MonagerDbContext(DbContextOptions<MonagerDbContext> options) : base(options)
        {
        }

        public DbSet<Entry> Entries { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entry>()
                .HasOne(e => e.User)
                .WithMany(e => e.Entries)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
