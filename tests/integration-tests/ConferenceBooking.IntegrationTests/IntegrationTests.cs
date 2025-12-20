using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Data;
using ConferenceBooking.Data.Repositories;
using ConferenceBooking.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace ConferenceBooking.IntegrationTests
{
    public class IntegrationTests
    {

        [Fact]
        public async Task GetAllBookings_WhenCalled_IsNotNull()
        {
            // Arrange
            string testDbConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ConfBookingTest;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            DbContextOptions<ApplicationDbContext> dbContextOptions
                = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(testDbConnectionString)
                .Options;
            ApplicationDbContext applicationDbContext = new(dbContextOptions);
            applicationDbContext.Database.Migrate();
            IBookingRepository bookingRepository = new BookingRepository(applicationDbContext);
            IRoomRepository roomRepository = new RoomRepository(applicationDbContext);
            IBookingService bookingService = new BookingService(bookingRepository, roomRepository);

            // Act
            var result = await bookingService.GetAllBookingsAsync();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Booking_WhenSaved_ShouldSaveCorrectlyInDatabase()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
