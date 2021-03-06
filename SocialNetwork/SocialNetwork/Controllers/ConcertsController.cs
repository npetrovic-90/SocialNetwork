using Microsoft.AspNet.Identity;
using SocialNetwork.Models;
using SocialNetwork.Persistence;
using SocialNetwork.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace SocialNetwork.Controllers
{
	public class ConcertsController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public ConcertsController(IUnitOfWork unitOfWork)
		{

			_unitOfWork = unitOfWork;
		}

		public ActionResult Details(int id)
		{
			var concert = _unitOfWork.Concerts.GetConcert(id);

			if (concert == null)
				return HttpNotFound();

			var viewModel = new ConcertDetailsViewModel { Concert = concert };

			if (User.Identity.IsAuthenticated)
			{
				var userId = User.Identity.GetUserId();

				viewModel.IsAttending = (_unitOfWork.Attendances.GetAttendance(concert.Id, userId) == null) ? false : true;


				viewModel.IsFollowing = (_unitOfWork.Followings.GetFollowing(userId, concert.ArtistId) == null ? false : true);
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
				Genres = _unitOfWork.Genres.GetGenres(),
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
				viewModel.Genres = _unitOfWork.Genres.GetGenres();
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
				viewModel.Genres = _unitOfWork.Genres.GetGenres();
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