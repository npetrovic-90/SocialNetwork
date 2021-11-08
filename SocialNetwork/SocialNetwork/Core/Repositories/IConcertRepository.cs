using SocialNetwork.Models;
using System.Collections.Generic;

namespace SocialNetwork.Repositories
{
	public interface IConcertRepository
	{
		Concert GetConcert(int concertId);
		IEnumerable<Concert> GetUpcomingConcertsByArtist(string artistId);
		Concert GetConcertWithAttendees(int concertId);
		IEnumerable<Concert> GetConcertsUserAttending(string userId);
		void Add(Concert concert);
		Concert GetConcertArtistIsAttending(int concertId, string artistId);
	}
}