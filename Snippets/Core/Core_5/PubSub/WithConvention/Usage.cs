namespace Core5.PubSub.WithConvention
{
    using NServiceBus;

    class Usage
    {
        Usage(BusConfiguration busConfiguration)
        {
            #region DefiningEventsAs

            var conventionsBuilder = busConfiguration.Conventions();
            conventionsBuilder.DefiningEventsAs(t =>
                t.Namespace != null &&
                t.Namespace.StartsWith("Domain") &&
                t.Name.EndsWith("Event"));

            #endregion
        }
    }
}