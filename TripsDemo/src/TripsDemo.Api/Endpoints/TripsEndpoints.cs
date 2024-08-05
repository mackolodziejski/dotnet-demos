using TripsDemo.Core.Entities;
using TripsDemo.Core.Services;

namespace TripsDemo.Api.Endpoints
{
    public static class TripsEndpoints
    {
        public static void MapTripsEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/trips", async (ITripsService service) => await service.GetAll());

            app.MapGet("/trips/{id}", async (int id, ITripsService service) =>
            {
                var trip = await service.Get(id);
                return trip != null ? Results.Ok(trip) : Results.NotFound();
            });

            app.MapPost("/trips", async (Trip trip, ITripsService service) =>
            {
                int id = await service.Add(trip);
                return Results.Created($"/trips/{trip.Id}", trip);
            });
        }
    }
}
