namespace Ticket_Reservation_System_API.Model
{
    public class BusSchedule
    {
        public Guid Id { get; set; }
        public Guid BusId { get; set; }
        public virtual Bus Bus { get; set; } = null!;
        public Guid RouteId { get; set; }
        public virtual Route Route { get; set; } = null!;
        public DateTime JourneyDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    }
}
