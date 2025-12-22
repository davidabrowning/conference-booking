using ConferenceBooking.Core.Dtos;

namespace ConferenceBooking.Core.Interfaces
{
    public interface IAppService
    {
        // ApplicationUser
        Task<IEnumerable<ApplicationUserDto>> GetUsersAsync();
        Task AddUserAsync(ApplicationUserDto applicationUserDto);
        Task<ApplicationUserDto> GetUserByIdAsync(int applicationUserId);
        Task<ApplicationUserDto> GetUserByUsernameAsync(string username);
        Task<bool> UsernameExistsAsync(string username);

        // Bookings
        Task<IEnumerable<BookingDto>> GetBookingsAsync();
        Task AddBookingAsync(BookingDto bookingDto);
        Task<BookingDto> GetBookingByIdAsync(int bookingId);
        Task<IEnumerable<BookingDto>> GetBookingsByRoomAsync(RoomDto roomDto1);

        // Rooms
        Task<IEnumerable<RoomDto>> GetRoomsAsync();
        Task AddRoomAsync(RoomDto roomDto);
        Task<RoomDto> GetRoomByIdAsync(int roomId);
        Task<RoomDto> GetRoomByNameAsync(string roomName);
        Task SeedRoomsAsync();
    }
}
