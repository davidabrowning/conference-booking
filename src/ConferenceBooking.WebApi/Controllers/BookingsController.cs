using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceBooking.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IAppService _appService;

        public BookingsController(IAppService appService)
        {
            _appService = appService;
        }

        // GET: api/<BookingsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> Get()
        {
            return Ok(await _appService.GetBookingsAsync());
        }

        // GET api/<BookingsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> Get(int id)
        {
            try
            {
                return Ok(await _appService.GetBookingByIdAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<BookingsController>/5
        [HttpPost("checkavailability")]
        public async Task<ActionResult<bool>> Get([FromBody] BookingDto bookingDto)
        {
            try
            {
                return Ok(await _appService.IsAvailable(bookingDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<BookingsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] BookingDto bookingDto)
        {
            try
            {
                await _appService.AddBookingAsync(bookingDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
