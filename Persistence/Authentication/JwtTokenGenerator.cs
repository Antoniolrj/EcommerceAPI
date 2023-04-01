using Application.Interfaces;
using Application.Interfaces.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Persistence.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {

        private readonly JwtSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;

        public JwtTokenGenerator(IDateTimeService dateTimeService, IOptions<JwtSettings> jwtSettings)
        {
            _dateTimeService = dateTimeService;
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(int userId, string firstName, string lastName)
        {
            var siginingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256
            );

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, firstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                expires: _dateTimeService.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: siginingCredentials,
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);

        }
    }
}
