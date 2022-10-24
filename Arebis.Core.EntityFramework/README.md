Arebis.Core.EntityFramework
===========================

.NET Core extensions for working with Entity Framework.


BaseDbContext<T>
----------------

A base class for DbContext offering extra services:

- Broader support for Data Annotations (setting default schema, setting composite primary keys, mapped fields, type discriminator, converters)
- Support for Context aware entitites
- Support for intercepting entities

Includes default Converters for:

- Dictionaries
- Lists
- TimeSpan in Days
- TimeSpan in Hours
- UtcDateTime

As well as a generic JsonConverter.

(All these converters require the use of BaseDbContext<T>)