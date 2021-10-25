using SocialNetwork.Models;
using System.Collections.Generic;

namespace SocialNetwork.ViewModels
{
	public class ConcertFormViewModel
	{
		public string Venue { get; set; }
		public string Date { get; set; }
		public string Time { get; set; }
		public byte Genre { get; set; }
		public IEnumerable<Genre> Genres { get; set; }

	}
}