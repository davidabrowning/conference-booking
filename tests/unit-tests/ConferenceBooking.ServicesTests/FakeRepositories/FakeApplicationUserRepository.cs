using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Models;

namespace ConferenceBooking.ServicesTests.FakeRepositories
{
    public class FakeApplicationUserRepository : IApplicationUserRepository
    {
        private static int NextId = 1;
        private List<ApplicationUser> _applicationUsers = new();

        public async Task AddAsync(ApplicationUser applicationUser)
        {
            applicationUser.Id = NextId++;
            _applicationUsers.Add(applicationUser);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return _applicationUsers.ToList();
        }

        public async Task<ApplicationUser?> GetByIdAsync(int id)
        {
            return _applicationUsers.FirstOrDefault(au => au.Id == id);
        }

        public async Task<ApplicationUser?> GetByUsername(string username)
        {
            return _applicationUsers.FirstOrDefault(au => au.Username == username);
        }
    }
}
