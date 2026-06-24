using Aqua.Core.Models;

namespace Aqua.Core.Contracts;

public record EmployeePatch(
	int Id,
	string? FullName,
	PositionType? Position,
	DateTime? BirthDate
);