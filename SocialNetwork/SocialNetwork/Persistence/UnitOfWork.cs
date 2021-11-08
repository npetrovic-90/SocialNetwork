﻿using SocialNetwork.Models;
using SocialNetwork.Repositories;

namespace SocialNetwork.Persistence
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _dbContext;

		public IConcertRepository Concerts { get; private set; }
		public IGenreRepository Genres { get; set; }
		public IFollowingRepository Followings { get; set; }
		public IAttendanceRepository Attendances { get; set; }

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