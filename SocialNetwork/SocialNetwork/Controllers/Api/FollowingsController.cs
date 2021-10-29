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

		[HttpPost]
		public IHttpActionResult Follow(FollowingDto dto)
		{
			var userId = User.Identity.GetUserId();

			if (_dbContext.Followings.Any(f => f.FolloweeId == userId && f.FolloweeId == dto.FolloweeId))
				return BadRequest("Following already exists");

			var following = new Following { FollowerId = userId, FolloweeId = dto.FolloweeId };

			_dbContext.Followings.Add(following);
			_dbContext.SaveChanges();

			return Ok();
		}
	}
}
