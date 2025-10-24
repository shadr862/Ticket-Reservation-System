namespace Ticket_Reservation_System_API.Dtos
{
    public class SeatPlanDto
    {
        public Guid BusScheduleId { get; set; }
        public List<SeatDto> Seats { get; set; } = new();
    }
}
