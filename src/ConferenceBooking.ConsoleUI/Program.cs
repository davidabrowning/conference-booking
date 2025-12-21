using ConferenceBooking.ConsoleUI.Menus;
using ConferenceBooking.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConferenceBooking.ConsoleUI
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddTransient<IUserMenu, MenuManager>();
            builder.Services.AddTransient<UserSelectionMenu>();
            builder.Services.AddTransient<BookingMenu>();
            builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7180");
            });

            IHost app = builder.Build();

            IUserMenu userMenu = app.Services.GetRequiredService<IUserMenu>();
            
            await userMenu.RunAsync();
        }
    }
}
