using Domain.Entities;

namespace Application.Common
{
    public record AuthenticationResult(User User, string Token);
}
