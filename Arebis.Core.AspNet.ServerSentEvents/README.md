# Arebis.Core.AspNet.ServerSentEvents

## Introduction

The **Arebis.Core.AspNet.ServerSentEvents** component provides ServerSent Events support for ASP.NET applications.

## Setup

### 1. Define client data

Create a class that inherits from **ServerSentEventsClientData** to hold client-specific data.
This data can be used to filter recipient clients when dispatching events.

For instance:

```
public class MySseClientData : ServerSentEventsClientData
{
    public string? Market { get; internal set; }
}
```

Properties of the client data object are automatically initialized with query-string values of the initiating request.
For instance, the "**/MySseEndpoint?Market=non-profit**" URL would initialize the **Market** property with the "_non-profit_" value.

To accept or reject connection requests, or perform special handling on connection, override the
**OnInitialize(HttpContext ctx)** method as described further.

### 2. Define a client data store

Define where to store client data. To store client data in memory of the server, you can define the **MemoryServerSentEventsClientsDataStore** as in:

```
builder.Services.AddSingleton<IServerSentEventsClientsDataStore<MySseClientData>, MemoryServerSentEventsClientsDataStore<MySseClientData>>();
```

Or you can create your own stores by implementing the **IServerSentEventsClientsDataStore\<T\>** interface.

### 3. Install the SSE service

```
builder.Services.AddServerSentEvents<MySseClientData>(builder.Configuration, (options) =>
{
    options.PathPrefix = "/MySseEndpoint";
});
```

Setting the **PathPrefix** is not mandatory. You can also define an **IsSseConnection** function. If both are missing, any incomming GET connection
with **Accept** header set to "text/event-stream" will be considered an ServerSent Event request.

### 4. Install the SSE middleware

Add the following line to install the SSE middleware:

```
app.UseServerSentEvents();
```

### 5. Server configuration

To allow KeepAlive connections, make sure to enable KeepAlive in the **web.config**:

```
<configuration>
    <system.webServer>
        <httpProtocol allowKeepAlive="true" />
    </system.webServer>
</configuration>
```
See also: https://stackoverflow.com/a/71310959/323122

## Implementing ServerSent Events

### Handling incomming connections

#### Accepting incomming connection requests

Accepting (or rejecting) incomming SSE connection requests is performed by the **OnInitialize**
method of the Client Data class.

If the method succeeds, the connection request will be accepted. If the connection failes (throws an exception),
the connection request will be rejected.

To gracefully reject a connection request, throw a **HttpRequestException** with **NoContent** status code. I.e:

```
public class MySseClientData : ServerSentEventsClientData
{
    ...

    public override void OnInitialize(HttpContext context)
    {
        if (!context.User.IsInRole("seller"))
        {
            throw new HttpRequestException("Disallowed", null, System.Net.HttpStatusCode.NoContent);
        }
    }
}
```

Within the **OnInitialize** method, you can use the **HttpContext** to retrieve the request and other context information, including
retrieving the logged in used. Be aware that the SSE connection is not by default aborted when the user logs out or logs in as a different user!

For more complex actions you may want to set up a service scope. For instance:

```
public class MySseClientData : ServerSentEventsClientData
{
    ...

    public override void OnInitialize(HttpContext context)
    {
        using (var scope = context.RequestServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>()!;
            this.Annoucements = dbContext.Announcements.Where(a => a.ForUser == context.User.Identity.Name).ToList();
        }
    }
}
```

#### Mapping query-string fields to client properties

Properties of the client data object are automatically initialized with query-string values of the initiating request.
For instance, the "**/MySseEndpoint?Market=non-profit**" would initialize a **Market** property (if such one exists) with the "_non-profit_" value.

Property matching is case-insensitive.

### Sending events

To send events, you need access to the **IServerSentEventsClientsDataStore\<TClientData\>** instance holding the client(s) to send events to.
Typically use dependency injection to obtain this instance.

#### Sending individual events

The **ServerSentEventsClientData** class holds a Guid **Identifier** property that can be used to identify one particular client.

To send an event to this client, call the **QueueNewEvent**() method given the event to send, the identifier of the client and an optional CancellationToken:

```
public void SendTimeTick([FromServices] IServerSentEventsClientsDataStore<MySseClientData> clients, Guid to)
{
    var e = new ServerSentEvent() { Type = "timetick", Data = DateTime.Now.ToTimeString() };
    clients.QueueNewEvent(e, to);
}
```

The **Identifier** property can be obtained within the **OnInitialize** method of the client data class.

### Sending bulk events

To send events to multiple clients, call the **QueueNewEvent**() overload that takes a lambda expression to identify the clients
to which to send the event:

```
public void SendTimeTick([FromServices] IServerSentEventsClientsDataStore<MySseClientData> clients, string market)
{
    var e = new ServerSentEvent() { Type = "timetick", Data = DateTime.Now.ToTimeString() };
    clients.QueueNewEvent(e, c => c.Market == market);
}
```
