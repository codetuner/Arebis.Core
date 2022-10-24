Arebis.Core.Data.Sql
====================

.NET Core extensions for working with SQL Server databases.

SqlConnectionSource
-------------------

An injectable source of SqlConnections that are shared within scope and allow to setup transactions
over multiple consumers of the same connection.
