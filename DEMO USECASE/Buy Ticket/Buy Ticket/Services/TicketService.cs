using Buy_Ticket.Data;
using Buy_Ticket.Data.Entities;
using Buy_Ticket.Models;
using Microsoft.EntityFrameworkCore;

namespace Buy_Ticket.Services;

public class TicketService : ITicketService
{
    private readonly AppDbContext _db;

    public TicketService(AppDbContext db)
    {
        _db = db;
    }

    public IReadOnlyList<DestinationViewModel> GetDestinations()
    {
        return _db.Destinations
            .AsNoTracking()
            .OrderBy(d => d.Id)
            .Select(d => new DestinationViewModel
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Icon = d.Icon
            })
            .ToList();
    }

    public IReadOnlyList<TripViewModel> GetTripsByDestination(int destinationId)
    {
        return _db.Trips
            .AsNoTracking()
            .Where(t => t.DestinationId == destinationId)
            .OrderBy(t => t.Id)
            .Select(t => new TripViewModel
            {
                Id = t.Id,
                DestinationId = t.DestinationId,
                DateText = t.DateText,
                DepartureTime = t.DepartureTime,
                Price = t.Price,
                AvailableSeats = t.AvailableSeats
            })
            .ToList();
    }

    public TicketSummaryViewModel? BuildSummary(int destinationId, int tripId, int quantity)
    {
        var destination = _db.Destinations.AsNoTracking().FirstOrDefault(d => d.Id == destinationId);
        var trip = _db.Trips.AsNoTracking().FirstOrDefault(t => t.Id == tripId && t.DestinationId == destinationId);

        if (destination is null || trip is null || quantity <= 0)
        {
            return null;
        }

        return new TicketSummaryViewModel
        {
            DestinationId = destinationId,
            TripId = tripId,
            DestinationName = destination.Name,
            DepartureTime = trip.DepartureTime,
            DateText = trip.DateText,
            Quantity = quantity,
            UnitPrice = trip.Price
        };
    }

    public TicketResultViewModel ConfirmAndSave(ConfirmTicketRequest request)
    {
        var summary = BuildSummary(request.DestinationId, request.TripId, request.Quantity)
                      ?? throw new InvalidOperationException("Invalid request.");

        var ticketNo = $"CT-{DateTime.Now:yyyyMMddHHmmss}-{Random.Shared.Next(100, 999)}";
        var paymentRef = $"PAY-{Guid.NewGuid().ToString("N")[..10].ToUpperInvariant()}";
        var qrText = $"TICKET:{ticketNo}|REF:{paymentRef}|TO:{summary.DestinationName}|QTY:{summary.Quantity}|TOTAL:{summary.TotalPrice}";

        var order = new TicketOrder
        {
            TicketNo = ticketNo,
            PaymentRef = paymentRef,
            DestinationName = summary.DestinationName,
            DepartureTime = summary.DepartureTime,
            DateText = summary.DateText,
            Quantity = summary.Quantity,
            TotalPrice = summary.TotalPrice,
            QrPayload = qrText,
            CreatedAt = DateTime.UtcNow
        };

        _db.TicketOrders.Add(order);
        _db.SaveChanges();

        return new TicketResultViewModel
        {
            TicketNo = ticketNo,
            PaymentRef = paymentRef,
            QrCode = qrText,
            DestinationName = summary.DestinationName,
            DepartureTime = summary.DepartureTime,
            DateText = summary.DateText,
            Quantity = summary.Quantity,
            TotalPrice = summary.TotalPrice
        };
    }
}
