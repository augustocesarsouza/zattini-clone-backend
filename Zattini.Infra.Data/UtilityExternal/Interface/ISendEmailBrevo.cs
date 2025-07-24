

using Zattini.Domain.Entities;
using Zattini.Domain.InfoErrors;

namespace Zattini.Infra.Data.UtilityExternal.Interface
{
    public interface ISendEmailBrevo
    {
        public InfoErrors SendEmail(User user, string url);
        public InfoErrors SendCode(User user, int codeRandon);
        public InfoErrors SendUrlChangePassword(User user, string? token);
    }
}
