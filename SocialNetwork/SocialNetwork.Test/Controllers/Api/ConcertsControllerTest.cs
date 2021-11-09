using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialNetwork.Controllers.Api;
using SocialNetwork.Models;
using SocialNetwork.Persistence;
using SocialNetwork.Repositories;
using SocialNetwork.Test.Extensions;
using System.Web.Http.Results;

namespace SocialNetwork.Test.Controllers.Api
{
	[TestClass]
	public class ConcertsControllerTest

	{

		private ConcertsController _controller;
		private Mock<IConcertRepository> _mockRepository;

		private string _userId;
		public ConcertsControllerTest()
		{
			_mockRepository = new Mock<IConcertRepository>();


			var mockUoW = new Mock<IUnitOfWork>();

			mockUoW.SetupGet(u => u.Concerts).Returns(_mockRepository.Object);

			_controller = new ConcertsController(mockUoW.Object);
			_userId = "1";
			_controller.MockCurrentUser(_userId, "user1@domain.com");

		}
		[TestMethod]
		public void Cancel_NoConcertWithGivenIdExists_ShouldReturnNotFound()
		{
			var result = _controller.Cancel(1);

			result.Should().BeOfType<NotFoundResult>();
		}

		[TestMethod]
		public void Cancel_ConcertIsCanceled_ShouldReturnNotFound()
		{
			var concert = new Concert();

			concert.Cancel();

			_mockRepository.Setup(r => r.GetConcertArtistIsAttending(1)).Returns(concert);
			var result = _controller.Cancel(1);
			result.Should().BeOfType<NotFoundResult>();
		}

		[TestMethod]
		public void Cancel_UserCancelingAnotherUserConcert_ShouldReturnUnauthorized()
		{
			var concert = new Concert { ArtistId = _userId + "-" };

			_mockRepository.Setup(r => r.GetConcertArtistIsAttending(1)).Returns(concert);
			var result = _controller.Cancel(1);
			result.Should().BeOfType<UnauthorizedResult>();
		}

		[TestMethod]
		public void Cancel_ValidRequest_ShouldReturnOk()
		{
			var concert = new Concert { ArtistId = _userId };

			_mockRepository.Setup(r => r.GetConcertArtistIsAttending(1)).Returns(concert);
			var result = _controller.Cancel(1);
			result.Should().BeOfType<OkResult>();
		}



	}
}
