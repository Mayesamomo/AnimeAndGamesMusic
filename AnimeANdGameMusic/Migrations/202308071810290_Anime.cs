namespace AnimeANdGameMusic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Anime : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animes",
                c => new
                    {
                        AnimeID = c.Int(nullable: false, identity: true),
                        AnimeName = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        AnimeHasPic = c.Boolean(nullable: false),
                        PicExtension = c.String(),
                    })
                .PrimaryKey(t => t.AnimeID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Animes");
        }
    }
}
