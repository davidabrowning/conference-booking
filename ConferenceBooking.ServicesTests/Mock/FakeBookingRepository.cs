using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Models;

namespace ConferenceBooking.ServicesTests.Mock
{
    public class FakeBookingRepository : IBookingRepository
    {
        private static int NextId = 1;
        private List<Booking> _bookings = new();
        private List<Room> _rooms = new();
        public void AddBooking(Booking booking)
        {
            booking.Id = NextId++;
            _bookings.Add(booking);
        }

        public void AddRoom(Room room)
        {
            room.Id = NextId++;
            _rooms.Add(room);
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return _bookings.ToList();
        }

        public Room? GetRoom(int roomId)
        {
            return _rooms.Where(r => r.Id == roomId).FirstOrDefault();
        }

        public Room? GetRoomByName(string name)
        {
            return _rooms.Where(r => r.Name == name).FirstOrDefault();
        }
    }
}
