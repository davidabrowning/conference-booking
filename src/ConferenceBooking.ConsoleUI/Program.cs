using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConferenceBooking.ConsoleUI
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddTransient<UserMenu, UserMenu>();

            var app = builder.Build();

            var userMenu = app.Services.GetRequiredService<UserMenu>();
            await userMenu.RunAsync();
        }
    }
}
