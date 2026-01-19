Arebis.Core.Configuration.SqlServer
===================================

A SQL server configuration source for .NET Core applications.

# Configuration setup

Use the following code to add the SQL Server configuration provider:

```csharp
builder.Configuration.AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!);
```

You can also specify the environment and refresh interval:

```csharp
builder.Configuration.AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!, environment: "Production", refreshInterval: TimeSpan.FromSeconds(10));
```

If not specified, the environment is taken from the `ASPNETCORE_ENVIRONMENT` variable (or defaults to "Production" if not set), and no refreshes are performed.

Not that to use refreshed option values, you should use `IOptionsSnapshot<T>` or `IOptionsMonitor<T>` instead of `IOptions<T>`.

# Database setup

Us the following script to create the required table:

```sql
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'config')
BEGIN
    EXECUTE ('CREATE SCHEMA [config] AUTHORIZATION [dbo]')
END
GO

CREATE TABLE [config].[AppConfiguration]
(
    [Id] int IDENTITY(1,1) NOT NULL,
    [Key] nvarchar(200) NOT NULL,
    [Value] nvarchar(max) NULL,
    [Environment] nvarchar(100) NULL,
    CONSTRAINT [PK_AppConfiguration] PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_AppConfiguration_Key_Environment] ON [config].[AppConfiguration] ([Key] ASC, [Environment] ASC)
GO
```

# Configuring values

Insert configuration values into the table as follows:

```sql
INSERT INTO [config].[AppConfiguration] ([Key], [Value], [Environment])
VALUES ('MySection:MyKey', 'MyValue', 'Production');
```

A value of NULL in the Environment column indicates that the setting is for all environments (unless overwritten for a specific environment).

Use the colon (:) character to indicate sections and subsections.

# Credits

Implementation is based on:
https://mousavi310.github.io/posts/a-refreshable-sql-server-configuration-provider-for-net-core/.
