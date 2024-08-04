using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using TripsDemo.Core.Configuration;
using TripsDemo.Core.Entities;

namespace TripsDemo.Core.Services;

public interface ITripsService
{
    Task<IEnumerable<Trip>> GetAll();
    Task<Trip?> Get(int id);
    Task<int> Add(Trip trip);
}

public class TripsService : ITripsService
{
    private readonly ITripsContext _context;
    private readonly IDistributedCache _cache;

    public TripsService(ITripsContext context, IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<IEnumerable<Trip>> GetAll() => await _context.Trips.ToListAsync();

    public async Task<Trip?> Get(int id)
    {
        string cacheKey = $"trips-{id}";
        var tripJson = await _cache.GetStringAsync(cacheKey);
        if (!string.IsNullOrWhiteSpace(tripJson))
        {
            return JsonSerializer.Deserialize<Trip>(tripJson);
        }

        Trip? trip = await _context.Trips.FindAsync(id);
        if (trip is not null)
        {
            tripJson = JsonSerializer.Serialize(trip);
            await _cache.SetStringAsync(cacheKey, tripJson);
        }

        return trip;
    }

    public async Task<int> Add(Trip trip)
    {
        await _context.Trips.AddAsync(trip);
        await _context.SaveChanges();

        return trip.Id;
    }
}
