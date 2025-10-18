using Microsoft.IdentityModel.Tokens; // To handle cryptography and signing of JWTs.
using System.IdentityModel.Tokens.Jwt; // To create and read JWT tokens.
using System.Security.Claims;
using System.Text;

namespace CoreBackend.Features.auth
{
    public class JwtService
    {
        private readonly IConfiguration _config;
        public JwtService(IConfiguration config) { 
            _config = config; 
        }
        
        public string GenerateToken(string userId, string role)
        {
            // Get secret key from config or fallback constant
            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? Consts.DEFAULT_KEY)
            );

            // Create signing credentials using the HMAC SHA-256 algorithm
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Add user info to token payload
            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId), // // User ID
                new Claim(ClaimTypes.Role, role) // User role
            };

            // Build the JWT
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"] ?? Consts.DEFAULT_ISSUER, // Who made it
                audience: _config["Jwt:Audience"] ?? Consts.DEFAULT_AUDIENCE, // Who can use it
                claims: claims, // Data inside
                expires: DateTime.UtcNow.AddDays(Consts.TOKEN_VALIDITY_DAYS), // When it expires
                signingCredentials: creds
            );
            // Turn token object into a string we can send to the client
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            // Turn the secret key (string) into bytes so it can be used for checking the token
            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? Consts.DEFAULT_KEY)
            );

            // This class knows how to read and check JWT tokens
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                TokenValidationParameters parameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, // check who created the token
                    ValidateAudience = true, // check who the token is for
                    ValidateLifetime = true, // make sure the token is not expired
                    ValidateIssuerSigningKey = true, // make sure the token was signed with our key
                    ValidIssuer = _config["Jwt:Issuer"] ?? Consts.DEFAULT_ISSUER, // expected creator
                    ValidAudience = _config["Jwt:Audience"] ?? Consts.DEFAULT_AUDIENCE,  // expected user
                    IssuerSigningKey = key, // our secret key
                    ClockSkew = TimeSpan.Zero // No tolerance for expired tokens
                };

                // Try to check and decode the token
                // If it's good, we get the user's info (claims) from it
                return tokenHandler.ValidateToken(token, parameters, out _);
            }
            catch
            {
                // If the token is bad, expired, or changed, return null
                return null;
            }
        }
    }
}
