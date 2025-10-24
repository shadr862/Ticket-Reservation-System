namespace Ticket_Reservation_System_API.Model
{
    public class Route
    {
        public Guid Id { get; set; }
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public double DistanceKm { get; set; }
       

    }
}
