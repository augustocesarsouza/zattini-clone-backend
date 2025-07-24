namespace Zattini.Domain.Entities
{
    public class UserAddress
    {
        public Guid? Id { get; private set; }
        public string? Cep { get; private set; } // 23232-323
        public string? TypeAddress { get; private set; }
        public string? Address { get; private set; }
        public int? Number { get; private set; }
        public string? Complement { get; private set; }
        public string? Neighborhood { get; private set; }
        public string? State { get; private set; }
        public string? City { get; private set; }
        public string? ReferencePoint { get; private set; }
        public Guid? UserId { get; private set; }
        public User? User { get; private set; }

        public UserAddress(Guid? id, string? cep, string? typeAddress, string? address, int? number, string? complement, 
            string? neighborhood, string? state, string? city, string? referencePoint, Guid? userId, User? user)
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
            User = user;
        }

        public UserAddress()
        {
        }

        public Guid? GetId() => Id;
        public void SetId(Guid? value) => Id = value;

        public string? GetCep() => Cep;
        public void SetCep(string? cep) => Cep = cep;

        public string? GetTypeAddress() => TypeAddress;
        public void SetTypeAddress(string? typeAddress) => TypeAddress = typeAddress;

        public string? GetAddress() => Address;
        public void SetAddress(string? address) => Address = address;

        public int? GetNumber() => Number;
        public void SetNumber(int? number) => Number = number;

        public string? GetComplement() => Complement;
        public void SetComplement(string? complement) => Complement = complement;

        public string? GetNeighborhood() => Neighborhood;
        public void SetNeighborhood(string? neighborhood) => Neighborhood = neighborhood;

        public string? GetState() => State;
        public void SetState(string? state) => State = state;

        public string? GetCity() => City;
        public void SetCity(string? city) => City = city;

        public string? GetReferencePoint() => ReferencePoint;
        public void SetReferencePoint(string? referencePoint) => ReferencePoint = referencePoint;

        public Guid? GetUserId() => UserId;
        public void SetUserId(Guid? userId) => UserId = userId;

        public User? GetUser() => User;
        public void SetUser(User? user) => User = user;
    }
}
