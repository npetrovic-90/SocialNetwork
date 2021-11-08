using SocialNetwork.Core.Repositories;
using SocialNetwork.Repositories;

namespace SocialNetwork.Persistence
{
	public interface IUnitOfWork
	{
		IAttendanceRepository Attendances { get; set; }
		IConcertRepository Concerts { get; }
		IFollowingRepository Followings { get; set; }
		IGenreRepository Genres { get; set; }

		INotificationRepository Notifications { get; set; }

		void Complete();
	}
}