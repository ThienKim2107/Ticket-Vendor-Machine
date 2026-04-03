namespace Buy_Ticket.Models;

public class TripViewModel
{
    public int Id { get; set; }
    public int DestinationId { get; set; }
    public string DepartureTime { get; set; } = string.Empty;
    public string DateText { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int AvailableSeats { get; set; }
}
