using JWTTokenDemo.DTO;
using JWTTokenDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTTokenDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Route("test-auth")]
        [Authorize]
        public IActionResult GetTest()
        {
            return Ok("Only authenticated user can consume this endpoint");
        }

        [Route("login-token")]
        [HttpPost]
        public IActionResult GetLoginToken(LoginDTO login)
        {
            var result = _accountService.GetAuthTokens(login);
            if (result == null)
            {
                return ValidationProblem("invalid credentials");
            }
            return Ok(result);
        }
    }
}
