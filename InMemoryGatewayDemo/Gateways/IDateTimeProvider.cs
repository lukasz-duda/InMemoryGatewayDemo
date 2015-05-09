using System;

namespace InMemoryGatewayDemo.Gateways
{
    public interface IDateTimeProvider
    {
        DateTime Now();
    }
}
