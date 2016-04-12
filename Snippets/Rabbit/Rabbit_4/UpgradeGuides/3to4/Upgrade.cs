﻿namespace Rabbit_4.UpgradeGuides._3to4
{
    using System;
    using NServiceBus;

    class Upgrade
    {
        void UseCustomCircuitBreakerSettings(EndpointConfiguration endpointConfiguration)
        {
            #region 3to4rabbitmq-custom-breaker-settings-code

            endpointConfiguration.UseTransport<RabbitMQTransport>()
                .TimeToWaitBeforeTriggeringCircuitBreaker(TimeSpan.FromMinutes(2));

            #endregion
        }

        void CallbackReceiverMaxConcurrency(EndpointConfiguration endpointConfiguration)
        {
            #region 3to4rabbitmq-config-callbackreceiver-thread-count
            endpointConfiguration.LimitMessageProcessingConcurrencyTo(10);

            #endregion
        }

        void UseDirectRoutingTopology(EndpointConfiguration endpointConfiguration)
        {
            #region 3to4rabbitmq-config-usedirectroutingtopology
            endpointConfiguration.UseTransport<RabbitMQTransport>()
                .UseDirectRoutingTopology(MyRoutingKeyConvention, (address, eventType) => "MyTopic");

            #endregion
        }

        string MyRoutingKeyConvention(Type type)
        {
            throw new NotImplementedException();
        }
    }
}