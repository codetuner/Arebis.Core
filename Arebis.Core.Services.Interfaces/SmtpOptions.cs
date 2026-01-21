using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Services.Interfaces
{
    /// <summary>
    /// SMTP options for email sending.
    /// </summary>
    public class SmtpOptions
    {
        /// <summary>
        /// Default section name in configuration files.
        /// </summary>
        public const string SectionName = "Smtp";

        /// <summary>
        /// SMTP server address.
        /// </summary>
        public string? Server { get; set; }

        /// <summary>
        /// SMTP server port.
        /// </summary>
        public int Port { get; set; } = 25;
        
        /// <summary>
        /// Default sender name.
        /// </summary>
        public string? SenderName { get; set; }
        
        /// <summary>
        /// Default sender receiver.
        /// </summary>
        public string? SenderEmail { get; set; }

        /// <summary>
        /// SMTP server login username.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// SMTP server login password.
        /// </summary>
        public string? Password { get; set; }
    }
}
