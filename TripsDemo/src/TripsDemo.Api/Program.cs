using Microsoft.EntityFrameworkCore;
using TripsDemo.Core.Configuration;
using TripsDemo.Core.Entities;
using TripsDemo.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddDbContext<TripsContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("TripsDb")));
builder.Services.AddScoped<ITripsContext, TripsContext>();
builder.Services.AddScoped<ITripsService, TripsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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

app.Run();
