using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace SocialNetwork.Models
{


	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Concert> Concerts { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Attendance> Attendances { get; set; }

		public DbSet<Following> Followings { get; set; }

		//notification data set
		public DbSet<Notification> Notifications { get; set; }

		public DbSet<UserNotification> UserNotifications { get; set; }


		public ApplicationDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Attendance>()
				.HasRequired(a => a.Concert)
				.WithMany(c => c.Attendances)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ApplicationUser>()
				.HasMany(u => u.Followers)
				.WithRequired(f => f.Followee)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ApplicationUser>()
				.HasMany(u => u.Followees)
				.WithRequired(f => f.Follower)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<UserNotification>()
				.HasRequired(n => n.User)
				.WithMany(u => u.UserNotifications)
				.WillCascadeOnDelete(false);

			base.OnModelCreating(modelBuilder);
		}
	}
}