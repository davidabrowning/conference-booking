using ConferenceBooking.Core.Dtos;

namespace ConferenceBooking.Core.Interfaces
{
    public interface IApiClient
    {
        Task<IEnumerable<ApplicationUserDto>> GetUsersAsync();
    }
}
