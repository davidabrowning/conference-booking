using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;

namespace ConferenceBooking.ConsoleUI.Menus
{
    public class BookingMenu
    {
        public bool UserWantsToContinue { get; private set; } = true;
        private readonly IApiClient _apiClient;
        private ApplicationUserDto? _currentUserDto;
        private IInput _input;
        private IOutput _output;

        public BookingMenu(IApiClient apiClient, IInput input, IOutput output)
        {
            _apiClient = apiClient;
            _input = input;
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
            _output.PrintListTitle("Main menu");
            _output.PrintListItem("1. List users");
            _output.PrintListItem("2. Create booking");
            _output.PrintListItem("3. List bookings");
            _output.PrintListItem("4. Check room availability");
            _output.PrintListItem("Q. Quit program");

            string choice = _input.GetStringInput("Your choice:");

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
            _output.PrintListTitle("User list:");
            try
            {
                List<ApplicationUserDto> users = (await _apiClient.GetUsersAsync()).OrderBy(u => u.Username).ToList();
                foreach (ApplicationUserDto applicationUserDto in users)
                    _output.PrintListItem($"{applicationUserDto.Username}");
            }
            catch (Exception ex)
            {
                _output.PrintError(ex.Message);
            }
            _output.ConfirmContinue();
        }

        private async Task CreateBookingAsync()
        {
            _output.PrintPageTitle("Create booking");
            try
            {
                IEnumerable<RoomDto> rooms = await _apiClient.GetRoomsAsync();
                RoomDto roomDto = _input.GetSelectionFromList<RoomDto>("Rooms", rooms);
                _output.PrintSuccess($"Selected {roomDto.Name}");
                DateTime startDateTime = _input.GetDateTimeInput("Start time (YYYY-MM-DD HH:MM):");
                _output.PrintSuccess($"Selected {startDateTime}");
                DateTime endDateTime = _input.GetDateTimeInput("End time (YYYY-MM-DD HH:MM):");
                _output.PrintSuccess($"Selected {endDateTime}");

                BookingDto bookingDto = new()
                {
                    RoomId = roomDto.Id,
                    ApplicationUserId = _currentUserDto.Id,
                    StartDateTime = startDateTime,
                    EndDateTime = endDateTime
                };
                await _apiClient.CreateBookingAsync(bookingDto);
                _output.PrintSuccess($"Created booking:\nRoom: {roomDto.Name}\nUser: {_currentUserDto.Username}\nStart: {startDateTime}\nEnd: {endDateTime}");
            }
            catch (Exception ex)
            {
                _output.PrintError(ex.Message);
            }
            _output.ConfirmContinue();
        }
    }
}
