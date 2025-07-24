using Zattini.Domain.Entities;

namespace Zattini.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User?> GetUserById(Guid? userId);
        public Task<User?> GetUserInfoToLogin(string email);
    }
}
