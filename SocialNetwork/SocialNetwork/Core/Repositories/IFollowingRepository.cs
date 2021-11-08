using SocialNetwork.Models;

namespace SocialNetwork.Repositories
{
	public interface IFollowingRepository
	{
		Following GetFollowing(string userId, string artistId);

		void Remove(Following following);

		void Add(Following following);

	}
}