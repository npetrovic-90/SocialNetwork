using SocialNetwork.Models;
using System.Linq;

namespace SocialNetwork.Repositories
{
	public class FollowingRepository
	{
		private readonly ApplicationDbContext _dbContext;
		public FollowingRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public bool GetFollowing(string userId, string artistId)
		{
			return _dbContext.Followings
					.Any(f => f.FolloweeId == artistId && f.FollowerId == userId);
		}
	}
}