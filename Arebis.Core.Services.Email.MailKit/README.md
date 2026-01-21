Arebis.Core.Services.Email.MailKit
==================================

MailKit-based implementation of the Arebis.Core.Services.IEmailSendService interface.

# Installation

Add the **Arebis.Core.Services.Email.MailKit** package to your .NET project by installing the NuGet package via your IDE or by running the following command in the .NET CLI:

```
dotnet add package Arebis.Core.Services.Email.MailKit
```

Register the service in your `Program.cs` or `Startup.cs`:

```CSharp
builder.Services.AddMailKitEmailSendService(config);
```

Pass options as parameter or define options in configuration:

```JSON
{
    "Smtp": {
        "Server": "mstp.myserver.net",
        "Port": "25",
        "SenderName": "John Smith",
        "SenderMail": "j.smith@example.com",
        "Username": "jsmith112233",
        "Password": "Secret123!",
    }
}
```

# Usage

The simplest way:

```CSharp
puvlic IActionResult SendEmail([FromServices] IEmailSendService emailSendService)
{
    emailSendService.SendEmail("p.pan@example.com", "Welcome!", "Welcome to our wesite !");
}
```