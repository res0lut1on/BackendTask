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
        public BackendTestTaskContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BackendTestTaskContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
