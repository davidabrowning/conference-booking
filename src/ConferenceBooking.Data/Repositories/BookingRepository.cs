using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceBooking.Data.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public BookingRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task AddAsync(Booking booking)
        {
            await _applicationDbContext.Bookings.AddAsync(booking);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _applicationDbContext.Bookings.ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Bookings.FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
