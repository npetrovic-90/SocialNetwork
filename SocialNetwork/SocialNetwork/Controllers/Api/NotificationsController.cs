using Microsoft.AspNet.Identity;
using SocialNetwork.Dto;
using SocialNetwork.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace SocialNetwork.Controllers.Api
{


	public class NotificationsController : ApiController
	{
		private readonly ApplicationDbContext _dbContext;

		public NotificationsController()
		{
			_dbContext = new ApplicationDbContext();

		}
		[HttpPost]
		public IHttpActionResult MarkAsRead()
		{
			var userId = User.Identity.GetUserId();
			var notifications = _dbContext.UserNotifications.Where(un => un.UserId == userId && !un.IsRead).ToList();

			notifications.ForEach(n => n.Read());

			_dbContext.SaveChanges();

			return Ok();

		}

		[HttpGet]
		public IEnumerable<NotificationDto> GetNewNotifications()
		{

			var userId = User.Identity.GetUserId();

			var notifications = _dbContext.UserNotifications
				.Where(un => !un.IsRead)
				.Select(un => un.Notification)
				.Include(n => n.Concert.Artist)
				.ToList();




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
