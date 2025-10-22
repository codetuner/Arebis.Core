<a name='assembly'></a>
# Arebis.Core.AspNet.Mvc.Sircl

## Contents

- [ContextExtensions](#T-Arebis-Core-AspNet-Mvc-Sircl-ContextExtensions 'Arebis.Core.AspNet.Mvc.Sircl.ContextExtensions')
  - [IsSirclPartial(request)](#M-Arebis-Core-AspNet-Mvc-Sircl-ContextExtensions-IsSirclPartial-Microsoft-AspNetCore-Http-HttpRequest- 'Arebis.Core.AspNet.Mvc.Sircl.ContextExtensions.IsSirclPartial(Microsoft.AspNetCore.Http.HttpRequest)')
  - [SetSirclAppId(response,appIdParts)](#M-Arebis-Core-AspNet-Mvc-Sircl-ContextExtensions-SetSirclAppId-Microsoft-AspNetCore-Http-HttpResponse,System-String[]- 'Arebis.Core.AspNet.Mvc.Sircl.ContextExtensions.SetSirclAppId(Microsoft.AspNetCore.Http.HttpResponse,System.String[])')
- [ControllerExtensions](#T-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions')
  - [SirclAlert(controller,message)](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclAlert-Microsoft-AspNetCore-Mvc-Controller,System-String- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclAlert(Microsoft.AspNetCore.Mvc.Controller,System.String)')
  - [SirclBack(controller,allowCaching,allowClosing,allowClosingPrompt)](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclBack-Microsoft-AspNetCore-Mvc-Controller,System-Boolean,System-Boolean,System-String- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclBack(Microsoft.AspNetCore.Mvc.Controller,System.Boolean,System.Boolean,System.String)')
  - [SirclRedirect(controller,url)](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclRedirect-Microsoft-AspNetCore-Mvc-Controller,System-String- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclRedirect(Microsoft.AspNetCore.Mvc.Controller,System.String)')
  - [SirclRedirectToAction(controller,actionName)](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclRedirectToAction-Microsoft-AspNetCore-Mvc-Controller,System-String- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclRedirectToAction(Microsoft.AspNetCore.Mvc.Controller,System.String)')
  - [SirclRedirectToAction(controller,actionName,actionValues)](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclRedirectToAction-Microsoft-AspNetCore-Mvc-Controller,System-String,System-Object- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclRedirectToAction(Microsoft.AspNetCore.Mvc.Controller,System.String,System.Object)')
  - [SirclRedirectToAction(controller,actionName,controllerName)](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclRedirectToAction-Microsoft-AspNetCore-Mvc-Controller,System-String,System-String- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclRedirectToAction(Microsoft.AspNetCore.Mvc.Controller,System.String,System.String)')
  - [SirclRedirectToAction(controller,actionName,controllerName,actionValues)](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclRedirectToAction-Microsoft-AspNetCore-Mvc-Controller,System-String,System-String,System-Object- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclRedirectToAction(Microsoft.AspNetCore.Mvc.Controller,System.String,System.String,System.Object)')
  - [SirclRefresh(controller,fullPage)](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclRefresh-Microsoft-AspNetCore-Mvc-Controller,System-Boolean- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclRefresh(Microsoft.AspNetCore.Mvc.Controller,System.Boolean)')
  - [SirclSetDocumentTitle(controller,title)](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetDocumentTitle-Microsoft-AspNetCore-Mvc-Controller,System-String- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclSetDocumentTitle(Microsoft.AspNetCore.Mvc.Controller,System.String)')
  - [SirclSetFormChanged()](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetFormChanged-Microsoft-AspNetCore-Mvc-Controller- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclSetFormChanged(Microsoft.AspNetCore.Mvc.Controller)')
  - [SirclSetLoad(controller,cssSelector)](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetLoad-Microsoft-AspNetCore-Mvc-Controller,System-String- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclSetLoad(Microsoft.AspNetCore.Mvc.Controller,System.String)')
  - [SirclSetReloading(controller,seconds)](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetReloading-Microsoft-AspNetCore-Mvc-Controller,System-Int32- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclSetReloading(Microsoft.AspNetCore.Mvc.Controller,System.Int32)')
  - [SirclSetTarget(controller,target)](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetTarget-Microsoft-AspNetCore-Mvc-Controller,System-String- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclSetTarget(Microsoft.AspNetCore.Mvc.Controller,System.String)')
  - [SirclSetTargetMethod(controller,method)](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetTargetMethod-Microsoft-AspNetCore-Mvc-Controller,System-String- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclSetTargetMethod(Microsoft.AspNetCore.Mvc.Controller,System.String)')
  - [SirclSetTargetMethodAppend()](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetTargetMethodAppend-Microsoft-AspNetCore-Mvc-Controller- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclSetTargetMethodAppend(Microsoft.AspNetCore.Mvc.Controller)')
  - [SirclSetTargetMethodContent()](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetTargetMethodContent-Microsoft-AspNetCore-Mvc-Controller- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclSetTargetMethodContent(Microsoft.AspNetCore.Mvc.Controller)')
  - [SirclSetTargetMethodPrepend()](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetTargetMethodPrepend-Microsoft-AspNetCore-Mvc-Controller- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclSetTargetMethodPrepend(Microsoft.AspNetCore.Mvc.Controller)')
  - [SirclSetTargetMethodReplace()](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetTargetMethodReplace-Microsoft-AspNetCore-Mvc-Controller- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclSetTargetMethodReplace(Microsoft.AspNetCore.Mvc.Controller)')
  - [SirclSetUrl(controller,url)](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetUrl-Microsoft-AspNetCore-Mvc-Controller,System-String- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclSetUrl(Microsoft.AspNetCore.Mvc.Controller,System.String)')
  - [SirclStopRefreshing()](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclStopRefreshing-Microsoft-AspNetCore-Mvc-Controller- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclStopRefreshing(Microsoft.AspNetCore.Mvc.Controller)')
  - [SirclToastr(controller,type,message,title)](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclToastr-Microsoft-AspNetCore-Mvc-Controller,System-String,System-String,System-String- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclToastr(Microsoft.AspNetCore.Mvc.Controller,System.String,System.String,System.String)')
  - [SirclToastrError(controller,message,title)](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclToastrError-Microsoft-AspNetCore-Mvc-Controller,System-String,System-String- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclToastrError(Microsoft.AspNetCore.Mvc.Controller,System.String,System.String)')
  - [SirclToastrInfo(controller,message,title)](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclToastrInfo-Microsoft-AspNetCore-Mvc-Controller,System-String,System-String- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclToastrInfo(Microsoft.AspNetCore.Mvc.Controller,System.String,System.String)')
  - [SirclToastrSuccess(controller,message,title)](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclToastrSuccess-Microsoft-AspNetCore-Mvc-Controller,System-String,System-String- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclToastrSuccess(Microsoft.AspNetCore.Mvc.Controller,System.String,System.String)')
  - [SirclToastrWarning(controller,message,title)](#M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclToastrWarning-Microsoft-AspNetCore-Mvc-Controller,System-String,System-String- 'Arebis.Core.AspNet.Mvc.Sircl.ControllerExtensions.SirclToastrWarning(Microsoft.AspNetCore.Mvc.Controller,System.String,System.String)')

<a name='T-Arebis-Core-AspNet-Mvc-Sircl-ContextExtensions'></a>
## ContextExtensions `type`

##### Namespace

Arebis.Core.AspNet.Mvc.Sircl

##### Summary

ASP.NET MVC Context extension methods to support Sircl.

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ContextExtensions-IsSirclPartial-Microsoft-AspNetCore-Http-HttpRequest-'></a>
### IsSirclPartial(request) `method`

##### Summary

Whether the request is a Sircl partial request.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [Microsoft.AspNetCore.Http.HttpRequest](#T-Microsoft-AspNetCore-Http-HttpRequest 'Microsoft.AspNetCore.Http.HttpRequest') | The HTTP request. |

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ContextExtensions-SetSirclAppId-Microsoft-AspNetCore-Http-HttpResponse,System-String[]-'></a>
### SetSirclAppId(response,appIdParts) `method`

##### Summary

Builds and sets the Sircl AppId by concatenating the given parts.
I.e: Context.Response.SetSirclAppId("MyApp", languageCode, userToken)

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| response | [Microsoft.AspNetCore.Http.HttpResponse](#T-Microsoft-AspNetCore-Http-HttpResponse 'Microsoft.AspNetCore.Http.HttpResponse') | The HTTP response. |
| appIdParts | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | The AppId parts that will be concatenated with '-' as separator. |

<a name='T-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions'></a>
## ControllerExtensions `type`

##### Namespace

Arebis.Core.AspNet.Mvc.Sircl

##### Summary

ASP.NET MVC Controller extension methods to support Sircl.

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclAlert-Microsoft-AspNetCore-Mvc-Controller,System-String-'></a>
### SirclAlert(controller,message) `method`

##### Summary

Sets the X-Sircl-Alert-Message header to render a message in an alert box.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| controller | [Microsoft.AspNetCore.Mvc.Controller](#T-Microsoft-AspNetCore-Mvc-Controller 'Microsoft.AspNetCore.Mvc.Controller') | The controller. |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message. |

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclBack-Microsoft-AspNetCore-Mvc-Controller,System-Boolean,System-Boolean,System-String-'></a>
### SirclBack(controller,allowCaching,allowClosing,allowClosingPrompt) `method`

##### Summary

Sets the X-Sircl-History header to instruct back navigation and returns a 204 (NoContent).

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| controller | [Microsoft.AspNetCore.Mvc.Controller](#T-Microsoft-AspNetCore-Mvc-Controller 'Microsoft.AspNetCore.Mvc.Controller') | The controller. |
| allowCaching | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Whether to allow the response to be retrieved from (browser) cache. |
| allowClosing | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Whether to allow closing the current window if this page initiated the window. |
| allowClosingPrompt | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Confirmation prompt to show before closing the window. |

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclRedirect-Microsoft-AspNetCore-Mvc-Controller,System-String-'></a>
### SirclRedirect(controller,url) `method`

##### Summary

Redirects to the given resource. For a partial (Sircl) request, redirection is done with status code 204.
Otherwise a regular redirect with status code 302 is done.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| controller | [Microsoft.AspNetCore.Mvc.Controller](#T-Microsoft-AspNetCore-Mvc-Controller 'Microsoft.AspNetCore.Mvc.Controller') | The controller. |
| url | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The URL to redirect to. |

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclRedirectToAction-Microsoft-AspNetCore-Mvc-Controller,System-String-'></a>
### SirclRedirectToAction(controller,actionName) `method`

##### Summary

Redirects to the given resource. For a partial (Sircl) request, redirection is done with status code 204.
Otherwise a regular redirect with status code 302 is done.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| controller | [Microsoft.AspNetCore.Mvc.Controller](#T-Microsoft-AspNetCore-Mvc-Controller 'Microsoft.AspNetCore.Mvc.Controller') | The controller. |
| actionName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The action name. |

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclRedirectToAction-Microsoft-AspNetCore-Mvc-Controller,System-String,System-Object-'></a>
### SirclRedirectToAction(controller,actionName,actionValues) `method`

##### Summary

Redirects to the given resource. For a partial (Sircl) request, redirection is done with status code 204.
Otherwise a regular redirect with status code 302 is done.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| controller | [Microsoft.AspNetCore.Mvc.Controller](#T-Microsoft-AspNetCore-Mvc-Controller 'Microsoft.AspNetCore.Mvc.Controller') | The controller. |
| actionName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The action name. |
| actionValues | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | An action values object. |

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclRedirectToAction-Microsoft-AspNetCore-Mvc-Controller,System-String,System-String-'></a>
### SirclRedirectToAction(controller,actionName,controllerName) `method`

##### Summary

Redirects to the given resource. For a partial (Sircl) request, redirection is done with status code 204.
Otherwise a regular redirect with status code 302 is done.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| controller | [Microsoft.AspNetCore.Mvc.Controller](#T-Microsoft-AspNetCore-Mvc-Controller 'Microsoft.AspNetCore.Mvc.Controller') | The controller. |
| actionName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The action name. |
| controllerName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The action controller name. |

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclRedirectToAction-Microsoft-AspNetCore-Mvc-Controller,System-String,System-String,System-Object-'></a>
### SirclRedirectToAction(controller,actionName,controllerName,actionValues) `method`

##### Summary

Redirects to the given resource. For a partial (Sircl) request, redirection is done with status code 204.
Otherwise a regular redirect with status code 302 is done.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| controller | [Microsoft.AspNetCore.Mvc.Controller](#T-Microsoft-AspNetCore-Mvc-Controller 'Microsoft.AspNetCore.Mvc.Controller') | The controller. |
| actionName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The action name. |
| controllerName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The action controller name. |
| actionValues | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | An action values object. |

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclRefresh-Microsoft-AspNetCore-Mvc-Controller,System-Boolean-'></a>
### SirclRefresh(controller,fullPage) `method`

##### Summary

Sets the X-Sircl-History header to instruct a main target reload and returns a 204 (NoContent).

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| controller | [Microsoft.AspNetCore.Mvc.Controller](#T-Microsoft-AspNetCore-Mvc-Controller 'Microsoft.AspNetCore.Mvc.Controller') | The controller. |
| fullPage | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Whether to perform a full page reload. |

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetDocumentTitle-Microsoft-AspNetCore-Mvc-Controller,System-String-'></a>
### SirclSetDocumentTitle(controller,title) `method`

##### Summary

Sets the X-Sircl-Document-Title header to specify the document title.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| controller | [Microsoft.AspNetCore.Mvc.Controller](#T-Microsoft-AspNetCore-Mvc-Controller 'Microsoft.AspNetCore.Mvc.Controller') | The controller. |
| title | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The new document title. |

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetFormChanged-Microsoft-AspNetCore-Mvc-Controller-'></a>
### SirclSetFormChanged() `method`

##### Summary

Sets the X-Sircl-Form-Changed header to "true" to mark the form as changed.

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetLoad-Microsoft-AspNetCore-Mvc-Controller,System-String-'></a>
### SirclSetLoad(controller,cssSelector) `method`

##### Summary

Sets the X-Sircl-Load header to instruct (re)loading the given selection.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| controller | [Microsoft.AspNetCore.Mvc.Controller](#T-Microsoft-AspNetCore-Mvc-Controller 'Microsoft.AspNetCore.Mvc.Controller') | The controller. |
| cssSelector | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The CSS selector to load. |

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetReloading-Microsoft-AspNetCore-Mvc-Controller,System-Int32-'></a>
### SirclSetReloading(controller,seconds) `method`

##### Summary

Sets the X-Sircl-Reload-After header to the given value to instruct automatic reloading.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| controller | [Microsoft.AspNetCore.Mvc.Controller](#T-Microsoft-AspNetCore-Mvc-Controller 'Microsoft.AspNetCore.Mvc.Controller') | The controller. |
| seconds | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Number of seconds before reloading. |

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetTarget-Microsoft-AspNetCore-Mvc-Controller,System-String-'></a>
### SirclSetTarget(controller,target) `method`

##### Summary

Sets the X-Sircl-Target header to specify or override the target.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| controller | [Microsoft.AspNetCore.Mvc.Controller](#T-Microsoft-AspNetCore-Mvc-Controller 'Microsoft.AspNetCore.Mvc.Controller') | The controller. |
| target | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The target. |

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetTargetMethod-Microsoft-AspNetCore-Mvc-Controller,System-String-'></a>
### SirclSetTargetMethod(controller,method) `method`

##### Summary

Sets the X-Sircl-Target-Method header to instruct how to full the target.
Supported values: "content", "prepend", "append" or "replace".

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| controller | [Microsoft.AspNetCore.Mvc.Controller](#T-Microsoft-AspNetCore-Mvc-Controller 'Microsoft.AspNetCore.Mvc.Controller') | The controller. |
| method | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The target method. |

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetTargetMethodAppend-Microsoft-AspNetCore-Mvc-Controller-'></a>
### SirclSetTargetMethodAppend() `method`

##### Summary

Sets the X-Sircl-Target-Method header to instruct how to full the target.

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetTargetMethodContent-Microsoft-AspNetCore-Mvc-Controller-'></a>
### SirclSetTargetMethodContent() `method`

##### Summary

Sets the X-Sircl-Target-Method header to instruct how to full the target.

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetTargetMethodPrepend-Microsoft-AspNetCore-Mvc-Controller-'></a>
### SirclSetTargetMethodPrepend() `method`

##### Summary

Sets the X-Sircl-Target-Method header to instruct how to full the target.

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetTargetMethodReplace-Microsoft-AspNetCore-Mvc-Controller-'></a>
### SirclSetTargetMethodReplace() `method`

##### Summary

Sets the X-Sircl-Target-Method header to instruct how to full the target.

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclSetUrl-Microsoft-AspNetCore-Mvc-Controller,System-String-'></a>
### SirclSetUrl(controller,url) `method`

##### Summary

Sets the X-Sircl-History-Replace header to display a different URL in the client.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| controller | [Microsoft.AspNetCore.Mvc.Controller](#T-Microsoft-AspNetCore-Mvc-Controller 'Microsoft.AspNetCore.Mvc.Controller') | The controller. |
| url | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The URL to set. |

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclStopRefreshing-Microsoft-AspNetCore-Mvc-Controller-'></a>
### SirclStopRefreshing() `method`

##### Summary

Sets the X-Sircl-Reload-After header to "0" to abort repeated reloadings.

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclToastr-Microsoft-AspNetCore-Mvc-Controller,System-String,System-String,System-String-'></a>
### SirclToastr(controller,type,message,title) `method`

##### Summary

Sets the X-Sircl-Toastr header to render a Toastr message.
Type of message is "success", "info", "warning" or "error".

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| controller | [Microsoft.AspNetCore.Mvc.Controller](#T-Microsoft-AspNetCore-Mvc-Controller 'Microsoft.AspNetCore.Mvc.Controller') | The controller. |
| type | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The type of message: "success", "info", "warning" or "error". |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message. |
| title | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional title. |

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclToastrError-Microsoft-AspNetCore-Mvc-Controller,System-String,System-String-'></a>
### SirclToastrError(controller,message,title) `method`

##### Summary

Sets the X-Sircl-Toastr header to render a Toastr message.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| controller | [Microsoft.AspNetCore.Mvc.Controller](#T-Microsoft-AspNetCore-Mvc-Controller 'Microsoft.AspNetCore.Mvc.Controller') | The controller. |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message. |
| title | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional title. |

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclToastrInfo-Microsoft-AspNetCore-Mvc-Controller,System-String,System-String-'></a>
### SirclToastrInfo(controller,message,title) `method`

##### Summary

Sets the X-Sircl-Toastr header to render a Toastr message.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| controller | [Microsoft.AspNetCore.Mvc.Controller](#T-Microsoft-AspNetCore-Mvc-Controller 'Microsoft.AspNetCore.Mvc.Controller') | The controller. |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message. |
| title | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional title. |

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclToastrSuccess-Microsoft-AspNetCore-Mvc-Controller,System-String,System-String-'></a>
### SirclToastrSuccess(controller,message,title) `method`

##### Summary

Sets the X-Sircl-Toastr header to render a Toastr message.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| controller | [Microsoft.AspNetCore.Mvc.Controller](#T-Microsoft-AspNetCore-Mvc-Controller 'Microsoft.AspNetCore.Mvc.Controller') | The controller. |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message. |
| title | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional title. |

<a name='M-Arebis-Core-AspNet-Mvc-Sircl-ControllerExtensions-SirclToastrWarning-Microsoft-AspNetCore-Mvc-Controller,System-String,System-String-'></a>
### SirclToastrWarning(controller,message,title) `method`

##### Summary

Sets the X-Sircl-Toastr header to render a Toastr message.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| controller | [Microsoft.AspNetCore.Mvc.Controller](#T-Microsoft-AspNetCore-Mvc-Controller 'Microsoft.AspNetCore.Mvc.Controller') | The controller. |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message. |
| title | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional title. |
