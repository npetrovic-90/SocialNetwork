using SocialNetwork.Models;
using System.Linq;

namespace SocialNetwork.Repositories
{
	public class FollowingRepository : IFollowingRepository
	{
		private readonly ApplicationDbContext _dbContext;
		public FollowingRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Following GetFollowing(string userId, string artistId)
		{
			return _dbContext.Followings
					.SingleOrDefault(f => f.FolloweeId == artistId && f.FollowerId == userId);
		}

		public void Remove(Following following)
		{
			_dbContext.Followings.Remove(following);
		}
		public void Add(Following following)
		{
			_dbContext.Followings.Add(following);
		}
	}
}