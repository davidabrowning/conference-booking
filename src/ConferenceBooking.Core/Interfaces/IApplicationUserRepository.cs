using ConferenceBooking.Core.Models;

namespace ConferenceBooking.Core.Interfaces
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser?> GetByUsername(string username);
    }
}
