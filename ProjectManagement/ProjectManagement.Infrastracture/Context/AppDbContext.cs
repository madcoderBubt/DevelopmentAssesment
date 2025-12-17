using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastracture.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TeamEntity> Teams { get; set; }
        public DbSet<Core.Entities.TaskEntity> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<UserEntity>().ToTable("Users");
            builder.Entity<TeamEntity>().ToTable("Teams");
            builder.Entity<TaskEntity>().ToTable("Tasks");

            base.OnModelCreating(builder);
        }
    }
}
