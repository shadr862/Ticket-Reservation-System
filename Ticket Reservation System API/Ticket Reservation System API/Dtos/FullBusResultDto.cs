namespace Ticket_Reservation_System_API.Dtos
{
    public class FullBusResultDto
    {
        public Guid BusId { get; set; }
        public string BusName { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public int TotalSeats { get; set; }

        public Guid RouteId { get; set; }
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;

        public Guid ScheduleId { get; set; }
        public DateTime JourneyDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public decimal Price { get; set; }

        public int SeatsCreated { get; set; }
    }
}
