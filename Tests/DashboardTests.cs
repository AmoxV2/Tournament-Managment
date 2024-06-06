using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WWW_APP_PROJECT.Controllers;
using WWW_APP_PROJECT.Interfaces;
using WWW_APP_PROJECT.Models;
using WWW_APP_PROJECT.ViewModels;
using Xunit;

namespace WWW_APP_PROJECT.Tests
{
    public class DashboardControllerTests
    {
        private readonly Mock<IDashboardRepository> _dashboardRepositoryMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<IPhotoService> _photoServiceMock;
        private readonly DashboardController _controller;

        public DashboardControllerTests()
        {
            _dashboardRepositoryMock = new Mock<IDashboardRepository>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _photoServiceMock = new Mock<IPhotoService>();
            _controller = new DashboardController(_dashboardRepositoryMock.Object, _httpContextAccessorMock.Object, _photoServiceMock.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"));

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(user);
        }

        [Fact]
        public void Settings_ReturnsViewResult()
        {
            // Act
            var result = _controller.Settings();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task EditUserProfile_GET_ReturnsViewResult_WithEditUserDashboardViewModel()
        {
            // Arrange
            var user = new AppUser
            {
                Id = "1",
                UserName = "testuser",
                FirstName = "Test",
                LastName = "User",
                ImageUrl = "http://example.com/image.jpg"
            };

            _dashboardRepositoryMock.Setup(repo => repo.GetUserById("1"))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.EditUserProfile();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<EditUserDashboardViewModel>(viewResult.ViewData.Model);
            Assert.Equal(user.Id, model.Id);
            Assert.Equal(user.UserName, model.UserName);
            Assert.Equal(user.FirstName, model.FirstName);
            Assert.Equal(user.LastName, model.LastName);
            Assert.Equal(user.ImageUrl, model.ProfileImageUrl);
        }

        [Fact]
        public async Task EditUserProfile_POST_ReturnsRedirectToActionResult_WhenModelIsValid()
        {
            // Arrange
            var editUserViewModel = new EditUserDashboardViewModel
            {
                Id = "1",
                UserName = "testuser",
                FirstName = "Test",
                LastName = "User",
                Image = new Mock<IFormFile>().Object
            };

            var user = new AppUser
            {
                Id = "1",
                UserName = "testuser",
                FirstName = "Test",
                LastName = "User",
                ImageUrl = "http://example.com/oldimage.jpg"
            };

            var photoResult = new ImageUploadResult { Url = new Uri("http://example.com/newimage.jpg") };

            _dashboardRepositoryMock.Setup(repo => repo.GetUserByIdNoTracking(editUserViewModel.Id))
                .ReturnsAsync(user);

            _photoServiceMock.Setup(service => service.AddPhotoAsync(editUserViewModel.Image))
                .ReturnsAsync(photoResult);

            // Act
            var result = await _controller.EditUserProfile(editUserViewModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _dashboardRepositoryMock.Verify(repo => repo.Update(It.IsAny<AppUser>()), Times.Once);
        }

        [Fact]
        public async Task EditUserProfile_POST_ReturnsViewResult_WhenModelIsInvalid()
        {
            // Arrange
            var editUserViewModel = new EditUserDashboardViewModel
            {
                Id = "1",
                UserName = "testuser",
                FirstName = "Test",
                LastName = "User"
            };

            _controller.ModelState.AddModelError("UserName", "Required");

            // Act
            var result = await _controller.EditUserProfile(editUserViewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(editUserViewModel, viewResult.ViewData.Model);
        }
    }
}
