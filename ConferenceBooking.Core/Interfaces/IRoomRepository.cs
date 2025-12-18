using ConferenceBooking.Core.Models;

namespace ConferenceBooking.Core.Interfaces
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<Room?> GetByNameAsync(string name);
    }
}
