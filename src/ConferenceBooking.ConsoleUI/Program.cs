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

            builder.Services.AddScoped<IApiClient, ApiClient>();
            builder.Services.AddTransient<IUserMenu, UserMenu>();

            IHost app = builder.Build();

            IUserMenu userMenu = app.Services.GetRequiredService<IUserMenu>();
            
            await userMenu.RunAsync();
        }
    }
}
