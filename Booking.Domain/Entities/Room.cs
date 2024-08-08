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
        public int MaxGuests { get; set; }
        public bool IsActive { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        public Room()
        {
        }

        public Room(int hotelId, decimal baseCost, decimal taxes, RoomTypeEnum roomType, int maxGuests, string location)
        {
            HotelId = hotelId;
            BaseCost = baseCost;
            Taxes = taxes;
            RoomType = roomType;
            MaxGuests = maxGuests;
            Location = location;
            IsActive = true;
        }

        public Room Update(int? hotelId, decimal? baseCost, decimal? taxes, RoomTypeEnum? roomType, int? maxGuests, string? location, bool isActive)
        {
            if (hotelId is not null && HotelId != hotelId) HotelId = hotelId.Value;
            if (baseCost is not null && BaseCost != baseCost) BaseCost = baseCost.Value;
            if (taxes is not null && Taxes != taxes) Taxes = taxes.Value;
            if (roomType is not null && RoomType != roomType) RoomType = roomType.Value;
            if (maxGuests is not null && MaxGuests != maxGuests) MaxGuests = maxGuests.Value;
            if (location is not null && Location?.Equals(location) is not true) Location = location;
            if (isActive != IsActive) IsActive = isActive;
            return this;
        }
    }
}
