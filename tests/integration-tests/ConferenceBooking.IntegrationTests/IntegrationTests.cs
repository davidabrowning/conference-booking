using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Models;
using ConferenceBooking.Data;
using ConferenceBooking.Data.Repositories;
using ConferenceBooking.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConferenceBooking.IntegrationTests
{
    public class IntegrationTests
    {
        private readonly IBookingService _bookingService;

        public IntegrationTests()
        {
            ApplicationDbContext applicationDbContext = IntegrationTestHelper.CreateContext();
            IntegrationTestHelper.UpdateDatabase(applicationDbContext);
            IBookingRepository bookingRepository = new BookingRepository(applicationDbContext);
            IRoomRepository roomRepository = new RoomRepository(applicationDbContext);
            _bookingService = new BookingService(bookingRepository, roomRepository);
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
            string roomName = "Room" + DateTime.Now; 
            RoomDto roomDto = new() { Name = roomName };
            await _bookingService.AddRoomAsync(roomDto);
            RoomDto createdRoomDto = await _bookingService.GetRoomByNameAsync(roomName);
            BookingDto bookingDto = new()
            {
                RoomId = createdRoomDto.Id,
                StartDateTime = DateTime.Now.AddSeconds(1),
                EndDateTime = DateTime.Now.AddSeconds(2),
            };

            // Act
            await _bookingService.AddBookingAsync(bookingDto);

            // Assert
            Assert.Single<BookingDto>(await _bookingService.GetBookingsByRoomAsync(createdRoomDto));
        }
    }
}
