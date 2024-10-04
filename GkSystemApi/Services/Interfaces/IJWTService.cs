using gk_system_api.Models;
using System.IdentityModel.Tokens.Jwt;

namespace gk_system_api.Services.Interfaces
{
    public interface IJWTService
    {
        LoginToken GetToken(DecodedUrl credentials, int? expirationInMins = null);
    }
}
