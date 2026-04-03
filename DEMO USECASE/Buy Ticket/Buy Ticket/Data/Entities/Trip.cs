namespace Buy_Ticket.Data.Entities;

public class Trip
{
    public int Id { get; set; }
    public int DestinationId { get; set; }
    public Destination Destination { get; set; } = null!;

    public string DateText { get; set; } = string.Empty;
    public string DepartureTime { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int AvailableSeats { get; set; }
}
