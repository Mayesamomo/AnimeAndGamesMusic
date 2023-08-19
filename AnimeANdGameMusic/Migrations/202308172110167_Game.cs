namespace AnimeANdGameMusic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Game : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameID = c.Int(nullable: false, identity: true),
                        GameName = c.String(),
                        ReleaseYear = c.Int(nullable: false),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                        GameHasPic = c.Boolean(nullable: false),
                        PicExtension = c.String(),
                        GenreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameID)
                .ForeignKey("dbo.Genres", t => t.GenreID, cascadeDelete: true)
                .Index(t => t.GenreID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "GenreID", "dbo.Genres");
            DropIndex("dbo.Games", new[] { "GenreID" });
            DropTable("dbo.Games");
        }
    }
}
