using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Models;

namespace ConferenceBooking.Services.Mappers
{
    public static class RoomMapper
    {
        public static RoomDto ToDto(Room room)
        {
            return new RoomDto() { 
                Id = room.Id,
                Name = room.Name 
            };
        }

        public static Room ToModel(RoomDto dto)
        {
            return new Room() { 
                Id = dto.Id,
                Name = dto.Name 
            };
        }
    }
}
