using Microsoft.AspNet.Identity;
using SocialNetwork.Dto;
using SocialNetwork.Models;
using SocialNetwork.Persistence;
using System.Web.Http;

namespace SocialNetwork.Controllers
{

	[Authorize]
	public class FollowingsController : ApiController
	{
		private readonly IUnitOfWork _unitOfWork;


		public FollowingsController(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpDelete]
		public IHttpActionResult Unfollow(string id)
		{
			var userId = User.Identity.GetUserId();

			var following = _unitOfWork.Followings.GetFollowing(userId, id);

			if (following == null)
				return NotFound();

			_unitOfWork.Followings.Remove(following);
			_unitOfWork.Complete();


			return Ok(id);
		}

		[HttpPost]
		public IHttpActionResult Follow(FollowingDto dto)
		{
			var userId = User.Identity.GetUserId();

			if (_unitOfWork.Followings.GetFollowing(userId, dto.FolloweeId) != null)
				return BadRequest("Following already exists");

			var following = new Following { FollowerId = userId, FolloweeId = dto.FolloweeId };


			_unitOfWork.Followings.Add(following);
			_unitOfWork.Complete();

			return Ok();
		}
	}
}
