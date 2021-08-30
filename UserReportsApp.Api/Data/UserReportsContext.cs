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

            modelBuilder.Entity<Report>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reports)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}
