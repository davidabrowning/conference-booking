using ConferenceBooking.Core.Dtos;

namespace ConferenceBooking.Core.Interfaces
{
    public interface IBookingService
    {
        // ApplicationUser
        Task<IEnumerable<ApplicationUserDto>> GetAllApplicationUsersAsync();
        Task AddApplicationUserAsync(ApplicationUserDto applicationUserDto);
        Task<ApplicationUserDto> GetApplicationUserById(int applicationUserId);
        Task<ApplicationUserDto> GetApplicationUserByUsername(string username);

        // Bookings
        Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
        Task AddBookingAsync(BookingDto bookingDto);
        Task<BookingDto> GetBookingByIdAsync(int bookingId);
        Task<IEnumerable<BookingDto>> GetBookingsByRoomAsync(RoomDto roomDto1);

        // Rooms
        Task<IEnumerable<RoomDto>> GetAllRoomsAsync();
        Task AddRoomAsync(RoomDto roomDto);
        Task<RoomDto> GetRoomById(int roomId);
        Task<RoomDto> GetRoomByNameAsync(string roomName);
    }
}
