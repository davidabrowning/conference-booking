using ConferenceBooking.Core.Dtos;

namespace ConferenceBooking.Core.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<BookingDto> GetAllBookings();
        void AddBooking(BookingDto bookingDto);
        IEnumerable<BookingDto> GetBookingsByRoom(RoomDto roomDto1);
        void AddRoom(RoomDto roomDto);
        RoomDto GetRoomByName(string roomName);
    }
}
