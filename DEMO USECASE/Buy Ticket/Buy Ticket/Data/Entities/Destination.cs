namespace Buy_Ticket.Data.Entities;

public class Destination
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;

    public ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
