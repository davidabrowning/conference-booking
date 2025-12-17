using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Models;

namespace ConferenceBooking.Data.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private ApplicationDbContext _applicationDbContext;
        public BookingRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Add(Booking booking)
        {
            _applicationDbContext.Bookings.Add(booking);
            _applicationDbContext.SaveChanges();
        }

        public IEnumerable<Booking> GetAll()
        {
            return _applicationDbContext.Bookings.ToList();
        }

        public Booking? GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
