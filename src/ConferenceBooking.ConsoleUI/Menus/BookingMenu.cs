using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;

namespace ConferenceBooking.ConsoleUI.Menus
{
    public class BookingMenu
    {
        public bool UserWantsToContinue { get; private set; } = true;
        private readonly IApiClient _apiClient;
        private ApplicationUserDto? _currentUserDto;
        private IOutput _output;

        public BookingMenu(IApiClient apiClient, IOutput output)
        {
            _apiClient = apiClient;
            _output = output;
        }

        public async Task RunAsync(ApplicationUserDto currentUserDto)
        {
            _currentUserDto = currentUserDto;
            await ShowMainMenu();
        }

        private async Task ShowMainMenu()
        {
            _output.PrintPageTitle("Conference Booking System");
            _output.PrintLoggedInStatus(_currentUserDto);
            _output.PrintMenuTitle("Main menu");
            _output.PrintMenuItem("1. List users");
            _output.PrintMenuItem("2. Create booking");
            _output.PrintMenuItem("3. List bookings");
            _output.PrintMenuItem("4. Check room availability");
            _output.PrintMenuItem("Q. Quit program");
            _output.PrintPrompt("Your choice: ");
            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    await ShowUserListAsync();
                    break;
                case "2":
                    await CreateBookingAsync();
                    break;
                case "Q":
                case "q":
                    UserWantsToContinue = false;
                    break;
                default:
                    _output.PrintWarning("Unknown choice.");
                    _output.ConfirmContinue();
                    break;
            }
        }

        private async Task ShowUserListAsync()
        {
            _output.PrintMenuTitle("User list:");
            try
            {
                List<ApplicationUserDto> users = (await _apiClient.GetUsersAsync()).OrderBy(u => u.Username).ToList();
                foreach (ApplicationUserDto applicationUserDto in users)
                    _output.PrintMenuItem($"{applicationUserDto.Username}");
            }
            catch (Exception ex)
            {
                _output.PrintError(ex.Message);
            }
            _output.ConfirmContinue();
        }

        private async Task CreateBookingAsync()
        {
            // List<RoomDto> rooms = await _apiClient.GetRoomsAsync();
        }
    }
}
