namespace Buy_Ticket.Models;

public class TicketSummaryViewModel
{
    public int DestinationId { get; set; }
    public int TripId { get; set; }
    public string DestinationName { get; set; } = string.Empty;
    public string From { get; set; } = "Mien Dong Bus Station";
    public string DepartureTime { get; set; } = string.Empty;
    public string DateText { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => Quantity * UnitPrice;
}
