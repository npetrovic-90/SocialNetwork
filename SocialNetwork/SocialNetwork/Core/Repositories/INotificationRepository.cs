
using SocialNetwork.Models;
using System.Collections.Generic;

namespace SocialNetwork.Core.Repositories
{
	public interface INotificationRepository
	{
		IEnumerable<UserNotification> GetUserNotifications(string userId);

		IEnumerable<Notification> GetAllNotifications();
	}
}