using SocialNetwork.Models;
using System.Collections.Generic;

namespace SocialNetwork.ViewModels
{
	public class ConcertsViewModel
	{
		public IEnumerable<Concert> UpcomingConcerts { get; set; }
		public bool ShowActions { get; set; }

		public string Heading { get; set; }

		public string SearchTerm { get; set; }
	}
}