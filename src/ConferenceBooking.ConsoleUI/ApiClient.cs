using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;
using Newtonsoft.Json;

namespace ConferenceBooking.ConsoleUI
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        public ApiClient()
        {
               _httpClient = new() { BaseAddress = new Uri("https://localhost:7180") };
        }
        public async Task<IEnumerable<ApplicationUserDto>> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync($"api/applicationusers");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            IEnumerable<ApplicationUserDto>? applicationUserDtos = JsonConvert.DeserializeObject<IEnumerable<ApplicationUserDto>>(content);
            return applicationUserDtos ?? new List<ApplicationUserDto>();
        }
    }
}
