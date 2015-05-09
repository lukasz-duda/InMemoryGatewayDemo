using InMemoryGatewayDemo.Entities;
using System.Collections.Generic;

namespace InMemoryGatewayDemo.Gateways
{
    public interface IEmployeeGateway
    {
        IList<Employee> FindEmployeesBySector(long sectorId);
    }
}
