using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zattini.Domain.Authentication;
using Zattini.Domain.Entities;
using Zattini.Domain.InfoErrors;

namespace Zattini.Infra.Data.Authentication
{
    public class TokenGeneratorUser : ITokenGeneratorUser
    {
        private readonly IConfiguration _configuration;

        public TokenGeneratorUser(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public InfoErrors<TokenOutValue> Generator(User user)
        {
            if (string.IsNullOrEmpty(user.Email))
                return InfoErrors.Fail(new TokenOutValue(), "Email null or empty");

            if (user == null)
                return InfoErrors.Fail(new TokenOutValue(), "user is null");

            var userId = user.Id.ToString();

            if (userId == null)
                return InfoErrors.Fail(new TokenOutValue(), "userId is null");

            var claims = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim("userId", userId),
            };

            var keySecret = Environment.GetEnvironmentVariable("KEY_JWT") ?? _configuration["Key:Jwt"];

            if (string.IsNullOrEmpty(keySecret) || keySecret.Length < 16)
                return InfoErrors.Fail(new TokenOutValue(), "error token related");

            var expires = DateTime.UtcNow.AddHours(5);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keySecret));
            var tokenData = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                expires: expires,
                claims: claims);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenData);
            var tokenValue = new TokenOutValue();
            var sucessfullyCreatedToken = tokenValue.ValidateToken(token, expires);

            if (sucessfullyCreatedToken)
            {
                return InfoErrors.Ok(tokenValue);
            }

            return InfoErrors.Fail(new TokenOutValue(), "error when creating token");
        }

        public InfoErrors<TokenOutValue> GeneratorTokenUrlChangeEmail(User user)
        {
            if (string.IsNullOrEmpty(user.Email))
                return InfoErrors.Fail(new TokenOutValue(), "Email null or empty");

            if (user == null)
                return InfoErrors.Fail(new TokenOutValue(), "user is null");

            var userId = user.Id.ToString();

            if (userId == null)
                return InfoErrors.Fail(new TokenOutValue(), "userId is null");

            var claims = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim("userId", userId),
            };

            var keySecret = Environment.GetEnvironmentVariable("KEY_JWT") ?? _configuration["Key:Jwt"];

            if (string.IsNullOrEmpty(keySecret) || keySecret.Length < 16)
                return InfoErrors.Fail(new TokenOutValue(), "error token related");

            var expires = DateTime.UtcNow.AddHours(1);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keySecret));
            var tokenData = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                expires: expires,
                claims: claims);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenData);
            var tokenValue = new TokenOutValue();
            var sucessfullyCreatedToken = tokenValue.ValidateToken(token, expires);

            if (sucessfullyCreatedToken)
            {
                return InfoErrors.Ok(tokenValue);
            }

            return InfoErrors.Fail(new TokenOutValue(), "error when creating token");
        }
    }
}
