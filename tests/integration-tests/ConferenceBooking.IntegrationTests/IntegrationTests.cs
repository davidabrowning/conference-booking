using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Models;
using ConferenceBooking.Data;
using ConferenceBooking.Data.Repositories;
using ConferenceBooking.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace ConferenceBooking.IntegrationTests
{
    public class IntegrationTests
    {
        private readonly IBookingService _bookingService;

        public IntegrationTests()
        {
            string testDbConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ConfBookingTest;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            DbContextOptions<ApplicationDbContext> dbContextOptions
                = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(testDbConnectionString)
                .Options;
            ApplicationDbContext applicationDbContext = new(dbContextOptions);
            ResetAndUpdateTestDatabase(applicationDbContext);
            IBookingRepository bookingRepository = new BookingRepository(applicationDbContext);
            IRoomRepository roomRepository = new RoomRepository(applicationDbContext);
            _bookingService = new BookingService(bookingRepository, roomRepository);
        }

        private void ResetAndUpdateTestDatabase(ApplicationDbContext applicationDbContext)
        {
            try { applicationDbContext.Database.EnsureDeleted(); }
            catch { }
            finally { applicationDbContext.Database.Migrate(); }
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
