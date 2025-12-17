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
            _bookingService = new BookingService(fakeBookingRepository);
        }

        [Fact]
        public void AddBooking_WhenCalledOnce_ShouldIncreaseNumberOfBookingsByOne()
        {
            // Arrange
            int expectedCount = _bookingService.GetAllBookings().Count() + 1;
            RoomDto roomDto = new() { Name = "Room1" };
            _bookingService.AddRoom(roomDto);
            roomDto = _bookingService.GetRoomByName("Room1");
            BookingDto bookingDto = new() { RoomId = roomDto.Id };

            // Act
            _bookingService.AddBooking(bookingDto);

            // Assert
            Assert.Equal(expectedCount, _bookingService.GetAllBookings().Count());
        }

        [Fact]
        public void GetByRoom_WhenRoomHasTwoBookings_ShouldReturnTwoBookings()
        {
            // Arrange
            int expectedCount = 2;
            string room1Name = "Room1";
            string room2Name = "Room2";
            _bookingService.AddRoom(new RoomDto() { Name = room1Name });
            _bookingService.AddRoom(new RoomDto() { Name = room2Name });
            RoomDto roomDto1 = _bookingService.GetRoomByName(room1Name);
            RoomDto roomDto2 = _bookingService.GetRoomByName(room2Name);
            _bookingService.AddBooking(new BookingDto { RoomId = roomDto1.Id });
            _bookingService.AddBooking(new BookingDto { RoomId = roomDto1.Id });
            _bookingService.AddBooking(new BookingDto { RoomId = roomDto2.Id });

            // Act
            IEnumerable<BookingDto> bookingDtos = _bookingService.GetBookingsByRoom(roomDto1);

            // Assert
            Assert.Equal(expectedCount, bookingDtos.Count());
        }
    }
}
