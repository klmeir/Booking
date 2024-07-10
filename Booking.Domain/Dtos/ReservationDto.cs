namespace Booking.Domain.Dtos
{
    public record ReservationDto(int Id, int HotelId, int RoomId, string CheckInDate, string CheckOutDate, int GuestCount, string EmergencyContactName, string EmergencyContactPhone, List<GuestDto> Guests);
}
