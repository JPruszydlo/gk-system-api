namespace gk_system_api.Models
{
    public class LoginToken
    {
        public string Token { get; set; }
        public int ExpiresInMin { get; set; }
    }
}
