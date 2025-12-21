using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;

namespace ConferenceBooking.ConsoleUI
{
    public class UserMenu : IUserMenu
    {
        private readonly IApiClient _apiClient;
        private ApplicationUserDto? _selectedUser;
        private bool _userWantsToContinue;

        public UserMenu(IApiClient apiClient)
        {
            _apiClient = apiClient;
            _selectedUser = null;
            _userWantsToContinue = true;
        }

        public async Task RunAsync()
        {
            while (_userWantsToContinue)
            {
                if (await NoUsersHaveBeenCreated())
                    await ShowCreateUserAsync();
                else if (NoUserHasBeenSelected())
                    await ShowSelectUserAsync();
                else
                    await ShowMainMenu();
            }
            ConsolePrinter.PrintPrompt("Press ENTER to exit.");
            Console.ReadLine();
        }

        private async Task<bool> NoUsersHaveBeenCreated()
        {
            return (await _apiClient.GetUsersAsync()).Count() == 0;
        }

        private bool NoUserHasBeenSelected()
        {
            return _selectedUser == null;
        }

        private async Task ShowMainMenu()
        {
            ConsolePrinter.PrintMenuTitle("Choose an option:");
            ConsolePrinter.PrintMenuItem("1. Display all users");
            ConsolePrinter.PrintMenuItem("2. Add a user");
            ConsolePrinter.PrintMenuItem("Q. Quit program");

            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    await ShowUserList();
                    break;
                case "2":
                    await ShowCreateUserAsync();
                    break;
                case "Q":
                case "q":
                    _userWantsToContinue = false;
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
                foreach (ApplicationUserDto applicationUserDto in await _apiClient.GetUsersAsync())
                    ConsolePrinter.PrintMenuItem($"{applicationUserDto.Id}. {applicationUserDto.Username}");
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintError(ex.Message);
            }
        }

        private async Task ShowCreateUserAsync()
        {
            ConsolePrinter.PrintPrompt("Enter username:");
            string username = Console.ReadLine() ?? string.Empty;
            if (username == string.Empty)
            {
                ConsolePrinter.PrintError("Error: Username cannot be empty.");
                return;
            }

            ApplicationUserDto applicationUserDto = new() { Username = username };
            try
            {
                await _apiClient.CreateUserAsync(applicationUserDto);
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintError(ex.Message);
            }
        }

        private async Task ShowSelectUserAsync()
        {
            try
            {
                ConsolePrinter.PrintMenuTitle("Select your user (Q to quit):");
                foreach (ApplicationUserDto applicationUserDto in await _apiClient.GetUsersAsync())
                {
                    ConsolePrinter.PrintMenuItem($"{applicationUserDto.Id}. {applicationUserDto.Username}");
                } 
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintError(ex.Message);
            }
            string choice = Console.ReadLine() ?? string.Empty;
            if (choice.ToUpper() == "Q")
            {
                _userWantsToContinue = false;
                return;
            }
            try
            {
                int choiceId = Convert.ToInt32(choice);
                ApplicationUserDto applicationUserDto = await _apiClient.GetUserByIdAsync(choiceId);
                _selectedUser = applicationUserDto;
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintError(ex.Message);
            }

        }
    }
}
