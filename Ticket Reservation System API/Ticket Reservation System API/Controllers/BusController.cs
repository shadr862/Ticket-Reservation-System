using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket_Reservation_System_API.Dtos;
using Ticket_Reservation_System_API.Interfaces;

namespace Ticket_Reservation_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusController : ControllerBase
    {
        private readonly IBusService _busService;

        public BusController(IBusService busService)
        {
            _busService = busService;
        }

        [HttpPost("add-full")]
        public async Task<IActionResult> AddFullBus([FromBody] AddFullBusDto dto)
        {
            var result = await _busService.AddFullBusAsync(dto);
            return CreatedAtAction(nameof(AddFullBus), new { id = result.BusId }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBuses()
        {
            var buses = _busService.GetAllBuses(); // get data from service
            return Ok(buses); // ✅ wrap in Ok() to return IActionResult
        }
    }
}
