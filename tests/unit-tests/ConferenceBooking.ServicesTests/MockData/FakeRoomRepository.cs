using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Models;

namespace ConferenceBooking.ServicesTests.MockData
{
    public class FakeRoomRepository : IRoomRepository
    {
        private static int NextId = 1;
        private List<Room> _rooms = new();

        public async Task AddAsync(Room room)
        {
            room.Id = NextId++;
            _rooms.Add(room);
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return _rooms.ToList();
        }

        public async Task<Room?> GetByIdAsync(int id)
        {
            return _rooms.FirstOrDefault(r => r.Id == id);
        }

        public async Task<Room?> GetByNameAsync(string name)
        {
            return _rooms.FirstOrDefault(r => r.Name == name);
        }
    }
}
