using Arebis.Core.Services.Interfaces;
using Arebis.Core.Services.Translation.OpenAI;
using Microsoft.AspNetCore.Mvc.Razor;
using OpenAI.Chat;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();
builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

#region Translations

builder.Services.AddSingleton<ChatClient>(serviceProvider =>
{
    var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
    var model = "gpt-5.2";
    return new ChatClient(model, apiKey);
});
builder.Services.AddTransient<ITranslationService, OpenAITranslationService>();
//builder.Services.AddTransient<ITranslationService, DeepLTranslationService>();
//builder.Services.AddTransient<ITranslationService, GoogleTranslationService>();
//builder.Services.AddTransient<ITranslationService, BingTranslationService>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
