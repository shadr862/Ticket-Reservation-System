namespace Ticket_Reservation_System_API.Model
{
    public class Seat
    {
        public Guid Id { get; set; }
        public Guid BusScheduleId { get; set; }
        public virtual BusSchedule BusSchedule { get; set; } = null!;
        public int Number { get; set; }
        public int Row { get; set; }
        public SeatStatus Status { get; set; } = SeatStatus.Available;
        public Guid? TicketId { get; set; }
        public virtual Ticket? Ticket { get; set; }
        public string SeatNumber { get; set; }
    }
}
