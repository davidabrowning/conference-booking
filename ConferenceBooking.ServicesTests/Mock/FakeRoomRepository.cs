using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Models;

namespace ConferenceBooking.ServicesTests.Mock
{
    public class FakeRoomRepository : IRoomRepository
    {
        private static int NextId = 1;
        private List<Room> _rooms = new();

        public void Add(Room room)
        {
            room.Id = NextId++;
            _rooms.Add(room);
        }

        public IEnumerable<Room> GetAll()
        {
            return _rooms.ToList();
        }

        public Room? GetById(int id)
        {
            return _rooms.FirstOrDefault(r => r.Id == id);
        }

        public Room? GetByName(string name)
        {
            return _rooms.FirstOrDefault(r => r.Name == name);
        }
    }
}
