using BackendTestTask.Database.Entities;
using BackendTestTask.Database.Models;
using BackendTestTask.UserContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Database
{
    public class BackendTestTaskContext : BaseContext
    {
        public DbSet<JournalEvent> JournalEvents { get; set; } = null!;
        public DbSet<Node> Nodes { get; set; } = null!;
        public DbSet<Tree> Trees { get; set; } = null!;

        public BackendTestTaskContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BackendTestTaskContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
