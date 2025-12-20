
using ConferenceBooking.Data;
using Microsoft.EntityFrameworkCore;

namespace ConferenceBooking.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("ProdDatabase") ?? string.Empty;
            builder.Services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(connectionString));
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
