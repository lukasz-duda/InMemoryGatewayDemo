using System;

namespace InMemoryGatewayDemo
{
    public interface IDateTimeProvider
    {
        DateTime Now();
    }
}
