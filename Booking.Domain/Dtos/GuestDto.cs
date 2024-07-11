using Booking.Domain.Enums;

namespace Booking.Domain.Dtos
{    
    public class GuestDto
    {
        public string Name { get; init; }
        public string Birthdate { get; init; }
        public GenderEnum Gender { get; init; }
        public DocumentTypeEnum DocumentType { get; init; }
        public string DocumentNumber { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }

        public GuestDto(string name, string birthdate, GenderEnum gender, DocumentTypeEnum documentType,
                       string documentNumber, string email, string phone)
        {
            Name = name;
            Birthdate = birthdate;
            Gender = gender;
            DocumentType = documentType;
            DocumentNumber = documentNumber;
            Email = email;
            Phone = phone;
        }

        public override string ToString()
        {
            return $"Name: {Name}\n" +
                   $"Birthdate: {Birthdate}\n" +
                   $"Gender: {Gender.ToString()}\n" +
                   $"Document Type: {DocumentType.ToString()}\n" +
                   $"Document Number: {DocumentNumber}\n" +
                   $"Email: {Email}\n" +
                   $"Phone: {Phone}";
        }
    }
}
