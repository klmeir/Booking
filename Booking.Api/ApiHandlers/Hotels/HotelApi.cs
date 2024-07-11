using Booking.Api.Filters;
using Booking.Application.Auth;
using Booking.Application.Hotels;
using Booking.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.ApiHandlers.Hotels
{
    public static class HotelApi
    {
        public static RouteGroupBuilder MapHotels(this IEndpointRouteBuilder routeHandler)
        {
            routeHandler.MapGet("/{id}", async (IMediator mediator, int id) =>
            {
                return Results.Ok(await mediator.Send(new HotelQuery(id)));
            })
            .WithName("GetHotel")
            .Produces(StatusCodes.Status200OK, typeof(HotelDto))
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(PolicyEnum.AgencyPolicy.ToString());

            routeHandler.MapGet("/search", async (IMediator mediator, [FromQuery] string? City, [FromQuery] int? Guests, [FromQuery] string? CheckInDate, [FromQuery] string? CheckOutDate) =>
            {
                return Results.Ok(await mediator.Send(new HotelSearchQuery(City, Guests.Value, CheckInDate, CheckOutDate)));
            })            
            .Produces(StatusCodes.Status200OK, typeof(HotelDto))
            .RequireAuthorization(PolicyEnum.AgencyOrTravelerPolicy.ToString());            

            routeHandler.MapPost("/", async (IMediator mediator, [Validate] HotelAddCommand hotel) =>
            {                                
                var savedHotel = await mediator.Send(hotel);
                return Results.CreatedAtRoute("GetHotel", new { id = savedHotel.Id }, savedHotel);                
            })
            .Produces(StatusCodes.Status201Created, typeof(HotelDto))
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization(PolicyEnum.AgencyPolicy.ToString());

            routeHandler.MapPut("/{id}", async (IMediator mediator, int id, [Validate] HotelUpdateCommand hotel) =>
            {
                if (id != hotel.Id) return Results.BadRequest();
                return Results.Ok(await mediator.Send(hotel));
            })
            .Produces(StatusCodes.Status200OK, typeof(HotelDto))
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization(PolicyEnum.AgencyPolicy.ToString());

            return (RouteGroupBuilder)routeHandler;
        }
    }
}
