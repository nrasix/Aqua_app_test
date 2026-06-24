namespace Aqua.Core.Entity;

public class Order
{
	public virtual int Id { get; set; }
	public virtual DateTime OrderDate { get; set; }
	public virtual decimal Amount { get; set; }

	public virtual Employee Employee { get; set; }
	public virtual Contractor Contractor { get; set; }
}