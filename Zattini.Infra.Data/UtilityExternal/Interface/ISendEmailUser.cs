using Zattini.Domain.Entities;
using Zattini.Domain.InfoErrors;

namespace Zattini.Infra.Data.UtilityExternal.Interface
{
    public interface ISendEmailUser
    {
        public Task<InfoErrors> SendEmail(User user);
        public InfoErrors SendCodeRandom(User user, int code);
        public InfoErrors SendUrlChangePassword(User user, string? token);
    }
}
