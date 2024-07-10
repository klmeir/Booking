namespace Booking.Domain.Entities
{
    public class Reservation : DomainEntity
    {
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public int GuestCount { get; set; }       
        
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }

        public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();

        public Reservation()
        {
        }

        public Reservation(int hotelId, int roomId, DateOnly checkInDate, DateOnly checkOutDate, int guestCount, string emergencyContactName, string emergencyContactPhone, List<Guest> guests)
        {         
            HotelId = hotelId;
            RoomId = roomId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            EmergencyContactName = emergencyContactName;
            EmergencyContactPhone = emergencyContactPhone;
            Guests = guests;
            GuestCount = guests.Count;
        }
    }
}

