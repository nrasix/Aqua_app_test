using Aqua.Core.Models;

namespace Aqua.Core.Entity;

public class Employee
{
	public virtual int Id { get; set; }
	public virtual string FullName { get; set; }
	public virtual PositionType Position { get; set; }
	public virtual DateTime BirthDate { get; set; }
}