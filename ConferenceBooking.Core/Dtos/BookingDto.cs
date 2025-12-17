namespace ConferenceBooking.Core.Dtos
{
    public class BookingDto
    {
        public int Id { get; set; }
        public required int RoomId { get; set; }
    }
}
