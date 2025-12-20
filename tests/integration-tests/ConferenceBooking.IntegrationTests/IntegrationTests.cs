using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Data;
using ConferenceBooking.Data.Repositories;
using ConferenceBooking.Services.Services;

namespace ConferenceBooking.IntegrationTests
{
    public class IntegrationTests
    {
        private readonly IBookingService _bookingService;

        public IntegrationTests()
        {
            ApplicationDbContext applicationDbContext = IntegrationTestsHelper.CreateDbContextForIntegrationTests();
            IBookingRepository bookingRepository = new BookingRepository(applicationDbContext);
            IRoomRepository roomRepository = new RoomRepository(applicationDbContext);
            _bookingService = new BookingService(bookingRepository, roomRepository);

            IntegrationTestsHelper.ResetAndUpdateTestDatabase(applicationDbContext);
        }

        [Fact]
        public async Task GetAllBookings_WhenCalled_IsNotNull()
        {
            // Arrange

            // Act
            IEnumerable<BookingDto> result = await _bookingService.GetAllBookingsAsync();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Room_Initially_ShouldShouldNotExistInDatabase()
        {
            // Arrange
            string roomName = "TestRoom";

            // Act
            

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () 
                => await _bookingService.GetRoomByNameAsync(roomName));
        }

        [Fact]
        public async Task Room_WhenSaved_ShouldSaveCorrectlyInDatabase()
        {
            // Arrange
            RoomDto newRoomDto = new() { Name = "TestRoom" };

            // Act
            await _bookingService.AddRoomAsync(newRoomDto);

            // Assert
            Assert.NotNull(await _bookingService.GetRoomByNameAsync(newRoomDto.Name));
        }
    }
}
