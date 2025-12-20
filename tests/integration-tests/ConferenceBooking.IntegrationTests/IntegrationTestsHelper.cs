using ConferenceBooking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConferenceBooking.IntegrationTests
{
    public static class IntegrationTestsHelper
    {
        public static ApplicationDbContext CreateDbContextForIntegrationTests()
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

        public static void ResetAndUpdateTestDatabase(ApplicationDbContext applicationDbContext)
        {
            try { applicationDbContext.Database.EnsureDeleted(); }
            catch { }
            finally { applicationDbContext.Database.Migrate(); }
        }
    }
}
