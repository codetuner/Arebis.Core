using Arebis.Core.Services.Interfaces;
using Arebis.Core.Services.Translation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Setup HostApplicationBuilder to support dependency injection:
// See: https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// Set up the configuration to read from user secrets:
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .Build();

// Add services to the container:
builder.Services.AddSingleton<ITranslationService, DeepLTranslationService>();

// Build the host:
using IHost host = builder.Build();

// Start the host and run the application:
await host.StartAsync();

// Resolve the translation service:
var translationService = host.Services.GetRequiredService<ITranslationService>();

// Translate a text:
var sources = new string[] { "Welcome to the show!" };
var results = await translationService.TranslateAsync("en", "fr", "text/plain", sources);
Console.WriteLine("=========================================");
Console.WriteLine($"Translated text: {results.First()}");
Console.WriteLine("=========================================");

// Stop the host:
await host.StopAsync();
