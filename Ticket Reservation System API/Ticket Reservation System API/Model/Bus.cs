namespace Ticket_Reservation_System_API.Model
{
    public class Bus
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string BusName { get; set; } = string.Empty;
        public int TotalSeats { get; set; }
        public virtual ICollection<BusSchedule> BusSchedules { get; set; }
    }
}
