using ConferenceBooking.Core.Dtos;

namespace ConferenceBooking.Core.Interfaces
{
    public interface IApiClient
    {
        // Booking
        Task<IEnumerable<BookingDto>> GetBookingsAsync();
        Task CreateBookingAsync(BookingDto bookingDto);
        Task<bool> CheckAvailability(BookingDto bookingDto);

        // Room
        Task<IEnumerable<RoomDto>> GetRoomsAsync();

        // User
        Task<IEnumerable<ApplicationUserDto>> GetUsersAsync();
        Task<ApplicationUserDto> GetUserByIdAsync(int userId);
        Task<ApplicationUserDto> CreateUserAsync(ApplicationUserDto applicationUserDto);
    }
}
