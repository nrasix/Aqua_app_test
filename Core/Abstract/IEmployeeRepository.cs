using Aqua.Core.Contracts;
using Aqua.Core.Entity;

namespace Aqua.Core.Abstract;

public interface IEmployeeRepository
{
	Task Add(Employee employee);
	Task Delete(int id);
	Task<IReadOnlyList<Employee>> GetAll();
	Task<Employee?> GetById(int id);
	Task Patch(EmployeePatch dto);
}