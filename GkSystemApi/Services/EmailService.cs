using gk_system_api.Entities;
using gk_system_api.Models;
using gk_system_api.Services.Interfaces;
using MailKit.Security;
using MimeKit;
using MimeKit.Utils;

namespace gk_system_api.Services
{
    public enum Mailbody
    {
        ThanksMailbody,
        StandardMailbody,
        ResetPassMailbody
    }
    public class EmailService : IEmailService
    {
        private Dictionary<Mailbody, string> _emailBodies = new Dictionary<Mailbody, string>
        {
            { Mailbody.ThanksMailbody, "thanksmailbody.html" },
            { Mailbody.StandardMailbody, "mailbody.html" },
            { Mailbody.ResetPassMailbody, "resetpassmailbody.html" }
        };

        private readonly IConfiguration _config;
        private readonly IDatabaseService _db;
        private readonly ILogger _log;

        public EmailService(IConfiguration config, IDatabaseService db, ILogger<EmailService> log)
        {
            _config = config;
            _db = db;
            _log = log;
        }

        public bool SendGkSystemEmails(FullEmailViewModel fullModel)
        {
            try
            {
                var email = new Email()
                {
                    EmailContent = fullModel.Email.Text,
                    SenderEmail = fullModel.Email.Email,
                    SenderName = fullModel.Email.Name,
                    SenderSurname = fullModel.Email.Surname,
                    SenderPhone = fullModel.Email.Phone,
                    SenderCity = fullModel.Visitor.Localisation.City,
                    SenderCountry = fullModel.Visitor.Localisation.country_name,
                    SenderCountryCode = fullModel.Visitor.Localisation.country_code,
                    SenderPostal = fullModel.Visitor.Localisation.Postal,
                    SenderState = fullModel.Visitor.Localisation.State,
                    SendAt = DateTime.Now.Ticks
                };
                _db.SaveModel(email);

                var model = fullModel.Email;
                SendEmail(
                    new MailboxAddress($"{model.Name} {model.Surname}", _config["EmailService:RECEIVER_EMAIL"]),
                    Mailbody.StandardMailbody,
                    new Dictionary<string, string>
                    {
                        {"[NAME]", model.Name },
                        {"[SURNAME]", model.Surname },
                        {"[CONTENT]", model.Text },
                        {"[EMAIL]", model.Email },
                        {"[PHONE]", model.Phone },
                    }
                );

                if (!string.IsNullOrEmpty(model.Email))
                {
                    var cfg = _db.GetGeneralConfig();
                    SendEmail(
                        new MailboxAddress($"{model.Name} {model.Surname}", model.Email),
                        Mailbody.ThanksMailbody,
                        new Dictionary<string, string>
                        {
                            {"[NAME]", model.Name },
                            {"[SURNAME]", model.Surname },
                            {"[CONTENT]", model.Text },
                            {"[cHttp]", cfg["mainHttp"].Value },
                            {"[cName]", cfg["companyName"].Value },
                            {"[cCeo]", cfg["ceoName"].Value },
                            {"[cPostCode]", cfg["postCode"].Value },
                            {"[cCity]", cfg["city"].Value },
                            {"[cStreet]", cfg["street"].Value },
                            {"[cHouseNumber]", cfg["houseNumber"].Value },
                            {"[cFlatNumber]", string.IsNullOrEmpty(cfg["flatNumber"].Value) ? string.Empty : $" / {cfg["flatNumber"].Value}" },
                            {"[cDistrict]", string.IsNullOrEmpty(cfg["district"].Value) ? string.Empty : $"woj. {cfg["district"].Value}" },
                            {"[cPhone]", cfg["phone"].Value },
                            {"[cEmail]", cfg["email"].Value },
                        },
                        emailTitle: $"Dziękujemy za wysłanie zapytania ({DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")})"
                    );
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return false;
            }
        }

        private void SendEmail(MailboxAddress receiver, Mailbody mailbody, Dictionary<string, string> dataToReplace, string? emailTitle = null, MailboxAddress? sender = null)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(
                    _config["EmailService:HOST"],
                    int.Parse(_config["EmailService:PORT"]),
                    SecureSocketOptions.StartTls
                );
                client.Authenticate(
                    _config["EmailService:USER"],
                    _config["EmailService:PASSWORD"]
                );

                var message = GetMimeMessage(receiver, mailbody, dataToReplace, emailTitle);

                client.Send(message);
                client.Disconnect(true);
            }
        }

        public bool SendPortfolioEmail(Contact contact)
        {
            try
            {
                SendEmail(
                    new MailboxAddress($"Jakub Pruszydło", "jakub.pruszydlo@gmail.com"),
                    Services.Mailbody.StandardMailbody,
                    new Dictionary<string, string>
                    {
                        {"[NAME]", contact.Name },
                        {"[CONTENT]", contact.Message },
                        {"[SURNAME]", "-" },
                        {"[EMAIL]", contact.Email },
                        {"[PHONE]", contact.Phone },
                    },
                    emailTitle: $"Ktoś do ciebie napisał ({DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")})",
                    sender: new MailboxAddress("PORTFOLIO", "contact@myshort.pl")
                );
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return false;
            }
        }

        public bool SendGkSystemResetLink(ResetPassRequestModel resetModel)
        {
            try
            {
                var expiredAt = DateTime.Now.AddMinutes(10);
                var token = Guid.NewGuid().ToString();
                var resetLink = $"http://admin.gk-system.myshort.pl/reset-password?token={token}";

                var user = _db.GetUserByEmail(resetModel.UserName, resetModel.Email);
                if (user == null)
                    return false;

                user.ResetToken = token;
                user.ResetTokenExpiredAt = expiredAt.Ticks;

                if (!_db.UpdateModel(user))
                    return false;

                SendEmail(
                    new MailboxAddress(resetModel.UserName, resetModel.Email),
                    Mailbody.ResetPassMailbody,
                    new Dictionary<string, string>{
                        {"[RESET_LINK]", resetLink}
                    },
                    emailTitle: "Resetowanie hasła"
                );
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return false;
            }
        }

        private MimeMessage GetMimeMessage(MailboxAddress receiver, Mailbody mailbody, Dictionary<string, string> dataToReplace, string? emailTitle = null)
        {
            var templateFileName = _emailBodies[mailbody];
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_config["EmailService:SENDER_NAME"], _config["EmailService:SENDER_EMAIL"]));
            message.To.Add(receiver);
            message.Subject = emailTitle ?? $"Zarejestrowano nowe zapytanie ({DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")})";

            var builder = new BodyBuilder();

            string mailContent = File.ReadAllText(templateFileName);
            if (mailContent.Contains("[CID]"))
            {
                var image = builder.LinkedResources.Add("logo2_black.png");
                image.ContentId = MimeUtils.GenerateMessageId();
                mailContent = mailContent.Replace("[CID]", image.ContentId);
            }

            foreach (var key in dataToReplace.Keys)
            {
                mailContent = mailContent.Replace(key, dataToReplace[key]);
            }
            builder.HtmlBody = mailContent;
            message.Body = builder.ToMessageBody();

            return message;
        }
    }
}
