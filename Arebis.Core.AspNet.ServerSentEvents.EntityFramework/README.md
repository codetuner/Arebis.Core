
# Arebis.Core.AspNet.ServerSentEvents.EntityFramework

## Introduction

The **Arebis.Core.AspNet.ServerSentEvents.EntityFramework** component provides an EntityFramework store for ServerSent Events support for ASP.NET applications.

This store offers the ability for browser clients to reconnect to different instances of a webfarm or webgarden and still catch up missed events.

## Setup

### 1. Define client data

Create a class that inherits from **ServerSentEventsClientData** to hold client-specific data.
This data can be used to filter recipient clients when dispatching events.

For instance:

```
public class MySseClientData : ServerSentEventsClientData
{
    public string? Market { get; internal set; }
}
```

See **Arebis.Core.AspNet.ServerSentEvents** for more details.

### 2. Define a, EntityFramework client data store

Define where to store client data as well as the DbContext to use and it's connection string:

```
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<EfServerSentEventsDbContext<MySseClientData>>(options => options.UseSqlServer(connectionString));
builder.Services.AddSingleton<IServerSentEventsClientsDataStore<MySseClientData>, EfServerSentEventsClientsDataStore<MySseClientData>>();
```

### 3. Create database schema

Create the required database schema. For SQL Server, the following script can be used:

```
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'sse')
BEGIN
   EXECUTE ('CREATE SCHEMA [sse] AUTHORIZATION [dbo]')
END
GO

CREATE TABLE [sse].[ClientData]
(
    [Identifier] UNIQUEIDENTIFIER NOT NULL,
	[Path] NVARCHAR(2000) NOT NULL,
	[LastUsedId] INT NOT NULL,
	[LastEventQueuedTime] DATETIME2 NULL,
	CONSTRAINT [PK_ClientData] PRIMARY KEY CLUSTERED ([Identifier] ASC)
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_ClientData_LastEventQueuedTime] ON [sse].[ClientData] ([LastEventQueuedTime] ASC)
GO

CREATE TABLE [sse].[ClientDataEvents]
(
    [ClientIdentifier] UNIQUEIDENTIFIER NOT NULL,
	[Id] INT NOT NULL,
	[Type] NVARCHAR(200) NOT NULL,
	[Data] NVARCHAR(MAX) NOT NULL,
	[ExpiryTime] DATETIME2 NULL,
	[IsSent] BIT NOT NULL CONSTRAINT [DF_ClientData_IsSet] DEFAULT 0,
	CONSTRAINT [PK_ClientDataEvents] PRIMARY KEY CLUSTERED ([ClientIdentifier] ASC, [Id] ASC)
) ON [PRIMARY]
GO
```

Extend the schema with the client-sepcific data, for instance:

```
ALTER TABLE [sse].[ClientData]
ADD [Market] NVARCHAR(200) NULL
GO
```

Consider adding indexes for properties that are used in bulk event send queries.

### 4. Further setup Server-Sent Events

Proceed with step 3 of the instructions to setup the **Arebis.Core.AspNet.ServerSentEvents** component.

## Further Considerations

### Data management

Data is currently not deleted from the database. Consider adding a scheduled job that regularly deletes deprectated clients and events. For instance using the following SQL commands:

```
DELETE FROM [sse].[ClientDataEvents] WHERE [IsSent] = 1 OR [ExpiryTime] < GETUTCDATE()
GO

DELETE FROM [sse].[ClientData] WHERE DATEADD(hh, 24, [LastEventQueuedTime]) < GETUTCDATE()
GO
```
