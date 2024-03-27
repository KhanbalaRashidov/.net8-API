using Entities.Models;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees(Guid compnayId, bool trackChnages);
        Employee GetEmployee(Guid companyId, Guid employeeId, bool trackChnages);
        void CreateEmployeeForCompany(Guid companyId, Employee employee);
        void DeleteEmployee(Employee employee);
    }

}
