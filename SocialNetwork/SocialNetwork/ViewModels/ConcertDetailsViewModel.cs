using SocialNetwork.Models;

namespace SocialNetwork.ViewModels
{
	public class ConcertDetailsViewModel
	{
		public Concert Concert { get; set; }
		public bool IsAttending { get; set; }
		public bool IsFollowing { get; set; }

	}
}