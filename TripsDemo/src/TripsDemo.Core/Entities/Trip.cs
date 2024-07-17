namespace TripsDemo.Core.Entities;

public record Trip(string Country, string City, DateOnly StartDate, DateOnly EndDate, int Cost) { }