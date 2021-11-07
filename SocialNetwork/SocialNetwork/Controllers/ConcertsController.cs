using Microsoft.AspNet.Identity;
using SocialNetwork.Models;
using SocialNetwork.Persistence;
using SocialNetwork.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SocialNetwork.Controllers
{
	public class ConcertsController : Controller
	{

		private readonly ApplicationDbContext _dbContext;




		private readonly UnitOfWork _unitOfWork;

		public ConcertsController()
		{
			_dbContext = new ApplicationDbContext();
			_unitOfWork = new UnitOfWork(_dbContext);
		}

		public ActionResult Details(int id)
		{
			var concert = _dbContext.Concerts
				.Include(c => c.Artist)
				.Include(c => c.Genre)
				.SingleOrDefault(c => c.Id == id);

			if (concert == null)
				return HttpNotFound();

			var viewModel = new ConcertDetailsViewModel { Concert = concert };

			if (User.Identity.IsAuthenticated)
			{
				var userId = User.Identity.GetUserId();

				viewModel.IsAttending = _unitOfWork.Attendances.GetAttendance(concert.Id, userId);


				viewModel.IsFollowing = _unitOfWork.Followings.GetFollowing(userId, concert.ArtistId);
			}

			return View("Details", viewModel);
		}


		[Authorize]
		public ActionResult Mine()
		{
			var userId = User.Identity.GetUserId();
			var concerts = _unitOfWork.Concerts.GetUpcomingConcertsByArtist(userId);

			return View(concerts);


		}

		[Authorize]
		public ActionResult Attending()
		{
			var userId = User.Identity.GetUserId();


			var viewModel = new ConcertsViewModel
			{
				UpcomingConcerts = _unitOfWork.Concerts.GetConcertsUserAttending(userId),
				ShowActions = User.Identity.IsAuthenticated,
				Heading = "Concerts i'm Attending",
				Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(a => a.ConcertId)
			};
			return View("Concerts", viewModel);

		}



		[HttpPost]
		public ActionResult Search(ConcertsViewModel viewModel)
		{
			return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
		}

		[Authorize]
		public ActionResult Create()
		{
			var viewModel = new ConcertFormViewModel()
			{
				Genres = _unitOfWork.Genres.GetGenres(),
				Heading = "Add a Concert"
			};

			return View("ConcertForm", viewModel);
		}

		[Authorize]
		public ActionResult Edit(int id)
		{


			var concert = _unitOfWork.Concerts.GetConcert(id);

			if (concert == null) return HttpNotFound();

			if (concert.ArtistId != User.Identity.GetUserId())
				return new HttpUnauthorizedResult();

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

			_unitOfWork.Concerts.Add(concert);
			_unitOfWork.Complete();

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

			var concert = _unitOfWork.Concerts.GetConcertWithAttendees(viewModel.Id);

			if (concert == null)
				return HttpNotFound();

			if (concert.ArtistId != User.Identity.GetUserId())
				return new HttpUnauthorizedResult();

			concert.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

			_unitOfWork.Complete();

			return RedirectToAction("Mine", "Concerts");
		}
	}
}