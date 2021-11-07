using SocialNetwork.Models;
using SocialNetwork.Repositories;

namespace SocialNetwork.Persistence
{
	public class UnitOfWork
	{
		private readonly ApplicationDbContext _dbContext;

		public ConcertRepository Concerts { get; private set; }
		public GenreRepository Genres { get; set; }
		public FollowingRepository Followings { get; set; }
		public AttendanceRepository Attendances { get; set; }

		public UnitOfWork(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
			Concerts = new ConcertRepository(_dbContext);
			Genres = new GenreRepository(_dbContext);
			Followings = new FollowingRepository(_dbContext);
			Attendances = new AttendanceRepository(_dbContext);
		}

		public void Complete()
		{
			_dbContext.SaveChanges();
		}


	}
}