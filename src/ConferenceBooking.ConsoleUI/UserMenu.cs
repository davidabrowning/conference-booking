using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;
using Newtonsoft.Json;

namespace ConferenceBooking.ConsoleUI
{
    public class UserMenu : IUserMenu
    {
        private readonly IApiClient _apiClient;

        public UserMenu(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task RunAsync()
        {
            bool userWantsToContinue = true;
            while (userWantsToContinue)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Display all users");
                Console.WriteLine("2. Add a user");
                Console.WriteLine("Q. Quit");

                string choice = Console.ReadLine() ?? string.Empty;

                switch(choice)
                {
                    case "1":
                        await DisplayUsersAsync();
                        break;
                    case "2":
                        await AddUserAsync();
                        break;
                    case "Q":
                    case "q":
                        userWantsToContinue = false;
                        break;
                    default:
                        Console.WriteLine("Unknown choice.");
                        break;
                }
            }
            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
        }

        private async Task DisplayUsersAsync()
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

        private async Task AddUserAsync()
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
    }
}
