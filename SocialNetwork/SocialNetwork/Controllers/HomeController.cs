using SocialNetwork.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SocialNetwork.Controllers
{
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext _dbcontext;

		public HomeController()
		{
			_dbcontext = new ApplicationDbContext();
		}
		public ActionResult Index()
		{
			var upcomingConcerts = _dbcontext.Concerts
				.Include(c => c.Artist)
				.Where(c => c.DateTime > DateTime.Now);

			return View(upcomingConcerts);
		}

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