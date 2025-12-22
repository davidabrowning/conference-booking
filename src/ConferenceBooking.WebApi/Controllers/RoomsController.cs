using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceBooking.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IAppService _appService;

        public RoomsController(IAppService appService)
        {
            _appService = appService;
        }

        // GET: api/<RoomsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDto>>> Get()
        {
            if ((await _appService.GetRoomsAsync()).Count() == 0)
                await _appService.SeedRoomsAsync();
            return Ok(await _appService.GetRoomsAsync());
        }

        // GET api/<RoomsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto>> Get(int id)
        {
            try
            {
                return Ok(await _appService.GetRoomByIdAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
