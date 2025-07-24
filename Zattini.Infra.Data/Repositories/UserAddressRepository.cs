using Microsoft.EntityFrameworkCore;
using Zattini.Domain.Entities;
using Zattini.Domain.Repositories;
using Zattini.Infra.Data.Context;

namespace Zattini.Infra.Data.Repositories
{
    public class UserAddressRepository : GenericRepository<UserAddress>, IUserAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public UserAddressRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserAddress?> GetUserAddressById(Guid? userAddressId)
        {
            var userAddress = await _context
                 .UserAddress
                 .Where(u => u.Id == userAddressId)
                 .FirstOrDefaultAsync();

            return userAddress;
        }
    }
}
