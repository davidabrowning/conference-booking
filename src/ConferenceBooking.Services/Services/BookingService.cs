using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Messages;
using ConferenceBooking.Core.Models;
using ConferenceBooking.Services.Mappers;

namespace ConferenceBooking.Services.Services
{
    public class BookingService : IBookingService
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingService(IApplicationUserRepository applicationUserRepository, IBookingRepository bookingRepository, IRoomRepository roomRepository)
        {
            _applicationUserRepository = applicationUserRepository;
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
        }

        public async Task<bool> IsAvailable(BookingDto bookingDto)
        {
            Room? room = await _roomRepository.GetByIdAsync(bookingDto.RoomId);
            if (room == null)
                throw new InvalidOperationException(ErrorMessages.RoomIsNull);
            return !(await _bookingRepository.GetAllAsync())
                .Where(b => b.RoomId == room.Id)
                .Where(b => b.StartDateTime < bookingDto.EndDateTime)
                .Where(b => b.EndDateTime > bookingDto.StartDateTime)
                .Any();
        }

        public async Task AddBookingAsync(BookingDto bookingDto)
        {
            if (bookingDto.EndDateTime <= bookingDto.StartDateTime)
                throw new InvalidOperationException(ErrorMessages.BookingEndIsBeforeStart);
            if (!await IsAvailable(bookingDto))
                throw new InvalidOperationException(ErrorMessages.RoomIsBooked);
            await _bookingRepository.AddAsync(BookingMapper.ToModel(bookingDto));
        }

        public async Task AddRoomAsync(RoomDto roomDto)
        {
            Room? preexistingRoom = await _roomRepository.GetByNameAsync(roomDto.Name);
            if (preexistingRoom != null)
                throw new InvalidOperationException(ErrorMessages.RoomNameAlreadyExists);
            await _roomRepository.AddAsync(RoomMapper.ToModel(roomDto));
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
            Room? room = await _roomRepository.GetByNameAsync(roomName);
            if (room == null)
                throw new InvalidOperationException(ErrorMessages.RoomIsNull);
            return RoomMapper.ToDto(room);
        }

        public async Task<BookingDto> GetBookingByIdAsync(int bookingId)
        {
            Booking? booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null)
                throw new InvalidOperationException(ErrorMessages.BookingIsNull);
            return BookingMapper.ToDto(booking);
        }

        public async Task<IEnumerable<ApplicationUserDto>> GetAllApplicationUsersAsync()
        {
            List<ApplicationUserDto> applicationUserDtos = new();
            IEnumerable<ApplicationUser> applicationUsers = await _applicationUserRepository.GetAllAsync();
            foreach (ApplicationUser applicationUser in applicationUsers)
                applicationUserDtos.Add(ApplicationUserMapper.ToDto(applicationUser));
            return applicationUserDtos;
        }

        public async Task AddApplicationUserAsync(ApplicationUserDto applicationUserDto)
        {
            ApplicationUser? applicationUser = await _applicationUserRepository.GetByUsername(applicationUserDto.Username);
            if (applicationUser != null)
                throw new InvalidOperationException(ErrorMessages.ApplicationUsernameAlreadyExists);
            await _applicationUserRepository.AddAsync(ApplicationUserMapper.ToModel(applicationUserDto));
        }

        public async Task<ApplicationUserDto> GetApplicationUserById(int applicationUserId)
        {
            ApplicationUser? applicationUser = await _applicationUserRepository.GetByIdAsync(applicationUserId);
            if (applicationUser == null)
                throw new InvalidOperationException(ErrorMessages.ApplicationUserIsNull);
            return ApplicationUserMapper.ToDto(applicationUser);
        }

        public async Task<ApplicationUserDto> GetApplicationUserByUsername(string username)
        {
            ApplicationUser? applicationUser = await _applicationUserRepository.GetByUsername(username);
            if (applicationUser == null)
                throw new InvalidOperationException(ErrorMessages.ApplicationUserIsNull);
            return ApplicationUserMapper.ToDto(applicationUser);
        }

        public async Task<IEnumerable<RoomDto>> GetAllRoomsAsync()
        {
            List<RoomDto> roomDtos = new();
            IEnumerable<Room> rooms = await _roomRepository.GetAllAsync();
            foreach (Room room in rooms)
                roomDtos.Add(RoomMapper.ToDto(room));
            return roomDtos;
        }

        public async Task<RoomDto> GetRoomById(int roomId)
        {
            Room? room = await _roomRepository.GetByIdAsync(roomId);
            if (room == null)
                throw new InvalidOperationException(ErrorMessages.RoomIsNull);
            return RoomMapper.ToDto(room);
        }
    }
}
