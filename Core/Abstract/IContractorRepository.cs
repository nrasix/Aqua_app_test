using Aqua.Core.Contracts;
using Aqua.Core.Entity;

namespace Aqua.Core.Abstract;

public interface IContractorRepository
{
	Task Add(Contractor contractor);
	Task Delete(int id);
	Task<IReadOnlyList<Contractor>> GetAll();
	Task<Contractor?> GetById(int id);
	Task Patch(ContractorPatch dto);
}