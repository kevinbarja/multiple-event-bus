using Foundatio.Messaging;
using SharedKernel;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Add redis
var conn = ConnectionMultiplexer.Connect("redis-server");
builder.Services.AddSingleton<IMessageBus>(s => new RedisMessageBus(new RedisMessageBusOptions
{
    Subscriber = conn.GetSubscriber()
}));
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapPost("/events", (MyEvent myEvent) =>
{
    var messageBus = app.Services.GetRequiredService<IMessageBus>();
    messageBus.PublishAsync(myEvent);
    return Results.Ok(myEvent);
});

app.Run();