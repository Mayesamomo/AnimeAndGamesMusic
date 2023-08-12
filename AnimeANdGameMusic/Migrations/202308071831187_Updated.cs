namespace AnimeANdGameMusic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Updated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                {
                    AlbumId = c.Int(nullable: false, identity: true),
                    AlbumTitle = c.String(nullable: false, maxLength: 100),
                    ReleaseYear = c.Int(nullable: false),
                    ArtistId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.AlbumId)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: true)
                .Index(t => t.ArtistId);

            CreateTable(
                "dbo.Artists",
                c => new
                {
                    ArtistId = c.Int(nullable: false, identity: true),
                    ArtistName = c.String(nullable: false, maxLength: 100),
                })
                .PrimaryKey(t => t.ArtistId);

            CreateTable(
                "dbo.Songs",
                c => new
                {
                    SongId = c.Int(nullable: false, identity: true),
                    SongTitle = c.String(nullable: false, maxLength: 100),
                    AlbumId = c.Int(nullable: false),
                    GenreId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.SongId)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.AlbumId)
                .Index(t => t.GenreId);

            CreateTable(
                "dbo.Genres",
                c => new
                {
                    GenreId = c.Int(nullable: false, identity: true),
                    GenreTitle = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.GenreId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Songs", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Songs", "AlbumId", "dbo.Albums");
            DropForeignKey("dbo.Albums", "ArtistId", "dbo.Artists");
            DropIndex("dbo.Songs", new[] { "GenreId" });
            DropIndex("dbo.Songs", new[] { "AlbumId" });
            DropIndex("dbo.Albums", new[] { "ArtistId" });
            DropTable("dbo.Genres");
            DropTable("dbo.Songs");
            DropTable("dbo.Artists");
            DropTable("dbo.Albums");
        }
    }
}