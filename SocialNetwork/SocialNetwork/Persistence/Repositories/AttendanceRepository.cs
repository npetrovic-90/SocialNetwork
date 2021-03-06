using SocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Repositories
{
	public class AttendanceRepository : IAttendanceRepository
	{
		private readonly ApplicationDbContext _dbContext;
		public AttendanceRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public IEnumerable<Attendance> GetFutureAttendances(string userId)
		{
			return _dbContext.Attendances
				.Where(a => a.AttendeeId == userId && a.Concert.DateTime > DateTime.Now)
				.ToList();
		}

		public Attendance GetAttendance(int concertId, string userId)
		{
			return _dbContext.Attendances.SingleOrDefault(a => a.ConcertId == concertId && a.AttendeeId == userId);

		}

		public void Remove(Attendance attendance)
		{
			_dbContext.Attendances.Remove(attendance);
		}

		public void Add(Attendance attendance)
		{
			_dbContext.Attendances.Add(attendance);
		}

	}
}