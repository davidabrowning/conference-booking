using ConferenceBooking.Data;
using Microsoft.EntityFrameworkCore;

namespace ConferenceBooking.DataTests.Mock
{
    public static class InMemoryDatabaseHelper
    {
        private static ApplicationDbContext contextSingleton;
        public static ApplicationDbContext CreateApplicationDbContext(string callingClassName)
        {
            ApplicationDbContextCreator applicationDbContextCreator = new(callingClassName);
            return applicationDbContextCreator.ApplicationDbContext;
        }
    }

    public class ApplicationDbContextCreator : IDisposable
    {
        public ApplicationDbContext ApplicationDbContext { get; }
        public ApplicationDbContextCreator(string callingClassName)
        {
            DbContextOptions<ApplicationDbContext> dbContextOptions
                = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestInMemoryDatabaseFor" + callingClassName)
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
