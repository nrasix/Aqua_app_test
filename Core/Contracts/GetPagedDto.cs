namespace Aqua.Core.Contracts;

public record GetPagedDto
{
	public int Page;
	public int PageSize;

	public GetPagedDto(int page = 1, int pageSize = 20)
	{
		if (page < 1)
			page = 1;

		if (pageSize < 1)
			pageSize = 20;

		Page = page;
		PageSize = pageSize;
	}
}