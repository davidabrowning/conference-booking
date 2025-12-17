using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Models;
using ConferenceBooking.Data;
using ConferenceBooking.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ConferenceBooking.DataTests
{
    public class BookingRepositoryTests : IDisposable
    {
        private ApplicationDbContext _applicationDbContext;
        private IBookingRepository _bookingRepository;

        public BookingRepositoryTests()
        {
            DbContextOptions<ApplicationDbContext> dbContextOptions
                = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestInMemoryDatabase")
                .Options;
            _applicationDbContext = new(dbContextOptions);
            _applicationDbContext.Database.EnsureCreated();
            _bookingRepository = new BookingRepository(_applicationDbContext);
        }

        public void Dispose()
        {
            _applicationDbContext.Database.EnsureDeleted();
        }

        [Fact]
        public void Add_WhenCalled_IncreasesGetAllCountByOne()
        {
            // Arrange
            Room room = new() { Name = "Room1" };
            Booking booking = new() { Room = room };
            int expectedCount = _bookingRepository.GetAll().Count() + 1;

            // Act
            _bookingRepository.Add(booking);

            // Assert
            Assert.Equal(expectedCount, _bookingRepository.GetAll().Count());
        }
    }
}
