using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Models;
using ConferenceBooking.Services.Mappers;

namespace ConferenceBooking.Services.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
        }

        public async Task AddBookingAsync(BookingDto bookingDto)
        {
            await _bookingRepository.AddAsync(BookingMapper.ToModel(bookingDto));
        }

        public async Task AddRoomAsync(RoomDto roomDto)
        {
            Room room = new()
            {
                Name = roomDto.Name,
            };
            await _roomRepository.AddAsync(room);
        }

        public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
        {
            List<BookingDto> bookingDtos = new();
            IEnumerable<Booking> bookings = await _bookingRepository.GetAllAsync();
            foreach (Booking booking in bookings)
                bookingDtos.Add(BookingMapper.ToDto(booking));
            return bookingDtos;
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByRoomAsync(RoomDto roomDto1)
        {
            return (await GetAllBookingsAsync()).Where(b => b.RoomId == roomDto1.Id);
        }

        public async Task<RoomDto> GetRoomByNameAsync(string roomName)
        {
            Room room = await _roomRepository.GetByNameAsync(roomName);
            RoomDto roomDto = new() { 
                Id = room.Id,
                Name = room.Name 
            };
            return roomDto;
        }
    }
}
