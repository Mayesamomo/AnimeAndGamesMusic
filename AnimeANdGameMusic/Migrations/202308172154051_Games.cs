namespace AnimeANdGameMusic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Games : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Games", new[] { "GenreID" });
            CreateIndex("dbo.Games", "GenreId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Games", new[] { "GenreId" });
            CreateIndex("dbo.Games", "GenreID");
        }
    }
}
