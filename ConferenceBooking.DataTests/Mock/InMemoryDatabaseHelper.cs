using ConferenceBooking.Data;
using Microsoft.EntityFrameworkCore;

namespace ConferenceBooking.DataTests.Mock
{
    public static class InMemoryDatabaseHelper
    {
        public static ApplicationDbContext CreateApplicationDbContext()
        {
            DbContextOptions<ApplicationDbContext> dbContextOptions
                = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestInMemoryDatabase")
                .Options;
            return new ApplicationDbContext(dbContextOptions);
        }
    }
}
