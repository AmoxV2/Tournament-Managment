using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WWW_APP_PROJECT.Data;
using WWW_APP_PROJECT.Models;
using Xunit;

namespace RunGroopWebApp.Data.Tests
{
    public class SeedTests
    {
        private readonly Mock<IServiceScopeFactory> _serviceScopeFactoryMock;
        private readonly Mock<IServiceScope> _serviceScopeMock;
        private readonly Mock<RoleManager<IdentityRole>> _roleManagerMock;
        private readonly Mock<UserManager<AppUser>> _userManagerMock;

        public SeedTests()
        {
            _serviceScopeFactoryMock = new Mock<IServiceScopeFactory>();
            _serviceScopeMock = new Mock<IServiceScope>();
            _roleManagerMock = new Mock<RoleManager<IdentityRole>>(
                new Mock<IRoleStore<IdentityRole>>().Object, null, null, null, null);
            _userManagerMock = new Mock<UserManager<AppUser>>(
                new Mock<IUserStore<AppUser>>().Object, null, null, null, null, null, null, null, null);

            _serviceScopeFactoryMock.Setup(x => x.CreateScope()).Returns(_serviceScopeMock.Object);
            _serviceScopeMock.Setup(x => x.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>)))
                             .Returns(_roleManagerMock.Object);
            _serviceScopeMock.Setup(x => x.ServiceProvider.GetService(typeof(UserManager<AppUser>)))
                             .Returns(_userManagerMock.Object);
        }

        [Fact]
        public async Task SeedUsersAndRolesAsync_CreatesAdminRoleIfNotExists()
        {
            // Arrange
            _roleManagerMock.Setup(x => x.RoleExistsAsync(UserRoles.Admin)).ReturnsAsync(false);

            var applicationBuilderMock = new Mock<IApplicationBuilder>();
            applicationBuilderMock.Setup(x => x.ApplicationServices.GetService(typeof(IServiceScopeFactory)))
                                  .Returns(_serviceScopeFactoryMock.Object);

            // Act
            await Seed.SeedUsersAndRolesAsync(applicationBuilderMock.Object);

            // Assert
            _roleManagerMock.Verify(x => x.CreateAsync(It.Is<IdentityRole>(r => r.Name == UserRoles.Admin)), Times.Once);
        }

        [Fact]
        public async Task SeedUsersAndRolesAsync_CreatesUserRoleIfNotExists()
        {
            // Arrange
            _roleManagerMock.Setup(x => x.RoleExistsAsync(UserRoles.User)).ReturnsAsync(false);

            var applicationBuilderMock = new Mock<IApplicationBuilder>();
            applicationBuilderMock.Setup(x => x.ApplicationServices.GetService(typeof(IServiceScopeFactory)))
                                  .Returns(_serviceScopeFactoryMock.Object);

            // Act
            await Seed.SeedUsersAndRolesAsync(applicationBuilderMock.Object);

            // Assert
            _roleManagerMock.Verify(x => x.CreateAsync(It.Is<IdentityRole>(r => r.Name == UserRoles.User)), Times.Once);
        }

        [Fact]
        public async Task SeedUsersAndRolesAsync_CreatesAdminUserIfNotExists()
        {
            // Arrange
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.Is<string>(email => email == "admin@gmail.com"))).ReturnsAsync((AppUser)null);

            var applicationBuilderMock = new Mock<IApplicationBuilder>();
            applicationBuilderMock.Setup(x => x.ApplicationServices.GetService(typeof(IServiceScopeFactory)))
                                  .Returns(_serviceScopeFactoryMock.Object);

            // Act
            await Seed.SeedUsersAndRolesAsync(applicationBuilderMock.Object);

            // Assert
            _userManagerMock.Verify(x => x.CreateAsync(It.Is<AppUser>(u => u.Email == "admin@gmail.com"), "Coding@1234?"), Times.Once);
            _userManagerMock.Verify(x => x.AddToRoleAsync(It.Is<AppUser>(u => u.Email == "admin@gmail.com"), UserRoles.Admin), Times.Once);
        }

        [Fact]
        public async Task SeedUsersAndRolesAsync_CreatesAppUserIfNotExists()
        {
            // Arrange
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.Is<string>(email => email == "user@etickets.com"))).ReturnsAsync((AppUser)null);

            var applicationBuilderMock = new Mock<IApplicationBuilder>();
            applicationBuilderMock.Setup(x => x.ApplicationServices.GetService(typeof(IServiceScopeFactory)))
                                  .Returns(_serviceScopeFactoryMock.Object);

            // Act
            await Seed.SeedUsersAndRolesAsync(applicationBuilderMock.Object);

            // Assert
            _userManagerMock.Verify(x => x.CreateAsync(It.Is<AppUser>(u => u.Email == "user@etickets.com"), "Coding@1234?"), Times.Once);
            _userManagerMock.Verify(x => x.AddToRoleAsync(It.Is<AppUser>(u => u.Email == "user@etickets.com"), UserRoles.User), Times.Once);
        }
    }
}
