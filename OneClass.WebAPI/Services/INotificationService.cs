namespace OneClass.WebAPI.Services;

public interface INotificationService
{
    Task notifyViaEmail<T>(T data, string[] recipients);
}

public record EmailField {
    public string address { get; init; }
}

public record NotificationRecipient {
    public EmailField EmailAddress { get; init; }
}

public record NotificationBody {
    public string ContentType = "text/html";
    public string Content { get; init; }
}

public record NotificationMessage {
    public string Subject { get; init; }
    public NotificationBody Body { get; init; }
    public NotificationRecipient[] ToRecipients { get; init; }
}

public record NotificationEmail {
    public NotificationMessage Message { get; init; }
}
