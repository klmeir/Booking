using Booking.Domain.Enums;

namespace Booking.Domain.Entities
{
    public class Guest : DomainEntity
    {
        public int ReservationId { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public GenderEnum Gender { get; set; }
        public DocumentTypeEnum DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }        
        
        public Guest(string name, DateTime birthdate, GenderEnum gender, DocumentTypeEnum documentType, string documentNumber, string email, string phone)
        {
            Name = name;
            Birthdate = birthdate;
            Gender = gender;
            DocumentType = documentType;
            DocumentNumber = documentNumber;
            Email = email;
            Phone = phone;
        }
    }

}
