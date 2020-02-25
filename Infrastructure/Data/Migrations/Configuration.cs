namespace WebApp4I.Infrastructure.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using WebApp4I.AppCore.Entities;
    using WebApp4I.Infrastructure.Data;

    internal sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.ImageInfo.AddOrUpdate(new ImageInfo
            {
                FileName = "Image01.jpg",
                ThumbnailFileName = "Image01_thumbnail.jpg"
            },
            new ImageInfo
            {
                FileName = "DSCN0010.jpg",
                ThumbnailFileName = "DSCN0010_thumbnail.jpg"
            },
            new ImageInfo
            {
                FileName = "hh-60-exif.jpg",
                ThumbnailFileName = "hh-60-exif_thumbnail.jpg"
            });
        }
    }
}
