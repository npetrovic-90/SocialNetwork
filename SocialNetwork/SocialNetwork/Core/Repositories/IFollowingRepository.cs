namespace SocialNetwork.Repositories
{
	public interface IFollowingRepository
	{
		bool GetFollowing(string userId, string artistId);
	}
}