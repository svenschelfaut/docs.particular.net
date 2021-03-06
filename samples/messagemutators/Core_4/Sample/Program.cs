﻿using System;
using NServiceBus;
using NServiceBus.Installation.Environments;

class Program
{
    static void Main()
    {
        Console.Title = "Samples.MessageMutators";
        Configure.Serialization.Json();
        var configure = Configure.With();
        configure.Log4Net();
        configure.DefineEndpointName("Samples.MessageMutators");
        configure.DefaultBuilder();
        configure.InMemorySagaPersister();
        configure.UseInMemoryTimeoutPersister();
        configure.InMemorySubscriptionStorage();
        configure.UseTransport<Msmq>();
        #region ComponentRegistartion
        configure.Configurer.ConfigureComponent<ValidationMessageMutator>(DependencyLifecycle.InstancePerCall);
        configure.Configurer.ConfigureComponent<TransportMessageCompressionMutator>(DependencyLifecycle.InstancePerCall);
        #endregion

        using (var startableBus = configure.UnicastBus().CreateBus())
        {
            var bus = startableBus.Start(() => configure.ForInstallationOn<Windows>().Install());
            Runner.Run(bus);
        }
    }
}