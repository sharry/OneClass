namespace OneClass.WebAPI.Assignment;

public sealed record ClassroomAssignmentRequest(
	string Title,
	string? Content,
	bool? HasDueDate,
	string? DueDate
);