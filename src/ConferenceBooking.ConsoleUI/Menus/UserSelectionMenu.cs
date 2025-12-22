using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Messages;

namespace ConferenceBooking.ConsoleUI.Menus
{
    public class UserSelectionMenu
    {
        public bool UserWantsToContinue { get; private set; } = true;
        private readonly IApiClient _apiClient;
        private readonly IInput _input;
        private readonly IOutput _output;

        public UserSelectionMenu(IApiClient apiClient, IInput input, IOutput output)
        {
            _apiClient = apiClient;
            _input = input;
            _output = output;
        }

        public async Task<ApplicationUserDto?> LoginAsync()
        {
            return await SelectUserAsync();
        }

        private async Task<ApplicationUserDto?> SelectUserAsync()
        {
            _output.PrintPageTitle("Conference Booking System Login");
            string username = _input.GetStringInput("Enter username:");

            IEnumerable<ApplicationUserDto> applicationUserDtos;
            try
            {
                applicationUserDtos = await _apiClient.GetUsersAsync();
            }
            catch (Exception ex)
            {
                _output.PrintError(ex.Message);
                return null;
            }

            ApplicationUserDto? existingUserDto = applicationUserDtos.FirstOrDefault(au => au.Username == username);
            if (existingUserDto != null)
            {
                _output.PrintSuccess($"Logged in as {existingUserDto.Username}");
                return existingUserDto;
            }

            try
            {
                ApplicationUserDto newUserDto = new() { Username = username };
                return await _apiClient.CreateUserAsync(newUserDto);
            }
            catch (Exception ex)
            {
                _output.PrintError(ex.Message);
                return null;
            }
        }
    }
}
