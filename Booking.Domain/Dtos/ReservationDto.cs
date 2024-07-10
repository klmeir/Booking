namespace Booking.Domain.Dtos
{
    public record ReservationDto(int Id, int HotelId, string Hotel, int RoomId, string RoomLocation, string CheckInDate, string CheckOutDate, int GuestCount, string EmergencyContactName, string EmergencyContactPhone, List<GuestDto> Guests);
}
