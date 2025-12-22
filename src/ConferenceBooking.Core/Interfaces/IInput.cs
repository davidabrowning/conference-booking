namespace ConferenceBooking.Core.Interfaces
{
    public interface IInput
    {
        string GetStringInput(string prompt);
        DateTime GetDateTimeInput(string prompt);
        T GetSelectionFromList<T>(string listTitle, IEnumerable<T> list);
    }
}
