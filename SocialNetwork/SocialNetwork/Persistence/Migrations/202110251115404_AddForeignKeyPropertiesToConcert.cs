namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyPropertiesToConcert : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Concerts", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.Concerts", new[] { "Genre_Id" });
            RenameColumn(table: "dbo.Concerts", name: "Artist_Id", newName: "ArtistId");
            RenameIndex(table: "dbo.Concerts", name: "IX_Artist_Id", newName: "IX_ArtistId");
            AddColumn("dbo.Concerts", "GenreId", c => c.Byte(nullable: false));
            AlterColumn("dbo.Concerts", "Genre_Id", c => c.Int());
            CreateIndex("dbo.Concerts", "Genre_Id");
            AddForeignKey("dbo.Concerts", "Genre_Id", "dbo.Genres", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Concerts", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.Concerts", new[] { "Genre_Id" });
            AlterColumn("dbo.Concerts", "Genre_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Concerts", "GenreId");
            RenameIndex(table: "dbo.Concerts", name: "IX_ArtistId", newName: "IX_Artist_Id");
            RenameColumn(table: "dbo.Concerts", name: "ArtistId", newName: "Artist_Id");
            CreateIndex("dbo.Concerts", "Genre_Id");
            AddForeignKey("dbo.Concerts", "Genre_Id", "dbo.Genres", "Id", cascadeDelete: true);
        }
    }
}
