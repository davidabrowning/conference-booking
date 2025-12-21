using ConferenceBooking.Core.Dtos;
using Newtonsoft.Json;

namespace ConferenceBooking.ConsoleUI
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient httpClient = new() { BaseAddress = new Uri("https://localhost:7180") };
            var response = await httpClient.GetAsync($"api/applicationusers");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            IEnumerable<ApplicationUserDto> applicationUserDtos = JsonConvert.DeserializeObject<IEnumerable<ApplicationUserDto>>(content);

            Console.WriteLine("User list:");
            foreach (ApplicationUserDto applicationUserDto in applicationUserDtos)
                Console.WriteLine($"{applicationUserDto.Id}. {applicationUserDto.Username}");
            Console.WriteLine("Press ENTER exit.");
            Console.ReadLine();
        }
    }
}
