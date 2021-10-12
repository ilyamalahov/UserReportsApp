using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using UserReportsApp.Api.Entities;

namespace UserReportsApp.Api.Data
{
    public class UserReportsContext : DbContext
    {
        public UserReportsContext([NotNull] DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Reports

            var reportEntity = modelBuilder.Entity<Report>();

            reportEntity.Property(r => r.Id)
                .ValueGeneratedOnAdd();

            reportEntity.Property(r => r.CreatedDate)
                .HasDefaultValueSql("getdate()");

            reportEntity.HasOne(r => r.User)
                .WithMany(u => u.Reports)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}
