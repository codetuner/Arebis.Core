Arebis.Core.Data.SqlClient
==========================

.NET Core extensions for working with SQL Server databases.

SqlConnectionExtensions
-----------------------

- **GetLastGeneratedIdentity()**: Gets the last generated identity value for the current connection, which is useful when inserting new records and retrieving their auto-generated primary key values.

SqlConnectionSource
-------------------

An injectable source of SqlConnections that are shared within scope and allow to setup transactions
over multiple consumers of the same connection.

SqlExceptionExtensions
----------------------

- **HasErrorNumber()**: Whether the exception contains an error with the specified number.
- **IsSqlDuplicateKeyViolation()**: Whether the exception is caused by a duplicate key violation.
- **IsSqlTransient()**: Whether the exception is caused by a transient error that may be retried.
- **IsSqlUniqueConstraintViolation()**: Whether the exception is caused by a unique constraint violation.