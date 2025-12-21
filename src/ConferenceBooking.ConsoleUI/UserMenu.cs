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
            Console.WriteLine("Press ENTER to exit.");
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
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Display all users");
            Console.WriteLine("2. Add a user");
            Console.WriteLine("Q. Quit program");

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
                    Console.WriteLine("Unknown choice.");
                    break;
            }
        }

        private async Task ShowUserList()
        {
            Console.WriteLine("User list:");
            try
            {
                foreach (ApplicationUserDto applicationUserDto in await _apiClient.GetUsersAsync())
                    Console.WriteLine($"{applicationUserDto.Id}. {applicationUserDto.Username}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task ShowCreateUserAsync()
        {
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine() ?? string.Empty;
            if (username == string.Empty)
            {
                Console.WriteLine("Error: Username cannot be empty.");
                return;
            }

            ApplicationUserDto applicationUserDto = new() { Username = username };
            try
            {
                await _apiClient.CreateUserAsync(applicationUserDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task ShowSelectUserAsync()
        {
            try
            {
                Console.WriteLine("Select your user (Q to quit):");
                foreach (ApplicationUserDto applicationUserDto in await _apiClient.GetUsersAsync())
                {
                    Console.WriteLine($"{applicationUserDto.Id}. {applicationUserDto.Username}");
                } 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
            }

        }
    }
}
