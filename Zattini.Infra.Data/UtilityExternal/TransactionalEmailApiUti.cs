using brevo_csharp.Api;
using brevo_csharp.Model;
using Zattini.Domain.InfoErrors;
using Zattini.Infra.Data.UtilityExternal.Interface;

namespace Zattini.Infra.Data.UtilityExternal
{
    public class TransactionalEmailApiUti : ITransactionalEmailApiUti
    {
        private readonly ITransactionalEmailsApi _transactionalEmailApi;

        public TransactionalEmailApiUti(ITransactionalEmailsApi transactionalEmailApi)
        {
            _transactionalEmailApi = transactionalEmailApi;
        }

        public InfoErrors SendTransacEmailWrapper(SendSmtpEmail sendSmtpEmail)
        {
            try
            {
                var createResult = _transactionalEmailApi.SendTransacEmail(sendSmtpEmail);
                if (createResult == null)
                    return InfoErrors.Fail("error not completed");

                return InfoErrors.Ok(createResult);
            }
            catch (Exception ex)
            {
                return InfoErrors.Fail($"Error exception, ${ex.Message}");
            }
        }
    }
}
