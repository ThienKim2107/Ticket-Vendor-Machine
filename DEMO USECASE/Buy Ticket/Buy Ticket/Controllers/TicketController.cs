using System.Diagnostics;
using Buy_Ticket.Data;
using Buy_Ticket.Models;
using Buy_Ticket.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Buy_Ticket.Controllers;

public class TicketController : Controller
{
    private readonly ITicketService _ticketService;
    private readonly AppDbContext _db;

    public TicketController(ITicketService ticketService, AppDbContext db)
    {
        _ticketService = ticketService;
        _db = db;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }

    /// <summary>Report: danh sách vé đã lưu trong SQL Server.</summary>
    public IActionResult Orders()
    {
        var orders = _db.TicketOrders
            .AsNoTracking()
            .OrderByDescending(o => o.Id)
            .Take(100)
            .ToList();

        return View(orders);
    }

    public IActionResult Welcome()
    {
        return View();
    }

    public IActionResult SelectDestination()
    {
        var destinations = _ticketService.GetDestinations();
        return View(destinations);
    }

    public IActionResult SelectTrip(int destinationId)
    {
        var destination = _ticketService.GetDestinations().FirstOrDefault(d => d.Id == destinationId);
        if (destination is null)
        {
            return RedirectToAction(nameof(SelectDestination));
        }

        ViewBag.Destination = destination;
        var trips = _ticketService.GetTripsByDestination(destinationId);
        return View(trips);
    }

    [HttpGet]
    public IActionResult Summary(int destinationId, int tripId, int quantity = 1)
    {
        var summary = _ticketService.BuildSummary(destinationId, tripId, quantity);
        if (summary is null)
        {
            return RedirectToAction(nameof(SelectDestination));
        }

        return View(summary);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Confirm(ConfirmTicketRequest request)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(SelectDestination));
        }

        var result = _ticketService.ConfirmAndSave(request);
        TempData["Saved"] = "1";

        return RedirectToAction(nameof(Ticket), new
        {
            ticketNo = result.TicketNo,
            paymentRef = result.PaymentRef,
            destinationName = result.DestinationName,
            departureTime = result.DepartureTime,
            dateText = result.DateText,
            quantity = result.Quantity,
            totalPrice = result.TotalPrice,
            qrCode = result.QrCode
        });
    }

    public IActionResult Ticket(
        string ticketNo,
        string paymentRef,
        string destinationName,
        string departureTime,
        string dateText,
        int quantity,
        decimal totalPrice,
        string qrCode)
    {
        if (TempData["Saved"] is null)
        {
            return RedirectToAction(nameof(SelectDestination));
        }

        var model = new TicketResultViewModel
        {
            TicketNo = ticketNo,
            PaymentRef = paymentRef,
            DestinationName = destinationName,
            DepartureTime = departureTime,
            DateText = dateText,
            Quantity = quantity,
            TotalPrice = totalPrice,
            QrCode = qrCode
        };

        return View(model);
    }
}
