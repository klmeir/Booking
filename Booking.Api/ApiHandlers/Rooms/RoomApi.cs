using Booking.Api.Filters;
using Booking.Application.Rooms;
using Booking.Domain.Dtos;
using MediatR;

namespace Booking.Api.ApiHandlers.Rooms
{
    public static class RoomApi
    {
        public static RouteGroupBuilder MapRooms(this IEndpointRouteBuilder routeHandler)
        {
            routeHandler.MapPost("/", async (IMediator mediator, [Validate] RoomAddCommand room) =>
            {
                return Results.Ok(await mediator.Send(room));
            })
            .Produces(StatusCodes.Status200OK, typeof(RoomDto));

            return (RouteGroupBuilder)routeHandler;
        }
    }
}