namespace OneClass.Domain.GraphModels;

public record TodoTask (
    string Id,
    string Title,
    string Content,
    bool IsCompleted,
    string CreatedAt,
    string UpdatedAt
);

public record TodoTaskRequest(
    string Title,
    TodoTaskBody Body,
    TodoTaskDueDate DueDateTime
);


public record TodoTaskBody(
    string Content,
    string ContentType = "text"
);

public record TodoTaskDueDate(
    string dateTime,
    string TimeZone = "UTC"
);
