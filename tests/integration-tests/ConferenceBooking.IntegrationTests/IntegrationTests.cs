using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Data;
using ConferenceBooking.Data.Repositories;
using ConferenceBooking.Services.Services;

namespace ConferenceBooking.IntegrationTests
{
    public class IntegrationTests
    {
        private readonly IAppService _bookingService;

        public IntegrationTests()
        {
            ApplicationDbContext applicationDbContext = IntegrationTestHelper.CreateContext();
            IntegrationTestHelper.UpdateDatabase(applicationDbContext);
            IApplicationUserRepository applicationUserRepository = new ApplicationUserRepository(applicationDbContext);
            IBookingRepository bookingRepository = new BookingRepository(applicationDbContext);
            IRoomRepository roomRepository = new RoomRepository(applicationDbContext);
            _bookingService = new BookingService(applicationUserRepository, bookingRepository, roomRepository);
        }

        [Fact]
        public async Task GetAllBookings_WhenCalled_IsNotNull()
        {
            // Arrange
            IEnumerable<BookingDto> bookingDtos;

            // Act
            bookingDtos = await _bookingService.GetAllBookingsAsync();

            // Assert
            Assert.NotNull(bookingDtos);
        }

        [Fact]
        public async Task Room_WhenSaved_ShouldSaveCorrectlyInDatabase()
        {
            // Arrange
            string roomName = "TestRoom" + DateTime.Now;
            RoomDto newRoomDto = new() { Name = roomName };

            // Act
            await _bookingService.AddRoomAsync(newRoomDto);

            // Assert
            Assert.NotNull(await _bookingService.GetRoomByNameAsync(newRoomDto.Name));
        }

        [Fact]
        public async Task Booking_WhenSaved_ShouldSaveCorrectlyToDatabase()
        {
            // Arrange
            string roomName = "Room" + DateTime.Now + Guid.NewGuid(); 
            RoomDto roomDto = new() { Name = roomName };
            await _bookingService.AddRoomAsync(roomDto);
            RoomDto createdRoomDto = await _bookingService.GetRoomByNameAsync(roomName);
            BookingDto bookingDto = new()
            {
                RoomId = createdRoomDto.Id,
                ApplicationUserId = 0,
                StartDateTime = DateTime.Now.AddSeconds(1),
                EndDateTime = DateTime.Now.AddSeconds(2),
            };

            // Act
            await _bookingService.AddBookingAsync(bookingDto);

            // Assert
            Assert.NotEmpty(await _bookingService.GetBookingsByRoomAsync(createdRoomDto));
        }

        [Fact]
        public async Task Booking_WhenExists_ShouldBeFetchedCorrectlyFromDatabase()
        {
            // Arrange
            string roomName = "Room" + DateTime.Now + Guid.NewGuid();
            RoomDto roomDto = new() { Name = roomName };
            await _bookingService.AddRoomAsync(roomDto);
            RoomDto createdRoomDto = await _bookingService.GetRoomByNameAsync(roomName);
            BookingDto bookingDto = new()
            {
                RoomId = createdRoomDto.Id,
                ApplicationUserId = 0,
                StartDateTime = DateTime.Now.AddSeconds(1),
                EndDateTime = DateTime.Now.AddSeconds(2),
            };
            await _bookingService.AddBookingAsync(bookingDto);
            IEnumerable<BookingDto> roomBookings = await _bookingService.GetBookingsByRoomAsync(createdRoomDto);
            BookingDto createdBookingDto = roomBookings.First();

            // Act & assert
            Assert.NotNull(await _bookingService.GetBookingByIdAsync(createdBookingDto.Id));
        }
    }
}
