Arebis.Core.AspNet.SqlServer
===================================

SQL Server based extensions for ASP.NET.

# Domain Settings

Domain Settings allow you to manage domain specific settings in your ASP.NET applications using SQL server as storage.

## Installation

1. Install the NuGet package:
   ```
   dotnet add package Arebis.Core.AspNet.SqlServer
   ```

2. Set up the required database table by executing the following SQL script:
   ```sql
   CREATE TABLE [config].[DomainSettings]
   (
       [Id] int IDENTITY(1,1) NOT NULL,
   	[DomainName] varchar(200) NOT NULL,
   	[Key] nvarchar(200) NULL,
   	[Value] nvarchar(max) NULL,
   	[AliasFor] varchar(200) NULL,
   	CONSTRAINT [PK_DomainSettings] PRIMARY KEY CLUSTERED ([Id] ASC)
   ) ON [PRIMARY]
   GO
   
   CREATE UNIQUE NONCLUSTERED INDEX [IX_DomainSettings_DomainName_Key] ON [config].[DomainSettings] ([DomainName] ASC, [Key] ASC)
   GO
   
   ALTER TABLE [config].[DomainSettings]
   ADD CONSTRAINT CK_DomainSettings_Key_XOR_Alias
   CHECK (
       ([Key] IS NOT NULL AND [AliasFor] IS NULL)
    OR ([Key] IS NULL AND [Value] IS NULL AND [AliasFor] IS NOT NULL)
   )
   GO
   ```
3. Configure the service in your ASP.NET application:
   ```csharp
   builder.Services.AddSqlServerDomainSettingsProvider(builder.Configuration.GetConnectionString("DefaultConnection")!);
   ```
4. Also make sure to have registered an IDistributedCache implementation:
   ```csharp
   builder.Services.AddDistributedMemoryCache();
   ```
   (Note: use the _Distributed**Memory**Cache_ only for testing or for single-server scenarios.)

## Usage

You can use the `IDomainSettingsProvider` interface to manage domain settings in your application. Here's an example of how to use it:
```csharp
public class HomeController : Controller
{
    private readonly IDomainSettingsProvider domainSettingsProvider;

    public HomeController(IDomainSettingsProvider domainSettingsProvider)
    {
        this.domainSettingsProvider = domainSettingsProvider;
    }

    public async Task<IActionResult> Index()
    {
        var settings = await this.domainSettingsProvider.GetDomainSettingsAsync();
        ViewBag.Tenant = settings["Tenant"];
        return View();
    }
}
```

Following methods are available on the `IDomainSettingsProvider` interface:

- `Task<IDictionary<string, string?>> GetDomainSettingsAsync(string domainName, CancellationToken cancellationToken = default);`

- `Task SetDomainAliasAsync(string domainName, string aliasFor, CancellationToken cancellationToken = default);`

- `Task SetDomainSettingAsync(string domainName, IDictionary<string, string?> settings, CancellationToken cancellationToken = default);`

- `Task DeleteDomainAsync(string domainName, CancellationToken cancellationToken = default);`
 
