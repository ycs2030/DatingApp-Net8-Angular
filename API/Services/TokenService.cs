global using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;

namespace API.Services;

    public class TokenService(IConfiguration config) : ITokenService
    {
        public string CreateToken(AppUser user)
        {
            var tokenKey = config["TokenKey"]
            ?? throw new Exception("Token key not found");

            if(tokenKey.Length <64)
               throw new Exception("Token: key is too short");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(tokenKey)
            );

            var credentials = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var creds = new SigningCredentials
            (key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(credentials),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
