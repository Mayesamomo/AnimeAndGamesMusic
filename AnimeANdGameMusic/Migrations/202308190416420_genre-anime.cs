namespace AnimeANdGameMusic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class genreanime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Animes", "GenreId", c => c.Int(nullable: false));
            CreateIndex("dbo.Animes", "GenreId");
            AddForeignKey("dbo.Animes", "GenreId", "dbo.Genres", "GenreId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Animes", "GenreId", "dbo.Genres");
            DropIndex("dbo.Animes", new[] { "GenreId" });
            DropColumn("dbo.Animes", "GenreId");
        }
    }
}
