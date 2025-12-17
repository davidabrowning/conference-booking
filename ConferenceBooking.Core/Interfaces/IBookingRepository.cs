using ConferenceBooking.Core.Models;

namespace ConferenceBooking.Core.Interfaces
{
    public interface IBookingRepository
    {
        void AddBooking(Booking booking);
        IEnumerable<Booking> GetAllBookings();
        Room? GetRoom(int roomId);
        Room? GetRoomByName(string name);
        void AddRoom(Room room);
    }
}
