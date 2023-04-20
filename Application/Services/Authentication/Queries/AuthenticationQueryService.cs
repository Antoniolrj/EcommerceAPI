using Application.Interfaces.Authentication;
using Application.Interfaces.Persistence;
using Domain.Common.Errors;
using Domain.Entities;
using ErrorOr;

namespace Application.Services.Authentication.Commands
{
    public class AuthenticationQueryService : IAuthenticationQueryService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);

            if (user is null)
            {
                return Errors.Authentication.InvalidCredential;
            }

            if (user.Password != password)
            {
                return Errors.Authentication.InvalidPassword;
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }

    }
}
