using OneClass.Domain.Utils;

namespace OneClass.Domain.DbModels;

public class ResourceData
{
    public string Id { get; set; }
    public string Content { get; set; }
    public Teacher Teacher { get; set; }
    public string ClassroomId { get; set; }
    public string CreatedAt { get; set; } = DateTime.UtcNow.ToIso8601String();
    public AttachedFile[] Attachments { get; set; }

    public void AddAttachment(AttachedFile attachment)
    {
        if (Attachments == null)
        {
            Attachments = new AttachedFile[] { attachment };
        }
        else
        {
            Attachments = Attachments.Append(attachment).ToArray();
        }
    }

    public void RemoveAttachment(string attachmentId)
    {
        if (Attachments == null)
        {
            return;
        }

        Attachments = Attachments.Where(a => a.Id != attachmentId).ToArray();
    }
}

public record Teacher
{
    public string Id { get; init; }
    public string Name { get; init; }
}
