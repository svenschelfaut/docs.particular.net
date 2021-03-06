using System;
using System.Threading;
using NServiceBus;
using NServiceBus.Logging;

public class Handler1 : IHandleMessages<HandlerMessage>
{
    static ILog logger = LogManager.GetLogger<Handler1>();
    static Random random = new Random();

    public void Handle(HandlerMessage message)
    {
        var milliseconds = random.Next(100, 1000);
        logger.InfoFormat("Message received going to Thread.Sleep({0}ms)", milliseconds);
        Thread.Sleep(milliseconds);
    }
}
