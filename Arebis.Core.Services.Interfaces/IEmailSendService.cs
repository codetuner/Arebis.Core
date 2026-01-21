using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Services.Interfaces
{
    /// <summary>
    /// Defines a service to send emails (via SMTP or other means).
    /// </summary>
    public interface IEmailSendService
    {
        /// <summary>
        /// Sends an email message.
        /// </summary>
        Task SendEmail(EmailMessage message);
    }

    /// <summary>
    /// Email send service convenience extension methods.
    /// </summary>
    public static class IEmailSendServiceExtensions
    {
        /// <summary>
        /// Sends an email message.
        /// </summary>
        public static async Task<EmailMessage> SendEmail(this IEmailSendService service, string recipientAddress, string subject, string body, string? contentType = "text/html")
        {
            var message = new EmailMessage(subject, body, contentType)
                .AddRecipient(recipientAddress);
            await service.SendEmail(message);
            return message;
        }
    }

    /// <summary>
    /// Representation of an email message.
    /// </summary>
    public class EmailMessage
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public EmailMessage()
        { }

        /// <summary>
        /// Convenience constructor.
        /// </summary>
        public EmailMessage(string subject, string body, string? contentType = "text/html")
        {
            Subject = subject;
            Body = body;
            ContentType = contentType;
        }

        /// <summary>
        /// Internal identifier of the message (optional).
        /// </summary>
        public string? Identifier { get; set; }

        /// <summary>
        /// Date/time the message was sent.
        /// </summary>
        public DateTime? TimeSent { get; set; }

        /// <summary>
        /// Sender of the message.
        /// </summary>
        public EmailMessageRecipient? Sender { get; set; }

        /// <summary>
        /// Recipients of the message (To, Cc and Bcc).
        /// </summary>
        public List<EmailMessageRecipient> Recipients { get; set; } = new();

        /// <summary>
        /// The subject of the message.
        /// </summary>
        public string? Subject { get; set; }

        /// <summary>
        /// The body of the message.
        /// </summary>
        public string? Body { get; set; }

        /// <summary>
        /// The MIME content type of the body content (e.g. "text/plain" or "text/html").
        /// </summary>
        public string? ContentType { get; set; }

        /// <summary>
        /// Attachments of the message.
        /// </summary>
        public List<EmailMessageAttachment> Attachments { get; set; } = new();

        /// <summary>
        /// Convenience method to add a recipient.
        /// </summary>
        public EmailMessage AddRecipient(string address, EmailMessageRecipientType recipientType = EmailMessageRecipientType.Receiver, string? name = null)
        {
            this.Recipients.Add(new EmailMessageRecipient
            {
                Address = address,
                Name = name,
                RecipientType = recipientType
            });
            return this;
        }

        /// <summary>
        /// Convenience method to add an attachment.
        /// </summary>
        public EmailMessage AddAttachment(string fileName, byte[] content, string? contentType = null, string? contentId = null)
        {
            this.Attachments.Add(new EmailMessageAttachment
            {
                FileName = fileName,
                Content = content,
                ContentType = contentType,
                ContentId = contentId
            });
            return this;
        }

        /// <summary>
        /// Convenience method to add an attachment.
        /// </summary>
        public EmailMessage AddAttachment(string fileName, string contentReference, string? contentType = null, string? contentId = null)
        {
            this.Attachments.Add(new EmailMessageAttachment
            {
                FileName = fileName,
                ContentReference = contentReference,
                ContentType = contentType,
                ContentId = contentId
            });
            return this;
        }
    }

    /// <summary>
    /// An email message recipient (or sender).
    /// </summary>
    public class EmailMessageRecipient
    {
        /// <summary>
        /// Optional name of the email recipient.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Email address of the recipient.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Recipient type.
        /// </summary>
        public EmailMessageRecipientType RecipientType { get; set; } = EmailMessageRecipientType.Receiver;
    }

    /// <summary>
    /// Types of email message recipients (and senders).
    /// </summary>
    public enum EmailMessageRecipientType
    {
        /// <summary>
        /// The sender of the message.
        /// </summary>
        Sender = 0,

        /// <summary>
        /// On behalf of the sender of the message.
        /// </summary>
        OnBehalfOfSender = 1,

        /// <summary>
        /// A primary receiver of the message.
        /// </summary>
        Receiver = 2,

        /// <summary>
        /// A carbon copy receiver of the message.
        /// </summary>
        CopyReceiver = 3,

        /// <summary>
        /// A blind carbon copy receiver of the message.
        /// </summary>
        BlindCopyReceiver = 4
    }

    /// <summary>
    /// An email message attachment.
    /// </summary>
    public class EmailMessageAttachment
    {
        /// <summary>
        /// Name of the attachment file.
        /// </summary>
        public string? FileName { get; set; }
        
        /// <summary>
        /// MIME content type of the attachment file.
        /// </summary>
        public string? ContentType { get; set; }
        
        /// <summary>
        /// Content of the attachment file.
        /// </summary>
        public byte[]? Content { get; set; }

        /// <summary>
        /// A reference to the content of the attachment file (e.g. a URL).
        /// </summary>
        public string? ContentReference { get; set; }

        /// <summary>
        /// The content ID of the attachment file (for inline attachments).
        /// </summary>
        public string? ContentId { get; set; }
    }
}
