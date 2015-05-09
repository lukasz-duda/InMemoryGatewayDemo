using InMemoryGatewayDemo.Entities;
using InMemoryGatewayDemo.Gateways;
using System.Collections.Generic;
using System.Linq;

namespace InMemoryGatewayDemo.UseCaseTests
{
    public class EmployeeInMemoryGateway : InMemoryGateway<Employee>, IEmployeeGateway
    {
        public IList<Employee> FindEmployeesBySector(long sectorId)
        {
            return GetAll().Where(employee => employee.Sector.Id == sectorId).ToList();
        }
    }
}
