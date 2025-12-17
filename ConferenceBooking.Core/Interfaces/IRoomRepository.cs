using ConferenceBooking.Core.Models;

namespace ConferenceBooking.Core.Interfaces
{
    public interface IRoomRepository : IRepository<Room>
    {
        Room? GetByName(string name);
    }
}
