using ConferenceBooking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceBooking.IntegrationTests
{
    public static class IntegrationTestHelper
    {
        public static ApplicationDbContext CreateContext()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            string testDbConnectionString = configuration.GetConnectionString("IntegrationTestsDatabase") ?? string.Empty;

            DbContextOptions<ApplicationDbContext> dbContextOptions
                = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(testDbConnectionString)
                .Options;
            ApplicationDbContext applicationDbContext = new(dbContextOptions);
            return applicationDbContext;
        }

        public static void UpdateDatabase(ApplicationDbContext applicationDbContext)
        {
            applicationDbContext.Database.Migrate();
        }
    }
}
