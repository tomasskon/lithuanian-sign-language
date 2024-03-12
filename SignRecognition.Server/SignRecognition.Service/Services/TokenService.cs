using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SignRecognition.Domain.Configurations;
using SignRecognition.Domain.Interfaces;

namespace SignRecognition.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtTokenConfiguration _jwtConfiguration;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public TokenService(JwtTokenConfiguration jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration;

            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public string GetJwtToken(Guid userId)
        {
            var tokenKey = Encoding.UTF8.GetBytes(_jwtConfiguration.Key);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature),
                Claims = new Dictionary<string, object>
                {
                    { "UserId", userId },
                }
            };
            
            var token = _tokenHandler.CreateToken(tokenDescriptor);
            return _tokenHandler.WriteToken(token);
        }
    }
}