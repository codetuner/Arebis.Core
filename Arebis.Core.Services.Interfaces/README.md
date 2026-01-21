Arebis.Core.Service.Interfaces
==============================

Common .NET Core service interfaces.

Implementation: https://github.com/codetuner/Arebis.Core

## ICurrentUserService

Providing access to information about the current user.

Known implementations:
- Arebis.Core.Services.AspNet.CurrentUserService

## IEmailSendService

Providing email sending services.

Known implementations:
- Arebis.Core.Services.Email.MailKit.MailKitEmailSendService

Note that this assembly also defines email related types:
- `EmailMessage`
- `EmailMessageRecipient`
- `EmailMessageRecipientType`
- `EmailMessageAttachment`

As well as an `SmtpOptions` class for SMTP configuration.

## ITranslationService

Providing translation services.

Known implementations:
- Arebis.Core.Services.Translation.BingTranslationService
- Arebis.Core.Services.Translation.DeepLTranslationService
- Arebis.Core.Services.Translation.GoogleTranslationService
