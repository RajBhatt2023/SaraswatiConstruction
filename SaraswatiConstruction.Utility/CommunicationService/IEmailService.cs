using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaraswatiConstruction.Utility.CommunicationService
{
    public interface IEmailService
    {
        public bool SendEmail(string? toEmail, StringBuilder body, string subject);
    }
}
