namespace Booking.Domain.Entities
{
    public class Hotel : DomainEntity
    {        
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public decimal Commission { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        public Hotel()
        {
        }

        public Hotel(string name, string description, string city, string address, decimal commission)
        {
            Name = name;
            Description = description;
            City = city;
            Address = address;
            Commission = commission;
            IsActive = true;
        }

        public Hotel Update(string? name, string? description, string? city, string? address, decimal? commission, bool isActive)
        {
            if (name is not null && Name?.Equals(name) is not true) Name = name;
            if (description is not null && Description?.Equals(description) is not true) Description = description;
            if (city is not null && City?.Equals(city) is not true) City = city;
            if (address is not null && Address?.Equals(address) is not true) Address = address;
            if (commission is not null && Commission != commission) Commission = commission.Value;
            if (isActive != IsActive) IsActive = isActive;
            return this;
        }
    }
}
