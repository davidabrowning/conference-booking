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

        public async Task<ApplicationUserDto> GetUserByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/applicationusers/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApplicationUserDto>(content)
                ?? throw new InvalidOperationException(ErrorMessages.ApplicationUserIsNull);
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

        public async Task CreateBookingAsync(BookingDto bookingDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/bookings", bookingDto);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsAsync()
        {
            var response = await _httpClient.GetAsync($"api/rooms");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<RoomDto>>(content)
                ?? new List<RoomDto>();
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsAsync()
        {
            var response = await _httpClient.GetAsync($"api/bookings");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<BookingDto>>(content)
                ?? new List<BookingDto>();
        }
    }
}
