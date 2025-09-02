Arebis.Core.Data
================

.NET Core extensions for working with databases.

## DataReaderExtensions

- **GetInt32OrNull(ordinal)** return a value or null if the column is DBNull
- **GetStringOrNull(ordinal)** return a string or null if the column is DBNull
- **GetDateTimerNull(ordinal)** return a value or null if the column is DBNull
- **GetBytesOrNull(ordinal)** return a byte array or null if the column is DBNull

## DbCommandExtensions

- **AddParameter(name, value, direction, dbType, size)** Creates and adds a parameter to the command.
- **ExecuteSingleRow()** returns an object array with the values of the first row, or null if no rows.
- **ExecuteDirectionary<K,V>()** returns a dictionary of all rows with the first column as key and the second column as value.

## DbConnectionExtensions

- **EnsureOpen()** Ensures that the connection is open.
- **EnsureOpenAsync()** Ensures that the connection is open.
- **CreateCommand(commandText, commandType, transaction)** Creates a command for the connection.

## QueryMapper

A simple mapper to map rows to objects.

I.e. the following code returns an IEnumerable of Customer objects with the properties Id, Name and Email filled:

```csharp
var sql = "SELECT Id, Name, Email FROM Customer WHERE Country = @cc AND Level > @lev";
using var conn = new SqlConnection(connectionString);
using var qm = new QueryMapper(conn, sql)
    .WithParameter("@cc", "DE")
    .WithParameter("@lev", 3);
return qm.TakeAll<Customer>();
```
