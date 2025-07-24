namespace Zattini.Application.DTOs
{
    public class UserAddressDTO
    {
        public Guid? Id { get; set; }
        public string? Cep { get; set; } // 23232-323
        public string? TypeAddress { get; set; }
        public string? Address { get; set; }
        public int? Number { get; set; }
        public string? Complement { get; set; }
        public string? Neighborhood { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? ReferencePoint { get; set; }
        public Guid? UserId { get; set; }
        public UserDTO? UserDTO { get; set; }

        public UserAddressDTO(Guid? id, string? cep, string? typeAddress, string? address, int? number, string? complement, string? neighborhood, 
            string? state, string? city, string? referencePoint, Guid? userId, UserDTO? userDTO)
        {
            Id = id;
            Cep = cep;
            TypeAddress = typeAddress;
            Address = address;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            State = state;
            City = city;
            ReferencePoint = referencePoint;
            UserId = userId;
            UserDTO = userDTO;
        }

        public UserAddressDTO()
        {
        }

        public void SetUserId(Guid? value) => UserId = value;
    }
}
