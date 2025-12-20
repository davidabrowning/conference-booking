using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Services.Services;
using ConferenceBooking.ServicesTests.FakeRepositories;

namespace ConferenceBooking.ServicesTests
{
    public class AppServiceTests
    {
        private readonly IAppService _appService;

        public AppServiceTests()
        {
            IApplicationUserRepository fakeApplicationUserRepository = new FakeApplicationUserRepository();
            IBookingRepository fakeBookingRepository = new FakeBookingRepository();
            IRoomRepository fakeRoomRepository = new FakeRoomRepository();
            _appService = new AppService(fakeApplicationUserRepository, fakeBookingRepository, fakeRoomRepository);
        }

        [Fact]
        public async Task AddBooking_WhenCalledOnce_ShouldIncreaseNumberOfBookingsByOne()
        {
            // Arrange
            int expectedCount = (await _appService.GetBookingsAsync()).Count() + 1;
            string roomName = "Room1";
            await _appService.AddRoomAsync(new RoomDto() { Name = roomName });
            RoomDto roomDto = await _appService.GetRoomByNameAsync("Room1");
            BookingDto bookingDto = new()
            {
                RoomId = roomDto.Id,
                ApplicationUserId = 0,
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now
            };

            // Act
            await _appService.AddBookingAsync(bookingDto);

            // Assert
            Assert.Equal(expectedCount, (await _appService.GetBookingsAsync()).Count());
        }

        [Fact]
        public async Task GetByRoom_WhenRoomHasTwoBookings_ShouldReturnTwoBookings()
        {
            // Arrange
            int expectedCount = 2;
            string room1Name = "Room1";
            string room2Name = "Room2";
            await _appService.AddRoomAsync(new RoomDto() { Name = room1Name });
            await _appService.AddRoomAsync(new RoomDto() { Name = room2Name });
            RoomDto roomDto1 = await _appService.GetRoomByNameAsync(room1Name);
            RoomDto roomDto2 = await _appService.GetRoomByNameAsync(room2Name);
            BookingDto bookingDto1 = new()
            {
                RoomId = roomDto1.Id,
                ApplicationUserId = 0,
                StartDateTime = DateTime.Now.AddHours(1),
                EndDateTime = DateTime.Now.AddHours(2)
            };
            BookingDto bookingDto2 = new()
            {
                RoomId = roomDto1.Id,
                ApplicationUserId = 0,
                StartDateTime = DateTime.Now.AddHours(3),
                EndDateTime = DateTime.Now.AddHours(4)
            };
            BookingDto bookingDto3 = new()
            {
                RoomId = roomDto2.Id,
                ApplicationUserId = 0,
                StartDateTime = DateTime.Now.AddHours(5),
                EndDateTime = DateTime.Now.AddHours(6)
            };
            await _appService.AddBookingAsync(bookingDto1);
            await _appService.AddBookingAsync(bookingDto2);
            await _appService.AddBookingAsync(bookingDto3);

            // Act
            IEnumerable<BookingDto> bookingDtos = await _appService.GetBookingsByRoomAsync(roomDto1);

            // Assert
            Assert.Equal(expectedCount, bookingDtos.Count());
        }

        [Fact]
        public async Task AddBooking_WhenEndIsBeforeStart_ShouldThrowException()
        {
            // Arrange
            string roomName = "TestRoom";
            await _appService.AddRoomAsync(new RoomDto() { Name = roomName });
            RoomDto roomDto = await _appService.GetRoomByNameAsync(roomName);
            BookingDto bookingDto1 = new()
            {
                RoomId = roomDto.Id,
                ApplicationUserId = 0,
                StartDateTime = DateTime.Now.AddHours(2),
                EndDateTime = DateTime.Now.AddHours(1)
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _appService.AddBookingAsync(bookingDto1));
        }

        [Fact]
        public async Task AddBooking_WhenRoomIsBooked_ShouldThrowException()
        {
            // Arrange
            string roomName = "TestRoom";
            await _appService.AddRoomAsync(new RoomDto() { Name = roomName });
            RoomDto roomDto = await _appService.GetRoomByNameAsync(roomName);
            DateTime startDateTime = DateTime.Now.AddHours(1);
            DateTime endDateTime = DateTime.Now.AddHours(2);
            BookingDto bookingDto1 = new()
            {
                RoomId = roomDto.Id,
                ApplicationUserId = 0,
                StartDateTime = startDateTime,
                EndDateTime = endDateTime
            };
            BookingDto bookingDto2 = new()
            {
                RoomId = roomDto.Id,
                ApplicationUserId = 0,
                StartDateTime = startDateTime.AddMinutes(5),
                EndDateTime = endDateTime
            };
            await _appService.AddBookingAsync(bookingDto1);

            // Act & assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () 
                => await _appService.AddBookingAsync(bookingDto2));
        }

        [Fact]
        public async Task AddRoom_WhenNameAlreadyExists_ThrowsException()
        {
            // Arrange
            string roomName = "Room" + DateTime.Now + Guid.NewGuid();
            await _appService.AddRoomAsync(new RoomDto() { Name = roomName });

            // Ac & assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () 
                => await _appService.AddRoomAsync(new RoomDto() { Name = roomName }));
        }

        [Fact]
        public async Task AddApplicationUser_WhenUsernameAlreadyExists_ThrowsException()
        {
            // Arrange
            string applicationUsername = "User" + DateTime.Now + Guid.NewGuid();
            await _appService.AddUserAsync(new ApplicationUserDto() { Username = applicationUsername });

            // Ac & assert
            await Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await _appService.AddUserAsync(new ApplicationUserDto() { Username = applicationUsername }));
        }
    }
}
