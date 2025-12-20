using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Models;

namespace ConferenceBooking.Services.Mappers
{
    public static class BookingMapper
    {
        public static BookingDto ToDto(Booking booking)
        {
            BookingDto dto = new() { 
                Id = booking.Id,
                RoomId = booking.RoomId,
                StartDateTime = booking.StartDateTime,
                EndDateTime = booking.EndDateTime
            };
            return dto;
        }

        public static Booking ToModel(BookingDto dto)
        {
            Booking booking = new()
            {
                Id = dto.Id,
                RoomId = dto.RoomId,
                StartDateTime = dto.StartDateTime,
                EndDateTime = dto.EndDateTime
            };
            return booking;
        }
    }
}
