using gk_system_api.Models;
using MimeKit;
using System.Collections.Generic;

namespace gk_system_api.Services.Interfaces
{
    public interface IEmailService
    {
        bool SendGkSystemEmails(FullEmailViewModel fullModel);

        bool SendPortfolioEmail(Contact contact);

        bool SendGkSystemResetLink(ResetPassRequestModel resetModel);
    }
}
