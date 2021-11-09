using Microsoft.AspNet.Identity;
using SocialNetwork.Models;
using SocialNetwork.Repositories;
using SocialNetwork.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SocialNetwork.Controllers
{
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly AttendanceRepository _attendanceRepository;

		public HomeController()
		{
			_dbContext = new ApplicationDbContext();
			_attendanceRepository = new AttendanceRepository(_dbContext);
		}
		public ActionResult Index(string query = null)
		{
			var upcomingConcerts = _dbContext.Concerts
				.Include(c => c.Artist)
				.Include(c => c.Genre)
				.Where(c => c.DateTime > DateTime.Now && !c.IsCanceled);

			if (!String.IsNullOrWhiteSpace(query))
			{
				upcomingConcerts = upcomingConcerts.Where(c =>
				  c.Artist.Name.Contains(query) ||
				  c.Genre.Name.Contains(query) ||
				  c.Venue.Contains(query));
			}

			var userId = User.Identity.GetUserId();

			var attendances = _attendanceRepository.GetFutureAttendances(userId).ToLookup(a => a.ConcertId);

			var viewModel = new ConcertsViewModel
			{
				UpcomingConcerts = upcomingConcerts,
				ShowActions = User.Identity.IsAuthenticated,
				Heading = "Upcoming Concerts",
				SearchTerm = query,
				Attendances = attendances
			};

			return View("Concerts", viewModel);
		}

		//finito

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}