
using SocialNetwork.Models;
using System.Collections.Generic;

namespace SocialNetwork.Core.Repositories
{
	public interface INotificationRepository
	{
		IEnumerable<Notification> GetNewNotificationsFor(string userId);

		IEnumerable<UserNotification> GetUserNotifications(string userId);

	}
}