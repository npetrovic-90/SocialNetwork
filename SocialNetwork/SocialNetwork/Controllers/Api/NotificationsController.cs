using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using SocialNetwork.Dto;
using SocialNetwork.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SocialNetwork.Controllers.Api
{


	public class NotificationsController : ApiController
	{

		private readonly IUnitOfWork _unitOfWork;



		public NotificationsController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;

		}
		[HttpPost]
		public IHttpActionResult MarkAsRead()
		{
			var userId = User.Identity.GetUserId();
			var notifications = _unitOfWork.Notifications.GetUserNotifications(userId);

			notifications.ForEach(n => n.Read());

			_unitOfWork.Complete();


			return Ok();

		}

		[HttpGet]
		public IEnumerable<NotificationDto> GetNewNotifications()
		{

			var userId = User.Identity.GetUserId();

			var notifications = _unitOfWork.Notifications.GetAllNotifications();



			//mapping to dto
			return notifications.Select(n => new NotificationDto()
			{
				DateTime = n.DateTime,
				Concert = new ConcertDto
				{
					Artist = new UserDto
					{
						Id = n.Concert.Artist.Id,
						Name = n.Concert.Artist.Name
					},
					DateTime = n.Concert.DateTime,
					Id = n.Concert.Id,
					IsCanceled = n.Concert.IsCanceled,
					Venue = n.Concert.Venue


				},
				OriginalDateTime = n.OriginalDateTime,
				OriginalVenue = n.OriginalVenue,
				Type = n.Type

			});
		}
	}
}
