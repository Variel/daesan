using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Daesan.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Daesan.Services
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Command> Commands { get; set; }
        public DbSet<User> Users { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Command>()
                .HasKey(c => new {c.Scene, c.Input});

            base.OnModelCreating(modelBuilder);
        }
    }
}
