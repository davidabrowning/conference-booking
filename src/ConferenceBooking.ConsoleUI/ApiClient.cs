using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Messages;
using Newtonsoft.Json;
using System.Net.Http.Json;

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

        public async Task<ApplicationUserDto> CreateUserAsync(ApplicationUserDto applicationUserDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/applicationusers", applicationUserDto);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            ApplicationUserDto? createdDto = JsonConvert.DeserializeObject<ApplicationUserDto>(content);
            if (createdDto == null)
                throw new InvalidOperationException(ErrorMessages.ApplicationUserIsNull);
            return createdDto;

        }
    }
}
