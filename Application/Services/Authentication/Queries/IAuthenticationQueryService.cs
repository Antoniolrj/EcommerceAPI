using Application.Services.Authentication.Common;
using ErrorOr;


namespace Application.Services.Authentication.Commands
{
    public interface IAuthenticationQueryService
    {
        ErrorOr<AuthenticationResult> Login(string email, string password);
    }
}
