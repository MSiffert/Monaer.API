using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Monager.DTOs;
using Monager.Services;

namespace Monager.Controllers
{
    [Route("api/authenticate")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<ActionResult> Authenticate([FromBody]Credentials credentials)
        {
            var token = await _authenticationService.AuthenticateAsync(credentials.Key);
            return Ok(token);
        }
    }
}
