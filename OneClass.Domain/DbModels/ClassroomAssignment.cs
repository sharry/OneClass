using OneClass.Domain.Utils;

namespace OneClass.Domain.DbModels;

public class ClassroomAssignment
{
	public string Id { get; set; } = Guid.NewGuid().ToString();
	public string ClassroomId { get; set; }
	public string CreatorId { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Content { get; set; } = string.Empty;
	public bool HasDueDate { get; set; } = false;
	public string DueDate { get; set; } = DateTime.MinValue.ToIso8601String();
	public AttachedFile[] AttachedFiles { get; set; } = Array.Empty<AttachedFile>();
	public AssignmentStatus Status { get; set; } = AssignmentStatus.Assigned;
	public string CreatedAt { get; set; } = DateTime.UtcNow.ToIso8601String();
	public ClassroomAssignment(string creatorId, string classroomId)
	{
		ClassroomId = classroomId;
		CreatorId = creatorId;
	}
}