using ConferenceBooking.Core.Dtos;

namespace ConferenceBooking.Core.Interfaces
{
    public interface IOutput
    {
        void PrintPageTitle(string message);
        void PrintListTitle(string message);
        void PrintListItem(string message);
        void PrintSuccess(string message);
        void PrintError(string message);
        void PrintWarning(string message);
        void PrintInfo(string message);
        void PrintPrompt(string message);
        void PrintLoggedInStatus(ApplicationUserDto? currentUser);
        void ConfirmContinue();
    }
}
