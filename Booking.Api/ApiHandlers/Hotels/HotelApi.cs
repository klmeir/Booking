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
            routeHandler.MapPost("/", async (IMediator mediator, [Validate] HotelAddCommand hotel) =>
            {
                return Results.Ok(await mediator.Send(hotel));
            })
            .Produces(StatusCodes.Status200OK, typeof(HotelDto));

            return (RouteGroupBuilder)routeHandler;
        }
    }
}
