using gk_system_api.Models;
using gk_system_api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace gk_system_api.Services
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _config;
        public JWTService(IConfiguration config)
        {
            _config = config;
        }
        public LoginToken GetToken(DecodedUrl credentials, int? expirationInMins = null)
        {
            var token = new JwtSecurityToken(
                      issuer: credentials.Login,
                      audience: _config["JwtSettings:Audience"],
                      claims: GetClaims(credentials),
                      expires: DateTime.Now.AddMinutes(expirationInMins ?? Convert.ToDouble(_config["JwtSettings:ExpirationTimeInMinutes"])),
                      signingCredentials: GetSigningCredentials()
                  );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return new LoginToken()
            {
                ExpiresInMin = expirationInMins ?? Convert.ToInt32(_config["JwtSettings:ExpirationTimeInMinutes"]),
                Token = jwt
            };
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_config["JwtSettings:SecurityKey"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private List<Claim> GetClaims(DecodedUrl user)
            => new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Login),
            };
    }
}
