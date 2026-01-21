using Arebis.Core.Services.Interfaces;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Services.Email.MailKit
{
    /// <summary>
    /// An email send service using MailKit.
    /// </summary>
    public class MailKitEmailSendService : IEmailSendService, IAsyncDisposable, IDisposable
    {
        private readonly IOptions<SmtpOptions> smtpOptions;
        private global::MailKit.Net.Smtp.SmtpClient? smtpClient = null;
        private bool disposed = false;

        /// <summary>
        /// Constructs an email send service using MailKit.
        /// </summary>
        public MailKitEmailSendService(IOptions<SmtpOptions> smtpOptions)
        {
            this.smtpOptions = smtpOptions;
        }

        /// <inheritdoc/>
        public async Task SendEmail(EmailMessage message)
        {
            if (this.smtpOptions.Value.Server == null) throw new InvalidOperationException("SMTP server is not configured. Define the \"Smtp:Server\" setting in configuration or configure in code.");
            if (this.smtpOptions.Value.Port == 0) throw new InvalidOperationException("SMTP port is not configured. Define the \"Smtp:Port\" setting in configuration or configure in code.");
            if (this.smtpOptions.Value.SenderEmail == null) throw new InvalidOperationException("SMTP sender email is not configured. Define the \"Smtp:SenderEmail\" setting in configuration or configure in code.");
            if (this.smtpOptions.Value.SenderName == null) throw new InvalidOperationException("SMTP sender name is not configured. Define the \"Smtp:SenderName\" setting in configuration or configure in code.");

            var email = new MimeMessage();

            // Set sender:
            email.From.Add(new MailboxAddress(message.Sender?.Name ?? Environment.ExpandEnvironmentVariables(smtpOptions.Value.SenderName!), message.Sender?.Address ?? Environment.ExpandEnvironmentVariables(smtpOptions.Value.SenderEmail!)));

            // Set recipients:
            foreach(var recipient in message.Recipients)
            {
                var mailboxAddress = new MailboxAddress(recipient.Name ?? string.Empty, recipient.Address);
                switch(recipient.RecipientType)
                {
                    case EmailMessageRecipientType.Receiver:
                        email.To.Add(mailboxAddress);
                        break;
                    case EmailMessageRecipientType.CopyReceiver:
                        email.Cc.Add(mailboxAddress);
                        break;
                    case EmailMessageRecipientType.BlindCopyReceiver:
                        email.Bcc.Add(mailboxAddress);
                        break;
                }
            }

            // Set subject and body:
            email.Subject = message.Body ?? String.Empty;
            if (message.ContentType?.ToLower() == "text/text")
            {
                email.Body = new TextPart("plain")
                {
                    Text = message.Body ?? String.Empty
                };
            }
            else
            {
                email.Body = new BodyBuilder
                {
                    HtmlBody = message.Body ?? String.Empty
                }.ToMessageBody();
            }

            // Add attachments:
            foreach(var attachment in message.Attachments)
            {
                if (attachment.Content == null && attachment.ContentReference == null) continue;

                var mimePart = new MimePart(attachment.ContentType ?? "application/octet-stream")
                {
                    Content = (attachment.Content != null)
                    ? new MimeContent(new System.IO.MemoryStream(attachment.Content))
                    : new MimeContent(new System.IO.MemoryStream(await this.RerieveContentByReference(attachment.ContentReference!))),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = attachment.FileName
                };
                if (!string.IsNullOrEmpty(attachment.ContentId))
                {
                    mimePart.ContentId = attachment.ContentId;
                }
                if (email.Body is Multipart multipart)
                {
                    multipart.Add(mimePart);
                }
                else
                {
                    var body = email.Body;
                    var newMultipart = new Multipart("mixed");
                    newMultipart.Add(body);
                    newMultipart.Add(mimePart);
                    email.Body = newMultipart;
                }
            }

            // Connect to SMTP:
            this.smtpClient ??= new global::MailKit.Net.Smtp.SmtpClient();
            if (!this.smtpClient.IsConnected) await this.smtpClient.ConnectAsync(Environment.ExpandEnvironmentVariables(smtpOptions.Value.Server!), smtpOptions.Value.Port, SecureSocketOptions.StartTls);
            if (!this.smtpClient.IsAuthenticated) await this.smtpClient.AuthenticateAsync(Environment.ExpandEnvironmentVariables(smtpOptions.Value.Username!), Environment.ExpandEnvironmentVariables(smtpOptions.Value.Password!));

            // Send message:
            await this.smtpClient.SendAsync(email);

            // Disconnect from SMTP:
            // Will be done when disposing this service.
        }

        /// <summary>
        /// Retrieve content by reference.
        /// Override to privde actual implementation.
        /// </summary>
        /// <exception cref="NotImplementedException">By default not implemented.</exception>
        public virtual Task<byte[]> RerieveContentByReference(string contentReference)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            // Fallback for sync callers
            DisposeAsync().AsTask().GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async ValueTask DisposeAsync()
        {
            if (disposed) return;

            // Async cleanup
            await DisposeInternalAsync();

            disposed = true;
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Actual async dispose implementation.
        /// </summary>
        protected virtual async Task DisposeInternalAsync()
        {
            if (this.smtpClient != null && this.smtpClient.IsConnected)
            {
                await this.smtpClient.DisconnectAsync(true);
            }
        }
    }
}
