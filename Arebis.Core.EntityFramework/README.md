Arebis.Core.EntityFramework
===========================

.NET Core extensions for working with Entity Framework.

Extensions
----------

dbContext.**AreProxiesEnabled()** - Check if proxies are enabled for the given DbContext.

dbSet.**GetDbContext()** - Get the DbContext for a given DbSet.

dbSet.**AddNew()** - Create a new entity (or proxy), add it to the DbSet and return it.

dbSet.**FindOrFail()** - Find an entity by primary key(s) or throw an exception if not found.

dbSet.**FindOrFailAsync()** - Find an entity by primary key(s) or throw an exception if not found.

entity.**MarkModified()** - Mark an entity as modified in the DbContext.
entity must be an IContextualEntity&lt;TDbContext&gt;.

queryable.**OrderBy()** - Order a queryable by a property name or path.

orderedQueryable.**ThenBy()** - ThenBy a queryable by a property name or path. I.e:

```
invoices.OrderBy("Customer.Name ASC");
        .ThenBy("Date DESC");
```


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
- Regex

As well as a generic JsonConverter.

(All these converters require the use of BaseDbContext<T>)