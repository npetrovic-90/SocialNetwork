namespace SocialNetwork.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class AddNotification : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.Notifications",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					DateTime = c.DateTime(nullable: false),
					Type = c.Int(nullable: false),
					OriginalDateTime = c.DateTime(),
					OriginalVenue = c.String(),
					Concert_Id = c.Int(nullable: false),
				})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Concerts", t => t.Concert_Id, cascadeDelete: true)
				.Index(t => t.Concert_Id);

			CreateTable(
				"dbo.UserNotifications",
				c => new
				{
					UserId = c.Int(nullable: false),
					NotificationId = c.Int(nullable: false),
					IsRead = c.Boolean(nullable: false),
					User_Id = c.String(maxLength: 128),
				})
				.PrimaryKey(t => new { t.UserId, t.NotificationId })
				.ForeignKey("dbo.Notifications", t => t.NotificationId, cascadeDelete: true)
				.ForeignKey("dbo.AspNetUsers", t => t.User_Id)
				.Index(t => t.NotificationId)
				.Index(t => t.User_Id);

		}

		public override void Down()
		{
			DropForeignKey("dbo.UserNotifications", "User_Id", "dbo.AspNetUsers");
			DropForeignKey("dbo.UserNotifications", "NotificationId", "dbo.Notifications");
			DropForeignKey("dbo.Notifications", "Concert_Id", "dbo.Concerts");
			DropIndex("dbo.UserNotifications", new[] { "User_Id" });
			DropIndex("dbo.UserNotifications", new[] { "NotificationId" });
			DropIndex("dbo.Notifications", new[] { "Concert_Id" });
			DropTable("dbo.UserNotifications");
			DropTable("dbo.Notifications");
		}
	}
}
