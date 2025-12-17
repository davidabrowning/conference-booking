using ConferenceBooking.Core.Models;

namespace ConferenceBooking.Core.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Room? GetRoom(int roomId);
        Room? GetRoomByName(string name);
        void AddRoom(Room room);
    }
}
