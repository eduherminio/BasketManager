using BasketManager.Service;
using Configuration.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace BasketManager.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        public readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        /// <summary>
        /// Login (no pwd)
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost]
        public string GenerateToken(string username)
        {
            return _loginService.GenerateToken(username);
        }

        /// <summary>
        /// RenewToken
        /// </summary>
        /// <returns></returns>
        [JwtTokenRequired]
        [HttpPost("renew")]
        public string RenewToken()
        {
            return _loginService.RenewToken(HttpContext.Request.Headers["Authorization"]);
        }
    }
}
