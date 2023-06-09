using JWTTokenDemo.Data;
using JWTTokenDemo.Data.Entities;
using JWTTokenDemo.DTO;
using JWTTokenDemo.Settings.TokenSettings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTTokenDemo.Services
{
    public class AccountService : IAccountService
    {
        private readonly MyAuthContext _myAuthContext;
        private readonly TokenSettings _tokenSettings;
        public AccountService(MyAuthContext myAuthContext,
        IOptions<TokenSettings> tokenSettings)
        {
            _myAuthContext = myAuthContext;
            _tokenSettings = tokenSettings.Value;
        }


        private string CreateJwtToken(User user)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_tokenSettings.SecretKey)
            );
            var credentials = new SigningCredentials(
                symmetricSecurityKey,
                SecurityAlgorithms.HmacSha256
            );

            var userCliams = new Claim[]{
        new Claim("email", user.Email??""),
        new Claim("phone", user.PhoneNumber??""),
    };

            var jwtToken = new JwtSecurityToken(
                issuer: _tokenSettings.Issuer,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials,
                claims: userCliams,
                audience: _tokenSettings.Audience
            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }

        public TokenDTO GetAuthTokens(LoginDTO login)
        {

            User user = _myAuthContext.User
            .Where(_ => _.Email.ToLower() == login.Email.ToLower() &&
            _.Password == login.Password).FirstOrDefault();

            if (user != null)
            {
                var token = new TokenDTO
                {
                    AccessToken = CreateJwtToken(user)
                };
                return token;
            }
            return null;
        }
    }
}
