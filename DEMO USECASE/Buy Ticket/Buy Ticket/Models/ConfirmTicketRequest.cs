using System.ComponentModel.DataAnnotations;

namespace Buy_Ticket.Models;

public class ConfirmTicketRequest
{
    [Required]
    public int DestinationId { get; set; }

    [Required]
    public int TripId { get; set; }

    [Range(1, 10)]
    public int Quantity { get; set; }
}
