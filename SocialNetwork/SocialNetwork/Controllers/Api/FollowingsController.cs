using Microsoft.AspNet.Identity;
using SocialNetwork.Dto;
using SocialNetwork.Models;
using System.Linq;
using System.Web.Http;

namespace SocialNetwork.Controllers
{

	[Authorize]
	public class FollowingsController : ApiController
	{
		private readonly ApplicationDbContext _dbContext;

		public FollowingsController()
		{
			_dbContext = new ApplicationDbContext();
		}

		[HttpDelete]
		public IHttpActionResult Unfollow(string id)
		{
			var userId = User.Identity.GetUserId();

			var following = _dbContext.Followings
				.SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == id);

			if (following == null)
				return NotFound();

			_dbContext.Followings.Remove(following);
			_dbContext.SaveChanges();

			return Ok(id);
		}

		[HttpPost]
		public IHttpActionResult Follow(FollowingDto dto)
		{
			var userId = User.Identity.GetUserId();

			if (_dbContext.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId))
				return BadRequest("Following already exists");

			var following = new Following { FollowerId = userId, FolloweeId = dto.FolloweeId };

			_dbContext.Followings.Add(following);
			_dbContext.SaveChanges();

			return Ok();
		}
	}
}
