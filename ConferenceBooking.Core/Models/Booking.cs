using ConferenceBooking.Core.Models;

namespace ConferenceBooking.Core.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public required Room Room { get; set; }
    }
}
