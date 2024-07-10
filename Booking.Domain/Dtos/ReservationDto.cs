namespace Booking.Domain.Dtos
{
    public class ReservationDto
    {
        public int Id { get; init; }
        public int HotelId { get; init; }
        public string Hotel { get; init; }
        public int RoomId { get; init; }
        public string RoomLocation { get; init; }
        public string CheckInDate { get; init; }
        public string CheckOutDate { get; init; }
        public int GuestCount { get; init; }
        public string EmergencyContactName { get; init; }
        public string EmergencyContactPhone { get; init; }
        public List<GuestDto> Guests { get; init; }

        public ReservationDto(int id, int hotelId, string hotel, int roomId, string roomLocation, string checkInDate, string checkOutDate, int guestCount, string emergencyContactName, string emergencyContactPhone, List<GuestDto> guests)
        {
            Id = id;
            HotelId = hotelId;
            Hotel = hotel;
            RoomId = roomId;
            RoomLocation = roomLocation;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            GuestCount = guestCount;
            EmergencyContactName = emergencyContactName;
            EmergencyContactPhone = emergencyContactPhone;
            Guests = guests;
        }

        public override string ToString()
        {
            return $"Reservation ID: {Id}\n" +
                   $"Hotel ID: {HotelId}\n" +
                   $"Hotel: {Hotel}\n" +
                   $"Room ID: {RoomId}\n" +
                   $"Room Location: {RoomLocation}\n" +
                   $"Check-In Date: {CheckInDate}\n" +
                   $"Check-Out Date: {CheckOutDate}\n" +
                   $"Guest Count: {GuestCount}\n" +
                   $"Emergency Contact Name: {EmergencyContactName}\n" +
                   $"Emergency Contact Phone: {EmergencyContactPhone}\n\n" +
                   $"Guests:\n{GetGuestsAsString()}";
        }

        private string GetGuestsAsString()
        {
            if (Guests == null || Guests.Count == 0)
                return "No guests listed.";

            var guestsInfo = "";
            foreach (var guest in Guests)
            {
                guestsInfo += $"{guest}\n\n";
            }
            return guestsInfo.Trim();
        }
    }
}
