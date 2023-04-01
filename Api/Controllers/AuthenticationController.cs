using Application.Services.Authentication;
using Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService; 

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterRequest request)
        {
            var authResult = _authenticationService.Register(request.FistName, request.LastName, request.Email, request.Password);
            var authResponse = new AuthenticationResponse(authResult.user.Id, authResult.user.FirstName, authResult.user.LastName, authResult.user.Email, authResult.Token);
            return Ok(authResponse);
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginRequest request)
        {
            return Ok(request);
        }
    }
}
