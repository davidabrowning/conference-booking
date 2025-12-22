namespace ConferenceBooking.Core.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public override string? ToString()
        {
            return Name;
        }
    }
}
