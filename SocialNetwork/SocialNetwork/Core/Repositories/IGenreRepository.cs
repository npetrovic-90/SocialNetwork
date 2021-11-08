using SocialNetwork.Models;
using System.Collections.Generic;

namespace SocialNetwork.Repositories
{
	public interface IGenreRepository
	{
		IEnumerable<Genre> GetGenres();
	}
}