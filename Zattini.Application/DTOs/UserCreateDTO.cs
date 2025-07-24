namespace Zattini.Application.DTOs
{
    public class UserCreateDTO
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? BirthDate { get; set; }
        public string? Cpf { get; set; } // 233.223.233-23
        public string? CellPhone { get; set; } // (23) 23232-3232
        public string? Password { get; set; }
        public string? UserImageBase64 { get; set; }
        public UserAddressDTO? UserAddressDTO { get; set; }

        public UserCreateDTO(string? name, string? lastName, string? gender, string? email,
            string? birthDate, string? cpf, string? cellPhone, string? password, string? userImageBase64, UserAddressDTO? userAddressDTO)
        {
            Name = name;
            LastName = lastName;
            Gender = gender;
            Email = email;
            BirthDate = birthDate;
            Cpf = cpf;
            CellPhone = cellPhone;
            Password = password;
            UserImageBase64 = userImageBase64;
            UserAddressDTO = userAddressDTO;
        }

        public UserCreateDTO()
        {
        }
    }
}
