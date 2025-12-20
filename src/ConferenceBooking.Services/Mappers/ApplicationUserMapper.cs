using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Models;

namespace ConferenceBooking.Services.Mappers
{
    public static class ApplicationUserMapper
    {
        public static ApplicationUserDto ToDto(ApplicationUser model)
        {
            return new ApplicationUserDto()
            {
                Id = model.Id,
                Username = model.Username
            };
        }

        public static ApplicationUser ToModel(ApplicationUserDto dto)
        {
            return new ApplicationUser()
            {
                Id = dto.Id,
                Username = dto.Username
            };
        }
    }
}
