Arebis.Core.AspNet
===================================

This project contains ASP.NET specific components of the Arebis Core Library.

## Filters

### AuthenticatedOnlyAttribute

An action filter that restricts access to authenticated users only without redirecting to a login page.
Instead, the action is considered not applicable and another matching action may be selected.

```csharp
[AuthenticatedOnly]
public IActionResult MySecureAction()
{
    // Action code here
}
```

### ExceptionHandlingFilter

An exception filter that converts `ResultException<IActionResult>` and `HttpResponseException` instances into HTTP responses.

Create a subclass to extend the behavior for other exception types.

Regsiter the filter globally in `Startup.cs`:
```csharp
services.AddControllers(options =>
{
    options.Filters.Add<ExceptionHandlingFilter>();
});
```

Or use it on specific controllers or actions:
```csharp
[ExceptionHandlingFilter]
public class MyController : Controller
{
    // Controller code here
}
```

