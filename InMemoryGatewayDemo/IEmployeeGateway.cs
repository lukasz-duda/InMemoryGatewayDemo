using System.Collections.Generic;

namespace InMemoryGatewayDemo
{
    public interface IEmployeeGateway
    {
        IList<Employee> FindEmployeesBySector(long sectorId);
    }
}
