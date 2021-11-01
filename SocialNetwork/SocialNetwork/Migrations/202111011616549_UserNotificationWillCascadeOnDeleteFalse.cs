namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserNotificationWillCascadeOnDeleteFalse : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UserNotifications", new[] { "User_Id" });
            AlterColumn("dbo.UserNotifications", "User_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.UserNotifications", "User_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserNotifications", new[] { "User_Id" });
            AlterColumn("dbo.UserNotifications", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserNotifications", "User_Id");
        }
    }
}
