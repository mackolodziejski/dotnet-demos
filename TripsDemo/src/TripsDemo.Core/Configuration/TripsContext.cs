using Microsoft.EntityFrameworkCore;
using TripsDemo.Core.Entities;

namespace TripsDemo.Core.Configuration;

public class TripsContext(DbContextOptions options) : DbContext(options), ITripsContext
{
    public DbSet<Trip> Trips { get; set; }

    async Task<int> ITripsContext.SaveChanges() => await SaveChangesAsync();
}
