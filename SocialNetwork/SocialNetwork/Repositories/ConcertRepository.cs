using SocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SocialNetwork.Repositories
{
	public class ConcertRepository
	{
		private readonly ApplicationDbContext _dbContext;
		public ConcertRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public Concert GetConcert(int concertId)
		{
			return _dbContext.Concerts.Include(c => c.Artist).Include(c => c.Genre).Single(g => g.Id == concertId);
		}
		public Concert GetConcertWithAttendees(int concertId)
		{
			return _dbContext.Concerts
				.Include(c => c.Attendances.Select(a => a.Attendee))
				.SingleOrDefault(g => g.Id == concertId);
		}

		public IEnumerable<Concert> GetUpcomingConcertsByArtist(string artistId)
		{
			return _dbContext.Concerts
				.Where(g => g.ArtistId == artistId && g.DateTime > DateTime.Now && !g.IsCanceled)
				.Include(g => g.Genre)
				.ToList();
		}
		public IEnumerable<Concert> GetConcertsUserAttending(string userId)
		{
			return _dbContext.Attendances
				.Where(a => a.AttendeeId == userId)
				.Select(a => a.Concert)
				.Include(c => c.Artist)
				.Include(c => c.Genre)
				.ToList();
		}

		public void Add(Concert concert)
		{
			_dbContext.Concerts.Add(concert);
		}
	}
}