-------------------------------------------------------------------
-- Standard schema:
-------------------------------------------------------------------

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

-------------------------------------------------------------------
-- Customizations:
-------------------------------------------------------------------

ALTER TABLE [sse].[ClientData]
ADD [Instance] VARCHAR(100) NULL
GO

/*
DROP TABLE [sse].[ClientDataEvents]
DROP TABLE [sse].[ClientData]
GO
*/
