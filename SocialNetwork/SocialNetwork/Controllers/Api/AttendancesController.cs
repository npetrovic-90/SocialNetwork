using Microsoft.AspNet.Identity;
using SocialNetwork.Models;
using System.Linq;
using System.Web.Http;

namespace SocialNetwork.Controllers
{
	public class AttendanceDto
	{
		public int ConcertId { get; set; }
	}
	[Authorize]
	public class AttendancesController : ApiController
	{
		private readonly ApplicationDbContext _dbContext;

		public AttendancesController()
		{
			_dbContext = new ApplicationDbContext();
		}

		[HttpDelete]
		public IHttpActionResult DeleteAttendance(int id)
		{
			var userId = User.Identity.GetUserId();

			var attendance = _dbContext.Attendances
				.SingleOrDefault(a => a.AttendeeId == userId && a.ConcertId == id);

			if (attendance == null)
				return NotFound();

			_dbContext.Attendances.Remove(attendance);
			_dbContext.SaveChanges();

			return Ok(id);
		}

		[HttpPost]
		public IHttpActionResult Attend(AttendanceDto dto)
		{
			var currUser = User.Identity.GetUserId();


			if (_dbContext.Attendances.Any(a => a.AttendeeId == currUser && a.ConcertId == dto.ConcertId))
				return BadRequest("The attendance already exists.");

			var attendance = new Attendance
			{
				ConcertId = dto.ConcertId,
				AttendeeId = currUser
			};

			_dbContext.Attendances.Add(attendance);
			_dbContext.SaveChanges();

			return Ok();
		}

	}
}
