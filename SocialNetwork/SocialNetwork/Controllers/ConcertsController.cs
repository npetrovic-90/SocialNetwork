using Microsoft.AspNet.Identity;
using SocialNetwork.Models;
using SocialNetwork.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SocialNetwork.Controllers
{
	public class ConcertsController : Controller
	{

		private readonly ApplicationDbContext _dbContext;

		public ConcertsController()
		{
			_dbContext = new ApplicationDbContext();
		}



		[Authorize]
		public ActionResult Mine()
		{
			var userId = User.Identity.GetUserId();
			var concerts = _dbContext.Concerts
				.Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now)
				.Include(g => g.Genre)
				.ToList();

			return View(concerts);


		}

		[Authorize]
		public ActionResult Attending()
		{
			var userId = User.Identity.GetUserId();

			var concerts = _dbContext.Attendances
				.Where(a => a.AttendeeId == userId)
				.Select(a => a.Concert)
				.Include(c => c.Artist)
				.Include(c => c.Genre)
				.ToList();
			var viewModel = new ConcertsViewModel
			{
				UpcomingConcerts = concerts,
				ShowActions = User.Identity.IsAuthenticated,
				Heading = "Concerts i'm Attending"
			};
			return View("Concerts", viewModel);

		}

		[Authorize]
		public ActionResult Create()
		{
			var viewModel = new ConcertFormViewModel()
			{
				Genres = _dbContext.Genres.ToList(),
				Heading = "Add a Concert"
			};

			return View("ConcertForm", viewModel);
		}

		[Authorize]
		public ActionResult Edit(int id)
		{
			var userId = User.Identity.GetUserId();

			var concert = _dbContext.Concerts.Single(g => g.Id == id && g.ArtistId == userId);

			var viewModel = new ConcertFormViewModel()
			{
				Id = concert.Id,
				Genres = _dbContext.Genres.ToList(),
				Date = concert.DateTime.ToString("d MMM yyyy"),
				Time = concert.DateTime.ToString("HH:mm"),
				Genre = concert.GenreId,
				Venue = concert.Venue,
				Heading = "Edit a Concert"

			};

			return View("ConcertForm", viewModel);
		}


		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(ConcertFormViewModel viewModel)
		{

			if (!ModelState.IsValid)
			{
				viewModel.Genres = _dbContext.Genres.ToList();
				return View("ConcertForm", viewModel);
			}

			var concert = new Concert
			{
				ArtistId = User.Identity.GetUserId(),
				DateTime = viewModel.GetDateTime(),
				GenreId = viewModel.Genre,
				Venue = viewModel.Venue
			};

			_dbContext.Concerts.Add(concert);
			_dbContext.SaveChanges();

			return RedirectToAction("Mine", "Concerts");
		}

		//updating a concert
		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Update(ConcertFormViewModel viewModel)
		{

			if (!ModelState.IsValid)
			{
				viewModel.Genres = _dbContext.Genres.ToList();
				return View("ConcertForm", viewModel);
			}

			var userId = User.Identity.GetUserId();
			var concert = _dbContext.Concerts.Single(g => g.Id == viewModel.Id && g.ArtistId == userId);



			concert.DateTime = viewModel.GetDateTime();
			concert.GenreId = viewModel.Genre;
			concert.Venue = viewModel.Venue;



			_dbContext.SaveChanges();

			return RedirectToAction("Mine", "Concerts");
		}
	}
}