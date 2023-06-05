using Application.Common;
using Application.Interfaces.Authentication;
using Application.Interfaces.Persistence;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;


namespace Application.Authentication.Queries
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetUserByEmail(query.Email);

            if (user is null)
            {
                return Errors.Authentication.InvalidCredential;
            }

            if (user.Password != query.Password)
            {
                return Errors.Authentication.InvalidPassword;
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
