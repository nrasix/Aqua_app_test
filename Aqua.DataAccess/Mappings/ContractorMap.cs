using Aqua.Core.Entity;
using FluentNHibernate.Mapping;

namespace AquaApp.Mappings;

public class ContractorMap : ClassMap<Contractor>
{
	public ContractorMap()
	{
		Table("Contractors");

		Id(x => x.Id);

		Map(x => x.Name);
		Map(x => x.Inn);

		References(x => x.Curator).Column("CuratorId");
	}
}
