using System.Data.Entity;
using WebApp4I.AppCore.Entities;

namespace WebApp4I.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ImageInfo> ImageInfo { get; set; }

        public AppDbContext() : base("name=SqlServer") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImageInfo>().ToTable("ImageInfo");

            base.OnModelCreating(modelBuilder);
        }
    }
}
