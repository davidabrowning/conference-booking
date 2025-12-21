namespace ConferenceBooking.Core.Messages
{
    public class ErrorMessages
    {
        public const string ApplicationUserIsNull = "Error: Application user is null.";
        public const string ApplicationUsernameAlreadyExists = "Error: Application username already exists.";
        public const string BookingEndIsBeforeStart = "Error: Booking end time must be after start time.";
        public const string BookingIsNull = "Error: Booking is null.";
        public const string RoomIsBooked = "Error: Room is booked";
        public const string RoomIsNull = "Error: Room is null.";
        public const string RoomNameAlreadyExists = "Error: Room name already exists.";
        public const string UsernameIsBlank = "Error: Username is blank.";
    }
}
