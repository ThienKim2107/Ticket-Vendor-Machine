using Buy_Ticket.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Buy_Ticket.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Destination> Destinations => Set<Destination>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<TicketOrder> TicketOrders => Set<TicketOrder>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Destination>(e =>
        {
            e.HasKey(d => d.Id);
            e.Property(d => d.Name).HasMaxLength(200);
            e.Property(d => d.Description).HasMaxLength(500);
            e.Property(d => d.Icon).HasMaxLength(20);
        });

        modelBuilder.Entity<Trip>(e =>
        {
            e.HasKey(t => t.Id);
            e.HasOne(t => t.Destination)
                .WithMany(d => d.Trips)
                .HasForeignKey(t => t.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);
            e.Property(t => t.DateText).HasMaxLength(80);
            e.Property(t => t.DepartureTime).HasMaxLength(40);
            e.Property(t => t.Price).HasPrecision(18, 2);
        });

        modelBuilder.Entity<TicketOrder>(e =>
        {
            e.HasKey(o => o.Id);
            e.Property(o => o.TicketNo).HasMaxLength(40);
            e.Property(o => o.PaymentRef).HasMaxLength(40);
            e.Property(o => o.DestinationName).HasMaxLength(200);
            e.Property(o => o.DepartureTime).HasMaxLength(40);
            e.Property(o => o.DateText).HasMaxLength(80);
            e.Property(o => o.TotalPrice).HasPrecision(18, 2);
            e.Property(o => o.QrPayload).HasMaxLength(1000);
        });

        Seed(modelBuilder);
    }

    private static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Destination>().HasData(
            new Destination { Id = 1, Name = "District 1", Description = "Cultural and financial heart of the city", Icon = "⭐" },
            new Destination { Id = 2, Name = "Tan Son Nhat Airport", Description = "Domestic & International Terminals", Icon = "✈️" },
            new Destination { Id = 3, Name = "Saigon Railway Station", Description = "District 3, Central Hub", Icon = "🚆" },
            new Destination { Id = 4, Name = "Da Lat Bus Station", Description = "East City Terminal Connections", Icon = "🚌" },
            new Destination { Id = 5, Name = "Thu Thiem New Area", Description = "Emerging District 2 Riverside", Icon = "🏙️" });

        modelBuilder.Entity<Trip>().HasData(
            new Trip { Id = 1, DestinationId = 4, DateText = "April 4, 2026", DepartureTime = "08:30 AM", Price = 350000, AvailableSeats = 22 },
            new Trip { Id = 2, DestinationId = 4, DateText = "April 4, 2026", DepartureTime = "01:45 PM", Price = 350000, AvailableSeats = 18 },
            new Trip { Id = 3, DestinationId = 4, DateText = "April 4, 2026", DepartureTime = "08:00 PM", Price = 370000, AvailableSeats = 12 },
            new Trip { Id = 4, DestinationId = 2, DateText = "April 4, 2026", DepartureTime = "09:00 AM", Price = 150000, AvailableSeats = 30 },
            new Trip { Id = 5, DestinationId = 3, DateText = "April 4, 2026", DepartureTime = "11:20 AM", Price = 180000, AvailableSeats = 26 },
            new Trip { Id = 6, DestinationId = 5, DateText = "April 4, 2026", DepartureTime = "07:40 AM", Price = 120000, AvailableSeats = 28 },
            new Trip { Id = 7, DestinationId = 1, DateText = "April 4, 2026", DepartureTime = "10:00 AM", Price = 200000, AvailableSeats = 40 });
    }
}
