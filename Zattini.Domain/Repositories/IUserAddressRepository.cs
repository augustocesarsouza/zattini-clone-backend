using Zattini.Domain.Entities;

namespace Zattini.Domain.Repositories
{
    public interface IUserAddressRepository : IGenericRepository<UserAddress>
    {
        public Task<UserAddress?> GetUserAddressById(Guid? userAddressId);
    }
}
