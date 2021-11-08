using SocialNetwork.Models;
using System.Collections.Generic;

namespace SocialNetwork.Repositories
{
	public interface IAttendanceRepository
	{
		Attendance GetAttendance(int concertId, string userId);
		IEnumerable<Attendance> GetFutureAttendances(string userId);

		void Remove(Attendance attendance);

		void Add(Attendance attendance);
	}
}