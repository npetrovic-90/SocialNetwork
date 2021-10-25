using System;

namespace SocialNetwork.Models
{
	public class Concert
	{
		public int Id { get; set; }
		public ApplicationUser Artist { get; set; }
		public DateTime DateTime { get; set; }
		public string Venue { get; set; }
		public Genre Genre { get; set; }

	}
}