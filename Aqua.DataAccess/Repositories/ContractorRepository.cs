using Aqua.Core.Abstract;
using Aqua.Core.Contracts;
using Aqua.Core.Entity;
using NHibernate;
using NHibernate.Linq;

namespace AquaApp.Repositories;

public class ContractorRepository : IContractorRepository
{
	private readonly ISessionFactory _sessionFactory;

	public ContractorRepository(
		ISessionFactory sessionFactory
	)
	{
		_sessionFactory = sessionFactory;
	}

	public async Task Add(Contractor contractor)
	{
		using var session = _sessionFactory.OpenSession();
		using var tx = session.BeginTransaction();

		await session.SaveAsync(contractor);

		await tx.CommitAsync();
	}

	public async Task Patch(ContractorPatch dto)
	{
		using var session = _sessionFactory.OpenSession();
		using var tx = session.BeginTransaction();

		var contractor = await session.GetAsync<Contractor>(dto.Id);
		if (contractor == null)
			return;

		contractor.Name = dto.Name ?? contractor.Name;
		contractor.Inn = dto.Inn ?? contractor.Inn;

		await session.UpdateAsync(contractor);
		await tx.CommitAsync();
	}

	public async Task<Contractor?> GetById(int id)
	{
		using var session = _sessionFactory.OpenSession();
		var contractor = await session.GetAsync<Contractor>(id);
		return contractor;
	}

	public async Task<IReadOnlyList<Contractor>> GetAll()
	{
		using var session = _sessionFactory.OpenSession();

		var contractors = await session
			.Query<Contractor>()
			.ToListAsync();

		return contractors;
	}

	public async Task Delete(int id)
	{
		using var session = _sessionFactory.OpenSession();
		using var tx = session.BeginTransaction();

		var contractor = await session.GetAsync<Contractor>(id);

		if (contractor != null)
			await session.DeleteAsync(contractor);

		await tx.CommitAsync();
	}
}