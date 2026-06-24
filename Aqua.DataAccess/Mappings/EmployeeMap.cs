using Aqua.Core.Entity;
using FluentNHibernate.Mapping;

namespace AquaApp.Mappings;

public class EmployeeMap : ClassMap<Employee>
{
	public EmployeeMap()
	{
		Table("Employees");

		Id(x => x.Id).GeneratedBy.Identity();

		Map(x => x.FullName);
		Map(x => x.Position).CustomType<int>();
		Map(x => x.BirthDate);
	}
}