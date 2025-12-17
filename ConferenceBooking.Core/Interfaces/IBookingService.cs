using ConferenceBooking.Core.Dtos;

namespace ConferenceBooking.Core.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
        Task AddBooking(BookingDto bookingDto);
        Task<IEnumerable<BookingDto>> GetBookingsByRoom(RoomDto roomDto1);
        Task AddRoom(RoomDto roomDto);
        Task<RoomDto> GetRoomByName(string roomName);
    }
}
