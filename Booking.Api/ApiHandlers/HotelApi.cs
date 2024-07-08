using Booking.Api.Filters;
using Booking.Application.Persons;
using Booking.Domain.Entities;
using MediatR;

namespace Booking.Api.ApiHandlers
{
    public static class HotelApi
    {
        public static RouteGroupBuilder MapHotels(this IEndpointRouteBuilder routeHandler)
        {
            routeHandler.MapPost("/", async (IMediator mediator, [Validate] HotelAddCommand hotel) =>
            {
                return Results.Ok(await mediator.Send(hotel));
            })
            .Produces(StatusCodes.Status200OK, typeof(Hotel));

            return (RouteGroupBuilder)routeHandler;
        }
    }
}
