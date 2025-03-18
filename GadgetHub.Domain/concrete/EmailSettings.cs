using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetHub.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress { get; set; } = "orders@example.com";
        public string MailFromAddress { get; set; } = "store@example.com";
        public bool UseSsl { get; set; } = true;
        public string Username { get; set; } = "your-email@example.com";
        public string Password { get; set; } = "your-email-password";
        public string ServerName { get; set; } = "smtp.example.com";
        public int ServerPort { get; set; } = 587;
        public bool WriteAsFile { get; set; } = false;
        public string FileLocation { get; set; } = @"C:\sports_store_emails";
    }

}
