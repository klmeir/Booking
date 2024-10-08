﻿using Booking.Api.Filters;
using Booking.Application.Auth;
using Booking.Application.Hotels;
using Booking.Application.Rooms;
using Booking.Domain.Dtos;
using MediatR;

namespace Booking.Api.ApiHandlers.Rooms
{
    public static class RoomApi
    {
        public static RouteGroupBuilder MapRooms(this IEndpointRouteBuilder routeHandler)
        {
            routeHandler.MapGet("/{id}", async (IMediator mediator, int id) =>
            {
                return Results.Ok(await mediator.Send(new RoomQuery(id)));
            })
            .WithName("GetRoom")
            .Produces(StatusCodes.Status200OK, typeof(RoomDto))
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(PolicyEnum.AgencyPolicy.ToString());

            routeHandler.MapPost("/", async (IMediator mediator, [Validate] RoomAddCommand room) =>
            {
                var savedRoom = await mediator.Send(room);
                return Results.CreatedAtRoute("GetRoom", new { id = savedRoom.Id }, savedRoom);
            })
            .Produces(StatusCodes.Status200OK, typeof(RoomDto))
            .RequireAuthorization(PolicyEnum.AgencyPolicy.ToString());

            routeHandler.MapPut("/{id}", async (IMediator mediator, int id, [Validate] RoomUpdateCommand room) =>
            {
                if (id != room.Id) return Results.BadRequest();
                return Results.Ok(await mediator.Send(room));
            })
            .Produces(StatusCodes.Status200OK, typeof(RoomDto))
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization(PolicyEnum.AgencyPolicy.ToString());

            return (RouteGroupBuilder)routeHandler;
        }
    }
}