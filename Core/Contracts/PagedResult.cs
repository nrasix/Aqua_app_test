namespace Aqua.Core.Contracts;

public record PagedResult<T>(
	IEnumerable<T> Collection,
	int TotalCount,
	int Page,
	int PageSize
);