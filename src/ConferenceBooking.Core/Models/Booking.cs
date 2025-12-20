namespace ConferenceBooking.Core.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public required int RoomId { get; set; }
        public required DateTime StartDateTime { get; set; }
        public required DateTime EndDateTime { get; set; }
    }
}
