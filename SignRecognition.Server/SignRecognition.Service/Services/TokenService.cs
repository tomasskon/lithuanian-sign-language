using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SignRecognition.Domain.Configurations;
using SignRecognition.Domain.Interfaces;
using SignRecognition.Domain.Models;

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

        public string GetJwtToken(User user)
        {
            var tokenKey = Encoding.UTF8.GetBytes(_jwtConfiguration.Key);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature),
                Claims = new Dictionary<string, object>
                {
                    { "Id", user.Id },
                    { "FirstName", user.FirstName },
                    { "LastName", user.LastName },
                    { "Email", user.Email }
                }
            };
            
            var token = _tokenHandler.CreateToken(tokenDescriptor);
            return _tokenHandler.WriteToken(token);
        }

        public Guid GetUserIdFromToken(string jwtToken)
        {
            var jsonToken = _tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;

            return Guid.Parse(jsonToken.Claims.Single(x => x.Type == "Id").Value);
        }
    }
}