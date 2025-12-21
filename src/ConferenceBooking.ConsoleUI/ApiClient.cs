using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;
using Newtonsoft.Json;

namespace ConferenceBooking.ConsoleUI
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        public ApiClient(HttpClient httpClient)
        {
               _httpClient = httpClient;
        }
        public async Task<IEnumerable<ApplicationUserDto>> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync($"api/applicationusers");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<ApplicationUserDto>>(content) 
                ?? new List<ApplicationUserDto>();
        }
    }
}
