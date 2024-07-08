using FluentValidation;
using Booking.Application.Persons;

namespace Booking.Api.ApiHandlers
{
    public class HotelAddValidator : AbstractValidator<HotelAddCommand>
    {        

        public HotelAddValidator()
        {            
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
