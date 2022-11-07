using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using unitofwork_core.Entities;

namespace unitofwork_core.Helper
{
    public interface IJWTHelper {
        string generateJwtToken(Actor actor, string roleName);
    }
    public class JWTHelper : IJWTHelper
    {
        private readonly IConfiguration _configuration;
        public JWTHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string generateJwtToken(Actor actor, string roleName)
        {
            JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            SecurityTokenDescriptor tokenDescription = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, actor.UserName),
                    new Claim(ClaimTypes.Email, actor.Email),
                    new Claim("id", actor.Id.ToString()),
                    new Claim("role", roleName)
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
