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
            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
        }
    }
}
