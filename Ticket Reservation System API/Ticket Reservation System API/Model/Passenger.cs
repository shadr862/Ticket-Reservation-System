namespace Ticket_Reservation_System_API.Model
{
    public class Passenger
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
    }
}
