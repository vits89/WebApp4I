namespace WebApp4I.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ImageInfo", "FileName", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.ImageInfo", "ThumbnailFileName", c => c.String(nullable: false, maxLength: 90));
            AlterColumn("dbo.ImageInfo", "Description", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ImageInfo", "Description", c => c.String());
            AlterColumn("dbo.ImageInfo", "ThumbnailFileName", c => c.String());
            AlterColumn("dbo.ImageInfo", "FileName", c => c.String(nullable: false));
        }
    }
}
