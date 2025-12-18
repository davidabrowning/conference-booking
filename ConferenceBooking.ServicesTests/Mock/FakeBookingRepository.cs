using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Models;

namespace ConferenceBooking.ServicesTests.Mock
{
    public class FakeBookingRepository : IBookingRepository
    {
        private static int NextId = 1;
        private List<Booking> _bookings = new();
        public async Task AddAsync(Booking booking)
        {
            booking.Id = NextId++;
            _bookings.Add(booking);
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return _bookings.ToList();
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            return _bookings.Where(b => b.Id == id).FirstOrDefault();
        }
    }
}
