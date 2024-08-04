namespace TripsDemo.Core.Entities;

public class Trip()
{
    public int Id { get; set; }
    public required string Country { get; set; }
    public required string City { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Cost { get; set; }
}