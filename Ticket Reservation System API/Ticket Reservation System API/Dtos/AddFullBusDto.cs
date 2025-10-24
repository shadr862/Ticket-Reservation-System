namespace Ticket_Reservation_System_API.Dtos
{
    public class AddFullBusDto
    {
        // Bus information
        public string CompanyName { get; set; } = string.Empty;
        public string BusName { get; set; } = string.Empty;
        public int TotalSeats { get; set; }

        // Route information
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;

        // Schedule information
        public DateTime JourneyDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public decimal Price { get; set; }
    }
}
