using SocialNetwork.Models;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Repositories
{
	public class GenreRepository
	{
		private readonly ApplicationDbContext _dbContext;
		public GenreRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IEnumerable<Genre> GetGenres()
		{
			return _dbContext.Genres.ToList();
		}
	}
}