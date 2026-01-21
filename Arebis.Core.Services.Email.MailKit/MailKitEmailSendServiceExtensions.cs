using Arebis.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Services.Email.MailKit
{
    /// <summary>
    /// Extension methods.
    /// </summary>
    public static class MailKitEmailSendServiceExtensions
    {
        /// <summary>
        /// Registers the MailKit email send service.
        /// </summary>
        public static IServiceCollection AddMailKitEmailSendService(this IServiceCollection services, IConfiguration configuration, Action<SmtpOptions>? optionsAction = null)
        {
            // Configure SmtpOptions:
            services.Configure<SmtpOptions>(options => {
                configuration.GetSection(SmtpOptions.SectionName).Bind(options);
                optionsAction?.Invoke(options);
            });

            // Register MailKit email send service:
            services.AddScoped<IEmailSendService, MailKitEmailSendService>();

            // Return services:            
            return services;
        }
    }
}
