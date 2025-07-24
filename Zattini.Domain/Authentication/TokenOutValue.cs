namespace Zattini.Domain.Authentication
{
    public class TokenOutValue
    {
        public string? Acess_Token { get; private set; }
        public DateTime Expirations { get; private set; }

        public TokenOutValue()
        {
        }

        public bool ValidateToken(string? acess_token, DateTime expirations)
        {
            if (string.IsNullOrEmpty(acess_token))
            {
                return false;
            }

            Acess_Token = acess_token;
            Expirations = expirations;

            return true;
        }
    }
}
