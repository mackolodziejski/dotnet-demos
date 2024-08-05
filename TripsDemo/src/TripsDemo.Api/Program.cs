using Microsoft.EntityFrameworkCore;
using TripsDemo.Api.Endpoints;
using TripsDemo.Core.Configuration;
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

app.MapTripsEndpoints();

app.Run();
