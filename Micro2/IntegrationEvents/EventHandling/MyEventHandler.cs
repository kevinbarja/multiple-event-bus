using Foundatio.Extensions.Hosting.Startup;
using Foundatio.Messaging;
using SharedKernel;

namespace Micro2.IntegrationEvents.EventHandling;

public class MyEventHandler : IStartupAction
{
    private readonly IMessageBus _messageBus;

    public MyEventHandler(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public async Task RunAsync(CancellationToken shutdownToken = default)
    {
        await _messageBus.SubscribeAsync<MyEvent>(Handle, cancellationToken: shutdownToken);
    }

    private Task Handle(MyEvent ec, CancellationToken cancellationToken = default)
    {
        Console.WriteLine(ec.ToString());
        return Task.CompletedTask;
    }
}
