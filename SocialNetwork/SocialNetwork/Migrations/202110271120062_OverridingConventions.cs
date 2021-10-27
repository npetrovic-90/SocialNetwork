namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverridingConventions : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Concerts", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.Concerts", new[] { "Genre_Id" });
            DropColumn("dbo.Concerts", "GenreId");
            RenameColumn(table: "dbo.Concerts", name: "Genre_Id", newName: "GenreId");
            AlterColumn("dbo.Concerts", "GenreId", c => c.Int(nullable: false));
            AlterColumn("dbo.Concerts", "GenreId", c => c.Int(nullable: false));
            CreateIndex("dbo.Concerts", "GenreId");
            AddForeignKey("dbo.Concerts", "GenreId", "dbo.Genres", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Concerts", "GenreId", "dbo.Genres");
            DropIndex("dbo.Concerts", new[] { "GenreId" });
            AlterColumn("dbo.Concerts", "GenreId", c => c.Int());
            AlterColumn("dbo.Concerts", "GenreId", c => c.Byte(nullable: false));
            RenameColumn(table: "dbo.Concerts", name: "GenreId", newName: "Genre_Id");
            AddColumn("dbo.Concerts", "GenreId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Concerts", "Genre_Id");
            AddForeignKey("dbo.Concerts", "Genre_Id", "dbo.Genres", "Id");
        }
    }
}
