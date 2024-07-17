using TripsDemo.Core.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var countries = new[]
{
    "Poland", "Germany", "Spain"
};

app.MapPost("/trips", async () => 
{
    await Task.Delay(1);

    var forecast =  Enumerable.Range(1, 2).Select(index =>
        new Trip
        (
            countries[Random.Shared.Next(countries.Length)],
            "city",
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(200, 1000)
        ))
        .ToArray();
    return forecast;
})
.WithName("AddTrip")
.WithOpenApi();

app.Run();
