using SocialNetwork.Controllers;
using SocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace SocialNetwork.ViewModels
{
	public class ConcertFormViewModel
	{

		public int Id { get; set; }
		[Required]
		public string Venue { get; set; }

		[Required]
		[FutureDate]
		public string Date { get; set; }

		[Required]
		[ValidTime]
		public string Time { get; set; }

		[Required]
		public int Genre { get; set; }
		public IEnumerable<Genre> Genres { get; set; }

		public string Heading { get; set; }

		public string Action
		{
			get
			{
				Expression<Func<ConcertsController, ActionResult>> update = (c => c.Update(this));
				Expression<Func<ConcertsController, ActionResult>> create = (c => c.Create(this));

				var action = (Id != 0) ? update : create;
				var actionName = (action.Body as MethodCallExpression).Method.Name;

				return actionName;

			}
		}
		public DateTime GetDateTime()
		{

			return DateTime.Parse(string.Format("{0} {1}", Date, Time));

		}

	}
}