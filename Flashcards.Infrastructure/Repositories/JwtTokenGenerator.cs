using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Flashcards.Infrastructure.Repositories
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GenerateToken(User user)
        {
            // Generate signing key from configured secret
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured.")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);             // Define how the token should be signed

            // Define claims to include in the token payload
            // Claims are pieces of information about the user (like user ID, email, roles) 
            // that are encoded into the JWT token. They help the server identify the user and 
            // their permissions without needing to query the database on every request.
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())  // Unique identifier for the token
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],           // Issuer of the token
                audience: _configuration["Jwt:Audience"],       // Audience for the token
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds);                     // Create the token with specified claims, expiration, and signing credentials

            return new JwtSecurityTokenHandler().WriteToken(token);              // Return the token as a serialized string

        }
    }
}
