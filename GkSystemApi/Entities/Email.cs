namespace gk_system_api.Entities
{
    public class Email
    {
        public int Id { get; set; }
        public string EmailContent { get; set; }
        public string SenderName { get; set; }
        public string SenderSurname { get; set; }
        public string SenderPhone { get; set; }
        public string SenderEmail { get; set; }
        public string SenderCity { get; set; }
        public string SenderPostal { get; set; }
        public string SenderCountry { get; set; }
        public string SenderCountryCode { get; set; }
        public string SenderState { get; set; }
        public long SendAt { get; set; }
    }
}
