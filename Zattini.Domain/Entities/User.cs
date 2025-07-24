using System.Net;

namespace Zattini.Domain.Entities
{
    public class User
    {// fazer as coisa normal validação e tal FAÇA AS VALIDAÇÃO IGUAL DO FRONT NÃO DEPENDE APENAS DO FROT PARA AS VALIDAÇÃO
        public Guid? Id { get; private set; }
        public string? Name { get; private set; }
        public string? LastName { get; private set; }
        public string? Gender { get; private set; }
        public string? Email { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public string? Cpf { get; private set; } // 233.223.233-23
        public string? CellPhone { get; private set; } // (23) 23232-3232
        public string? PasswordHash { get; private set; }
        public string? Salt { get; private set; }
        public string? UserImage { get; private set; }
        public ICollection<UserAddress> UserAddresses { get; private set; } = new List<UserAddress>();

        public User(Guid? id, string? name, string? lastName, string? gender, string? email, DateTime? birthDate,
            string? cpf, string? cellPhone, string? passwordHash, string? salt, string? userImage, ICollection<UserAddress> userAddresses)
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
            UserAddresses = userAddresses;
        }

        public User()
        {
        }

        public Guid? GetId() => Id;
        public void SetId(Guid? id) => Id = id;

        public string? GetName() => Name;
        public void SetName(string? name) => Name = name;

        public string? GetLastName() => LastName;
        public void SetLastName(string? lastName) => LastName = lastName;

        public string? GetGender() => Gender;
        public void SetGender(string? gender) => Gender = gender;

        public string? GetEmail() => Email;
        public void SetEmail(string? email) => Email = email;

        public DateTime? GetBirthDate() => BirthDate;
        public void SetBirthDate(DateTime? birthDate) => BirthDate = birthDate;

        public string? GetCpf() => Cpf;
        public void SetCpf(string? cpf) => Cpf = cpf;

        public string? GetCellPhone() => CellPhone;
        public void SetCellPhone(string? cellPhone) => CellPhone = cellPhone;

        public string? GetPasswordHash() => PasswordHash;
        public void SetPasswordHash(string? passwordHash) => PasswordHash = passwordHash;

        public string? GetSalt() => Salt;
        public void SetSalt(string? salt) => Salt = salt;

        public string? GetUserImage() => UserImage;
        public void SetUserImage(string? userImage) => UserImage = userImage;

        public ICollection<UserAddress> GetUserAddresses() => UserAddresses;
        public void SetUserAddresses(ICollection<UserAddress> addresses) => UserAddresses = addresses;
    }
}
