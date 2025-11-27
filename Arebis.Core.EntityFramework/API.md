<a name='assembly'></a>
# Arebis.Core.EntityFramework

## Contents

- [BaseContextualEntity\`1](#T-Arebis-Core-EntityFramework-BaseContextualEntity`1 'Arebis.Core.EntityFramework.BaseContextualEntity`1')
  - [Context](#P-Arebis-Core-EntityFramework-BaseContextualEntity`1-Context 'Arebis.Core.EntityFramework.BaseContextualEntity`1.Context')
- [BaseDbContextExtensions](#T-Arebis-Core-EntityFramework-BaseDbContextExtensions 'Arebis.Core.EntityFramework.BaseDbContextExtensions')
  - [UseStoreEmptyAsNullAttributes(optionsBuilder)](#M-Arebis-Core-EntityFramework-BaseDbContextExtensions-UseStoreEmptyAsNullAttributes-Microsoft-EntityFrameworkCore-DbContextOptionsBuilder- 'Arebis.Core.EntityFramework.BaseDbContextExtensions.UseStoreEmptyAsNullAttributes(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)')
  - [UseStringTrimming(optionsBuilder,storeEmptyAsNull)](#M-Arebis-Core-EntityFramework-BaseDbContextExtensions-UseStringTrimming-Microsoft-EntityFrameworkCore-DbContextOptionsBuilder,System-Boolean- 'Arebis.Core.EntityFramework.BaseDbContextExtensions.UseStringTrimming(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder,System.Boolean)')
  - [UseValidation(optionsBuilder,validatePropertyValues,alsoValidateUnchanged)](#M-Arebis-Core-EntityFramework-BaseDbContextExtensions-UseValidation-Microsoft-EntityFrameworkCore-DbContextOptionsBuilder,System-Boolean,System-Boolean- 'Arebis.Core.EntityFramework.BaseDbContextExtensions.UseValidation(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder,System.Boolean,System.Boolean)')
- [BaseDbContext\`1](#T-Arebis-Core-EntityFramework-BaseDbContext`1 'Arebis.Core.EntityFramework.BaseDbContext`1')
  - [#ctor()](#M-Arebis-Core-EntityFramework-BaseDbContext`1-#ctor-Microsoft-EntityFrameworkCore-DbContextOptions- 'Arebis.Core.EntityFramework.BaseDbContext`1.#ctor(Microsoft.EntityFrameworkCore.DbContextOptions)')
  - [AddAfterSaveAction()](#M-Arebis-Core-EntityFramework-BaseDbContext`1-AddAfterSaveAction-System-Action- 'Arebis.Core.EntityFramework.BaseDbContext`1.AddAfterSaveAction(System.Action)')
  - [ConfigureDefaultConverters()](#M-Arebis-Core-EntityFramework-BaseDbContext`1-ConfigureDefaultConverters-System-Collections-Generic-IDictionary{System-Type,Arebis-Core-EntityFramework-ValueConversion-ConverterAttribute}- 'Arebis.Core.EntityFramework.BaseDbContext`1.ConfigureDefaultConverters(System.Collections.Generic.IDictionary{System.Type,Arebis.Core.EntityFramework.ValueConversion.ConverterAttribute})')
  - [OnEntitySaving()](#M-Arebis-Core-EntityFramework-BaseDbContext`1-OnEntitySaving-Microsoft-EntityFrameworkCore-ChangeTracking-EntityEntry- 'Arebis.Core.EntityFramework.BaseDbContext`1.OnEntitySaving(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry)')
  - [OnEntityTracked()](#M-Arebis-Core-EntityFramework-BaseDbContext`1-OnEntityTracked-Microsoft-EntityFrameworkCore-ChangeTracking-EntityEntry- 'Arebis.Core.EntityFramework.BaseDbContext`1.OnEntityTracked(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry)')
  - [OnModelCreating()](#M-Arebis-Core-EntityFramework-BaseDbContext`1-OnModelCreating-Microsoft-EntityFrameworkCore-ModelBuilder- 'Arebis.Core.EntityFramework.BaseDbContext`1.OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder)')
  - [SaveChanges()](#M-Arebis-Core-EntityFramework-BaseDbContext`1-SaveChanges 'Arebis.Core.EntityFramework.BaseDbContext`1.SaveChanges')
  - [SaveChanges()](#M-Arebis-Core-EntityFramework-BaseDbContext`1-SaveChanges-System-Boolean- 'Arebis.Core.EntityFramework.BaseDbContext`1.SaveChanges(System.Boolean)')
  - [SaveChangesAsync()](#M-Arebis-Core-EntityFramework-BaseDbContext`1-SaveChangesAsync-System-Threading-CancellationToken- 'Arebis.Core.EntityFramework.BaseDbContext`1.SaveChangesAsync(System.Threading.CancellationToken)')
  - [SaveChangesAsync()](#M-Arebis-Core-EntityFramework-BaseDbContext`1-SaveChangesAsync-System-Boolean,System-Threading-CancellationToken- 'Arebis.Core.EntityFramework.BaseDbContext`1.SaveChangesAsync(System.Boolean,System.Threading.CancellationToken)')
- [BaseReadOnlyEntity](#T-Arebis-Core-EntityFramework-BaseReadOnlyEntity 'Arebis.Core.EntityFramework.BaseReadOnlyEntity')
  - [OnSaving()](#M-Arebis-Core-EntityFramework-BaseReadOnlyEntity-OnSaving-Microsoft-EntityFrameworkCore-ChangeTracking-EntityEntry- 'Arebis.Core.EntityFramework.BaseReadOnlyEntity.OnSaving(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry)')
- [BaseReadOnlyEntity\`1](#T-Arebis-Core-EntityFramework-BaseReadOnlyEntity`1 'Arebis.Core.EntityFramework.BaseReadOnlyEntity`1')
  - [Context](#P-Arebis-Core-EntityFramework-BaseReadOnlyEntity`1-Context 'Arebis.Core.EntityFramework.BaseReadOnlyEntity`1.Context')
- [ConverterAttribute](#T-Arebis-Core-EntityFramework-ValueConversion-ConverterAttribute 'Arebis.Core.EntityFramework.ValueConversion.ConverterAttribute')
  - [#ctor()](#M-Arebis-Core-EntityFramework-ValueConversion-ConverterAttribute-#ctor-System-Type,System-Object[]- 'Arebis.Core.EntityFramework.ValueConversion.ConverterAttribute.#ctor(System.Type,System.Object[])')
  - [#ctor()](#M-Arebis-Core-EntityFramework-ValueConversion-ConverterAttribute-#ctor-System-Type,System-Type- 'Arebis.Core.EntityFramework.ValueConversion.ConverterAttribute.#ctor(System.Type,System.Type)')
  - [#ctor()](#M-Arebis-Core-EntityFramework-ValueConversion-ConverterAttribute-#ctor-System-Type,System-Object[],System-Type,System-Object[]- 'Arebis.Core.EntityFramework.ValueConversion.ConverterAttribute.#ctor(System.Type,System.Object[],System.Type,System.Object[])')
  - [ComparerConstructorArgs](#P-Arebis-Core-EntityFramework-ValueConversion-ConverterAttribute-ComparerConstructorArgs 'Arebis.Core.EntityFramework.ValueConversion.ConverterAttribute.ComparerConstructorArgs')
  - [ComparerType](#P-Arebis-Core-EntityFramework-ValueConversion-ConverterAttribute-ComparerType 'Arebis.Core.EntityFramework.ValueConversion.ConverterAttribute.ComparerType')
  - [ConverterConstructorArgs](#P-Arebis-Core-EntityFramework-ValueConversion-ConverterAttribute-ConverterConstructorArgs 'Arebis.Core.EntityFramework.ValueConversion.ConverterAttribute.ConverterConstructorArgs')
  - [ConverterType](#P-Arebis-Core-EntityFramework-ValueConversion-ConverterAttribute-ConverterType 'Arebis.Core.EntityFramework.ValueConversion.ConverterAttribute.ConverterType')
- [DateOnlyValueComparer](#T-Arebis-Core-EntityFramework-ValueConversion-DateOnlyValueComparer 'Arebis.Core.EntityFramework.ValueConversion.DateOnlyValueComparer')
  - [#ctor()](#M-Arebis-Core-EntityFramework-ValueConversion-DateOnlyValueComparer-#ctor 'Arebis.Core.EntityFramework.ValueConversion.DateOnlyValueComparer.#ctor')
- [DateOnlyValueConverter](#T-Arebis-Core-EntityFramework-ValueConversion-DateOnlyValueConverter 'Arebis.Core.EntityFramework.ValueConversion.DateOnlyValueConverter')
  - [#ctor()](#M-Arebis-Core-EntityFramework-ValueConversion-DateOnlyValueConverter-#ctor 'Arebis.Core.EntityFramework.ValueConversion.DateOnlyValueConverter.#ctor')
- [DefaultSchemaAttribute](#T-Arebis-Core-EntityFramework-DefaultSchemaAttribute 'Arebis.Core.EntityFramework.DefaultSchemaAttribute')
  - [#ctor()](#M-Arebis-Core-EntityFramework-DefaultSchemaAttribute-#ctor-System-String- 'Arebis.Core.EntityFramework.DefaultSchemaAttribute.#ctor(System.String)')
  - [SchemaName](#P-Arebis-Core-EntityFramework-DefaultSchemaAttribute-SchemaName 'Arebis.Core.EntityFramework.DefaultSchemaAttribute.SchemaName')
- [DictionaryValueConverter\`3](#T-Arebis-Core-EntityFramework-ValueConversion-DictionaryValueConverter`3 'Arebis.Core.EntityFramework.ValueConversion.DictionaryValueConverter`3')
  - [#ctor()](#M-Arebis-Core-EntityFramework-ValueConversion-DictionaryValueConverter`3-#ctor 'Arebis.Core.EntityFramework.ValueConversion.DictionaryValueConverter`3.#ctor')
  - [#ctor()](#M-Arebis-Core-EntityFramework-ValueConversion-DictionaryValueConverter`3-#ctor-System-String,System-String- 'Arebis.Core.EntityFramework.ValueConversion.DictionaryValueConverter`3.#ctor(System.String,System.String)')
  - [#ctor()](#M-Arebis-Core-EntityFramework-ValueConversion-DictionaryValueConverter`3-#ctor-System-String,System-String,System-String,System-String- 'Arebis.Core.EntityFramework.ValueConversion.DictionaryValueConverter`3.#ctor(System.String,System.String,System.String,System.String)')
- [Extensions](#T-Arebis-Core-EntityFramework-Extensions 'Arebis.Core.EntityFramework.Extensions')
  - [AddNew\`\`1()](#M-Arebis-Core-EntityFramework-Extensions-AddNew``1-Microsoft-EntityFrameworkCore-DbSet{``0},System-Object[]- 'Arebis.Core.EntityFramework.Extensions.AddNew``1(Microsoft.EntityFrameworkCore.DbSet{``0},System.Object[])')
  - [AreProxiesEnabled()](#M-Arebis-Core-EntityFramework-Extensions-AreProxiesEnabled-Microsoft-EntityFrameworkCore-DbContext- 'Arebis.Core.EntityFramework.Extensions.AreProxiesEnabled(Microsoft.EntityFrameworkCore.DbContext)')
  - [FindOrFailAsync\`\`1()](#M-Arebis-Core-EntityFramework-Extensions-FindOrFailAsync``1-Microsoft-EntityFrameworkCore-DbSet{``0},System-Object[]- 'Arebis.Core.EntityFramework.Extensions.FindOrFailAsync``1(Microsoft.EntityFrameworkCore.DbSet{``0},System.Object[])')
  - [FindOrFailAsync\`\`1()](#M-Arebis-Core-EntityFramework-Extensions-FindOrFailAsync``1-Microsoft-EntityFrameworkCore-DbSet{``0},System-Object[],System-Threading-CancellationToken- 'Arebis.Core.EntityFramework.Extensions.FindOrFailAsync``1(Microsoft.EntityFrameworkCore.DbSet{``0},System.Object[],System.Threading.CancellationToken)')
  - [FindOrFail\`\`1()](#M-Arebis-Core-EntityFramework-Extensions-FindOrFail``1-Microsoft-EntityFrameworkCore-DbSet{``0},System-Object[]- 'Arebis.Core.EntityFramework.Extensions.FindOrFail``1(Microsoft.EntityFrameworkCore.DbSet{``0},System.Object[])')
  - [GetDbContext\`\`1()](#M-Arebis-Core-EntityFramework-Extensions-GetDbContext``1-Microsoft-EntityFrameworkCore-DbSet{``0}- 'Arebis.Core.EntityFramework.Extensions.GetDbContext``1(Microsoft.EntityFrameworkCore.DbSet{``0})')
  - [MarkModified\`\`1()](#M-Arebis-Core-EntityFramework-Extensions-MarkModified``1-Arebis-Core-EntityFramework-IContextualEntity{``0}- 'Arebis.Core.EntityFramework.Extensions.MarkModified``1(Arebis.Core.EntityFramework.IContextualEntity{``0})')
  - [OrderBy\`\`1(query,orderByExpression)](#M-Arebis-Core-EntityFramework-Extensions-OrderBy``1-System-Linq-IQueryable{``0},System-String- 'Arebis.Core.EntityFramework.Extensions.OrderBy``1(System.Linq.IQueryable{``0},System.String)')
  - [ThenBy\`\`1(query,orderByExpression)](#M-Arebis-Core-EntityFramework-Extensions-ThenBy``1-System-Linq-IOrderedQueryable{``0},System-String- 'Arebis.Core.EntityFramework.Extensions.ThenBy``1(System.Linq.IOrderedQueryable{``0},System.String)')
- [IContextualEntity\`1](#T-Arebis-Core-EntityFramework-IContextualEntity`1 'Arebis.Core.EntityFramework.IContextualEntity`1')
  - [Context](#P-Arebis-Core-EntityFramework-IContextualEntity`1-Context 'Arebis.Core.EntityFramework.IContextualEntity`1.Context')
- [IInterceptingEntity](#T-Arebis-Core-EntityFramework-IInterceptingEntity 'Arebis.Core.EntityFramework.IInterceptingEntity')
  - [OnSaving()](#M-Arebis-Core-EntityFramework-IInterceptingEntity-OnSaving-Microsoft-EntityFrameworkCore-ChangeTracking-EntityEntry- 'Arebis.Core.EntityFramework.IInterceptingEntity.OnSaving(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry)')
- [JsonValueConverter](#T-Arebis-Core-EntityFramework-ValueConversion-JsonValueConverter 'Arebis.Core.EntityFramework.ValueConversion.JsonValueConverter')
  - [SerializerOptions](#P-Arebis-Core-EntityFramework-ValueConversion-JsonValueConverter-SerializerOptions 'Arebis.Core.EntityFramework.ValueConversion.JsonValueConverter.SerializerOptions')
- [JsonValueConverter\`1](#T-Arebis-Core-EntityFramework-ValueConversion-JsonValueConverter`1 'Arebis.Core.EntityFramework.ValueConversion.JsonValueConverter`1')
  - [#ctor()](#M-Arebis-Core-EntityFramework-ValueConversion-JsonValueConverter`1-#ctor 'Arebis.Core.EntityFramework.ValueConversion.JsonValueConverter`1.#ctor')
- [ListValueConverter\`2](#T-Arebis-Core-EntityFramework-ValueConversion-ListValueConverter`2 'Arebis.Core.EntityFramework.ValueConversion.ListValueConverter`2')
  - [#ctor()](#M-Arebis-Core-EntityFramework-ValueConversion-ListValueConverter`2-#ctor 'Arebis.Core.EntityFramework.ValueConversion.ListValueConverter`2.#ctor')
  - [#ctor()](#M-Arebis-Core-EntityFramework-ValueConversion-ListValueConverter`2-#ctor-System-String- 'Arebis.Core.EntityFramework.ValueConversion.ListValueConverter`2.#ctor(System.String)')
  - [#ctor()](#M-Arebis-Core-EntityFramework-ValueConversion-ListValueConverter`2-#ctor-System-String,System-String,System-String- 'Arebis.Core.EntityFramework.ValueConversion.ListValueConverter`2.#ctor(System.String,System.String,System.String)')
- [MappedFieldAttribute](#T-Arebis-Core-EntityFramework-MappedFieldAttribute 'Arebis.Core.EntityFramework.MappedFieldAttribute')
  - [#ctor()](#M-Arebis-Core-EntityFramework-MappedFieldAttribute-#ctor 'Arebis.Core.EntityFramework.MappedFieldAttribute.#ctor')
- [RegexValueConverter](#T-Arebis-Core-EntityFramework-ValueConversion-RegexValueConverter 'Arebis.Core.EntityFramework.ValueConversion.RegexValueConverter')
  - [#ctor()](#M-Arebis-Core-EntityFramework-ValueConversion-RegexValueConverter-#ctor 'Arebis.Core.EntityFramework.ValueConversion.RegexValueConverter.#ctor')
  - [#ctor()](#M-Arebis-Core-EntityFramework-ValueConversion-RegexValueConverter-#ctor-System-Text-RegularExpressions-RegexOptions- 'Arebis.Core.EntityFramework.ValueConversion.RegexValueConverter.#ctor(System.Text.RegularExpressions.RegexOptions)')
- [StoreEmptyAsNullAttribute](#T-Arebis-Core-EntityFramework-StoreEmptyAsNullAttribute 'Arebis.Core.EntityFramework.StoreEmptyAsNullAttribute')
  - [AllowNullOnStorage](#P-Arebis-Core-EntityFramework-StoreEmptyAsNullAttribute-AllowNullOnStorage 'Arebis.Core.EntityFramework.StoreEmptyAsNullAttribute.AllowNullOnStorage')
  - [InstanceType](#P-Arebis-Core-EntityFramework-StoreEmptyAsNullAttribute-InstanceType 'Arebis.Core.EntityFramework.StoreEmptyAsNullAttribute.InstanceType')
- [StoreEmptyAsNullInterceptor](#T-Arebis-Core-EntityFramework-StoreEmptyAsNullInterceptor 'Arebis.Core.EntityFramework.StoreEmptyAsNullInterceptor')
  - [InitializedInstance()](#M-Arebis-Core-EntityFramework-StoreEmptyAsNullInterceptor-InitializedInstance-Microsoft-EntityFrameworkCore-Diagnostics-MaterializationInterceptionData,System-Object- 'Arebis.Core.EntityFramework.StoreEmptyAsNullInterceptor.InitializedInstance(Microsoft.EntityFrameworkCore.Diagnostics.MaterializationInterceptionData,System.Object)')
  - [SavingChanges()](#M-Arebis-Core-EntityFramework-StoreEmptyAsNullInterceptor-SavingChanges-Microsoft-EntityFrameworkCore-Diagnostics-DbContextEventData,Microsoft-EntityFrameworkCore-Diagnostics-InterceptionResult{System-Int32}- 'Arebis.Core.EntityFramework.StoreEmptyAsNullInterceptor.SavingChanges(Microsoft.EntityFrameworkCore.Diagnostics.DbContextEventData,Microsoft.EntityFrameworkCore.Diagnostics.InterceptionResult{System.Int32})')
  - [SavingChangesAsync()](#M-Arebis-Core-EntityFramework-StoreEmptyAsNullInterceptor-SavingChangesAsync-Microsoft-EntityFrameworkCore-Diagnostics-DbContextEventData,Microsoft-EntityFrameworkCore-Diagnostics-InterceptionResult{System-Int32},System-Threading-CancellationToken- 'Arebis.Core.EntityFramework.StoreEmptyAsNullInterceptor.SavingChangesAsync(Microsoft.EntityFrameworkCore.Diagnostics.DbContextEventData,Microsoft.EntityFrameworkCore.Diagnostics.InterceptionResult{System.Int32},System.Threading.CancellationToken)')
- [StringTrimmingInterceptor](#T-Arebis-Core-EntityFramework-StringTrimmingInterceptor 'Arebis.Core.EntityFramework.StringTrimmingInterceptor')
  - [#ctor()](#M-Arebis-Core-EntityFramework-StringTrimmingInterceptor-#ctor-System-Boolean- 'Arebis.Core.EntityFramework.StringTrimmingInterceptor.#ctor(System.Boolean)')
  - [StoreEmptyAsNull](#P-Arebis-Core-EntityFramework-StringTrimmingInterceptor-StoreEmptyAsNull 'Arebis.Core.EntityFramework.StringTrimmingInterceptor.StoreEmptyAsNull')
  - [SavingChanges()](#M-Arebis-Core-EntityFramework-StringTrimmingInterceptor-SavingChanges-Microsoft-EntityFrameworkCore-Diagnostics-DbContextEventData,Microsoft-EntityFrameworkCore-Diagnostics-InterceptionResult{System-Int32}- 'Arebis.Core.EntityFramework.StringTrimmingInterceptor.SavingChanges(Microsoft.EntityFrameworkCore.Diagnostics.DbContextEventData,Microsoft.EntityFrameworkCore.Diagnostics.InterceptionResult{System.Int32})')
  - [SavingChangesAsync()](#M-Arebis-Core-EntityFramework-StringTrimmingInterceptor-SavingChangesAsync-Microsoft-EntityFrameworkCore-Diagnostics-DbContextEventData,Microsoft-EntityFrameworkCore-Diagnostics-InterceptionResult{System-Int32},System-Threading-CancellationToken- 'Arebis.Core.EntityFramework.StringTrimmingInterceptor.SavingChangesAsync(Microsoft.EntityFrameworkCore.Diagnostics.DbContextEventData,Microsoft.EntityFrameworkCore.Diagnostics.InterceptionResult{System.Int32},System.Threading.CancellationToken)')
- [TimeOnlyValueComparer](#T-Arebis-Core-EntityFramework-ValueConversion-TimeOnlyValueComparer 'Arebis.Core.EntityFramework.ValueConversion.TimeOnlyValueComparer')
  - [#ctor()](#M-Arebis-Core-EntityFramework-ValueConversion-TimeOnlyValueComparer-#ctor 'Arebis.Core.EntityFramework.ValueConversion.TimeOnlyValueComparer.#ctor')
- [TimeOnlyValueConverter](#T-Arebis-Core-EntityFramework-ValueConversion-TimeOnlyValueConverter 'Arebis.Core.EntityFramework.ValueConversion.TimeOnlyValueConverter')
  - [#ctor()](#M-Arebis-Core-EntityFramework-ValueConversion-TimeOnlyValueConverter-#ctor 'Arebis.Core.EntityFramework.ValueConversion.TimeOnlyValueConverter.#ctor')
- [TimeSpanDaysValueConverter](#T-Arebis-Core-EntityFramework-ValueConversion-TimeSpanDaysValueConverter 'Arebis.Core.EntityFramework.ValueConversion.TimeSpanDaysValueConverter')
  - [#ctor()](#M-Arebis-Core-EntityFramework-ValueConversion-TimeSpanDaysValueConverter-#ctor 'Arebis.Core.EntityFramework.ValueConversion.TimeSpanDaysValueConverter.#ctor')
- [TimeSpanHoursValueConverter](#T-Arebis-Core-EntityFramework-ValueConversion-TimeSpanHoursValueConverter 'Arebis.Core.EntityFramework.ValueConversion.TimeSpanHoursValueConverter')
  - [#ctor()](#M-Arebis-Core-EntityFramework-ValueConversion-TimeSpanHoursValueConverter-#ctor 'Arebis.Core.EntityFramework.ValueConversion.TimeSpanHoursValueConverter.#ctor')
- [TimeSpanTicksValueConverter](#T-Arebis-Core-EntityFramework-ValueConversion-TimeSpanTicksValueConverter 'Arebis.Core.EntityFramework.ValueConversion.TimeSpanTicksValueConverter')
  - [#ctor()](#M-Arebis-Core-EntityFramework-ValueConversion-TimeSpanTicksValueConverter-#ctor 'Arebis.Core.EntityFramework.ValueConversion.TimeSpanTicksValueConverter.#ctor')
- [TimeSpanValueComparer](#T-Arebis-Core-EntityFramework-ValueConversion-TimeSpanValueComparer 'Arebis.Core.EntityFramework.ValueConversion.TimeSpanValueComparer')
  - [#ctor()](#M-Arebis-Core-EntityFramework-ValueConversion-TimeSpanValueComparer-#ctor 'Arebis.Core.EntityFramework.ValueConversion.TimeSpanValueComparer.#ctor')
- [TypeDiscriminatorAttribute](#T-Arebis-Core-EntityFramework-TypeDiscriminatorAttribute 'Arebis.Core.EntityFramework.TypeDiscriminatorAttribute')
  - [#ctor(propertyName)](#M-Arebis-Core-EntityFramework-TypeDiscriminatorAttribute-#ctor-System-String- 'Arebis.Core.EntityFramework.TypeDiscriminatorAttribute.#ctor(System.String)')
  - [#ctor(propertyName,propertyType)](#M-Arebis-Core-EntityFramework-TypeDiscriminatorAttribute-#ctor-System-String,System-Type- 'Arebis.Core.EntityFramework.TypeDiscriminatorAttribute.#ctor(System.String,System.Type)')
  - [PropertyName](#P-Arebis-Core-EntityFramework-TypeDiscriminatorAttribute-PropertyName 'Arebis.Core.EntityFramework.TypeDiscriminatorAttribute.PropertyName')
  - [PropertyType](#P-Arebis-Core-EntityFramework-TypeDiscriminatorAttribute-PropertyType 'Arebis.Core.EntityFramework.TypeDiscriminatorAttribute.PropertyType')
- [TypeDiscriminatorValueAttribute](#T-Arebis-Core-EntityFramework-TypeDiscriminatorValueAttribute 'Arebis.Core.EntityFramework.TypeDiscriminatorValueAttribute')
  - [#ctor()](#M-Arebis-Core-EntityFramework-TypeDiscriminatorValueAttribute-#ctor-System-Object- 'Arebis.Core.EntityFramework.TypeDiscriminatorValueAttribute.#ctor(System.Object)')
  - [Value](#P-Arebis-Core-EntityFramework-TypeDiscriminatorValueAttribute-Value 'Arebis.Core.EntityFramework.TypeDiscriminatorValueAttribute.Value')
- [UtcDateTimeValueConverter](#T-Arebis-Core-EntityFramework-ValueConversion-UtcDateTimeValueConverter 'Arebis.Core.EntityFramework.ValueConversion.UtcDateTimeValueConverter')
  - [#ctor()](#M-Arebis-Core-EntityFramework-ValueConversion-UtcDateTimeValueConverter-#ctor 'Arebis.Core.EntityFramework.ValueConversion.UtcDateTimeValueConverter.#ctor')
- [ValidationInterceptor](#T-Arebis-Core-EntityFramework-ValidationInterceptor 'Arebis.Core.EntityFramework.ValidationInterceptor')
  - [#ctor(validatePropertyValues,alsoValidateUnchanged)](#M-Arebis-Core-EntityFramework-ValidationInterceptor-#ctor-System-Boolean,System-Boolean- 'Arebis.Core.EntityFramework.ValidationInterceptor.#ctor(System.Boolean,System.Boolean)')
  - [AlsoValidateUnchanged](#P-Arebis-Core-EntityFramework-ValidationInterceptor-AlsoValidateUnchanged 'Arebis.Core.EntityFramework.ValidationInterceptor.AlsoValidateUnchanged')
  - [ValidatePropertyValues](#P-Arebis-Core-EntityFramework-ValidationInterceptor-ValidatePropertyValues 'Arebis.Core.EntityFramework.ValidationInterceptor.ValidatePropertyValues')
  - [SavingChanges()](#M-Arebis-Core-EntityFramework-ValidationInterceptor-SavingChanges-Microsoft-EntityFrameworkCore-Diagnostics-DbContextEventData,Microsoft-EntityFrameworkCore-Diagnostics-InterceptionResult{System-Int32}- 'Arebis.Core.EntityFramework.ValidationInterceptor.SavingChanges(Microsoft.EntityFrameworkCore.Diagnostics.DbContextEventData,Microsoft.EntityFrameworkCore.Diagnostics.InterceptionResult{System.Int32})')
  - [SavingChangesAsync()](#M-Arebis-Core-EntityFramework-ValidationInterceptor-SavingChangesAsync-Microsoft-EntityFrameworkCore-Diagnostics-DbContextEventData,Microsoft-EntityFrameworkCore-Diagnostics-InterceptionResult{System-Int32},System-Threading-CancellationToken- 'Arebis.Core.EntityFramework.ValidationInterceptor.SavingChangesAsync(Microsoft.EntityFrameworkCore.Diagnostics.DbContextEventData,Microsoft.EntityFrameworkCore.Diagnostics.InterceptionResult{System.Int32},System.Threading.CancellationToken)')

<a name='T-Arebis-Core-EntityFramework-BaseContextualEntity`1'></a>
## BaseContextualEntity\`1 `type`

##### Namespace

Arebis.Core.EntityFramework

##### Summary

Abstract base entity class that implements [IContextualEntity\`1](#T-Arebis-Core-EntityFramework-IContextualEntity`1 'Arebis.Core.EntityFramework.IContextualEntity`1') to provide access to the context.

<a name='P-Arebis-Core-EntityFramework-BaseContextualEntity`1-Context'></a>
### Context `property`

##### Summary

*Inherit from parent.*

<a name='T-Arebis-Core-EntityFramework-BaseDbContextExtensions'></a>
## BaseDbContextExtensions `type`

##### Namespace

Arebis.Core.EntityFramework

##### Summary

BaseDbContext extension helpers.

<a name='M-Arebis-Core-EntityFramework-BaseDbContextExtensions-UseStoreEmptyAsNullAttributes-Microsoft-EntityFrameworkCore-DbContextOptionsBuilder-'></a>
### UseStoreEmptyAsNullAttributes(optionsBuilder) `method`

##### Summary

Installs an interceptor to support [StoreEmptyAsNull] attributes.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| optionsBuilder | [Microsoft.EntityFrameworkCore.DbContextOptionsBuilder](#T-Microsoft-EntityFrameworkCore-DbContextOptionsBuilder 'Microsoft.EntityFrameworkCore.DbContextOptionsBuilder') | OnConfiguring method parameter |

<a name='M-Arebis-Core-EntityFramework-BaseDbContextExtensions-UseStringTrimming-Microsoft-EntityFrameworkCore-DbContextOptionsBuilder,System-Boolean-'></a>
### UseStringTrimming(optionsBuilder,storeEmptyAsNull) `method`

##### Summary

Installs an interceptor to trim changed strings before storing them.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| optionsBuilder | [Microsoft.EntityFrameworkCore.DbContextOptionsBuilder](#T-Microsoft-EntityFrameworkCore-DbContextOptionsBuilder 'Microsoft.EntityFrameworkCore.DbContextOptionsBuilder') | OnConfiguring method parameter |
| storeEmptyAsNull | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Whether to store empty strings as null if the property is nullable. |

<a name='M-Arebis-Core-EntityFramework-BaseDbContextExtensions-UseValidation-Microsoft-EntityFrameworkCore-DbContextOptionsBuilder,System-Boolean,System-Boolean-'></a>
### UseValidation(optionsBuilder,validatePropertyValues,alsoValidateUnchanged) `method`

##### Summary

Installs an interceptor that validates entities before saving.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| optionsBuilder | [Microsoft.EntityFrameworkCore.DbContextOptionsBuilder](#T-Microsoft-EntityFrameworkCore-DbContextOptionsBuilder 'Microsoft.EntityFrameworkCore.DbContextOptionsBuilder') | OnConfiguring method parameter |
| validatePropertyValues | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Whether to validate not only whether the property has a value (if [Required]),
but also whether the value is valid towards other annotations as [Range] or [StringLength].
True by default. |
| alsoValidateUnchanged | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Whether to also validate unchanged entities. This can be required to validate rules based
on the count of related entities. By default false: only modified and added entities are validated. |

<a name='T-Arebis-Core-EntityFramework-BaseDbContext`1'></a>
## BaseDbContext\`1 `type`

##### Namespace

Arebis.Core.EntityFramework

##### Summary

A base class for DbContext with additional services.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TContext | The concrete DbContext type. |

<a name='M-Arebis-Core-EntityFramework-BaseDbContext`1-#ctor-Microsoft-EntityFrameworkCore-DbContextOptions-'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='M-Arebis-Core-EntityFramework-BaseDbContext`1-AddAfterSaveAction-System-Action-'></a>
### AddAfterSaveAction() `method`

##### Summary

Registers an action to be executed after the next successful SaveChanges() call.

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-EntityFramework-BaseDbContext`1-ConfigureDefaultConverters-System-Collections-Generic-IDictionary{System-Type,Arebis-Core-EntityFramework-ValueConversion-ConverterAttribute}-'></a>
### ConfigureDefaultConverters() `method`

##### Summary

Override this method to configure default converter attributes for property types.

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-EntityFramework-BaseDbContext`1-OnEntitySaving-Microsoft-EntityFrameworkCore-ChangeTracking-EntityEntry-'></a>
### OnEntitySaving() `method`

##### Summary

Called before saving an added, changed or deleted entity.

##### Returns

True if the method may have performed changes on entities, false otherwise.

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-EntityFramework-BaseDbContext`1-OnEntityTracked-Microsoft-EntityFrameworkCore-ChangeTracking-EntityEntry-'></a>
### OnEntityTracked() `method`

##### Summary

Called when an entity is being tracked.

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-EntityFramework-BaseDbContext`1-OnModelCreating-Microsoft-EntityFrameworkCore-ModelBuilder-'></a>
### OnModelCreating() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

##### Remarks

When overriding, call base.OnModelCreating(modelBuilder) first.

<a name='M-Arebis-Core-EntityFramework-BaseDbContext`1-SaveChanges'></a>
### SaveChanges() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-EntityFramework-BaseDbContext`1-SaveChanges-System-Boolean-'></a>
### SaveChanges() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-EntityFramework-BaseDbContext`1-SaveChangesAsync-System-Threading-CancellationToken-'></a>
### SaveChangesAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-EntityFramework-BaseDbContext`1-SaveChangesAsync-System-Boolean,System-Threading-CancellationToken-'></a>
### SaveChangesAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-Arebis-Core-EntityFramework-BaseReadOnlyEntity'></a>
## BaseReadOnlyEntity `type`

##### Namespace

Arebis.Core.EntityFramework

##### Summary

A base class for read-only entities that are associated with a specific DbContext.
Prevents saving changes to the database.

<a name='M-Arebis-Core-EntityFramework-BaseReadOnlyEntity-OnSaving-Microsoft-EntityFrameworkCore-ChangeTracking-EntityEntry-'></a>
### OnSaving() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-Arebis-Core-EntityFramework-BaseReadOnlyEntity`1'></a>
## BaseReadOnlyEntity\`1 `type`

##### Namespace

Arebis.Core.EntityFramework

##### Summary

A base class for read-only entities that are associated with a specific DbContext.
Prevents saving changes to the database.
Implements [IContextualEntity\`1](#T-Arebis-Core-EntityFramework-IContextualEntity`1 'Arebis.Core.EntityFramework.IContextualEntity`1') to provide access to the context.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TContext | The context type the entity type belongs to. |

<a name='P-Arebis-Core-EntityFramework-BaseReadOnlyEntity`1-Context'></a>
### Context `property`

##### Summary

*Inherit from parent.*

<a name='T-Arebis-Core-EntityFramework-ValueConversion-ConverterAttribute'></a>
## ConverterAttribute `type`

##### Namespace

Arebis.Core.EntityFramework.ValueConversion

##### Summary

Attribute to declare a specific value converter (and comparer) for the decorated property.
Requires the DbContext to inherit from BaseDbContext.

<a name='M-Arebis-Core-EntityFramework-ValueConversion-ConverterAttribute-#ctor-System-Type,System-Object[]-'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='M-Arebis-Core-EntityFramework-ValueConversion-ConverterAttribute-#ctor-System-Type,System-Type-'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='M-Arebis-Core-EntityFramework-ValueConversion-ConverterAttribute-#ctor-System-Type,System-Object[],System-Type,System-Object[]-'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='P-Arebis-Core-EntityFramework-ValueConversion-ConverterAttribute-ComparerConstructorArgs'></a>
### ComparerConstructorArgs `property`

##### Summary

Constructor arguments for the comparer.

<a name='P-Arebis-Core-EntityFramework-ValueConversion-ConverterAttribute-ComparerType'></a>
### ComparerType `property`

##### Summary

Type of comparer.

<a name='P-Arebis-Core-EntityFramework-ValueConversion-ConverterAttribute-ConverterConstructorArgs'></a>
### ConverterConstructorArgs `property`

##### Summary

Constructor arguments for the converter.

<a name='P-Arebis-Core-EntityFramework-ValueConversion-ConverterAttribute-ConverterType'></a>
### ConverterType `property`

##### Summary

Type of converter.

<a name='T-Arebis-Core-EntityFramework-ValueConversion-DateOnlyValueComparer'></a>
## DateOnlyValueComparer `type`

##### Namespace

Arebis.Core.EntityFramework.ValueConversion

##### Summary

A value comparer to compare DateOnly values.

<a name='M-Arebis-Core-EntityFramework-ValueConversion-DateOnlyValueComparer-#ctor'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='T-Arebis-Core-EntityFramework-ValueConversion-DateOnlyValueConverter'></a>
## DateOnlyValueConverter `type`

##### Namespace

Arebis.Core.EntityFramework.ValueConversion

##### Summary

A value converter that stores DateOnly as DateTime.

<a name='M-Arebis-Core-EntityFramework-ValueConversion-DateOnlyValueConverter-#ctor'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='T-Arebis-Core-EntityFramework-DefaultSchemaAttribute'></a>
## DefaultSchemaAttribute `type`

##### Namespace

Arebis.Core.EntityFramework

##### Summary

Default relational schema for this DbContext.
Requires the DbContext to inherit from BaseDbContext.

<a name='M-Arebis-Core-EntityFramework-DefaultSchemaAttribute-#ctor-System-String-'></a>
### #ctor() `constructor`

##### Summary

Sets the default relational schema for this DbContext.

##### Parameters

This constructor has no parameters.

<a name='P-Arebis-Core-EntityFramework-DefaultSchemaAttribute-SchemaName'></a>
### SchemaName `property`

##### Summary

The default relational schema of this DbContext.

<a name='T-Arebis-Core-EntityFramework-ValueConversion-DictionaryValueConverter`3'></a>
## DictionaryValueConverter\`3 `type`

##### Namespace

Arebis.Core.EntityFramework.ValueConversion

##### Summary

Converts a Dictionary to a string representation.

<a name='M-Arebis-Core-EntityFramework-ValueConversion-DictionaryValueConverter`3-#ctor'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='M-Arebis-Core-EntityFramework-ValueConversion-DictionaryValueConverter`3-#ctor-System-String,System-String-'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='M-Arebis-Core-EntityFramework-ValueConversion-DictionaryValueConverter`3-#ctor-System-String,System-String,System-String,System-String-'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='T-Arebis-Core-EntityFramework-Extensions'></a>
## Extensions `type`

##### Namespace

Arebis.Core.EntityFramework

##### Summary

Entity Framework extension methods.

<a name='M-Arebis-Core-EntityFramework-Extensions-AddNew``1-Microsoft-EntityFrameworkCore-DbSet{``0},System-Object[]-'></a>
### AddNew\`\`1() `method`

##### Summary

Creates new entity or entity proxy and adds it to the context.

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-EntityFramework-Extensions-AreProxiesEnabled-Microsoft-EntityFrameworkCore-DbContext-'></a>
### AreProxiesEnabled() `method`

##### Summary

Tests whether proxies are enabled on the context.

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-EntityFramework-Extensions-FindOrFailAsync``1-Microsoft-EntityFrameworkCore-DbSet{``0},System-Object[]-'></a>
### FindOrFailAsync\`\`1() `method`

##### Summary

Finds an entity by primary key or throws NullReferenceException if not found.

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-EntityFramework-Extensions-FindOrFailAsync``1-Microsoft-EntityFrameworkCore-DbSet{``0},System-Object[],System-Threading-CancellationToken-'></a>
### FindOrFailAsync\`\`1() `method`

##### Summary

Finds an entity by primary key or throws NullReferenceException if not found.

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-EntityFramework-Extensions-FindOrFail``1-Microsoft-EntityFrameworkCore-DbSet{``0},System-Object[]-'></a>
### FindOrFail\`\`1() `method`

##### Summary

Finds an entity by primary key or throws NullReferenceException if not found.

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-EntityFramework-Extensions-GetDbContext``1-Microsoft-EntityFrameworkCore-DbSet{``0}-'></a>
### GetDbContext\`\`1() `method`

##### Summary

Get the DbContext of a DbSet.

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-EntityFramework-Extensions-MarkModified``1-Arebis-Core-EntityFramework-IContextualEntity{``0}-'></a>
### MarkModified\`\`1() `method`

##### Summary

Marks a contextual entity as modified.

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-EntityFramework-Extensions-OrderBy``1-System-Linq-IQueryable{``0},System-String-'></a>
### OrderBy\`\`1(query,orderByExpression) `method`

##### Summary

Performs ordering given a string expression.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| query | [System.Linq.IQueryable{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.IQueryable 'System.Linq.IQueryable{``0}') | The query to order. |
| orderByExpression | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The property (path) to order by. Append " ASC" or " DESC" to be explicit about the ordering direction.
Can contain multiple terms separated by comma, i.e. "Name, Town ASC, Country DESC". |

<a name='M-Arebis-Core-EntityFramework-Extensions-ThenBy``1-System-Linq-IOrderedQueryable{``0},System-String-'></a>
### ThenBy\`\`1(query,orderByExpression) `method`

##### Summary

Performs further ordering given a string expression.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| query | [System.Linq.IOrderedQueryable{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.IOrderedQueryable 'System.Linq.IOrderedQueryable{``0}') | The query to order. |
| orderByExpression | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The property (path) to order by. Append " ASC" or " DESC" to be explicit about the ordering direction.
Can contain multiple terms separated by comma, i.e. "Name, Town ASC, Country DESC". |

<a name='T-Arebis-Core-EntityFramework-IContextualEntity`1'></a>
## IContextualEntity\`1 `type`

##### Namespace

Arebis.Core.EntityFramework

##### Summary

Interface implemented by context-aware entities.
Context-aware entities are entities that know their context.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TContext | Type of the context. |

<a name='P-Arebis-Core-EntityFramework-IContextualEntity`1-Context'></a>
### Context `property`

##### Summary

Context of the entity.

<a name='T-Arebis-Core-EntityFramework-IInterceptingEntity'></a>
## IInterceptingEntity `type`

##### Namespace

Arebis.Core.EntityFramework

##### Summary

An entity that can intercept its own save operation.

<a name='M-Arebis-Core-EntityFramework-IInterceptingEntity-OnSaving-Microsoft-EntityFrameworkCore-ChangeTracking-EntityEntry-'></a>
### OnSaving() `method`

##### Summary

Called when saving this entity.

##### Returns

True if the method may have perofrmed changes on entities, false otherwise.

##### Parameters

This method has no parameters.

<a name='T-Arebis-Core-EntityFramework-ValueConversion-JsonValueConverter'></a>
## JsonValueConverter `type`

##### Namespace

Arebis.Core.EntityFramework.ValueConversion

##### Summary

Non-generic JsonConverter class holding serializer options.

<a name='P-Arebis-Core-EntityFramework-ValueConversion-JsonValueConverter-SerializerOptions'></a>
### SerializerOptions `property`

##### Summary

Options to use when (de)serializing values.

<a name='T-Arebis-Core-EntityFramework-ValueConversion-JsonValueConverter`1'></a>
## JsonValueConverter\`1 `type`

##### Namespace

Arebis.Core.EntityFramework.ValueConversion

##### Summary

A value converter that stores objects as Json.

<a name='M-Arebis-Core-EntityFramework-ValueConversion-JsonValueConverter`1-#ctor'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='T-Arebis-Core-EntityFramework-ValueConversion-ListValueConverter`2'></a>
## ListValueConverter\`2 `type`

##### Namespace

Arebis.Core.EntityFramework.ValueConversion

##### Summary

Converts a list into a string representation.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TList |  |
| TItem |  |

<a name='M-Arebis-Core-EntityFramework-ValueConversion-ListValueConverter`2-#ctor'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='M-Arebis-Core-EntityFramework-ValueConversion-ListValueConverter`2-#ctor-System-String-'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='M-Arebis-Core-EntityFramework-ValueConversion-ListValueConverter`2-#ctor-System-String,System-String,System-String-'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='T-Arebis-Core-EntityFramework-MappedFieldAttribute'></a>
## MappedFieldAttribute `type`

##### Namespace

Arebis.Core.EntityFramework

##### Summary

Marks a field as being mapped.
Allowd mapping database columns to private fields without exposing a public property.

<a name='M-Arebis-Core-EntityFramework-MappedFieldAttribute-#ctor'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='T-Arebis-Core-EntityFramework-ValueConversion-RegexValueConverter'></a>
## RegexValueConverter `type`

##### Namespace

Arebis.Core.EntityFramework.ValueConversion

##### Summary

A value converter that stores a Regex as string.

<a name='M-Arebis-Core-EntityFramework-ValueConversion-RegexValueConverter-#ctor'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='M-Arebis-Core-EntityFramework-ValueConversion-RegexValueConverter-#ctor-System-Text-RegularExpressions-RegexOptions-'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='T-Arebis-Core-EntityFramework-StoreEmptyAsNullAttribute'></a>
## StoreEmptyAsNullAttribute `type`

##### Namespace

Arebis.Core.EntityFramework

##### Summary

Specifies that the decorated property is to be stored as a NULL value
when it is empty (or whitespace string). When materializing, NULL values
are converted to empty collections or empty strings.
Requires the DbContext to inherit from BaseDbContext and UseStoreEmptyAsNullAttribute()
to be invoked from the context's OnConfiguring method.

<a name='P-Arebis-Core-EntityFramework-StoreEmptyAsNullAttribute-AllowNullOnStorage'></a>
### AllowNullOnStorage `property`

##### Summary

Whether to allow Null to be stored. By default true. If false,
this means that empty strings or collections are not allowed.

<a name='P-Arebis-Core-EntityFramework-StoreEmptyAsNullAttribute-InstanceType'></a>
### InstanceType `property`

##### Summary

The instance type to create when a NULL value is dematerialized.

<a name='T-Arebis-Core-EntityFramework-StoreEmptyAsNullInterceptor'></a>
## StoreEmptyAsNullInterceptor `type`

##### Namespace

Arebis.Core.EntityFramework

##### Summary

EntityFramework interceptor to support [StoreEmptyAsNull] attributes.

<a name='M-Arebis-Core-EntityFramework-StoreEmptyAsNullInterceptor-InitializedInstance-Microsoft-EntityFrameworkCore-Diagnostics-MaterializationInterceptionData,System-Object-'></a>
### InitializedInstance() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-EntityFramework-StoreEmptyAsNullInterceptor-SavingChanges-Microsoft-EntityFrameworkCore-Diagnostics-DbContextEventData,Microsoft-EntityFrameworkCore-Diagnostics-InterceptionResult{System-Int32}-'></a>
### SavingChanges() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-EntityFramework-StoreEmptyAsNullInterceptor-SavingChangesAsync-Microsoft-EntityFrameworkCore-Diagnostics-DbContextEventData,Microsoft-EntityFrameworkCore-Diagnostics-InterceptionResult{System-Int32},System-Threading-CancellationToken-'></a>
### SavingChangesAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-Arebis-Core-EntityFramework-StringTrimmingInterceptor'></a>
## StringTrimmingInterceptor `type`

##### Namespace

Arebis.Core.EntityFramework

##### Summary

Interceptor that trimms changed string values before storing them.

<a name='M-Arebis-Core-EntityFramework-StringTrimmingInterceptor-#ctor-System-Boolean-'></a>
### #ctor() `constructor`

##### Summary

Interceptor that trimms changed string values before storing them.

##### Parameters

This constructor has no parameters.

<a name='P-Arebis-Core-EntityFramework-StringTrimmingInterceptor-StoreEmptyAsNull'></a>
### StoreEmptyAsNull `property`

##### Summary

Whether to store empty strings as null if the property is nullable.

<a name='M-Arebis-Core-EntityFramework-StringTrimmingInterceptor-SavingChanges-Microsoft-EntityFrameworkCore-Diagnostics-DbContextEventData,Microsoft-EntityFrameworkCore-Diagnostics-InterceptionResult{System-Int32}-'></a>
### SavingChanges() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-EntityFramework-StringTrimmingInterceptor-SavingChangesAsync-Microsoft-EntityFrameworkCore-Diagnostics-DbContextEventData,Microsoft-EntityFrameworkCore-Diagnostics-InterceptionResult{System-Int32},System-Threading-CancellationToken-'></a>
### SavingChangesAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-Arebis-Core-EntityFramework-ValueConversion-TimeOnlyValueComparer'></a>
## TimeOnlyValueComparer `type`

##### Namespace

Arebis.Core.EntityFramework.ValueConversion

##### Summary

A value comparer to compare TimeOnly values.

<a name='M-Arebis-Core-EntityFramework-ValueConversion-TimeOnlyValueComparer-#ctor'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='T-Arebis-Core-EntityFramework-ValueConversion-TimeOnlyValueConverter'></a>
## TimeOnlyValueConverter `type`

##### Namespace

Arebis.Core.EntityFramework.ValueConversion

##### Summary

A value converter that stores DateOnly as DateTime.

<a name='M-Arebis-Core-EntityFramework-ValueConversion-TimeOnlyValueConverter-#ctor'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='T-Arebis-Core-EntityFramework-ValueConversion-TimeSpanDaysValueConverter'></a>
## TimeSpanDaysValueConverter `type`

##### Namespace

Arebis.Core.EntityFramework.ValueConversion

##### Summary

A value converter that stores TimeSpans as days.

<a name='M-Arebis-Core-EntityFramework-ValueConversion-TimeSpanDaysValueConverter-#ctor'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='T-Arebis-Core-EntityFramework-ValueConversion-TimeSpanHoursValueConverter'></a>
## TimeSpanHoursValueConverter `type`

##### Namespace

Arebis.Core.EntityFramework.ValueConversion

##### Summary

A value converter that stores TimeSpans as hours.

<a name='M-Arebis-Core-EntityFramework-ValueConversion-TimeSpanHoursValueConverter-#ctor'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='T-Arebis-Core-EntityFramework-ValueConversion-TimeSpanTicksValueConverter'></a>
## TimeSpanTicksValueConverter `type`

##### Namespace

Arebis.Core.EntityFramework.ValueConversion

##### Summary

A value converter that stores TimeSpans as ticks.

<a name='M-Arebis-Core-EntityFramework-ValueConversion-TimeSpanTicksValueConverter-#ctor'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='T-Arebis-Core-EntityFramework-ValueConversion-TimeSpanValueComparer'></a>
## TimeSpanValueComparer `type`

##### Namespace

Arebis.Core.EntityFramework.ValueConversion

##### Summary

A value comparer to compare TimeSpan values.

<a name='M-Arebis-Core-EntityFramework-ValueConversion-TimeSpanValueComparer-#ctor'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='T-Arebis-Core-EntityFramework-TypeDiscriminatorAttribute'></a>
## TypeDiscriminatorAttribute `type`

##### Namespace

Arebis.Core.EntityFramework

##### Summary

Marks the hierarchy root type as having a type discriminator property.
Concrete subtypes must have a [TypeDiscriminatorValue] attribute.
If this type is concrete, it must als have the [TypeDiscriminatorValue] attribute.
Requires the DbContext to inherit from BaseDbContext.

<a name='M-Arebis-Core-EntityFramework-TypeDiscriminatorAttribute-#ctor-System-String-'></a>
### #ctor(propertyName) `constructor`

##### Summary

Defines a TypeDiscriminator property of type string.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| propertyName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the discriminator property. |

<a name='M-Arebis-Core-EntityFramework-TypeDiscriminatorAttribute-#ctor-System-String,System-Type-'></a>
### #ctor(propertyName,propertyType) `constructor`

##### Summary

Defines a TypeDiscriminator property.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| propertyName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the discriminator property. |
| propertyType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of the discriminator property. |

<a name='P-Arebis-Core-EntityFramework-TypeDiscriminatorAttribute-PropertyName'></a>
### PropertyName `property`

##### Summary

Name of the discriminator property.

<a name='P-Arebis-Core-EntityFramework-TypeDiscriminatorAttribute-PropertyType'></a>
### PropertyType `property`

##### Summary

Type of the discriminator property.

<a name='T-Arebis-Core-EntityFramework-TypeDiscriminatorValueAttribute'></a>
## TypeDiscriminatorValueAttribute `type`

##### Namespace

Arebis.Core.EntityFramework

##### Summary

Specifies the value of the type discriminator property that corresponds to the decorated type.
The root type of the hierarchy must have a [TypeDiscriminator] attribute.

<a name='M-Arebis-Core-EntityFramework-TypeDiscriminatorValueAttribute-#ctor-System-Object-'></a>
### #ctor() `constructor`

##### Summary

Defines a value for the type discriminator property to match this entity type.

##### Parameters

This constructor has no parameters.

<a name='P-Arebis-Core-EntityFramework-TypeDiscriminatorValueAttribute-Value'></a>
### Value `property`

##### Summary

Value of the discriminator property.

<a name='T-Arebis-Core-EntityFramework-ValueConversion-UtcDateTimeValueConverter'></a>
## UtcDateTimeValueConverter `type`

##### Namespace

Arebis.Core.EntityFramework.ValueConversion

##### Summary

A value converter that stores DateTimes in UTC.

<a name='M-Arebis-Core-EntityFramework-ValueConversion-UtcDateTimeValueConverter-#ctor'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='T-Arebis-Core-EntityFramework-ValidationInterceptor'></a>
## ValidationInterceptor `type`

##### Namespace

Arebis.Core.EntityFramework

##### Summary

An interceptor that validates entities befora saving.

<a name='M-Arebis-Core-EntityFramework-ValidationInterceptor-#ctor-System-Boolean,System-Boolean-'></a>
### #ctor(validatePropertyValues,alsoValidateUnchanged) `constructor`

##### Summary

An interceptor that validates entities before saving.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| validatePropertyValues | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Whether to validate not only whether the property has a value (if [Required]),
but also whether the value is valid towards other annotations as [Range] or [StringLength].
True by default. |
| alsoValidateUnchanged | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Whether to also validate unchanged entities. This can be required to validate rules based
on the count of related entities. By default false: only modified and added entities are validated. |

<a name='P-Arebis-Core-EntityFramework-ValidationInterceptor-AlsoValidateUnchanged'></a>
### AlsoValidateUnchanged `property`

##### Summary

Validate not only modified and added entities, but also unchanged entities.
Can be required to validate rules based on the count of related entities.

<a name='P-Arebis-Core-EntityFramework-ValidationInterceptor-ValidatePropertyValues'></a>
### ValidatePropertyValues `property`

##### Summary

Validate not only whether the property has a value (if [Required]),
but also whether the value is valid towards other annotations as [Range] or [StringLength].

<a name='M-Arebis-Core-EntityFramework-ValidationInterceptor-SavingChanges-Microsoft-EntityFrameworkCore-Diagnostics-DbContextEventData,Microsoft-EntityFrameworkCore-Diagnostics-InterceptionResult{System-Int32}-'></a>
### SavingChanges() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-EntityFramework-ValidationInterceptor-SavingChangesAsync-Microsoft-EntityFrameworkCore-Diagnostics-DbContextEventData,Microsoft-EntityFrameworkCore-Diagnostics-InterceptionResult{System-Int32},System-Threading-CancellationToken-'></a>
### SavingChangesAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.
