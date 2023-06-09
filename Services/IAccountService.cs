using JWTTokenDemo.DTO;

namespace JWTTokenDemo.Services
{
    public interface IAccountService
    {
        TokenDTO GetAuthTokens(LoginDTO login);
    }
}
