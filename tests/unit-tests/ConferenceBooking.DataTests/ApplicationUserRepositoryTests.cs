using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Models;
using ConferenceBooking.Data.Repositories;
using ConferenceBooking.DataTests.MockDb;

namespace ConferenceBooking.DataTests
{
    public class ApplicationUserRepositoryTests
    {
        private readonly IApplicationUserRepository _applicationUserRepository;

        public ApplicationUserRepositoryTests()
        {
            _applicationUserRepository = new ApplicationUserRepository(
                InMemoryDatabaseHelper.CreateApplicationDbContext());
        }

        [Fact]
        public async Task Add_WhenCalledOnce_ShouldIncreaseGetAllCountByOne()
        {
            // Arrange
            ApplicationUser applicationUser = new() { Username = "TestUser" + DateTime.Now } ;
            int expectedCount = (await _applicationUserRepository.GetAllAsync()).Count() + 1;

            // Act
            await _applicationUserRepository.AddAsync(applicationUser);

            // Assert
            Assert.Equal(expectedCount, (await _applicationUserRepository.GetAllAsync()).Count());
        }
    }
}
