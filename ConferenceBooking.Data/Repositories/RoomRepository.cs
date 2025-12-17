using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceBooking.Data.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RoomRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task AddAsync(Room room)
        {
            await _applicationDbContext.Rooms.AddAsync(room);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _applicationDbContext.Rooms.ToListAsync();
        }

        public async Task<Room?> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Rooms.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Room?> GetByNameAsync(string name)
        {
            return await _applicationDbContext.Rooms.FirstOrDefaultAsync(r => r.Name == name);
        }
    }
}
