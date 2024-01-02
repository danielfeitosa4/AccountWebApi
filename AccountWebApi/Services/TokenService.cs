using AccountWebApi.Model;
using AccountWebApi.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AccountWebApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audienc"];

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: new []
                {
                    new Claim(type: "id", user.Id.ToString()),
                    new Claim(type: ClaimTypes.Name, user.Name),
                    new Claim(type: ClaimTypes.Email, user.Email),
                    new Claim(type: "cpf", user.CPF),
                    new Claim(type: ClaimTypes.Role, user.Roles),
                },
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: signinCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }

        /*
        private static ClaimsIdentity GenerateClaims(User user)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim("Id", user.Id.ToString()));
            ci.AddClaim(new Claim(ClaimTypes.Name, user.Email));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            ci.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));

            foreach (var role in user.Roles)
            {
                ci.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return ci;
        }
        */
    }
}
