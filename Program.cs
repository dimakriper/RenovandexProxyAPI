using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using System.Reflection;
using System;

using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services and configure the app

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<OperationIdFilter>();
    var xmlFile = "IO.Swagger.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MapsBookingPartnersAPI v1"));
}

// Instantiate the SDK API instance
var apiInstance = new DefaultApi();
apiInstance.Configuration.BasePath = "http://localhost:7235";

// Endpoint for /companies/feed
app.MapGet("/v1/companies/feed", async () =>
{
    try
    {
        // Use the SDK method to fetch companies feed data
        var response = await apiInstance.CompaniesFeedGetAsync();

        // Process the response and return an appropriate result
        return Results.Ok(response);
    }
    catch (ApiException ex)
    {
        // Handle API-specific exceptions, if needed
        return Results.BadRequest($"Error: {ex.Message}");
    }
    catch (Exception ex)
    {
        // Handle other exceptions
        return Results.BadRequest($"Error: {ex.Message}");
    }
})
.WithTags("Companies");

/*// Endpoint for /companies/{companyId}/services
app.MapGet("/v1/companies/{companyId:int}/services", (int companyId) =>
{
    // Check if the companyId is valid (for illustration purposes)
    if (companyId < 1 || companyId > 100) // Adjust the range based on your actual requirements
    {
        return Results.BadRequest(new { code = "ResourceNotFoundError" });
    }

    var exampleServices = new List<Service>
    {
        new Service { Name = "Service 1" },
        new Service { Name = "Service 2" }
        // Add other services as needed
    };

    var response = new Dictionary<string, List<Service>>
    {
        { "services", exampleServices }
    };

    return Results.Ok(response);
})
.WithTags("Services");*/

/*// Endpoint for /companies/{companyId}/resources
app.MapGet("/v1/companies/{companyId:int}/resources", (int companyId, [FromBody] List<int> serviceIds) =>
{
    // Check if the companyId is valid and handle serviceIds (for illustration purposes)
    if (companyId < 1 || companyId > 100 || serviceIds == null || serviceIds.Count == 0)
    {
        return Results.BadRequest(new { code = "ServiceNotFoundError" });
    }

    var exampleResources = new List<Resource>
    {
        new Resource { Id = 1, Name = "Resource 1" },
        new Resource { Id = 2, Name = "Resource 2" }
        // Add other resources as needed
    };

    var response = new Dictionary<string, List<Resource>>
    {
        { "resources", exampleResources }
    };

    return Results.Ok(response);
})
.WithTags("Resources");*/


//// Endpoint for /companies/{companyId}/resources/{resourceId}/reviews
//app.MapGet("/v1/companies/{companyId:int}/resources/{resourceId:int}/reviews", (int companyId, int resourceId) =>
//{
//    // Check if the companyId and resourceId are valid (for illustration purposes)
//    if (companyId < 1 || companyId > 100 || resourceId < 1 || resourceId > 100)
//    {
//        return Results.BadRequest(new { code = "InvalidParameters" });
//    }

//    var exampleReviews = new List<Review>
//    {
//        new Review { Id = 1, Comment = "Review 1" },
//        new Review { Id = 2, Comment = "Review 2" }
//        // Add other reviews as needed
//    };

//    var response = new Dictionary<string, List<Review>>
//    {
//        { "reviews", exampleReviews }
//    };

//    return Results.Ok(response);
//})
//.WithTags("Reviews");

//app.MapGet("/v1/companies/{companyId:int}/available_dates", (HttpContext context, int companyId, DateTime from, DateTime to) =>
//{
//    // Check if the companyId is valid and handle other parameters (for illustration purposes)
//    if (companyId < 1 || companyId > 100)
//    {
//        return Results.BadRequest(new { code = "ServiceNotFoundError" });
//    }

//    // Retrieve servicesIds from the query string
//    var servicesIds = context.Request.Query["servicesIds"].ToArray();

//    // Check if servicesIds is provided
//    if (servicesIds == null || servicesIds.Length == 0)
//    {
//        return Results.BadRequest(new { code = "RequiredServiceIdsMissing" });
//    }

//    // Your logic for handling the request here...

//    var exampleAvailableDates = new List<AvailableDate>
//    {
//        new AvailableDate { Date = DateTime.Now.AddDays(1) },
//        new AvailableDate { Date = DateTime.Now.AddDays(2) }
//        // Add other available dates as needed
//    };

//    var response = new Dictionary<string, List<AvailableDate>>
//    {
//        { "availableDates", exampleAvailableDates }
//    };

//    return Results.Ok(response);
//})
//.WithTags("Available Dates")
//.Produces(StatusCodes.Status200OK)
//.Produces<object>(StatusCodes.Status400BadRequest);




//// Endpoint for /companies/{companyId}/available_time_slots
//app.MapGet("/v1/companies/{companyId:int}/available_time_slots", async (HttpContext context, int companyId, DateTime date) =>
//{
//    // Check if the companyId is valid (for illustration purposes)
//    if (companyId < 1 || companyId > 100)
//    {
//        return Results.BadRequest(new { code = "ServiceNotFoundError" });
//    }

//    // Retrieve servicesIds from the query string
//    var servicesIds = context.Request.Query["servicesIds"].ToArray();

//    // Check if servicesIds is provided
//    if (servicesIds == null || servicesIds.Length == 0)
//    {
//        return Results.BadRequest(new { code = "RequiredServiceIdsMissing" });
//    }

//    // Your logic for handling the request here...

//    var exampleAvailableTimeSlots = new List<AvailableTimeSlot>
//    {
//        new AvailableTimeSlot { StartTime = DateTime.Now.AddHours(9), EndTime = DateTime.Now.AddHours(10) },
//        new AvailableTimeSlot { StartTime = DateTime.Now.AddHours(14), EndTime = DateTime.Now.AddHours(15) }
//        // Add other available time slots as needed
//    };

//    var response = new Dictionary<string, List<AvailableTimeSlot>>
//    {
//        { "availableTimeSlots", exampleAvailableTimeSlots }
//    };

//    return Results.Ok(response);
//})
//.WithTags("Available Time Slots")
//.Produces(StatusCodes.Status200OK)
//.Produces<object>(StatusCodes.Status400BadRequest);



//// Endpoint for creating a booking
//app.MapPost("/v1/bookings", (BookingInput bookingInput) =>
//{
//    // Check if the bookingInput is valid and handle other parameters (for illustration purposes)
//    if (bookingInput == null || bookingInput.CompanyId < 1 || string.IsNullOrEmpty(bookingInput.UserPhone))
//    {
//        return Results.BadRequest(new { code = "InvalidInput" });
//    }

//    // Assume you have a function to create a booking and return the created booking
//    var createdBooking = bookingRepository.CreateBooking(bookingInput);

//    var response = new Dictionary<string, Booking>
//    {
//        { "booking", createdBooking }
//    };

//    return Results.Ok(response);
//})
//.WithTags("Bookings");

//// Endpoint for getting information about a booking
//app.MapGet("/v1/bookings/{bookingId}", (string bookingId) =>
//{
//    // Assume you have a function to get a booking by ID
//    var booking = bookingRepository.GetBookingById(bookingId);

//    if (booking == null)
//    {
//        return Results.NotFound();
//    }

//    var response = new Dictionary<string, Booking>
//    {
//        { "booking", booking }
//    };

//    return Results.Ok(response);
//})
//.WithTags("Bookings");

//// Endpoint for updating a booking
//app.MapPut("/v1/bookings/{bookingId}", (string bookingId, BookingUpdate bookingUpdate) =>
//{
//    // Check if the bookingId and bookingUpdate are valid (for illustration purposes)
//    if (string.IsNullOrEmpty(bookingId) || bookingUpdate == null)
//    {
//        return Results.BadRequest(new { code = "InvalidInput" });
//    }

//    // Assume you have a function to update a booking and return the updated booking
//    var updatedBooking = bookingRepository.UpdateBooking(bookingId, bookingUpdate);

//    if (updatedBooking == null)
//    {
//        return Results.NotFound();
//    }

//    var response = new Dictionary<string, Booking>
//    {
//        { "booking", updatedBooking }
//    };

//    return Results.Ok(response);
//})
//.WithTags("Bookings");

////Endpoint for deleting a booking
//app.MapDelete("/v1/bookings/{bookingId}", (string bookingId) =>
//{
//    // Assume you have a function to delete a booking
//    var isDeleted = bookingRepository.DeleteBooking(bookingId);

//if (!isDeleted)
//{
//    return Results.NotFound();
//}

//return Results.Ok();
//})
//.WithTags("Bookings");

app.Run();

public class OperationIdFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.MethodInfo?.DeclaringType != null)
        {
            operation.OperationId = $"{context.MethodInfo.DeclaringType.Name}_{context.MethodInfo.Name}";
        }
    }
}