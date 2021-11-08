using Microsoft.AspNet.Identity;
using SocialNetwork.Persistence;
using System.Web.Http;

namespace SocialNetwork.Controllers.Api
{
	[Authorize]
	public class ConcertsController : ApiController
	{
		private readonly IUnitOfWork _unitOfWork;


		public ConcertsController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpDelete]
		public IHttpActionResult Cancel(int id)
		{

			var userId = User.Identity.GetUserId();

			var concert = _unitOfWork.Concerts.GetConcertArtistIsAttending(id, userId);

			if (concert.IsCanceled)
				return NotFound();

			concert.Cancel();

			_unitOfWork.Complete();

			return Ok();
		}
	}
}
