using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceBooking.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {
        private readonly IAppService _appService;

        public ApplicationUsersController(IAppService appService)
        {
            _appService = appService;
        }

        // GET: api/<ApplicationUsersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUserDto>>> Get()
        {
            return Ok(await _appService.GetUsersAsync());
        }

        // GET api/<ApplicationUsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserDto>> Get(int id)
        {
            try
            {
                return Ok(await _appService.GetUserByIdAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ApplicationUsersController>
        [HttpPost]
        public async Task<ActionResult<ApplicationUserDto>> Post([FromBody] ApplicationUserDto applicationUserDto)
        {
            try
            {
                await _appService.AddUserAsync(applicationUserDto);
                ApplicationUserDto createdDto = await _appService.GetUserByUsernameAsync(applicationUserDto.Username);
                return Ok(createdDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
