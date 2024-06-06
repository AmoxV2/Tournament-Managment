using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using WWW_APP_PROJECT.Controllers;
using WWW_APP_PROJECT.Models;
using Xunit;

namespace WWW_APP_PROJECT.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<UserManager<AppUser>> _userManagerMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            var store = new Mock<IUserStore<AppUser>>();
            _userManagerMock = new Mock<UserManager<AppUser>>(store.Object, null, null, null, null, null, null, null, null);

            _controller = new UserController(_userManagerMock.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfUsers()
        {
            // Arrange
            var users = new List<AppUser>
            {
                new AppUser { UserName = "user1" },
                new AppUser { UserName = "user2" }
            };

            _userManagerMock.Setup(um => um.Users)
                            .Returns(GetQueryableMockDbSet(users).Object);

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<AppUser>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count);
        }

        private Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            return dbSet;
        }
    }
}