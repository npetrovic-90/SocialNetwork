namespace SocialNetwork.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class PopulateGenresTable : DbMigration
	{
		public override void Up()
		{
			Sql("INSERT INTO Genres (Name) Values('Jazz')");
			Sql("INSERT INTO Genres (Name) Values('Blues')");
			Sql("INSERT INTO Genres (Name) Values('Rock')");
			Sql("INSERT INTO Genres (Name) Values('Country')");
		}

		public override void Down()
		{
			Sql("DELETE FROM Genres WHERE Name='Jazz'");
			Sql("DELETE FROM Genres WHERE Name='Blues'");
			Sql("DELETE FROM Genres WHERE Name='Rock'");
			Sql("DELETE FROM Genres WHERE Name='Country'");
		}
	}
}
