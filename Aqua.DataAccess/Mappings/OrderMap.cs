using Aqua.Core.Entity;
using FluentNHibernate.Mapping;

namespace AquaApp.Mappings;

public class OrderMap : ClassMap<Order>
{
	public OrderMap()
	{
		Table("Orders");

		Id(x => x.Id);

		Map(x => x.OrderDate);
		Map(x => x.Amount).CustomSqlType("decimal(18,2)").Precision(18).Scale(2);

		References(x => x.Employee).Column("EmployeeId");
		References(x => x.Contractor).Column("ContractorId");
	}
}