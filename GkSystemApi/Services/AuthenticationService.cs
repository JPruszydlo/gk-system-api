using gk_system_api.Models;

namespace gk_system_api.Services.Interfaces
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJWTService _jwtService;
        private readonly IDatabaseService _db;
        private readonly ILogger _log;

        public AuthenticationService(IJWTService jwtService, IDatabaseService db, ILogger<AuthenticationService> log)
        {
            _jwtService = jwtService;
            _db = db;
            _log = log;
        }

        public LoginToken AuthenticateUser(DecodedUrl user)
            => _jwtService.GetToken(user);

        public LoginToken AuthorizeClient(DecodedUrl user)
            => _jwtService.GetToken(user, 10);

        public bool SetNewPassword(DecodedUrl credentials, string token)
        {
            try
            {
                if (credentials == null)
                    return false;

                var user = _db.GetUserByLogin(credentials.Login);
                if (user == null)
                    return false;

                if (user.ResetToken != token)
                    return false;

                if (DateTime.Now.Ticks > user.ResetTokenExpiredAt)
                    return false;

                user.Password = credentials.Password;
                user.ResetToken = string.Empty;
                user.ResetTokenExpiredAt = 0;

                _db.UpdateModel(user);
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return false;
            }
        }
    }
}
