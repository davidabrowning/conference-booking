using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Services.Services;
using ConferenceBooking.ServicesTests.Mock;

namespace ConferenceBooking.ServicesTests
{
    public class BookingServiceTests
    {
        private readonly IBookingService _bookingService;

        public BookingServiceTests()
        {
            IBookingRepository fakeBookingRepository = new FakeBookingRepository();
            IRoomRepository fakeRoomRepository = new FakeRoomRepository();
            _bookingService = new BookingService(fakeBookingRepository, fakeRoomRepository);
        }

        [Fact]
        public async Task AddBooking_WhenCalledOnce_ShouldIncreaseNumberOfBookingsByOne()
        {
            // Arrange
            int expectedCount = (await _bookingService.GetAllBookingsAsync()).Count() + 1;
            string roomName = "Room1";
            await _bookingService.AddRoomAsync(new RoomDto() { Name = roomName });
            RoomDto roomDto = await _bookingService.GetRoomByNameAsync("Room1");
            BookingDto bookingDto = new() { RoomId = roomDto.Id };

            // Act
            await _bookingService.AddBookingAsync(bookingDto);

            // Assert
            Assert.Equal(expectedCount, (await _bookingService.GetAllBookingsAsync()).Count());
        }

        [Fact]
        public async Task GetByRoom_WhenRoomHasTwoBookings_ShouldReturnTwoBookings()
        {
            // Arrange
            int expectedCount = 2;
            string room1Name = "Room1";
            string room2Name = "Room2";
            await _bookingService.AddRoomAsync(new RoomDto() { Name = room1Name });
            await _bookingService.AddRoomAsync(new RoomDto() { Name = room2Name });
            RoomDto roomDto1 = await _bookingService.GetRoomByNameAsync(room1Name);
            RoomDto roomDto2 = await _bookingService.GetRoomByNameAsync(room2Name);
            for (int i = 0; i < expectedCount; i++)
                await _bookingService.AddBookingAsync(new BookingDto { RoomId = roomDto1.Id });
            await _bookingService.AddBookingAsync(new BookingDto { RoomId = roomDto2.Id });

            // Act
            IEnumerable<BookingDto> bookingDtos = await _bookingService.GetBookingsByRoomAsync(roomDto1);

            // Assert
            Assert.Equal(expectedCount, bookingDtos.Count());
        }
    }
}
