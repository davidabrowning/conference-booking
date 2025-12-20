using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Models;
using ConferenceBooking.Data.Repositories;
using ConferenceBooking.DataTests.Mock;

namespace ConferenceBooking.DataTests
{
    public class RoomRepositoryTests
    {
        private IRoomRepository _roomRepository;

        public RoomRepositoryTests()
        {
            _roomRepository = new RoomRepository(
                InMemoryDatabaseHelper.CreateApplicationDbContext());
        }

        [Fact]
        public async Task Add_WhenCalledOnce_ShouldIncreaseGetAllCountByOne()
        {
            // Arrange
            Room room = new() { Name = "RoomX" };
            int expectedCount = (await _roomRepository.GetAllAsync()).Count() + 1;

            // Act
            await _roomRepository.AddAsync(room);

            // Assert
            Assert.Equal(expectedCount, (await _roomRepository.GetAllAsync()).Count());
        }
    }
}
