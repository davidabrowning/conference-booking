using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Models;

namespace ConferenceBooking.ServicesTests.Mock
{
    public class FakeBookingRepository : IBookingRepository
    {
        private static int NextId = 1;
        private List<Booking> _bookings = new();
        public void Add(Booking booking)
        {
            booking.Id = NextId++;
            _bookings.Add(booking);
        }

        public IEnumerable<Booking> GetAll()
        {
            return _bookings.ToList();
        }

        public Booking GetById(int id)
        {
            return _bookings.Where(b => b.Id == id).FirstOrDefault();
        }
    }
}
