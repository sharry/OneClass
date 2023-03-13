namespace OneClass.Domain.DbModels;

public record SubmittedWork(
	string Id,
	string AssignmentId,
	string StudentId,
	AttachedFile[] Files,
	DateTimeOffset CreatedAt,
	DateTimeOffset UpdatedAt
);