using Booking.Application.Reservations;
using FluentValidation;

namespace Booking.Api.ApiHandlers.Reservations
{
    public class ReservationAddValidator : AbstractValidator<ReservationAddCommand>
    {
        private readonly string DateFormat = "yyyy-MM-dd";

        public ReservationAddValidator()
        {
            RuleFor(x => x.HotelId).NotEmpty();
            RuleFor(x => x.RoomId).NotEmpty();
            RuleFor(x => x.CheckInDate).NotEmpty().Must(BeValidDate).WithMessage($"Invalid date format. The date should be in format '{DateFormat}'"); ;
            RuleFor(x => x.CheckOutDate).NotEmpty().Must(BeValidDate).WithMessage($"Invalid date format. The date should be in format '{DateFormat}'"); ;
            RuleFor(x => x.GuestCount).NotEmpty();
            RuleFor(x => x.EmergencyContactName).NotEmpty();
            RuleFor(x => x.EmergencyContactPhone).NotEmpty();
            RuleFor(x => x.Guests).NotNull().NotEmpty();
            RuleForEach(x => x.Guests).ChildRules(guest =>
            {
                guest.RuleFor(x => x.Name).NotEmpty();
                guest.RuleFor(x => x.DocumentType).NotEmpty().IsInEnum();
                guest.RuleFor(x => x.DocumentNumber).NotEmpty();
                guest.RuleFor(x => x.Gender).NotEmpty().IsInEnum();
                guest.RuleFor(x => x.Birthdate).NotEmpty().Must(BeValidDate).WithMessage($"Invalid date format. The date should be in format '{DateFormat}'");
                guest.RuleFor(x => x.Phone).NotEmpty();
                guest.RuleFor(x => x.Email).NotEmpty().EmailAddress();
            });
        }

        private bool BeValidDate(string date)
        {
            if (string.IsNullOrWhiteSpace(date))
                return true;

            return DateTime.TryParseExact(date, DateFormat, null, System.Globalization.DateTimeStyles.None, out _);
        }
    }
}
