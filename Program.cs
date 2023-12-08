using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;

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

// Create a HttpClient instance to send requests
var client = new HttpClient();

// Set the base address and the API key for the requests
client.BaseAddress = new Uri("https://example.com/api/");
client.DefaultRequestHeaders.Add("x-api-key", "your-api-key");

// Define a GET endpoint that takes a query parameter and returns the response from the URL
app.MapGet("/query", async (string q) =>
{
    // Send a GET request to the URL with the query parameter
    var response = await client.GetAsync($"?q={q}");

    // Check if the response is successful
    if (response.IsSuccessStatusCode)
    {
        // Read the response content as a string
        var content = await response.Content.ReadAsStringAsync();

        // Return the content as the response
        return Results.Text(content, "application/json");
    }
    else
    {
        // Return an error message as the response
        return Results.Problem("Something went wrong");
    }
});

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}