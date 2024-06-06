using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WWW_APP_PROJECT.Controllers;
using WWW_APP_PROJECT.Data.Enum;
using WWW_APP_PROJECT.Interfaces;
using WWW_APP_PROJECT.Models;
using WWW_APP_PROJECT.ViewModels;
using Xunit;

namespace WWW_APP_PROJECT.Tests
{
    public class TournamentControllerTests
    {
        private readonly Mock<IPhotoService> _photoServiceMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<ITournamentRepository> _tournamentRepositoryMock;
        private readonly Mock<ITeamRepository> _teamRepositoryMock;
        private readonly Mock<ITeamToTournamentRepository> _teamToTournamentRepositoryMock;
        private readonly Mock<IShareRepository> _shareRepositoryMock;
        private readonly Mock<IMatchRepository> _matchRepositoryMock;
        private readonly TournamentController _controller;

        public TournamentControllerTests()
        {
            _photoServiceMock = new Mock<IPhotoService>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _tournamentRepositoryMock = new Mock<ITournamentRepository>();
            _teamRepositoryMock = new Mock<ITeamRepository>();
            _teamToTournamentRepositoryMock = new Mock<ITeamToTournamentRepository>();
            _shareRepositoryMock = new Mock<IShareRepository>();
            _matchRepositoryMock = new Mock<IMatchRepository>();

            _controller = new TournamentController(
                _photoServiceMock.Object,
                _httpContextAccessorMock.Object,
                _tournamentRepositoryMock.Object,
                _teamRepositoryMock.Object,
                _teamToTournamentRepositoryMock.Object,
                _matchRepositoryMock.Object,
                _shareRepositoryMock.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"));

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(user);
        }

        [Fact]
        public void Create_Get_ReturnsViewResult_WithCreateTournamentViewModel()
        {
            // Act
            var result = _controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CreateTournamentViewModel>(viewResult.ViewData.Model);
            Assert.NotNull(model.AppUserId);
        }

        [Fact]
        public async Task Create_Post_RedirectsToSetNumberOfTeams_WhenModelIsValid()
        {
            // Arrange
            var tournamentVM = new CreateTournamentViewModel { Name = "Tournament 1" };

            // Act
            var result = await _controller.Create(tournamentVM);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("SetNumberOfTeams", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Create_Post_ReturnsViewResult_WithModelError_WhenModelIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "Required");

            var tournamentVM = new CreateTournamentViewModel { Name = "" };

            // Act
            var result = await _controller.Create(tournamentVM);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CreateTournamentViewModel>(viewResult.ViewData.Model);
            Assert.Equal(tournamentVM, model);
            Assert.True(_controller.ModelState.ContainsKey("Name"));
        }

        [Fact]
        public async Task Edit_Get_ReturnsViewResult_WithEditTournamentViewModel()
        {
            // Arrange
            var tournament = new TeamTournament
            {
                Id = 1,
                Name = "Tournament 1",
                Description = "Description 1"
            };

            _tournamentRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(tournament);

            // Act
            var result = await _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<EditTournamentViewModel>(viewResult.ViewData.Model);
            Assert.Equal(tournament.Id, model.Id);
            Assert.Equal(tournament.Name, model.Name);
            Assert.Equal(tournament.Description, model.Description);
        }

        [Fact]
        public async Task Edit_Post_RedirectsToDetail_WhenModelIsValid()
        {
            // Arrange
            var tournamentVM = new EditTournamentViewModel { Id = 1, Name = "Updated Tournament" };

            var tournament = new TeamTournament { Id = 1 };

            _tournamentRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(tournament);

            // Act
            var result = await _controller.Edit(tournamentVM);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Detail", redirectToActionResult.ActionName);
            Assert.Equal("Tournament", redirectToActionResult.ControllerName);
        }

        [Fact]
        public async Task Edit_Post_ReturnsViewResult_WithModelError_WhenModelIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "Required");

            var tournamentVM = new EditTournamentViewModel { Id = 1, Name = "" };

            // Act
            var result = await _controller.Edit(tournamentVM);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<EditTournamentViewModel>(viewResult.ViewData.Model);
            Assert.Equal(tournamentVM, model);
            Assert.True(_controller.ModelState.ContainsKey("Name"));
        }

        [Fact]
        public async Task Manage_ReturnsRedirectToActionResult_BasedOnTournamentState()
        {
            // Arrange
            var tournament = new TeamTournament { Id = 1, NumberOfTeams = 4, WinnerTeamId = null, TournamentType = TournamentType.League };
            var teams = new List<Team> { new Team { Id = 1 }, new Team { Id = 2 }, new Team { Id = 3 }, new Team { Id = 4 } };

            _tournamentRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(tournament);
            _tournamentRepositoryMock.Setup(repo => repo.GetTeams(1)).ReturnsAsync(teams);
            _tournamentRepositoryMock.Setup(repo => repo.hasTorunamentStarted(1)).Returns(false);

            // Act
            var result = await _controller.Manage(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("AddTeams", redirectToActionResult.ActionName);
        }
    }
}
