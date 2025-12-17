using ConferenceBooking.Data;
using Microsoft.EntityFrameworkCore;

namespace ConferenceBooking.DataTests.Mock
{
    public static class InMemoryDatabaseHelper
    {
        public static ApplicationDbContext CreateApplicationDbContext()
        {
            ApplicationDbContextCreator applicationDbContextCreator = new();
            return applicationDbContextCreator.ApplicationDbContext;
        }
    }

    public class ApplicationDbContextCreator : IDisposable
    {
        public ApplicationDbContext ApplicationDbContext { get; }
        public ApplicationDbContextCreator()
        {
            DbContextOptions<ApplicationDbContext> dbContextOptions
                = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestInMemoryDatabase")
                .Options;
            ApplicationDbContext = new ApplicationDbContext(dbContextOptions);
            ApplicationDbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            ApplicationDbContext.Database.EnsureDeleted();
        }
    }
}
