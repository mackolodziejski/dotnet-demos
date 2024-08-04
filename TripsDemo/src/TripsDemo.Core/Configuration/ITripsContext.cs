using Microsoft.EntityFrameworkCore;
using TripsDemo.Core.Entities;

namespace TripsDemo.Core.Configuration;

public interface ITripsContext
{
    DbSet<Trip> Trips { get; set; }
    Task<int> SaveChanges();
}
