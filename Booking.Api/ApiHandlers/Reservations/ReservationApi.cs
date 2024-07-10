using Booking.Api.Filters;
using Booking.Application.Reservations;
using Booking.Domain.Dtos;
using MediatR;

namespace Booking.Api.ApiHandlers.Reservations
{
    public static class ReservationApi
    {
        public static RouteGroupBuilder MapReservations(this IEndpointRouteBuilder routeHandler){
            routeHandler.MapGet("/{id}", async (IMediator mediator, int id) =>
            {
                return Results.Ok(await mediator.Send(new ReservationQuery(id)));
            })
            .WithName("GetReservation")
            .Produces(StatusCodes.Status200OK, typeof(ReservationDto))
            .Produces(StatusCodes.Status404NotFound);

            routeHandler.MapPost("/", async (IMediator mediator, [Validate] ReservationAddCommand reservation) =>
            {
                var savedReservation = await mediator.Send(reservation);
                return Results.CreatedAtRoute("GetReservation", new { id = savedReservation.Id }, savedReservation);
            })
            .Produces(StatusCodes.Status201Created, typeof(ReservationDto))
            .Produces(StatusCodes.Status400BadRequest);            

            return (RouteGroupBuilder)routeHandler;
        }
    }
}
