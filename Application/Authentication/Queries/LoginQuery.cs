using Application.Common;
using ErrorOr;
using MediatR;


namespace Application.Authentication.Queries
{
    public record LoginQuery(
        string Email,
        string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}
