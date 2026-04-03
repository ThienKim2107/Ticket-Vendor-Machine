using Buy_Ticket.Models;

namespace Buy_Ticket.Services;

public interface ITicketService
{
    IReadOnlyList<DestinationViewModel> GetDestinations();
    IReadOnlyList<TripViewModel> GetTripsByDestination(int destinationId);
    TicketSummaryViewModel? BuildSummary(int destinationId, int tripId, int quantity);
    TicketResultViewModel ConfirmAndSave(ConfirmTicketRequest request);
}
