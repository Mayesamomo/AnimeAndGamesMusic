namespace AnimeANdGameMusic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatealbumclass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "HasPic", c => c.Boolean(nullable: false));
            AddColumn("dbo.Albums", "PicExtension", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "PicExtension");
            DropColumn("dbo.Albums", "HasPic");
        }
    }
}
