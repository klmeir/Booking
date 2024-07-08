using Booking.Domain.Enums;

namespace Booking.Domain.Entities
{
    public class Room : DomainEntity
    {    
        public int HotelId { get; set; }
        public decimal BaseCost { get; set; }
        public decimal Taxes { get; set; }
        public RoomTypeEnum RoomType { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        public Room(int hotelId, decimal baseCost, decimal taxes, RoomTypeEnum roomType, string location)
        {
            HotelId = hotelId;
            BaseCost = baseCost;
            Taxes = taxes;
            RoomType = roomType;
            Location = location;
            IsActive = true;
        }
    }
}
