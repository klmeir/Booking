using Booking.Api.Filters;
using Booking.Application.Auth;
using MediatR;

namespace Booking.Api.ApiHandlers.Hotels
{
    public static class AuthApi
    {
        public static RouteGroupBuilder MapAuth(this IEndpointRouteBuilder routeHandler)
        {                       
            routeHandler.MapPost("/login", async (IMediator mediator, [Validate] LoginCommand login) =>
            {                                
                return await mediator.Send(login);             
            })
            .Produces(StatusCodes.Status200OK, typeof(LoginResponseDto));

            return (RouteGroupBuilder)routeHandler;
        }
    }
}
