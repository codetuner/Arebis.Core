Arebis.Core.Service.Interfaces
==============================

Common .NET Core service interfaces.

Implementation: https://github.com/codetuner/Arebis.Core

## ICurrentUserService

Providing access to information about the current user.

Known implementations:
- Arebis.Core.Services.AspNet.CurrentUserService

## ITranslationService

Providing translation services.

Known implementations:
- Arebis.Core.Services.Translation.BingTranslationService
- Arebis.Core.Services.Translation.DeepLTranslationService
- Arebis.Core.Services.Translation.GoogleTranslationService
