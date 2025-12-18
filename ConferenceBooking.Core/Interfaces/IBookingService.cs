using ConferenceBooking.Core.Dtos;

namespace ConferenceBooking.Core.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
        Task AddBookingAsync(BookingDto bookingDto);
        Task<IEnumerable<BookingDto>> GetBookingsByRoomAsync(RoomDto roomDto1);
        Task AddRoomAsync(RoomDto roomDto);
        Task<RoomDto> GetRoomByNameAsync(string roomName);
    }
}
