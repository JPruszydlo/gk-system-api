using gk_system_api.Models;

namespace gk_system_api.Services.Interfaces
{
    public interface IAuthenticationService
    {
        bool SetNewPassword(DecodedUrl credentials, string token);

        LoginToken AuthenticateUser(DecodedUrl user);
        LoginToken AuthorizeClient(DecodedUrl user);
    }
}
