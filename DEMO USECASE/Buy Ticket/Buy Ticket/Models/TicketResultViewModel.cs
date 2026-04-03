namespace Buy_Ticket.Models;

public class TicketResultViewModel
{
    public string TicketNo { get; set; } = string.Empty;
    public string QrCode { get; set; } = string.Empty;
    public string PaymentRef { get; set; } = string.Empty;
    public string DestinationName { get; set; } = string.Empty;
    public string DepartureTime { get; set; } = string.Empty;
    public string DateText { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}
