using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA.Domain.Models
{
    public class SmtpVM
    {
        public string SmtpHost { get; set; } = null!;
        public int SmtpPort { get; set; }
        public bool SslEnabled { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string ToEmail { get; set; } = null!;
        public string ToEmailDisplayName { get; set; } = null!;
    }
}
