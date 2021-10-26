﻿using Microsoft.AspNet.Identity;
using SocialNetwork.Models;
using SocialNetwork.ViewModels;
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
		public ActionResult Create()
		{
			var viewModel = new ConcertFormViewModel()
			{
				Genres = _dbContext.Genres.ToList()
			};

			return View(viewModel);
		}

		[Authorize]
		[HttpPost]
		public ActionResult Create(ConcertFormViewModel viewModel)
		{

			if (!ModelState.IsValid)
			{
				viewModel.Genres = _dbContext.Genres.ToList();
				return View("Create", viewModel);
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

			return RedirectToAction("Index", "Home");
		}
	}
}