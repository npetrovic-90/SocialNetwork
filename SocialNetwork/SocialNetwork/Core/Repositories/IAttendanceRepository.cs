using SocialNetwork.Models;
using System.Collections.Generic;

namespace SocialNetwork.Repositories
{
	public interface IAttendanceRepository
	{
		bool GetAttendance(int concertId, string userId);
		IEnumerable<Attendance> GetFutureAttendances(string userId);
	}
}