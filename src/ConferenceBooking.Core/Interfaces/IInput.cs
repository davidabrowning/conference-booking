namespace ConferenceBooking.Core.Interfaces
{
    public interface IInput
    {
        string GetStringInput(string prompt);
        T GetSelectionFromList<T>(string listTitle, IEnumerable<T> list);
    }
}
