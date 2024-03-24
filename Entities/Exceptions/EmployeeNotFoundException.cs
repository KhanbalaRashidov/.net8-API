namespace Entities.Exceptions
{
    public class EmployeeNotFoundException : NotFoundException
    {
        public EmployeeNotFoundException(Guid companyId)
            : base($"Employee with id: {companyId} doesn't exist in the database.")
        {

        }
    }
}
