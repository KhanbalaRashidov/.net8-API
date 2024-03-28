using Entities.Models;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync(Guid compnayId, bool trackChnages);
        Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChnages);
        void CreateEmployeeForCompany(Guid companyId, Employee employee);
        void DeleteEmployee(Employee employee);
    }

}
