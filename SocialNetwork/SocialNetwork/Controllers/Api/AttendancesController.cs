using Microsoft.AspNet.Identity;
using SocialNetwork.Models;
using SocialNetwork.Persistence;
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
		private readonly IUnitOfWork _unitOfWork;


		public AttendancesController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;

		}

		[HttpDelete]
		public IHttpActionResult DeleteAttendance(int id)
		{
			var userId = User.Identity.GetUserId();

			var attendance = _unitOfWork.Attendances.GetAttendance(id, userId);

			if (attendance == null)
				return NotFound();

			_unitOfWork.Attendances.Remove(attendance);
			_unitOfWork.Complete();

			return Ok(id);
		}

		[HttpPost]
		public IHttpActionResult Attend(AttendanceDto dto)
		{
			var currUser = User.Identity.GetUserId();


			if (_unitOfWork.Attendances.GetAttendance(dto.ConcertId, currUser) != null)
				return BadRequest("The attendance already exists.");

			var attendance = new Attendance
			{
				ConcertId = dto.ConcertId,
				AttendeeId = currUser
			};


			_unitOfWork.Attendances.Add(attendance);
			_unitOfWork.Complete();


			return Ok();
		}

	}
}
