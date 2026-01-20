Arebis.Core
===========

Common .NET Core extensions and extension methods.

## Features

### Root namespace

#### `Current`

The `Current` class provides access to the current application context such as the current DateTime in a way that mocking is possible.
The class also provides a `SoftRecycle` event that can be used to trigger a soft recycle of the application, which is useful in scenarios like web applications where you want to refresh the application state without a full restart.

#### `LiveAssert`

Assertion methods to ease validation in business methods.

#### `ResultException`

An exception that encapsulates a result to be returned on a higher level when unwinding the stack.

#### `Selection`

Represents a virtual selection of a collection, to which adding or removing items is reflected on the underlying collection.

Example:
```csharp
var customers = new Selection<Contact>(contacts, c => c.IsCustomer);
customers.Add(new Contact { Name = "GeeBar", IsCustomer = true });
```

### Extensions

#### `ArrayExtensions`

Extension methods for arrays.

#### `BitArrayExtensions`

Extension methods for `BitArray`.

#### `ByteArrayExtensions`

Extension methods for byte arrays.

#### `CollectionExtensions`

Extension methods for collections.

#### `DataOnlyExtensions`

Extensions for `DateOnly`.

#### `DateTimeExtensions`

Extensions for `DateTime`.

#### `DateTimeOffsetExtensions`

Extensions for `DateTimeOffset`.

#### `DictionaryExtensions`

Extensions for dictionaries.

#### `DistributedCacheExtensions`

Extensions for IDistributedCache.

#### `EnumerableExtensions`

Extensions for enumerables.

#### `ExceptionExtensions`

Extensions for exceptions.

#### `FileSystemExtensions`

Extensions for file system classes:

- **`FileInfo.ToUniqueName()`** returns a FileInfo with a unique name based on the original file name, ensuring that the file can be saved without overwriting existing files.

#### `ListExtensions`

Extensions for lists.

#### `NumberExtensions`

Extensions for number types.

#### `ObjectExtensions`

Extensions for objects.

#### `PredicateExtensions`
_(to be completed)_

Allows for easy predicate composition.

Example:

```csharp
Expression<Func<Customer, bool>> predicate = i => false;
foreach (var filter in filters)
{
    predicate = predicate.OrElse(filter);
}
```

#### `QueueExtensions`

Extensions for queues.

#### `StreamExtensions`

Extensions for streams.

#### `StringExtensions`

Extensions for strings.

#### `TimeOnlyExtensions`

Extensions for `TimeOnly`.

#### `TimeSpanExtensions`

Extensions for `TimeSpan`.

#### `TypeExtensions`

Extensions for types.

### Factories

Factories to create DateTime mocks to work with the `Current` class.

### Globalizaton

#### `CultureScope`

A scope for culture settings that allows you to set a specific culture for the duration of a block of code, ensuring that culture-specific operations are consistent.

Example:
```csharp
using (new CultureScope("en-US"))
{
    // Both CurrentCulture and CurrentUICulture are "en-US" here.
}
```

### Numerics

Classes to work with numerics in different bases, such as binary, octal, decimal, and hexadecimal. But also base 51, 62, 64, 256 or any base really.

Also provides a `RomanNumnber` class to work with Roman numerals, including conversion to and from integers.

### Source

#### `CodeSourceAttribute`

Allows to annotate your code with a source attribute that can be used to track the origin of the code, such as a URL or a file path, track the author and/or copyright.

#### `CodeToDo`

Allows to annotate a section of code that needs to be implemented or completed.

#### `HardcodedSection`

Allows to annotate a section of code that is hardcoded and should not be changed, such as a specific value or a configuration setting.

