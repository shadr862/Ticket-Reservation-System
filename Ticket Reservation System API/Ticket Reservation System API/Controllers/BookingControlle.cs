using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket_Reservation_System_API.Dtos;
using Ticket_Reservation_System_API.Interfaces;

namespace Ticket_Reservation_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController: ControllerBase
    {
        private readonly IBookingService _booking;
        public BookingController(IBookingService booking)
        {
            _booking = booking;
        }

        [HttpGet("{busScheduleId}/seatplan")]
        public async Task<IActionResult> GetSeatPlan(Guid busScheduleId)
        {
            var plan = await _booking.GetSeatPlanAsync(busScheduleId);
            if (plan == null) return NotFound();
            return Ok(plan);
        }

        [HttpPost]
        public async Task<IActionResult> Book([FromBody] BookSeatInputDto input)
        {
            var res = await _booking.BookSeatAsync(input);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }
    }
}
