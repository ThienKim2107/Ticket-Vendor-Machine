namespace Buy_Ticket.Data.Entities;

public class TicketOrder
{
    public int Id { get; set; }
    public string TicketNo { get; set; } = string.Empty;
    public string PaymentRef { get; set; } = string.Empty;
    public string DestinationName { get; set; } = string.Empty;
    public string DepartureTime { get; set; } = string.Empty;
    public string DateText { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public string QrPayload { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
