using Booking.Application.Ports;
using Booking.Domain.Dtos;
using Booking.Domain.Entities;
using Booking.Domain.Services;
using MediatR;
using System.Globalization;

namespace Booking.Application.Reservations
{
    public class ReservationAddCommandHandler : IRequestHandler<ReservationAddCommand, ReservationDto>
    {
        private readonly ReservationService _service;
        private readonly IEmailService _emailService;
        private readonly string DateFormat = "yyyy-MM-dd";

        public ReservationAddCommandHandler(ReservationService service, IEmailService emailService) 
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }            

        public async Task<ReservationDto> Handle(ReservationAddCommand request, CancellationToken cancellationToken)
        {
            DateTime.TryParseExact(request.CheckInDate, DateFormat, null, DateTimeStyles.None, out var checkInDateDt);
            DateTime.TryParseExact(request.CheckOutDate, DateFormat, null, DateTimeStyles.None, out var checkOutDateDt);

            var checkInDate = new DateOnly(checkInDateDt.Year, checkInDateDt.Month, checkInDateDt.Day);
            var checkOutDate = new DateOnly(checkOutDateDt.Year, checkOutDateDt.Month, checkOutDateDt.Day);

            var reservationSaved = await _service.SaveReservationAsync(
                new Reservation(request.HotelId, request.RoomId, checkInDate, checkOutDate, request.GuestCount, request.EmergencyContactName, request.EmergencyContactPhone,
                request.Guests.Select(g => new Guest(g.Name, DateOnly.ParseExact(g.Birthdate, DateFormat, CultureInfo.InvariantCulture), g.Gender, g.DocumentType, g.DocumentNumber, g.Email, g.Phone)).ToList()), cancellationToken);

            var reservationDto = new ReservationDto(reservationSaved.Id, reservationSaved.HotelId, reservationSaved.Hotel.Name, reservationSaved.RoomId, reservationSaved.Room.Location, reservationSaved.CheckInDate.ToString(DateFormat), reservationSaved.CheckOutDate.ToString(DateFormat), reservationSaved.GuestCount, reservationSaved.EmergencyContactName, reservationSaved.EmergencyContactPhone,
                reservationSaved.Guests.Select(g => new GuestDto(g.Name, g.Birthdate.ToString(DateFormat), g.Gender, g.DocumentType, g.DocumentNumber, g.Email, g.Phone)).ToList());

            await _emailService.SendAsync(new EmailRequest(reservationSaved.Guests.Select(g => g.Email).ToList(), "New reservation", $"A new reservation has been made \n\n{reservationDto.ToString()}"), cancellationToken);

            return reservationDto;
        }
    }
}
