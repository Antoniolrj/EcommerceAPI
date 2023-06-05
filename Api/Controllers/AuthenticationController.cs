using Contracts.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Domain.Common.Errors;
using Application.Services.Authentication.Commands;
using MediatR;
using Application.Common;
using Application.Authentication.Commands.Register;
using Application.Authentication.Queries;

namespace Api.Controllers
{
    [Route("[controller]")]

    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterCommand(
                request.FistName,
                request.LastName,
                request.Email,
                request.Password);

            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

            return authResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors));
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = new LoginQuery(request.Email, request.Password);

            var authResult = await _mediator.Send(query);

            if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredential)
            {
                return Problem(
                    statusCode: StatusCodes.Status401Unauthorized, 
                    title: authResult.FirstError.Description);
            }   

            return authResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors));
        }

        private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(
                authResult.User.Id, 
                authResult.User.FirstName, 
                authResult.User.LastName, 
                authResult.User.Email, 
                authResult.Token);
        }
    }
}
