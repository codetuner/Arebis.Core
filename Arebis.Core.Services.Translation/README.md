Arebis.Core.Services.Translation
================================

.NET Core translation service implementations.

## Translation Services

This package contains the following .NET Core translation service implementations:

### BingTranslationService

Register the Bing translation service with the following code:

    builder.Services.AddTransient<ITranslationService, BingTranslationService>();

In addition the API key, or subscription key, must be made available, together with the region, ideally in User Secrets. For instance (non-operational example key):

    "BingApi": {
      "SubscriptionKey": "0a123bc45678d90efa12345bc6789d0e",
      "Region": "eastus"
    }

For more information, see: http://api.microsofttranslator.com.

### DeepLTranslationService

Register the DeepL translation service with the following code:

    builder.Services.AddTransient<ITranslationService, DeepLTranslationService>();

An API key must be made available, preferably via User Secrets, as in here (non-operational example key):

    "DeepLApi": {
      "ApiKey": "DeepL-Auth-Key ab12345c-678d-90e1-fa23-b456cd7890ef:fx"
    }

Note that the API key should start with "DeepL-Auth-Key " and typically ends with ":fx".

You will also have configure the TranslationServiceUrl as this is different depending on the service type. This configuration is typically added in appsettings.json:

    "DeepLApi": {
      "TranslationServiceUrl": "https://api-free.deepl.com/v2/translate",
    }

For more information, see: https://www.deepl.com/en/docs-api.

### GoogleTranslationService

Setup an account and access to the Google Translate service here if you haven't done so yet.

Register the Google translation service with the following code:

    builder.Services.AddTransient<ITranslationService, GoogleTranslationService>();

An API key must be made available, preferably via User Secrets, as in here (non-operational example key):

    "GoogleApi": {
      "ApiKey": "AbcDEfGhij01KLmNop23QrS-t4U56Vw7XYz8a90"
    }

For more information, see: https://cloud.google.com/translate.

