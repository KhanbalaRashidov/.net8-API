using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        Task<PagedList<Employee>> GetEmployeesAsync(Guid compnayId, EmployeeParameters employeeParameters, bool trackChnages);
        Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChnages);
        void CreateEmployeeForCompany(Guid companyId, Employee employee);
        void DeleteEmployee(Employee employee);
    }

}
