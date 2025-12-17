using ConferenceBooking.Core.Models;

namespace ConferenceBooking.Core.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public Room Room { get; set; } = new();
    }
}
