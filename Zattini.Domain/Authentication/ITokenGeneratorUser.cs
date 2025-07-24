using Zattini.Domain.Entities;
using Zattini.Domain.InfoErrors;

namespace Zattini.Domain.Authentication
{
    public interface ITokenGeneratorUser
    {
        InfoErrors<TokenOutValue> Generator(User user);
        InfoErrors<TokenOutValue> GeneratorTokenUrlChangeEmail(User user);
    }
}
