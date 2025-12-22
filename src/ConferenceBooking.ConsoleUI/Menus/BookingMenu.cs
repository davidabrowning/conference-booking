using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;

namespace ConferenceBooking.ConsoleUI.Menus
{
    public class BookingMenu
    {
        public bool UserWantsToContinue { get; private set; } = true;
        private readonly IApiClient _apiClient;
        private ApplicationUserDto? _currentUserDto;

        public BookingMenu(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task RunAsync(ApplicationUserDto currentUserDto)
        {
            _currentUserDto = currentUserDto;
            await ShowMainMenu();
        }

        private async Task ShowMainMenu()
        {
            ConsolePrinter.PrintPageTitle("Conference Booking System");
            ConsolePrinter.PrintLoggedInStatus(_currentUserDto);
            ConsolePrinter.PrintMenuTitle("Main menu");
            ConsolePrinter.PrintMenuItem("1. List users");
            ConsolePrinter.PrintMenuItem("2. Create booking");
            ConsolePrinter.PrintMenuItem("3. List bookings");
            ConsolePrinter.PrintMenuItem("4. Check room availability");
            ConsolePrinter.PrintMenuItem("Q. Quit program");
            ConsolePrinter.PrintPrompt("Your choice: ");
            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    await ShowUserList();
                    break;
                case "Q":
                case "q":
                    UserWantsToContinue = false;
                    break;
                default:
                    ConsolePrinter.PrintWarning("Unknown choice.");
                    break;
            }
        }

        private async Task ShowUserList()
        {
            ConsolePrinter.PrintMenuTitle("User list:");
            try
            {
                List<ApplicationUserDto> users = (await _apiClient.GetUsersAsync()).OrderBy(u => u.Username).ToList();
                foreach (ApplicationUserDto applicationUserDto in users)
                    ConsolePrinter.PrintMenuItem($"{applicationUserDto.Username}");
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintError(ex.Message);
            }
            ConsolePrinter.ConfirmContinue();
        }
    }
}
