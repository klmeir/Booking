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

        public Hotel(string name, string description, string city, string address, decimal commission)
        {
            Name = name;
            Description = description;
            City = city;
            Address = address;
            Commission = commission;
            IsActive = true;
        }
    }
}
