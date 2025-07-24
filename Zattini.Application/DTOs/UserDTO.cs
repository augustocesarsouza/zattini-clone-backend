namespace Zattini.Application.DTOs
{
    public class UserDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Cpf { get; set; } // 233.223.233-23
        public string? CellPhone { get; set; } // (23) 23232-3232
        public string? PasswordHash { get; set; }
        public string? Salt { get; set; }
        public string? UserImage { get; set; }
        public string? Token { get; set; }
        public ICollection<UserAddressDTO> UserAddressDTOs { get; set; } = new List<UserAddressDTO>();

        public UserDTO(Guid? id, string? name, string? lastName, string? gender, string? email, DateTime? birthDate, 
            string? cpf, string? cellPhone, string? passwordHash, string? salt, string? userImage, ICollection<UserAddressDTO> userAddressDTOs)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Gender = gender;
            Email = email;
            BirthDate = birthDate;
            Cpf = cpf;
            CellPhone = cellPhone;
            PasswordHash = passwordHash;
            Salt = salt;
            UserImage = userImage;
            UserAddressDTOs = userAddressDTOs;
        }

        public UserDTO()
        {
        }

        public Guid? GetId() => Id;
        public void SetId(Guid? id) => Id = id;

        public string? GetName() => Name;
        public void SetName(string? name) => Name = name;

        public string? GetLastName() => LastName;
        public void SetLastName(string? lastName) => LastName = lastName;

        public string? GetEmail() => Email;
        public void SetEmail(string? email) => Email = email;

        public string? GetToken() => Token;
        public void SetToken(string? token) => Token = token;
    }
}
