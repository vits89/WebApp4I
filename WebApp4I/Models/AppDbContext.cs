using System.Data.Entity;

namespace WebApp4I.Models
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
