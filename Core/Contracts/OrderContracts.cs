namespace Aqua.Core.Contracts;

public record OrderPatch(
	int Id,
	DateTime? OrderDate,
	decimal? Amount
);