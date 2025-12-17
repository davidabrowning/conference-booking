using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Models;

namespace ConferenceBooking.Services.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public void AddBooking(BookingDto bookingDto)
        {
            Booking booking = new()
            {
                Room = _bookingRepository.GetRoom(bookingDto.RoomId),
            };
            _bookingRepository.AddBooking(booking);
        }

        public void AddRoom(RoomDto roomDto)
        {
            Room room = new()
            {
                Name = roomDto.Name,
            };
            _bookingRepository.AddRoom(room);
        }

        public IEnumerable<BookingDto> GetAllBookings()
        {
            List<BookingDto> bookingDtos = new();
            IEnumerable<Booking> bookings = _bookingRepository.GetAllBookings();
            foreach (Booking booking in bookings)
                bookingDtos.Add(new BookingDto() { Id = booking.Id, RoomId = booking.Room.Id });
            return bookingDtos;
        }

        public IEnumerable<BookingDto> GetBookingsByRoom(RoomDto roomDto1)
        {
            return GetAllBookings().Where(b => b.RoomId == roomDto1.Id);
        }

        public RoomDto GetRoomByName(string roomName)
        {
            Room room = _bookingRepository.GetRoomByName(roomName);
            RoomDto roomDto = new() { 
                Id = room.Id,
                Name = room.Name 
            };
            return roomDto;
        }
    }
}
