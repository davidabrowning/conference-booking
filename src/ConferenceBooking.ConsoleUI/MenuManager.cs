using ConferenceBooking.ConsoleUI.Menus;
using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;

namespace ConferenceBooking.ConsoleUI
{
    public class MenuManager : IUserMenu
    {
        private readonly IApiClient _apiClient;
        private ApplicationUserDto? _selectedUser;
        private bool _userWantsToContinue;
        private UserSelectionMenu _userSelectionMenu;

        public MenuManager(IApiClient apiClient, UserSelectionMenu userSelectionMenu)
        {
            _apiClient = apiClient;
            _selectedUser = null;
            _userWantsToContinue = true;
            _userSelectionMenu = userSelectionMenu;
        }

        public async Task RunAsync()
        {
            while (_userWantsToContinue)
            {
                if (NoUserHasBeenSelected())
                    _selectedUser = await _userSelectionMenu.LoginAsync();
                else
                    await ShowMainMenu();
            }
            ConsolePrinter.PrintPrompt("Press ENTER to exit.");
            Console.ReadLine();
        }

        private bool NoUserHasBeenSelected()
        {
            return _selectedUser == null;
        }

        private async Task ShowMainMenu()
        {
            ConsolePrinter.PrintMenuTitle("Choose an option:");
            ConsolePrinter.PrintMenuItem("1. Display all users");
            ConsolePrinter.PrintMenuItem("Q. Quit program");

            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    await ShowUserList();
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
    }
}
