using System;

namespace InMemoryGatewayDemo
{
    public class DateTimeProviderStub : IDateTimeProvider
    {
        private DateTime dateTime;

        public DateTimeProviderStub(DateTime dateTime)
        {
            this.dateTime = dateTime;
        }

        public DateTime Now()
        {
            return dateTime;
        }
    }
}
