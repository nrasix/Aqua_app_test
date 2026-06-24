using Aqua.Core.Contracts;
using Aqua.Core.Entity;

namespace Aqua.Core.Abstract;

public interface IOrderRepository
{
	Task Add(Order order);
	Task Delete(int id);
	Task<IReadOnlyList<Order>> GetAll();
	Task<Order?> GetById(int id);
	Task Patch(OrderPatch dto);
}