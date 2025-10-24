namespace Ticket_Reservation_System_API.Dtos
{
    public class BookSeatInputDto
    {
        public Guid BusScheduleId { get; set; }
        public List<Guid> SeatIds { get; set; } = new();
        public string PassengerName { get; set; } = string.Empty;
        public string PassengerMobile { get; set; } = string.Empty;
        public string BoardingPoint { get; set; } = string.Empty;
        public string DroppingPoint { get; set; } = string.Empty;
    }
}
