using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SocialNetwork.Models
{
	public class Concert
	{
		public int Id { get; set; }

		public bool IsCanceled { get; private set; }
		public ApplicationUser Artist { get; set; }

		[Required]
		public string ArtistId { get; set; }
		public DateTime DateTime { get; set; }
		[Required]
		[StringLength(255)]
		public string Venue { get; set; }



		public Genre Genre { get; set; }

		[Required]
		public int GenreId { get; set; }

		public ICollection<Attendance> Attendances { get; private set; }

		public Concert()
		{
			Attendances = new Collection<Attendance>();
		}

		public void Cancel()
		{
			IsCanceled = true;

			var notification = Notification.ConcertCanceled(this);

			foreach (var attendee in Attendances.Select(a => a.Attendee))
			{
				attendee.Notify(notification);
			}
		}

		public void Modify(DateTime dateTime, string venue, int genre)
		{
			var notification = Notification.ConcertUpdated(this, DateTime, Venue);


			Venue = venue;
			DateTime = dateTime;
			GenreId = genre;

			foreach (var attendee in Attendances.Select(a => a.Attendee))
				attendee.Notify(notification);
		}

	}
}