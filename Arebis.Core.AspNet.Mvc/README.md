<a name='assembly'></a>
# Arebis.Core.AspNet.Mvc

## Contents

- [ApplicationBuilderExtensions](#T-Arebis-Core-AspNet-Mvc-ApplicationBuilderExtensions 'Arebis.Core.AspNet.Mvc.ApplicationBuilderExtensions')
- [AuthorizationPolicyTagHelper](#T-Arebis-Core-AspNet-Mvc-TagHelpers-AuthorizationPolicyTagHelper 'Arebis.Core.AspNet.Mvc.TagHelpers.AuthorizationPolicyTagHelper')
  - [#ctor()](#M-Arebis-Core-AspNet-Mvc-TagHelpers-AuthorizationPolicyTagHelper-#ctor-Microsoft-AspNetCore-Http-IHttpContextAccessor,Microsoft-AspNetCore-Authorization-IAuthorizationPolicyProvider,Microsoft-AspNetCore-Authorization-Policy-IPolicyEvaluator- 'Arebis.Core.AspNet.Mvc.TagHelpers.AuthorizationPolicyTagHelper.#ctor(Microsoft.AspNetCore.Http.IHttpContextAccessor,Microsoft.AspNetCore.Authorization.IAuthorizationPolicyProvider,Microsoft.AspNetCore.Authorization.Policy.IPolicyEvaluator)')
  - [AuthenticationSchemes](#P-Arebis-Core-AspNet-Mvc-TagHelpers-AuthorizationPolicyTagHelper-AuthenticationSchemes 'Arebis.Core.AspNet.Mvc.TagHelpers.AuthorizationPolicyTagHelper.AuthenticationSchemes')
  - [Enabled](#P-Arebis-Core-AspNet-Mvc-TagHelpers-AuthorizationPolicyTagHelper-Enabled 'Arebis.Core.AspNet.Mvc.TagHelpers.AuthorizationPolicyTagHelper.Enabled')
  - [Policy](#P-Arebis-Core-AspNet-Mvc-TagHelpers-AuthorizationPolicyTagHelper-Policy 'Arebis.Core.AspNet.Mvc.TagHelpers.AuthorizationPolicyTagHelper.Policy')
  - [Roles](#P-Arebis-Core-AspNet-Mvc-TagHelpers-AuthorizationPolicyTagHelper-Roles 'Arebis.Core.AspNet.Mvc.TagHelpers.AuthorizationPolicyTagHelper.Roles')
  - [ProcessAsync()](#M-Arebis-Core-AspNet-Mvc-TagHelpers-AuthorizationPolicyTagHelper-ProcessAsync-Microsoft-AspNetCore-Razor-TagHelpers-TagHelperContext,Microsoft-AspNetCore-Razor-TagHelpers-TagHelperOutput- 'Arebis.Core.AspNet.Mvc.TagHelpers.AuthorizationPolicyTagHelper.ProcessAsync(Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext,Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput)')
- [ConditionalTagHelper](#T-Arebis-Core-AspNet-Mvc-TagHelpers-ConditionalTagHelper 'Arebis.Core.AspNet.Mvc.TagHelpers.ConditionalTagHelper')
  - [Condition](#P-Arebis-Core-AspNet-Mvc-TagHelpers-ConditionalTagHelper-Condition 'Arebis.Core.AspNet.Mvc.TagHelpers.ConditionalTagHelper.Condition')
  - [ElseFor](#P-Arebis-Core-AspNet-Mvc-TagHelpers-ConditionalTagHelper-ElseFor 'Arebis.Core.AspNet.Mvc.TagHelpers.ConditionalTagHelper.ElseFor')
  - [Id](#P-Arebis-Core-AspNet-Mvc-TagHelpers-ConditionalTagHelper-Id 'Arebis.Core.AspNet.Mvc.TagHelpers.ConditionalTagHelper.Id')
  - [ViewContext](#P-Arebis-Core-AspNet-Mvc-TagHelpers-ConditionalTagHelper-ViewContext 'Arebis.Core.AspNet.Mvc.TagHelpers.ConditionalTagHelper.ViewContext')
  - [Process()](#M-Arebis-Core-AspNet-Mvc-TagHelpers-ConditionalTagHelper-Process-Microsoft-AspNetCore-Razor-TagHelpers-TagHelperContext,Microsoft-AspNetCore-Razor-TagHelpers-TagHelperOutput- 'Arebis.Core.AspNet.Mvc.TagHelpers.ConditionalTagHelper.Process(Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext,Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput)')
- [ControllerExtensions](#T-Arebis-Core-AspNet-Mvc-ControllerExtensions 'Arebis.Core.AspNet.Mvc.ControllerExtensions')
  - [BindModelAsync\`\`1(controller,prefix,initialize)](#M-Arebis-Core-AspNet-Mvc-ControllerExtensions-BindModelAsync``1-Microsoft-AspNetCore-Mvc-Controller,System-String,System-Action{``0}- 'Arebis.Core.AspNet.Mvc.ControllerExtensions.BindModelAsync``1(Microsoft.AspNetCore.Mvc.Controller,System.String,System.Action{``0})')
  - [BindModelAsync\`\`1(controller,model,prefix)](#M-Arebis-Core-AspNet-Mvc-ControllerExtensions-BindModelAsync``1-Microsoft-AspNetCore-Mvc-Controller,``0,System-String- 'Arebis.Core.AspNet.Mvc.ControllerExtensions.BindModelAsync``1(Microsoft.AspNetCore.Mvc.Controller,``0,System.String)')
- [CultureInvariantModelBinder](#T-Arebis-Core-AspNet-Mvc-ModelBinding-CultureInvariantModelBinder 'Arebis.Core.AspNet.Mvc.ModelBinding.CultureInvariantModelBinder')
  - [#ctor()](#M-Arebis-Core-AspNet-Mvc-ModelBinding-CultureInvariantModelBinder-#ctor-System-Globalization-CultureInfo- 'Arebis.Core.AspNet.Mvc.ModelBinding.CultureInvariantModelBinder.#ctor(System.Globalization.CultureInfo)')
  - [BindModelAsync()](#M-Arebis-Core-AspNet-Mvc-ModelBinding-CultureInvariantModelBinder-BindModelAsync-Microsoft-AspNetCore-Mvc-ModelBinding-ModelBindingContext- 'Arebis.Core.AspNet.Mvc.ModelBinding.CultureInvariantModelBinder.BindModelAsync(Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext)')
- [CultureInvariantModelBinderProvider](#T-Arebis-Core-AspNet-Mvc-ModelBinding-CultureInvariantModelBinderProvider 'Arebis.Core.AspNet.Mvc.ModelBinding.CultureInvariantModelBinderProvider')
  - [#ctor()](#M-Arebis-Core-AspNet-Mvc-ModelBinding-CultureInvariantModelBinderProvider-#ctor 'Arebis.Core.AspNet.Mvc.ModelBinding.CultureInvariantModelBinderProvider.#ctor')
  - [#ctor()](#M-Arebis-Core-AspNet-Mvc-ModelBinding-CultureInvariantModelBinderProvider-#ctor-System-Globalization-CultureInfo- 'Arebis.Core.AspNet.Mvc.ModelBinding.CultureInvariantModelBinderProvider.#ctor(System.Globalization.CultureInfo)')
  - [GetBinder()](#M-Arebis-Core-AspNet-Mvc-ModelBinding-CultureInvariantModelBinderProvider-GetBinder-Microsoft-AspNetCore-Mvc-ModelBinding-ModelBinderProviderContext- 'Arebis.Core.AspNet.Mvc.ModelBinding.CultureInvariantModelBinderProvider.GetBinder(Microsoft.AspNetCore.Mvc.ModelBinding.ModelBinderProviderContext)')
- [IdentifierEnumModelBinder](#T-Arebis-Core-AspNet-Mvc-ModelBinding-IdentifierEnumModelBinder 'Arebis.Core.AspNet.Mvc.ModelBinding.IdentifierEnumModelBinder')
  - [BindModelAsync()](#M-Arebis-Core-AspNet-Mvc-ModelBinding-IdentifierEnumModelBinder-BindModelAsync-Microsoft-AspNetCore-Mvc-ModelBinding-ModelBindingContext- 'Arebis.Core.AspNet.Mvc.ModelBinding.IdentifierEnumModelBinder.BindModelAsync(Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext)')
- [IdentifierEnumModelBinderProvider](#T-Arebis-Core-AspNet-Mvc-ModelBinding-IdentifierEnumModelBinderProvider 'Arebis.Core.AspNet.Mvc.ModelBinding.IdentifierEnumModelBinderProvider')
  - [GetBinder()](#M-Arebis-Core-AspNet-Mvc-ModelBinding-IdentifierEnumModelBinderProvider-GetBinder-Microsoft-AspNetCore-Mvc-ModelBinding-ModelBinderProviderContext- 'Arebis.Core.AspNet.Mvc.ModelBinding.IdentifierEnumModelBinderProvider.GetBinder(Microsoft.AspNetCore.Mvc.ModelBinding.ModelBinderProviderContext)')
- [OrderableTableHeaderTagHelper](#T-Arebis-Core-AspNet-Mvc-TagHelpers-OrderableTableHeaderTagHelper 'Arebis.Core.AspNet.Mvc.TagHelpers.OrderableTableHeaderTagHelper')
  - [CurrentOrder](#P-Arebis-Core-AspNet-Mvc-TagHelpers-OrderableTableHeaderTagHelper-CurrentOrder 'Arebis.Core.AspNet.Mvc.TagHelpers.OrderableTableHeaderTagHelper.CurrentOrder')
  - [FieldName](#P-Arebis-Core-AspNet-Mvc-TagHelpers-OrderableTableHeaderTagHelper-FieldName 'Arebis.Core.AspNet.Mvc.TagHelpers.OrderableTableHeaderTagHelper.FieldName')
  - [Name](#P-Arebis-Core-AspNet-Mvc-TagHelpers-OrderableTableHeaderTagHelper-Name 'Arebis.Core.AspNet.Mvc.TagHelpers.OrderableTableHeaderTagHelper.Name')
  - [ProcessAsync()](#M-Arebis-Core-AspNet-Mvc-TagHelpers-OrderableTableHeaderTagHelper-ProcessAsync-Microsoft-AspNetCore-Razor-TagHelpers-TagHelperContext,Microsoft-AspNetCore-Razor-TagHelpers-TagHelperOutput- 'Arebis.Core.AspNet.Mvc.TagHelpers.OrderableTableHeaderTagHelper.ProcessAsync(Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext,Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput)')
- [PageSizeSelectTagHelper](#T-Arebis-Core-AspNet-Mvc-TagHelpers-PageSizeSelectTagHelper 'Arebis.Core.AspNet.Mvc.TagHelpers.PageSizeSelectTagHelper')
  - [#ctor()](#M-Arebis-Core-AspNet-Mvc-TagHelpers-PageSizeSelectTagHelper-#ctor-Microsoft-AspNetCore-Mvc-ViewFeatures-IHtmlGenerator- 'Arebis.Core.AspNet.Mvc.TagHelpers.PageSizeSelectTagHelper.#ctor(Microsoft.AspNetCore.Mvc.ViewFeatures.IHtmlGenerator)')
  - [Sizes](#P-Arebis-Core-AspNet-Mvc-TagHelpers-PageSizeSelectTagHelper-Sizes 'Arebis.Core.AspNet.Mvc.TagHelpers.PageSizeSelectTagHelper.Sizes')
  - [Init()](#M-Arebis-Core-AspNet-Mvc-TagHelpers-PageSizeSelectTagHelper-Init-Microsoft-AspNetCore-Razor-TagHelpers-TagHelperContext- 'Arebis.Core.AspNet.Mvc.TagHelpers.PageSizeSelectTagHelper.Init(Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext)')
  - [Process()](#M-Arebis-Core-AspNet-Mvc-TagHelpers-PageSizeSelectTagHelper-Process-Microsoft-AspNetCore-Razor-TagHelpers-TagHelperContext,Microsoft-AspNetCore-Razor-TagHelpers-TagHelperOutput- 'Arebis.Core.AspNet.Mvc.TagHelpers.PageSizeSelectTagHelper.Process(Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext,Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput)')
- [PaginationNavTagHelper](#T-Arebis-Core-AspNet-Mvc-TagHelpers-PaginationNavTagHelper 'Arebis.Core.AspNet.Mvc.TagHelpers.PaginationNavTagHelper')
  - [For](#P-Arebis-Core-AspNet-Mvc-TagHelpers-PaginationNavTagHelper-For 'Arebis.Core.AspNet.Mvc.TagHelpers.PaginationNavTagHelper.For')
  - [HasNextPage](#P-Arebis-Core-AspNet-Mvc-TagHelpers-PaginationNavTagHelper-HasNextPage 'Arebis.Core.AspNet.Mvc.TagHelpers.PaginationNavTagHelper.HasNextPage')
  - [Keyboard](#P-Arebis-Core-AspNet-Mvc-TagHelpers-PaginationNavTagHelper-Keyboard 'Arebis.Core.AspNet.Mvc.TagHelpers.PaginationNavTagHelper.Keyboard')
  - [Max](#P-Arebis-Core-AspNet-Mvc-TagHelpers-PaginationNavTagHelper-Max 'Arebis.Core.AspNet.Mvc.TagHelpers.PaginationNavTagHelper.Max')
  - [Min](#P-Arebis-Core-AspNet-Mvc-TagHelpers-PaginationNavTagHelper-Min 'Arebis.Core.AspNet.Mvc.TagHelpers.PaginationNavTagHelper.Min')
  - [NextText](#P-Arebis-Core-AspNet-Mvc-TagHelpers-PaginationNavTagHelper-NextText 'Arebis.Core.AspNet.Mvc.TagHelpers.PaginationNavTagHelper.NextText')
  - [PreviousText](#P-Arebis-Core-AspNet-Mvc-TagHelpers-PaginationNavTagHelper-PreviousText 'Arebis.Core.AspNet.Mvc.TagHelpers.PaginationNavTagHelper.PreviousText')
  - [StyleTemplate](#P-Arebis-Core-AspNet-Mvc-TagHelpers-PaginationNavTagHelper-StyleTemplate 'Arebis.Core.AspNet.Mvc.TagHelpers.PaginationNavTagHelper.StyleTemplate')
  - [Process()](#M-Arebis-Core-AspNet-Mvc-TagHelpers-PaginationNavTagHelper-Process-Microsoft-AspNetCore-Razor-TagHelpers-TagHelperContext,Microsoft-AspNetCore-Razor-TagHelpers-TagHelperOutput- 'Arebis.Core.AspNet.Mvc.TagHelpers.PaginationNavTagHelper.Process(Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext,Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput)')
- [PolymorphObjectModelBinder](#T-Arebis-Core-AspNet-Mvc-ModelBinding-PolymorphObjectModelBinder 'Arebis.Core.AspNet.Mvc.ModelBinding.PolymorphObjectModelBinder')
  - [#ctor()](#M-Arebis-Core-AspNet-Mvc-ModelBinding-PolymorphObjectModelBinder-#ctor-System-Collections-Generic-Dictionary{System-String,System-ValueTuple{Microsoft-AspNetCore-Mvc-ModelBinding-ModelMetadata,Microsoft-AspNetCore-Mvc-ModelBinding-IModelBinder}}- 'Arebis.Core.AspNet.Mvc.ModelBinding.PolymorphObjectModelBinder.#ctor(System.Collections.Generic.Dictionary{System.String,System.ValueTuple{Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata,Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder}})')
  - [BindModelAsync()](#M-Arebis-Core-AspNet-Mvc-ModelBinding-PolymorphObjectModelBinder-BindModelAsync-Microsoft-AspNetCore-Mvc-ModelBinding-ModelBindingContext- 'Arebis.Core.AspNet.Mvc.ModelBinding.PolymorphObjectModelBinder.BindModelAsync(Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext)')
- [PolymorphObjectModelBinderProvider](#T-Arebis-Core-AspNet-Mvc-ModelBinding-PolymorphObjectModelBinderProvider 'Arebis.Core.AspNet.Mvc.ModelBinding.PolymorphObjectModelBinderProvider')
  - [GetBinder()](#M-Arebis-Core-AspNet-Mvc-ModelBinding-PolymorphObjectModelBinderProvider-GetBinder-Microsoft-AspNetCore-Mvc-ModelBinding-ModelBinderProviderContext- 'Arebis.Core.AspNet.Mvc.ModelBinding.PolymorphObjectModelBinderProvider.GetBinder(Microsoft.AspNetCore.Mvc.ModelBinding.ModelBinderProviderContext)')

<a name='T-Arebis-Core-AspNet-Mvc-ApplicationBuilderExtensions'></a>
## ApplicationBuilderExtensions `type`

##### Namespace

Arebis.Core.AspNet.Mvc

##### Summary

ApplicationBuilder extensions for Arebis.Core.AspNet.Mvc features.

<a name='T-Arebis-Core-AspNet-Mvc-TagHelpers-AuthorizationPolicyTagHelper'></a>
## AuthorizationPolicyTagHelper `type`

##### Namespace

Arebis.Core.AspNet.Mvc.TagHelpers

##### Summary

Authorization taghelper.
Add the "asp-authorize" attribute alone or in combination with "asp-roles" or "asp-policy" attributes.
Set authentication schemes with "asp-authentication-schemes".

##### Example

```html
<button asp-authorize asp-roles="Admin,Manager">Admin or Manager Button</button>
```

<a name='M-Arebis-Core-AspNet-Mvc-TagHelpers-AuthorizationPolicyTagHelper-#ctor-Microsoft-AspNetCore-Http-IHttpContextAccessor,Microsoft-AspNetCore-Authorization-IAuthorizationPolicyProvider,Microsoft-AspNetCore-Authorization-Policy-IPolicyEvaluator-'></a>
### #ctor() `constructor`

##### Summary

Constructs an AuthorizationPolicyTagHelper.

##### Parameters

This constructor has no parameters.

<a name='P-Arebis-Core-AspNet-Mvc-TagHelpers-AuthorizationPolicyTagHelper-AuthenticationSchemes'></a>
### AuthenticationSchemes `property`

##### Summary

Gets or sets a comma delimited list of schemes from which user information is constructed.

<a name='P-Arebis-Core-AspNet-Mvc-TagHelpers-AuthorizationPolicyTagHelper-Enabled'></a>
### Enabled `property`

##### Summary

Whether authorization is required. If set, user must be logged in.

<a name='P-Arebis-Core-AspNet-Mvc-TagHelpers-AuthorizationPolicyTagHelper-Policy'></a>
### Policy `property`

##### Summary

Gets or sets the policy name that determines access to the HTML block.

<a name='P-Arebis-Core-AspNet-Mvc-TagHelpers-AuthorizationPolicyTagHelper-Roles'></a>
### Roles `property`

##### Summary

Gets or sets a comma delimited list of roles that are allowed to access the HTML  block.

<a name='M-Arebis-Core-AspNet-Mvc-TagHelpers-AuthorizationPolicyTagHelper-ProcessAsync-Microsoft-AspNetCore-Razor-TagHelpers-TagHelperContext,Microsoft-AspNetCore-Razor-TagHelpers-TagHelperOutput-'></a>
### ProcessAsync() `method`

##### Summary

Processes the taghelper.

##### Parameters

This method has no parameters.

<a name='T-Arebis-Core-AspNet-Mvc-TagHelpers-ConditionalTagHelper'></a>
## ConditionalTagHelper `type`

##### Namespace

Arebis.Core.AspNet.Mvc.TagHelpers

##### Summary

Condition taghelper. Determines whether to render depending on the condition outcome.

##### Example

An if can be expressed as follows:

```html
<div id="c1" asp-if="Model.Number < 10">...less than 10...</div>
```

The id attribute is optional but required when using elsefor, as in:

```html
<div asp-elsefor="c1" asp-if="Model.Number < 100">...from 10 to 100...</div>
```

Note how an else-if branch uses asp-elsefor to refer to the original if condition, in combination with an asp-if. The final else branch would be:

```html
<div asp-elsefor="c1">...100 or more...</div>
```

The tags hving asp-if and asp-elsefor attributes do not need to be subsequent, they can be anywhere in the page.

<a name='P-Arebis-Core-AspNet-Mvc-TagHelpers-ConditionalTagHelper-Condition'></a>
### Condition `property`

##### Summary

Condition to be true for this element to be rendered.

<a name='P-Arebis-Core-AspNet-Mvc-TagHelpers-ConditionalTagHelper-ElseFor'></a>
### ElseFor `property`

##### Summary

Id of the element holding the original if condition this is an else branch of.

<a name='P-Arebis-Core-AspNet-Mvc-TagHelpers-ConditionalTagHelper-Id'></a>
### Id `property`

##### Summary

Optional id of this element.

<a name='P-Arebis-Core-AspNet-Mvc-TagHelpers-ConditionalTagHelper-ViewContext'></a>
### ViewContext `property`

##### Summary

ViewContext for this taghelper.

<a name='M-Arebis-Core-AspNet-Mvc-TagHelpers-ConditionalTagHelper-Process-Microsoft-AspNetCore-Razor-TagHelpers-TagHelperContext,Microsoft-AspNetCore-Razor-TagHelpers-TagHelperOutput-'></a>
### Process() `method`

##### Summary

Processes the taghelper.

##### Parameters

This method has no parameters.

<a name='T-Arebis-Core-AspNet-Mvc-ControllerExtensions'></a>
## ControllerExtensions `type`

##### Namespace

Arebis.Core.AspNet.Mvc

##### Summary

Controller extension methods.

<a name='M-Arebis-Core-AspNet-Mvc-ControllerExtensions-BindModelAsync``1-Microsoft-AspNetCore-Mvc-Controller,System-String,System-Action{``0}-'></a>
### BindModelAsync\`\`1(controller,prefix,initialize) `method`

##### Summary

Binds the request to a model of the given type T.

##### Returns

The bound model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| controller | [Microsoft.AspNetCore.Mvc.Controller](#T-Microsoft-AspNetCore-Mvc-Controller 'Microsoft.AspNetCore.Mvc.Controller') | The controller handling the request. |
| prefix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional prefix of the model. |
| initialize | [System.Action{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action{``0}') | Optional model initilizer. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The model type. |

##### Example

```csharp
public async Task<IActionResult> Submit()
{
    var model = await this.BindModelAsync<MyModel>();
    ...
    // Optionally validate the model:
    TryValidateModel(model);
    if (ModelState.IsValid)
    ...
}
```

<a name='M-Arebis-Core-AspNet-Mvc-ControllerExtensions-BindModelAsync``1-Microsoft-AspNetCore-Mvc-Controller,``0,System-String-'></a>
### BindModelAsync\`\`1(controller,model,prefix) `method`

##### Summary

Binds the request to a model of the given type T.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| controller | [Microsoft.AspNetCore.Mvc.Controller](#T-Microsoft-AspNetCore-Mvc-Controller 'Microsoft.AspNetCore.Mvc.Controller') | The controller handling the request. |
| model | [\`\`0](#T-``0 '``0') | The model to bind. |
| prefix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional prefix of the model. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The model type. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Raised if no model is given. |

##### Example

```csharp
public async Task<IActionResult> Submit()
{
    var model = new MyModel();
    await this.BindModelAsync(model);
    ...
    // Optionally validate the model:
    TryValidateModel(model);
    if (ModelState.IsValid)
    ...
}
```

<a name='T-Arebis-Core-AspNet-Mvc-ModelBinding-CultureInvariantModelBinder'></a>
## CultureInvariantModelBinder `type`

##### Namespace

Arebis.Core.AspNet.Mvc.ModelBinding

##### Summary

A model binder that binds values using the given culture instead of the ambient culture.

<a name='M-Arebis-Core-AspNet-Mvc-ModelBinding-CultureInvariantModelBinder-#ctor-System-Globalization-CultureInfo-'></a>
### #ctor() `constructor`

##### Summary

Constructs a CultureInvariantModelBinder using the given culture.

##### Parameters

This constructor has no parameters.

<a name='M-Arebis-Core-AspNet-Mvc-ModelBinding-CultureInvariantModelBinder-BindModelAsync-Microsoft-AspNetCore-Mvc-ModelBinding-ModelBindingContext-'></a>
### BindModelAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-Arebis-Core-AspNet-Mvc-ModelBinding-CultureInvariantModelBinderProvider'></a>
## CultureInvariantModelBinderProvider `type`

##### Namespace

Arebis.Core.AspNet.Mvc.ModelBinding

##### Summary

Provides a model binder that binds values using the given culture instead of the ambient culture.

<a name='M-Arebis-Core-AspNet-Mvc-ModelBinding-CultureInvariantModelBinderProvider-#ctor'></a>
### #ctor() `constructor`

##### Summary

Constructs a CultureInvariantModelBinderProvider for the InvariantCulture.

##### Parameters

This constructor has no parameters.

<a name='M-Arebis-Core-AspNet-Mvc-ModelBinding-CultureInvariantModelBinderProvider-#ctor-System-Globalization-CultureInfo-'></a>
### #ctor() `constructor`

##### Summary

Constructs a CultureInvariantModelBinderProvider for the given culture.

##### Parameters

This constructor has no parameters.

<a name='M-Arebis-Core-AspNet-Mvc-ModelBinding-CultureInvariantModelBinderProvider-GetBinder-Microsoft-AspNetCore-Mvc-ModelBinding-ModelBinderProviderContext-'></a>
### GetBinder() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-Arebis-Core-AspNet-Mvc-ModelBinding-IdentifierEnumModelBinder'></a>
## IdentifierEnumModelBinder `type`

##### Namespace

Arebis.Core.AspNet.Mvc.ModelBinding

##### Summary

The IdentifierEnumModelBinder binds int values to enums even if there is no matching enum value
provided the enum type has the [IdentifierEnum] attribute.

<a name='M-Arebis-Core-AspNet-Mvc-ModelBinding-IdentifierEnumModelBinder-BindModelAsync-Microsoft-AspNetCore-Mvc-ModelBinding-ModelBindingContext-'></a>
### BindModelAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-Arebis-Core-AspNet-Mvc-ModelBinding-IdentifierEnumModelBinderProvider'></a>
## IdentifierEnumModelBinderProvider `type`

##### Namespace

Arebis.Core.AspNet.Mvc.ModelBinding

##### Summary

Provides a model binder that binds int values to enums even if there is no matching enum value
provided the enum type has the [IdentifierEnum] attribute.

<a name='M-Arebis-Core-AspNet-Mvc-ModelBinding-IdentifierEnumModelBinderProvider-GetBinder-Microsoft-AspNetCore-Mvc-ModelBinding-ModelBinderProviderContext-'></a>
### GetBinder() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-Arebis-Core-AspNet-Mvc-TagHelpers-OrderableTableHeaderTagHelper'></a>
## OrderableTableHeaderTagHelper `type`

##### Namespace

Arebis.Core.AspNet.Mvc.TagHelpers

##### Summary

Creates a sortable table header.
Set the "asp-order" attribute with as value the current ordering expression to make the header sortable.
Add a "field-name" attribute if the table header innertext is different from the field name.

##### Example

Typically, use a property on the model to hold the current order expression, and use the taghelper as follows:

```html
<tr>
  <th asp-order="@Model.OrderBy">Name</th>
  <th asp-order="@Model.OrderBy">Town</th>
  <th asp-order="@Model.OrderBy" field-name="DateOfBirth">Date of birth</th>
</tr>
```

<a name='P-Arebis-Core-AspNet-Mvc-TagHelpers-OrderableTableHeaderTagHelper-CurrentOrder'></a>
### CurrentOrder `property`

##### Summary

Current order expression.

<a name='P-Arebis-Core-AspNet-Mvc-TagHelpers-OrderableTableHeaderTagHelper-FieldName'></a>
### FieldName `property`

##### Summary

Name of the field.

<a name='P-Arebis-Core-AspNet-Mvc-TagHelpers-OrderableTableHeaderTagHelper-Name'></a>
### Name `property`

##### Summary

Name of the form fields (by default "order").

<a name='M-Arebis-Core-AspNet-Mvc-TagHelpers-OrderableTableHeaderTagHelper-ProcessAsync-Microsoft-AspNetCore-Razor-TagHelpers-TagHelperContext,Microsoft-AspNetCore-Razor-TagHelpers-TagHelperOutput-'></a>
### ProcessAsync() `method`

##### Summary

Processes the taghelper.

##### Parameters

This method has no parameters.

<a name='T-Arebis-Core-AspNet-Mvc-TagHelpers-PageSizeSelectTagHelper'></a>
## PageSizeSelectTagHelper `type`

##### Namespace

Arebis.Core.AspNet.Mvc.TagHelpers

##### Summary

A Page-size Select tag-helper.

##### Example

Assuming a "PageSize" property on the model, use as follows:

```html
<pagesize-select asp-for="PageSize" class="form-select"></pagesize-select>
```

<a name='M-Arebis-Core-AspNet-Mvc-TagHelpers-PageSizeSelectTagHelper-#ctor-Microsoft-AspNetCore-Mvc-ViewFeatures-IHtmlGenerator-'></a>
### #ctor() `constructor`

##### Summary

Constructs a PageSizeSelectTagHelper instance.

##### Parameters

This constructor has no parameters.

<a name='P-Arebis-Core-AspNet-Mvc-TagHelpers-PageSizeSelectTagHelper-Sizes'></a>
### Sizes `property`

##### Summary

Space-separated list of available sizes.
By default: "5 10 25 50 100 250".

<a name='M-Arebis-Core-AspNet-Mvc-TagHelpers-PageSizeSelectTagHelper-Init-Microsoft-AspNetCore-Razor-TagHelpers-TagHelperContext-'></a>
### Init() `method`

##### Summary

Initializes the TagHelper.

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-AspNet-Mvc-TagHelpers-PageSizeSelectTagHelper-Process-Microsoft-AspNetCore-Razor-TagHelpers-TagHelperContext,Microsoft-AspNetCore-Razor-TagHelpers-TagHelperOutput-'></a>
### Process() `method`

##### Summary

Processes the TagHelper.

##### Parameters

This method has no parameters.

<a name='T-Arebis-Core-AspNet-Mvc-TagHelpers-PaginationNavTagHelper'></a>
## PaginationNavTagHelper `type`

##### Namespace

Arebis.Core.AspNet.Mvc.TagHelpers

##### Summary

A Pagination tag-helper.

##### Example

Assuming Model PageIndex and PageCount properties, use as follows:

```html
<pagination-nav asp-for="PageIndex" max="Model.PageCount" style-template="default" keyboard="true" />
```

<a name='P-Arebis-Core-AspNet-Mvc-TagHelpers-PaginationNavTagHelper-For'></a>
### For `property`

##### Summary

The model expression holding the current page number.

<a name='P-Arebis-Core-AspNet-Mvc-TagHelpers-PaginationNavTagHelper-HasNextPage'></a>
### HasNextPage `property`

##### Summary

Whether there is a next page.

<a name='P-Arebis-Core-AspNet-Mvc-TagHelpers-PaginationNavTagHelper-Keyboard'></a>
### Keyboard `property`

##### Summary

Whether to support keyboard shortcuts (Left- and Right-arrow for previous and next page, Home and End for first and last page).

<a name='P-Arebis-Core-AspNet-Mvc-TagHelpers-PaginationNavTagHelper-Max'></a>
### Max `property`

##### Summary

If set, the maximum page number (last page's number).

<a name='P-Arebis-Core-AspNet-Mvc-TagHelpers-PaginationNavTagHelper-Min'></a>
### Min `property`

##### Summary

The minimum page number (defaults to 1).

<a name='P-Arebis-Core-AspNet-Mvc-TagHelpers-PaginationNavTagHelper-NextText'></a>
### NextText `property`

##### Summary

Text on the next page link.

<a name='P-Arebis-Core-AspNet-Mvc-TagHelpers-PaginationNavTagHelper-PreviousText'></a>
### PreviousText `property`

##### Summary

Text on the previous page link.

<a name='P-Arebis-Core-AspNet-Mvc-TagHelpers-PaginationNavTagHelper-StyleTemplate'></a>
### StyleTemplate `property`

##### Summary

Styling to apply ("default", "small",...)

<a name='M-Arebis-Core-AspNet-Mvc-TagHelpers-PaginationNavTagHelper-Process-Microsoft-AspNetCore-Razor-TagHelpers-TagHelperContext,Microsoft-AspNetCore-Razor-TagHelpers-TagHelperOutput-'></a>
### Process() `method`

##### Summary

Process the TagHelper.

##### Parameters

This method has no parameters.

<a name='T-Arebis-Core-AspNet-Mvc-ModelBinding-PolymorphObjectModelBinder'></a>
## PolymorphObjectModelBinder `type`

##### Namespace

Arebis.Core.AspNet.Mvc.ModelBinding

##### Summary

A model binder for complex plymorphic types.
Identifies the subtypes by means of a "~.$type" value that matches the descriminator on a [JsonDerivedType] attribute on the base type.

<a name='M-Arebis-Core-AspNet-Mvc-ModelBinding-PolymorphObjectModelBinder-#ctor-System-Collections-Generic-Dictionary{System-String,System-ValueTuple{Microsoft-AspNetCore-Mvc-ModelBinding-ModelMetadata,Microsoft-AspNetCore-Mvc-ModelBinding-IModelBinder}}-'></a>
### #ctor() `constructor`

##### Summary

Constructs a PolymorphObjectModelBinder given binders for each of the concrete types.

##### Parameters

This constructor has no parameters.

<a name='M-Arebis-Core-AspNet-Mvc-ModelBinding-PolymorphObjectModelBinder-BindModelAsync-Microsoft-AspNetCore-Mvc-ModelBinding-ModelBindingContext-'></a>
### BindModelAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-Arebis-Core-AspNet-Mvc-ModelBinding-PolymorphObjectModelBinderProvider'></a>
## PolymorphObjectModelBinderProvider `type`

##### Namespace

Arebis.Core.AspNet.Mvc.ModelBinding

##### Summary

Provides a model binder for complex plymorphic types.
Identifies the subtypes by means of a "~.$type" value that matches the descriminator on a [JsonDerivedType] attribute on the base type.

<a name='M-Arebis-Core-AspNet-Mvc-ModelBinding-PolymorphObjectModelBinderProvider-GetBinder-Microsoft-AspNetCore-Mvc-ModelBinding-ModelBinderProviderContext-'></a>
### GetBinder() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.
