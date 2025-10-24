using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket_Reservation_System_API.Interfaces;

namespace Ticket_Reservation_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _search;
        public SearchController(ISearchService search) => _search = search;

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string from, [FromQuery] string to, [FromQuery] DateTime journeyDate)
        {
            var result = await _search.SearchAvailableBusesAsync(from, to, journeyDate);
            return Ok(result);
        }
    }
}
