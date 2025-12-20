using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Models;
using ConferenceBooking.Data.Repositories;
using ConferenceBooking.DataTests.MockDb;

namespace ConferenceBooking.DataTests
{
    public class BookingRepositoryTests
    {
        private IBookingRepository _bookingRepository;
        private Room _room;

        public BookingRepositoryTests()
        {
            _bookingRepository = new BookingRepository(InMemoryDatabaseHelper
                .CreateApplicationDbContext());
            _room = new() { Name = "BookRepo test room" };
        }

        [Fact]
        public async Task Add_WhenCalledOnce_ShouldIncreaseGetAllCountByOne()
        {
            // Arrange
            Booking booking = new() { 
                RoomId = 1,
                ApplicationUserId = 0,
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now
            };
            int expectedCount = (await _bookingRepository.GetAllAsync()).Count() + 1;

            // Act
            await _bookingRepository.AddAsync(booking);

            // Assert
            Assert.Equal(expectedCount, (await _bookingRepository.GetAllAsync()).Count());
        }

        [Fact]
        public async Task GetById_Initially_ShouldReturnNull()
        {
            // Arrange
            Booking? result;

            // Act
            result = await _bookingRepository.GetByIdAsync(0);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetById_AfterAddingBooking_ShouldNotReturnNull()
        {
            // Arrange
            Booking? result;
            Booking booking = new()
            {
                RoomId = 1,
                ApplicationUserId = 0,
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now
            };
            await _bookingRepository.AddAsync(booking);
            IEnumerable<Booking> bookings = await _bookingRepository.GetAllAsync();
            int bookingId = bookings.First().Id;

            // Act
            result = await _bookingRepository.GetByIdAsync(bookingId);

            // Assert
            Assert.NotNull(result);
        }
    }
}
