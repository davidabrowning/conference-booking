using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceBooking.Data.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ApplicationUserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task AddAsync(ApplicationUser entity)
        {
            await _applicationDbContext.ApplicationUsers.AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _applicationDbContext.ApplicationUsers.ToListAsync();
        }

        public async Task<ApplicationUser?> GetByIdAsync(int id)
        {
            return await _applicationDbContext.ApplicationUsers.FirstOrDefaultAsync(au => au.Id == id);
        }

        public async Task<ApplicationUser?> GetByUsername(string username)
        {
            return await _applicationDbContext.ApplicationUsers.FirstOrDefaultAsync(au => au.Username == username);
        }
    }
}
