using System.Collections.Generic;
using System.Linq;

namespace InMemoryGatewayDemo
{
    public class EmployeeInMemoryGateway : InMemoryGateway<Employee>, IEmployeeGateway
    {
        public IList<Employee> FindEmployeesBySector(long sectorId)
        {
            return GetAll().Where(employee => employee.Sector.Id == sectorId).ToList();
        }
    }
}
