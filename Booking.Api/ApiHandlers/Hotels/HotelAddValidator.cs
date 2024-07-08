using FluentValidation;
using Booking.Application.Hotels;

namespace Booking.Api.ApiHandlers.Hotels
{
    public class HotelAddValidator : AbstractValidator<HotelAddCommand>
    {

        public HotelAddValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Commission).NotEmpty();
        }
    }
}
