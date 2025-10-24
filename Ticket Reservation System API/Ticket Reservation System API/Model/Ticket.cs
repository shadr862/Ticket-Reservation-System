namespace Ticket_Reservation_System_API.Model
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public Guid BusScheduleId { get; set; }
        public virtual BusSchedule BusSchedule { get; set; } = null!;
        public DateTime BookingTime { get; set; }
        public Guid PassengerId { get; set; }
        public virtual Passenger Passenger { get; set; } = null!;
        public virtual ICollection<Seat> Seats { get; set; } 
        public decimal TotalAmount { get; set; }
        public string BoardingPoint { get; set; } = string.Empty;
        public string DroppingPoint { get; set; } = string.Empty;
    }
}
