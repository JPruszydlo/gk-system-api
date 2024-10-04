namespace gk_system_api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool ResetPassword { get; set; }
        public bool Active { get; set; }
        public string ResetToken { get; set; }
        public long ResetTokenExpiredAt { get; set; }

    }
}
