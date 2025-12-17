using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Models;
using ConferenceBooking.Data;
using ConferenceBooking.Data.Repositories;
using ConferenceBooking.DataTests.Mock;

namespace ConferenceBooking.DataTests
{
    public class BookingRepositoryTests
    {
        private IBookingRepository _bookingRepository;

        public BookingRepositoryTests()
        {
            ApplicationDbContext applicationDbContext = InMemoryDatabaseHelper.CreateApplicationDbContext();
            _bookingRepository = new BookingRepository(applicationDbContext);
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
