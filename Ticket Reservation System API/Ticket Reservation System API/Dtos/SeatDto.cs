using Ticket_Reservation_System_API.Model;

namespace Ticket_Reservation_System_API.Dtos
{
    public class SeatDto
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public int Row { get; set; }
        public SeatStatus Status { get; set; }
    }
}
