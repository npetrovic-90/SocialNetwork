using SocialNetwork.Core.Repositories;
using SocialNetwork.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SocialNetwork.Persistence.Repositories
{
	public class NotificationRepository : INotificationRepository
	{
		private readonly ApplicationDbContext _dbContext;
		public NotificationRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IEnumerable<Notification> GetNewNotificationsFor(string userId)
		{
			return _dbContext.UserNotifications
				.Where(un => !un.IsRead && un.UserId == userId)
				.Select(un => un.Notification)
				.Include(n => n.Concert.Artist)
				.ToList();
		}


		public IEnumerable<UserNotification> GetUserNotifications(string userId)
		{
			return _dbContext.UserNotifications.Where(un => un.UserId == userId && !un.IsRead).ToList();
		}
	}
}