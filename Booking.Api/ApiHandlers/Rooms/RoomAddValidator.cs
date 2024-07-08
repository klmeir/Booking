using Booking.Application.Rooms;
using FluentValidation;

namespace Booking.Api.ApiHandlers
{
    public class RoomAddValidator : AbstractValidator<RoomAddCommand>
    {        

        public RoomAddValidator()
        {                        
            RuleFor(x => x.HotelId).NotEmpty();
            RuleFor(x => x.BaseCost).NotEmpty();
            RuleFor(x => x.Taxes).NotEmpty();
            RuleFor(x => x.RoomType).NotEmpty().IsInEnum();
            RuleFor(x => x.Location).NotEmpty();
        }
    }
}
