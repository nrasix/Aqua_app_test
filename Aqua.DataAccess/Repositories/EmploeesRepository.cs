using Aqua.Core.Abstract;
using Aqua.Core.Contracts;
using Aqua.Core.Entity;
using NHibernate;
using NHibernate.Linq;

namespace AquaApp.Repositories;

public class EmploeesRepository : IEmployeeRepository
{
	private readonly ISessionFactory _sessionFactory;

	public EmploeesRepository(ISessionFactory sessionFactory)
	{
		_sessionFactory = sessionFactory;
	}

	public async Task Add(Employee employee)
	{
		using var session = _sessionFactory.OpenSession();
		using var tx = session.BeginTransaction();

		await session.SaveAsync(employee);

		await tx.CommitAsync();
	}

	public async Task Patch(EmployeePatch dto)
	{
		using var session = _sessionFactory.OpenSession();
		using var tx = session.BeginTransaction();

		var employee = await session.GetAsync<Employee>(dto.Id);

		if (employee == null)
			return;

		employee.FullName = dto.FullName ?? employee.FullName;
		employee.Position = dto.Position ?? employee.Position;
		employee.BirthDate = dto.BirthDate ?? employee.BirthDate;

		await session.UpdateAsync(employee);

		await tx.CommitAsync();
	}

	public async Task<Employee?> GetById(int id)
	{
		using var session = _sessionFactory.OpenSession();

		var employee = await session.GetAsync<Employee>(id);

		return employee;
	}

	public async Task<IReadOnlyList<Employee>> GetAll()
	{
		using var session = _sessionFactory.OpenSession();

		var employees = await session
			.Query<Employee>()
			.ToListAsync();

		return employees;
	}

	public async Task Delete(int id)
	{
		using var session = _sessionFactory.OpenSession();
		using var tx = session.BeginTransaction();

		var employee = await session.GetAsync<Employee>(id);

		if (employee != null)
			await session.DeleteAsync(employee);

		await tx.CommitAsync();
	}
}