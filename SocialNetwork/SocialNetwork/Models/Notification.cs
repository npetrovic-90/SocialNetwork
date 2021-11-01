using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Models
{
	public class Notification
	{
		public int Id { get; private set; }
		public DateTime DateTime { get; private set; }
		public NotificationType Type { get; private set; }
		public DateTime? OriginalDateTime { get; set; }
		public string OriginalVenue { get; set; }

		[Required]
		public Concert Concert { get; private set; }

		protected Notification()
		{

		}

		public Notification(NotificationType type, Concert concert)
		{
			if (concert == null)
				throw new ArgumentNullException("concert");

			Type = type;
			Concert = concert;
			DateTime = DateTime.Now;
		}

	}
}