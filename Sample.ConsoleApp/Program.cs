using Arebis.Core.Services.Interfaces;
using Arebis.Core.Services.Translation;
using Arebis.Core.Services.Translation.OpenAI;
using Arebis.Core.EntityFramework;
using Arebis.Core.Services.Email.MailKit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI.Chat;
using Sample.ConsoleApp;
using Microsoft.EntityFrameworkCore;
using Arebis.Types.Collections.Generic;

// Setup HostApplicationBuilder to support dependency injection:
// See: https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// Set up the configuration to read from user secrets:
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .Build();

// Add services to the container:
//builder.Services.AddSingleton<ChatClient>(serviceProvider =>
//{
//    var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
//    var model = "gpt-5.2";
//
//    return new ChatClient(model, apiKey);
//});
//builder.Services.AddTransient<ITranslationService, OpenAITranslationService>();
builder.Services.AddSingleton<ITranslationService, DeepLTranslationService>();

builder.Services.AddMailKitEmailSendService(config, options =>
{
    options.Server = config["Smtp:Server"] ?? "smtp.sendgrid.net";
    options.Port = int.TryParse(config["Smtp:Port"], out var port) ? port : 587;
    options.SenderName = config["Smtp:SenderName"] ?? "Arebis Core TestConsole";
    options.SenderEmail = config["Smtp:SenderEmail"] ?? "admin@onlinedogshows.eu";
    options.Username = config["Smtp:Username"] ?? "apikey";
    options.Password = config["Smtp:Password"] ?? Environment.GetEnvironmentVariable("SendGrid__ApiKey");
});

// Build the host:
using IHost host = builder.Build();

// Start the host and run the application:
await host.StartAsync();

// Try out the translation service:
if (false)
{
    // Resolve the translation service:
    var translationService = host.Services.GetRequiredService<ITranslationService>();

    // Translate a text:
    var sources = new string[] { "Welcome to the show!" };
    var results = await translationService.TranslateAsync("en", "fr", "text/plain", sources);
    Console.WriteLine("=========================================");
    Console.WriteLine($"Translated text: {results.First()}");
    Console.WriteLine("=========================================");
}

// Try out the email send service:
if (false)
{
    // Resolve the email send service:
    var emailSendService = host.Services.GetRequiredService<IEmailSendService>();
    // Send a test email:
    await emailSendService.SendEmail("it@onlinedogshows.eu", "Testing EmailSendService", "<h1>Hi, a little test mail.</h1>");
}

// Test Entity Framework Core with JsonValueConverter and StoreEmptyAsNullAttribute:
if (false)
{
    int id;
    var options = new DbContextOptionsBuilder<MyDbContext>()
        .UseSqlServer("Data Source=(local);Initial Catalog=Playground;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True")
        .Options;
    using (var context = new MyDbContext(options))
    {
        //var customer = context.Customers.AddNew();
        var customer = context.Customers.Find(17) ?? throw new Exception("Customer not found");
        customer.Name = "John Doe " + DateTime.Now.ToString();
        customer.Properties["Age"] = "30";
        customer.Properties["Country"] = "USA";

        customer.MarkModified();

        await context.SaveChangesAsync();
        id = customer.Id;

        Console.WriteLine("=========================================");
        Console.WriteLine($"Customer {customer.Id}: {customer.Name}");
        Console.WriteLine($"Properties: {string.Join(", ", customer.Properties.Select(kv => $"{kv.Key}={kv.Value}"))}");
        Console.WriteLine("=========================================");
    }

    using (var context = new MyDbContext(options))
    {
        var customer = context.Customers.Find(id);

        if (customer != null)
        {
            Console.WriteLine("=========================================");
            Console.WriteLine($"Customer {customer.Id}: {customer.Name}");
            Console.WriteLine($"Properties: {string.Join(", ", customer.Properties.Select(kv => $"{kv.Key}={kv.Value}"))}");
            Console.WriteLine("=========================================");
        }
    }
}

// Stop the host:
await host.StopAsync();
