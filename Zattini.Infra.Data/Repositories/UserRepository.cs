using Microsoft.EntityFrameworkCore;
using System.Net;
using Zattini.Domain.Entities;
using Zattini.Domain.Repositories;
using Zattini.Infra.Data.Context;

namespace Zattini.Infra.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetUserById(Guid? userId)
        {
            var user = await _context
                 .Users
                 .Where(u => u.Id == userId)
                 .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetUserInfoToLogin(string email)
        {
            var user = await _context 
                 .Users
                 .Where(u => u.Email == email)
                 .Select(x => new User(x.Id, x.Name, null, null, x.Email, null, null, null, x.PasswordHash, x.Salt, null, new List<UserAddress>()))
                 .FirstOrDefaultAsync();

            return user;
        }
    }
}
