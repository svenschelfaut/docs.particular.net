﻿using System;
using NServiceBus;
using NServiceBus.Installation.Environments;

class Program
{
    static void Main()
    {
        Configure.Serialization.Json();
        Configure configure = Configure.With();
        configure.Log4Net();
        configure.DefineEndpointName("Samples.PerfCounters");
        configure.DefaultBuilder();
        configure.InMemorySagaPersister();
        configure.UseInMemoryTimeoutPersister();
        configure.InMemorySubscriptionStorage();
        configure.UseTransport<Msmq>();

        #region enable-counters
        configure.EnablePerformanceCounters();
        configure.SetEndpointSLA(TimeSpan.FromSeconds(100));
        #endregion

        using (IStartableBus startableBus = configure.UnicastBus().CreateBus())
        {
            IBus bus = startableBus.Start(() => configure.ForInstallationOn<Windows>().Install());

            Console.WriteLine("Press any key to send 10 messages with random sleep");

            Console.WriteLine("To exit, press Escape");
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                for (int i = 0; i < 10; i++)
                {
                    bus.SendLocal(new MyMessage());
                }
            }
        }

    }
}