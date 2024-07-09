using Booking.Api.Filters;
using Booking.Application.Hotels;
using Booking.Domain.Dtos;
using MediatR;

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
            .Produces(StatusCodes.Status404NotFound);

            routeHandler.MapPost("/", async (IMediator mediator, [Validate] HotelAddCommand hotel) =>
            {                                
                var savedHotel = await mediator.Send(hotel);
                return Results.CreatedAtRoute("GetHotel", new { id = savedHotel.Id }, savedHotel);                
            })
            .Produces(StatusCodes.Status201Created, typeof(HotelDto))
            .Produces(StatusCodes.Status400BadRequest);

            routeHandler.MapPut("/{id}", async (IMediator mediator, int id, [Validate] HotelUpdateCommand hotel) =>
            {
                if (id != hotel.Id) return Results.BadRequest();
                return Results.Ok(await mediator.Send(hotel));
            })
            .Produces(StatusCodes.Status200OK, typeof(HotelDto))
            .Produces(StatusCodes.Status400BadRequest);

            return (RouteGroupBuilder)routeHandler;
        }
    }
}
