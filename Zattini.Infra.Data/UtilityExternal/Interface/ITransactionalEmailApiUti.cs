using brevo_csharp.Model;
using Zattini.Domain.InfoErrors;

namespace Zattini.Infra.Data.UtilityExternal.Interface
{
    public interface ITransactionalEmailApiUti
    {
        public InfoErrors SendTransacEmailWrapper(SendSmtpEmail sendSmtpEmail);
    }
}
