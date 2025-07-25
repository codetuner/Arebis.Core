Arebis.Core.EntityFramework
===========================

.NET Core extensions for working with Entity Framework.


BaseDbContext<T>
----------------

A base class for DbContext offering extra services:

- Broader support for Data Annotations (setting default schema, mapped fields, type discriminator, converters)
- Support for Context aware entitites
- Support for intercepting entities
- Support for validation
- Support for after save actions

Includes default Converters (and Comparers) for:

- Dictionaries
- Lists
- DateOnly
- TimeOnly
- TimeSpan in Days
- TimeSpan in Hours
- TimeSpan as Ticks
- UtcDateTime

As well as a generic JsonConverter.

(All these converters require the use of BaseDbContext<T>)