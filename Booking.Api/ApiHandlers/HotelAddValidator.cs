using FluentValidation;
using Booking.Application.Persons;

namespace Booking.Api.ApiHandlers
{
    public class HotelAddValidator : AbstractValidator<HotelAddCommand>
    {        

        public HotelAddValidator()
        {                        
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            //RuleFor(x => x.City).NotEmpty();
            //RuleFor(x => x.Address).NotEmpty();
            //RuleFor(x => x.Commission).NotEmpty();
        }
    }
}
