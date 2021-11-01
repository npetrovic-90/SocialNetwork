using Microsoft.AspNet.Identity;
using SocialNetwork.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace SocialNetwork.Controllers.Api
{
	[Authorize]
	public class ConcertsController : ApiController
	{
		private readonly ApplicationDbContext _dbContext;

		public ConcertsController()
		{
			_dbContext = new ApplicationDbContext();
		}

		[HttpDelete]
		public IHttpActionResult Cancel(int id)
		{

			var userId = User.Identity.GetUserId();

			var concert = _dbContext.Concerts
				.Include(c => c.Attendances.Select(a => a.Attendee))
				.Single(c => c.Id == id && c.ArtistId == userId);

			if (concert.IsCanceled)
				return NotFound();

			concert.Cancel();

			_dbContext.SaveChanges();

			return Ok();
		}
	}
}
