namespace ConferenceBooking.Core.Messages
{
    public class ErrorMessages
    {
        public const string BookingEndIsBeforeStart = "Error: Booking end time must be after start time.";
        public const string BookingIsNull = "Error: Booking is null.";
        public const string RoomIsBooked = "Error: Room is booked";
        public const string RoomIsNull = "Error: Room is null.";
    }
}
