using Aqua.Core.Abstract;
using Aqua.Core.Contracts;
using Aqua.Core.Entity;
using NHibernate;
using NHibernate.Linq;

namespace AquaApp.Repositories;

public class OrderRepository : IOrderRepository
{
	private readonly ISessionFactory _sessionFactory;

	public OrderRepository(ISessionFactory sessionFactory)
	{
		_sessionFactory = sessionFactory;
	}

	public async Task Add(Order order)
	{
		using var session = _sessionFactory.OpenSession();

		using var tx = session.BeginTransaction();

		await session.SaveAsync(order);
		await tx.CommitAsync();
	}

	public async Task Delete(int id)
	{
		using var session = _sessionFactory.OpenSession();
		using var tx = session.BeginTransaction();

		var order = await session.GetAsync<Order>(id);

		if (order != null)
			await session.DeleteAsync(order);

		await tx.CommitAsync();
	}

	public async Task Patch(OrderPatch dto)
	{
		using var session = _sessionFactory.OpenSession();
		using var tx = session.BeginTransaction();

		var order = await session.GetAsync<Order>(dto.Id);

		if (order == null)
			return;

		order.OrderDate = dto.OrderDate ?? order.OrderDate;
		order.Amount = dto.Amount ?? order.Amount;

		await session.UpdateAsync(order);

		await tx.CommitAsync();
	}

	public async Task<IReadOnlyList<Order>> GetAll()
	{
		using var session = _sessionFactory.OpenSession();

		var orders = await session
			.Query<Order>()
			.ToListAsync();

		return orders;
	}

	public async Task<Order?> GetById(int id)
	{
		using var session = _sessionFactory.OpenSession();

		var order = await session.GetAsync<Order>(id);

		return order;
	}
}