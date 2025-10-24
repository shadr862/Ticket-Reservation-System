namespace Ticket_Reservation_System_API.Dtos
{
    public class BookSeatResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public Guid? TicketId { get; set; }
    }
}
