using Contracts;
using Entities.Models;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public IEnumerable<Employee> GetEmployees(Guid compnayId, bool trackChnages) =>
            FindByCondition(e => e.CompanyId.Equals(compnayId), trackChnages)
            .OrderBy(e => e.Name).ToList();

        public Employee GetEmployee(Guid companyId, Guid employeeId, bool trackChnages) =>
            FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(employeeId), trackChnages)
            .SingleOrDefault();

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee) => Delete(employee);
    }
}
